using System;
using System.Threading;
using Robot;
using MonoBrickFirmware.Sensors;

namespace Robot
{
	public class Test_FindEdge : Task
	{
		private Movement move;
		private TouchSensor touch;
		private Random direction_generator;   //for random turn

		internal override void Loop() {
			move.Forward (50);
			while (running) {
				if (this.touch.IsPressed ()) {
					//move back a little bit
					this.move.Backward (90, 700);

					//generate number between 0 and 1
					double next_direction = direction_generator.Next (0,1);

					//generate number between 90 and 180 for random degree 
					int random_degree = direction_generator.Next (90, 180);

					//random turn
					if (next_direction > 0.5) {

						this.move.TurnLeft (random_degree);

					} else {

						this.move.TurnRight (random_degree);
					}

					//move on
					this.move.Forward (50);

				}

				//create random number for random turns while moving forward
				int random_turn = direction_generator.Next (0,100);

				if(random_turn == 60){

					this.move.TurnLeft ();

				}else if (random_turn == 30){

					this.move.TurnRight ();
				}

				Thread.Sleep (0);
			}
		move.Brake ();
		}

		public override void Init() {
			this.proc = new Thread (new ThreadStart (Loop));

			this.move = new Movement();
			this.touch = new TouchSensor(SensorPort.In4);
		}
	}
}

