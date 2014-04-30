using System;
using System.Threading;
using Robot;
using MonoBrickFirmware.Sensors;
using MonoBrickFirmware.Sound;

namespace Robot
{
	public class Test_FindBeep : Task
	{
		private Movement move;
		private IRSensor dist;
		private Speaker speak;

		internal override void Loop() {
			move.Forward (50);
			while (running) {
				if (this.dist.Read () < 5) {
					move.Brake ();
					speak.Beep (50, 100);
					running = false;
				}
				Thread.Sleep (0);
			}
		}

		public override void Init() {
			this.proc = new Thread (new ThreadStart (Loop));

			this.move = new Movement();
			this.dist = new IRSensor (SensorPort.In1, IRMode.Proximity);

			this.speak = new Speaker (100);
		}
	}
}

