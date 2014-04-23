using System;
using LTF4;
using MonoBrickFirmware.UserInput;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.Display.Dialogs;

namespace LTF4
{
	class MainClass
	{
		public static void MainLoop() {

		}

		public static void Main (string[] args)
		{
			Log.Info("Lego Task Force 4");
			Time.WaitSec ();


			Process p = new Process ();
			ButtonEvents but = new ButtonEvents();

			//stop program if user presses escape button
			but.EscapePressed += delegate() {
				p.Stop ();
				Log.Info ("LTF4 finished");
				Time.Wait (500);
			};

			//start program
			p.Start ();
		}
	}
}