using System;
using System.Threading;
using LTF4;
using MonoBrickFirmware.Sensors;
using MonoBrickFirmware.Sound;

namespace LTF4
{
	public class Test_FindEdge : Process
	{
		private Speaker speak;
		private Movement move;
		private TouchSensor touch;

		internal override void Loop() {
			move.Forward (50);
			while (running) {
				if (this.touch.IsPressed ()) {
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
			this.speak = new Speaker(100);

			this.move = new Movement();
			this.touch = new TouchSensor(SensorPort.In4);

			//beep so user knows init finished
			this.speak.Beep (50 ,100);
		}
	}
}

