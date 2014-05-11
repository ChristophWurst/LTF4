using System;
using System.Threading;
using Robot;
using MonoBrickFirmware.Sensors;
using MonoBrickFirmware.Sound;

namespace Robot
{
	public class Find : Task
	{

		private Movement move;
		private TouchSensor touch;
		private IRSensor dist;
		private EV3ColorSensor color;
		private Speaker speak;
		private Random direction_generator;   //for random turn

		internal override void Loop() {
		
			//TODO read enemy color and save it
			//if enemy color dedected -> touch enemy and beep 
			//default color -> escape

			bool color_saved = false;
			Color enemy;

			//get enemy color
			while (!color_saved) {

				move.Forward (30);

				if (this.color.ReadColor()) {

					enemy = this.color.ReadColor ();
					color_saved = true;
				}
			}

			move.Forward (70);

			while (running) {
				if (this.touch.IsPressed ()) {
					//move back a little bit
					this.move.Backward (90, 700);

					//generate number between 0 and 1
					double next_direction = direction_generator.Next (0, 1);

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
				int random_turn = direction_generator.Next (0, 100);

				if (random_turn == 60) {

					this.move.TurnLeft ();

				} else if (random_turn == 30) {

					this.move.TurnRight ();
				}

				if (this.dist.Read () < 20) {

					this.move.Forward (40);   //move slow

					if (this.dist.Read () < 5) {

						//found something
						this.move.Brake ();

						switch (this.color.ReadColor ()) {

						default:

							Log.Info ("found friend!");   //friend -> escape

							this.move.Backward (90, 700);

							int next_direction = direction_generator.Next (0, 1);

								//turn arround
							if (next_direction > 0.5) {

								this.move.TurnLeft (180);

							} else {

								this.move.TurnRight (180);
							}

								//and move on
							this.move.Forward (80);

							break;

						case Color == enemy:

							Log.Info ("found enemy!");  //enemy
							this.move.Forward (30, 1);
							this.move.Brake ();
							speak.Beep (50, 100);

							break;
						}
					}
					Thread.Sleep (0);
				}
			}
			this.move.Brake ();
		}


		public override void Init() {
			this.proc = new Thread (new ThreadStart (Loop));
		}
	}
}

