using System;
using LTF4;
using MonoBrickFirmware.UserInput;

namespace LTF4
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//TODO: little menu
			//TODO: init dialog

			Log.Info("Lego Task Force 4");
			Time.WaitSec ();

			Process p = new Test_FindEdge ();
			ButtonEvents but = new ButtonEvents();

			//stop program if user presses escape button
			but.EscapePressed += delegate() {
				p.Stop ();
				Log.Info ("LTF4 finished");
				Time.Wait (500);
			};

			//start program
			p.Start ();
			//TODO: start when finished init and button pushed
		}
	}
}