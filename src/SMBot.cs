using System.Drawing;
using System.Windows.Forms;

using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;

namespace SMBot
{
	[ExternalTool("SMBot")]
	[ExternalToolApplicability.RomWhitelist(
		CoreSystem.NES,
		"EA343F4E445A9050D4B4FBAC2C77D0693B1D0922", // U
		"AB30029EFEC6CCFC5D65DFDA7FBC6E6489A80805")] // E
	public sealed class SMBAutosaveToolForm : ToolFormBase, IExternalToolForm
	{
		public ApiContainer? _maybeAPIContainer { get; set; }

		private readonly Label _lblLevel = new() { AutoSize = true };

		private string _prevLevel = "1-1";

		private int? _prevSlot = null;

		private ApiContainer APIs
			=> _maybeAPIContainer!;

		protected override string WindowTitleStatic
			=> "SMB Autosave";

		public SMBAutosaveToolForm()
		{
			ClientSize = new Size(480, 320);
			SuspendLayout();
			Controls.Add(_lblLevel);
			ResumeLayout(performLayout: false);
			PerformLayout();
		}

		private string ReadLevel()
		{
			var bytes = APIs.Memory.ReadByteRange(0x075CL, 9);
			return bytes[8] is 0 or 0xFF
				? _prevLevel // in the main menu
				: $"{bytes[3] + 1}-{bytes[0] + 1}";
		}

		public override void Restart()
		{
			_prevLevel = "1-1"; // ReadLevel returns this when in the main menu, need to reset it
			_lblLevel.Text = $"You are in World {ReadLevel()}";
			APIs.EmuClient.StateLoaded += (_, _) => _prevLevel = ReadLevel(); // without this, loading a state would cause UpdateAfter to save a state because the level would be different
		}

		protected override void UpdateAfter()
		{
			var level = ReadLevel();
			if (level == _prevLevel) return; // no change, short-circuit
			// else the player has just gone to the next level
			var nextSlot = ((_prevSlot ?? 0) + 1) % 10;
			APIs.SaveState.SaveSlot(nextSlot);
			_lblLevel.Text = $"You are in World {level}, load slot {nextSlot} to restart";
			if (_prevSlot is not null) _lblLevel.Text += $" or {_prevSlot} to go back to {_prevLevel}";
			_prevSlot = nextSlot;
			_prevLevel = level;
		}
	}
}