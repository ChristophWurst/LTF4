using System;
using Robot;
using MonoBrickFirmware.UserInput;
using MonoBrickFirmware.Sound;

using System.Threading;


namespace Robot{

	class MainClass
	{

		public static void Main (string[] args)
		{
			//TODO: init dialog

			Log.Info("Robot started");
			Time.WaitSec ();
			Log.Info("Left Button = find mode \n Right Button = escape mode");

			Task t = new Test_MoveTouch ();
			ButtonEvents but = new ButtonEvents();
			ButtonEvents start_program = new ButtonEvents ();

			bool begin = false;

			while (!begin) {

				//escape mode - right button
				start_program.RightPressed += delegate() {

					t = new Escape ();
					t.Start ();
					begin = true;
					//TODO: start when finished init and button pushed
			};

			//find mode - left button
			start_program.LeftPressed += delegate() {

				t = new Find ();
				t.Start ();
				begin = true;

			};

		}
			//stop program if user presses escape button
			but.EscapePressed += delegate() {
				t.Stop ();
				Log.Info ("Robot finished");
				Time.Wait (500);
			};

			//init process
			t.Init ();
			//beep so user knows init finished
			Speaker speaker = new Speaker (100);
			speaker.Beep (50 ,100);

		}
	}
}
