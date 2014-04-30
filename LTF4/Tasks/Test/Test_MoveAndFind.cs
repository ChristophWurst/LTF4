using System;
using System.Threading;
using LTF4;
using MonoBrickFirmware.Sensors;

namespace LTF4
{
	public class Test_MoveAndFind : Process
	{
		private Movement move;
		private TouchSensor touch;
		private IRSensor dist;
		private EV3ColorSensor color;

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

			this.move = new Movement();
			this.touch = new TouchSensor(SensorPort.In4);
			this.dist = new IRSensor (SensorPort.In1, IRMode.Proximity);
			this.color = new EV3ColorSensor (SensorPort.In2, ColorMode.Color);
		}
	}
}

