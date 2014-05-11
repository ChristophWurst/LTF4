using System;
using System.Threading;
using Robot;
using MonoBrickFirmware.Sensors;

namespace Robot {

	public class Escape : Task {

		private Movement move;
		private TouchSensor touch;
		private IRSensor dist;
		private EV3ColorSensor color;
		private Random direction_generator;   //for random turn
	
		internal override void Loop() {

			move.Forward (70);

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
					this.move.Forward (70);

				}

				//create random number for random turns while moving forward
				int random_turn = direction_generator.Next (0,100);

				int rand_deg = direction_generator.Next(70, 130);

				//rand turn + rand degree
				if(random_turn == 60){

					this.move.TurnLeft (rand_deg);

				}else if (random_turn == 30){

					this.move.TurnRight (rand_deg);
				}

				//dedected object
				if (this.dist.Read () < 50) {

					this.move.Backward (90, 700);

					int next_direction = direction_generator.Next (0,1);

					//turn arround
					if (next_direction > 0.5) {

						this.move.TurnLeft (180);

					} else {

						this.move.TurnRight (180);
					}

					//and move on
					this.move.Forward (80);
				}

				Thread.Sleep (0);
			}

			this.move.Brake ();
		}

	
			
		public override void Init() {
			this.proc = new Thread (new ThreadStart (Loop));
			this.move = new Movement();
			this.touch = new TouchSensor(SensorPort.In4);
			this.dist = new IRSensor (SensorPort.In1, IRMode.Proximity);
		}
	}
}

