using System;
using System.Threading;
using Robot;
using MonoBrickFirmware.Sensors;

namespace Robot
{
	public class Escape : Task
	{
		private Movement move;
		private TouchSensor touch;
		private IRSensor dist;
	
		internal override void Loop() {
			move.Forward (50);
			while (running) {
				if ((this.touch.IsPressed ()) || (this.dist.Read () < 20)) {
					//move back a little bit
					this.move.Backward (90, 700);
					//turn
					//TODO: random turn
					this.move.TurnLeft (180);
					//move on
					this.move.Forward (50);
				}
				Thread.Sleep (0);
			}
			move.Brake ();
		}

		public override void Init() {
			this.proc = new Thread (new ThreadStart (Loop));

			this.move = new Movement();
			this.touch = new TouchSensor(SensorPort.In4);
			this.dist = new IRSensor (SensorPort.In1, IRMode.Proximity);
		}
	}
}

