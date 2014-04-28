using System;
using System.Threading;
using LTF4;
using MonoBrickFirmware.Sensors;
using MonoBrickFirmware.Sound;

namespace LTF4
{
	public class Test_FindObject : Process
	{
		private Speaker speak;
		private Movement move;
		private IRSensor dist;
		private EV3ColorSensor color;

		internal override void Loop() {
			move.Forward (50);
			while (running) {
				if (this.dist.Read () < 5) {
					//found something
					this.move.Brake ();
					switch (this.color.ReadColor ()) {
					case Color.Blue:
						Log.Info ("found something blue");
						break;
					case Color.Red:
						Log.Info ("found something red");
						break;
					default:
						Log.Info ("unknown color found");
						break;
					}
				}
				Thread.Sleep (0);
			}
			move.Brake ();
		}

		public override void Init() {
			this.proc = new Thread (new ThreadStart (Loop));
			this.speak = new Speaker(100);

			this.move = new Movement();
			this.dist = new IRSensor (SensorPort.In1, IRMode.Proximity);
			this.color = new EV3ColorSensor (SensorPort.In2, ColorMode.Color);

			//beep so user knows init finished
			this.speak.Beep (50 ,100);
		}
	}
}

