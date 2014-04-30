using System;
using System.Threading;
using Robot;

namespace Robot
{
	public class Find : Task
	{
		internal override void Loop() {
			while (running) {
				//TODO: implementation
				Thread.Sleep (0);
			}
		}

		public override void Init() {
			this.proc = new Thread (new ThreadStart (Loop));
		}
	}
}

