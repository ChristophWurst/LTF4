using System;
using LTF4;
using MonoBrickFirmware.UserInput;
using MonoBrickFirmware.Sound;

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

			Task t = new Test_MoveTouch ();
			ButtonEvents but = new ButtonEvents();

			//stop program if user presses escape button
			but.EscapePressed += delegate() {
				t.Stop ();
				Log.Info ("LTF4 finished");
				Time.Wait (500);
			};

			//init process
			t.Init ();
			//beep so user knows init finished
			Speaker speaker = new Speaker (100);
			speaker.Beep (50 ,100);

			//start process
			t.Start ();
			//TODO: start when finished init and button pushed
		}
	}
}