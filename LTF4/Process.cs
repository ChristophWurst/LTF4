using System;
using System.Threading;
using LTF4;
using MonoBrickFirmware.Movement;
using MonoBrickFirmware.Sensors;
using MonoBrickFirmware.Sound;

namespace LTF4
{
	public class Process
	{
		private Boolean running = false;
		private Thread proc = null;
		private Speaker speak;

		private Movement move;
		private TouchSensor touch;
		private IRSensor dist;
		private EV3ColorSensor color;

		private void Loop() {
			move.Forward (50);
			while (running) {
				if (this.touch.IsPressed()) {
					//move back a little bit
					this.move.Backward (90, 700);
					//turn
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

		public Process() {
			Log.Debug ("process initializing");
			this.proc = new Thread (new ThreadStart (Loop));
			this.speak = new Speaker(100);

			this.move = new Movement();
			this.touch = new TouchSensor(SensorPort.In4);
			this.dist = new IRSensor (SensorPort.In1, IRMode.Proximity);
			this.color = new EV3ColorSensor (SensorPort.In2, ColorMode.Color);
		}

		public void Start() {
			Log.Debug ("process starting");
			running = true;
			this.speak.Beep (50 ,100);
			proc.Start ();
			Log.Debug ("process started");
		}

		public void Stop() {
			Log.Debug ("process stopping");
			running = false;
			proc.Join ();
			Log.Debug ("process stopped");
		}
	}
}

