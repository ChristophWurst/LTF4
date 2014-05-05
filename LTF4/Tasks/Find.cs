using System;
using System.Threading;
using Robot;
using MonoBrickFirmware.Sensors;

namespace Robot
{
	public class Find : Task
	{

		private Movement move;
		private TouchSensor touch;
		private IRSensor dist;
		private EV3ColorSensor color;
		private Random direction_generator;   //for random turn

		internal override void Loop() {
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
		}

		public override void Init() {
			this.proc = new Thread (new ThreadStart (Loop));
		}
	}
}

