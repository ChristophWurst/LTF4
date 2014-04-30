using System;
using System.Threading;
using LTF4;

namespace LTF4
{
	public class Find : Process
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

