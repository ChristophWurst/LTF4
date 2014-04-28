using System;
using System.Threading;
using LTF4;

namespace LTF4
{
	public abstract class Process
	{
		internal Boolean running = false;
		internal Thread proc = null;

		internal virtual void Loop() {}
		public virtual void Init () {}

		public void Start() {
			Log.Debug ("process starting");
			running = true;
			proc.Start ();
			Log.Debug ("process started");
		}

		public void Stop() {
			Log.Debug ("process stopping");
			running = false;
			//wait for threat to finish
			proc.Join ();
			Log.Debug ("process stopped");
		}
	}
}

