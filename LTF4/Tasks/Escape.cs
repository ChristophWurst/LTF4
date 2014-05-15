using System;
using System.Threading;
using Robot;
using MonoBrickFirmware.Sensors;

namespace Robot {

	public class Escape : Task {
		const sbyte speedForward = 100;
		const int timeBackward = 600;

		private Movement move;
		private TouchSensor touch;
		private IRSensor dist;
		private Random rand;
	
		private void DriveBackAndTurnRandom() {
			this.move.Brake ();
			//move back a little bit
			this.move.Backward (90, timeBackward);

			//random turn
			if (rand.Next(1, 3) == 1) {
				this.move.TurnLeft (rand.Next(90,180));
			} else {
				this.move.TurnRight (rand.Next(90,180));
			}

			//move on
			this.move.Forward (speedForward);
		}

		private void CheckTouch() {
			if (this.touch.IsPressed ()) {
				Log.Debug ("touch pressed");
				this.DriveBackAndTurnRandom ();
			}
		}

		private void CheckDistance() {
			if (this.dist.Read () < 30) {
				Log.Debug ("near object found");
				this.DriveBackAndTurnRandom ();
			}
		}

		internal override void Loop() {
			try {
				move.Forward (speedForward);
				while (running) {
					this.CheckTouch ();
					this.CheckDistance ();
					Thread.Sleep (0);
				}
				this.move.Off ();
			} catch (Exception e) {
				Console.WriteLine("{0} Exception caught.", e);
				Log.Error (e.ToString() + " Exception");
			}
		}
			
		public override void Init() {
			this.proc = new Thread (new ThreadStart (Loop));
			this.move = new Movement();
			this.touch = new TouchSensor(SensorPort.In4);
			this.dist = new IRSensor (SensorPort.In1, IRMode.Proximity);
			this.rand = new Random();
		}
	}
}

