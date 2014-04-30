using System;
using MonoBrickFirmware.Movement;
using Robot;

namespace Robot
{
	public class Movement
	{
		private MotorSync sync;

		private void Wait(int time) {
			if (time > 0) {
				Log.Debug ("time: " + time.ToString());
				Time.Wait (time);
				this.Off ();
			}
		}

		public Movement () {
			Log.Debug("Movement init started");
			sync = new MotorSync(MotorPort.OutA, MotorPort.OutD);
			this.Off ();
		}

		public void Forward(sbyte speed, int time = 0) {
			Log.Debug("move forward");
			this.sync.On(speed, 0, 0, false, false);
			this.Wait (time);
		}

		public void TurnLeft(int degrees = 90) {
			Log.Debug ("turn left");
			this.sync.Brake ();
			this.sync.On (85, 200, 0, true, false);
			this.Wait (degrees * 4);
		}

		public void Backward(sbyte speed, int time = 0) {
			Log.Debug ("move backward");
			this.sync.On((sbyte)-speed, 0, 0, false, false);
			this.Wait (time);
		}

		public void Off() {
			Log.Debug ("motors off");
			sync.Off ();
		}

		public void Brake() {
			Log.Debug ("motors brake");
			sync.Brake ();
		}
	}
}