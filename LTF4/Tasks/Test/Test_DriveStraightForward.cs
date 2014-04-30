using System;
using System.Threading;
using LTF4;
using MonoBrickFirmware.Sensors;

namespace LTF4
{
	public class Test_DriveStraightForward : Process
	{
		private Movement move;

		internal override void Loop() {
			move.Forward (50);
			while (running) {
				//do nothing
				Thread.Sleep (0);
			}
			move.Brake ();
		}

		public override void Init() {
			this.proc = new Thread (new ThreadStart (Loop));

			this.move = new Movement();
		}
	}
}

