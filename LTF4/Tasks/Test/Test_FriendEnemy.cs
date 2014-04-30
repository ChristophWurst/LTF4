using System;
using System.Threading;
using LTF4;
using MonoBrickFirmware.Sensors;

namespace LTF4
{
	public class Test_FriendEnemy : Process
	{
		private IRSensor dist;
		private EV3ColorSensor color;

		internal override void Loop() {
			while (running) {
				if (this.dist.Read () < 5) {
					//found something
					switch (this.color.ReadColor ()) {
					case Color.Blue:
						Log.Info ("friend (blue)");
						break;
					case Color.Red:
						Log.Info ("enemy (red)");
						break;
					default:
						Log.Info ("unknown color");
						break;
					}
				}
				Thread.Sleep (100);
			}
		}

		public override void Init() {
			this.proc = new Thread (new ThreadStart (Loop));

			this.dist = new IRSensor (SensorPort.In1, IRMode.Proximity);
			this.color = new EV3ColorSensor (SensorPort.In2, ColorMode.Color);
		}
	}
}

