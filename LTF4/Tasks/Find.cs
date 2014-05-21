using System;
using System.Threading;
using Robot;
using MonoBrickFirmware.Sensors;
using MonoBrickFirmware.Sound;

namespace Robot {
	public class Find : Task {
		const sbyte speedForward = 100;
		const int timeBackward = 600;

		private Movement move;
		private TouchSensor touch;
		private IRSensor dist;
		private EV3ColorSensor color;
		private Speaker speak;
		private Random rand;
		private int loopCount;

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

		private void TurnRandom() {
			//create random number for random turns while moving forward
			if (loopCount > 10000) {
				if (rand.Next (10000) == 60) {
					this.move.TurnLeft ();
				} else {
					this.move.TurnRight ();
				}
				loopCount = 0;
			}
		}

		private void CheckDistance() {
			if (this.dist.Read () < 5) {
				//found something
				//this.move.Brake ();
				Thread.Sleep (500);
				Log.Info("scanning color...");
				if (this.color.ReadColor () == enemyColor) {
					Log.Info ("found enemy!");  //enemy
					this.move.Forward (30, 1);
					this.move.Brake ();
					speak.Beep (50, 500);
					speak.Beep(
				} else {
					Log.Info ("found friend!");   //friend -> escape
				}
				this.DriveBackAndTurnRandom ();
			}
		}

		internal override void Loop() {
			try {
				move.Forward (speedForward);
				//this.loopCount = 0;
				while (running) {
					this.loopCount++;
					this.CheckTouch ();
					//this.TurnRandom ();
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
			this.color = new EV3ColorSensor(SensorPort.In2);
			this.dist = new IRSensor (SensorPort.In1, IRMode.Proximity);
			this.speak = new Speaker (100);
			this.rand = new Random ();
		}
	}
}

