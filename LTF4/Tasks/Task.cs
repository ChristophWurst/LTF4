using System;
using System.Threading;
using Robot;
using MonoBrickFirmware.UserInput;
using MonoBrickFirmware.Sensors;
using MonoBrickFirmware.Sound;

namespace Robot
{
	public abstract class Task
	{
		internal Boolean running = false;
		internal Thread proc = null;
		internal virtual void Loop() {}
		public virtual void Init () {}

		public Color enemyColor;

		public void Start() {
			Log.Debug ("process starting");
			running = true;
			proc.Start ();
			Log.Debug ("process started");
		}

		public void Stop() {
			Log.Debug ("process stopping");
			if (running) {
				running = false;
				//wait for threat to finish
				proc.Join ();
			}
			Log.Debug ("process stopped");
		}
	}
}

