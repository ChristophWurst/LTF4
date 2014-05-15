using System;
using Robot;
using MonoBrickFirmware.UserInput;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.Display.Dialogs;
using MonoBrickFirmware.Display.Menus;
using MonoBrickFirmware.Sensors;

using System.Threading;
using System.Collections.Generic;

namespace Robot {
	class MainClass {
		//task IDs
		const int taskFind = 1;
		const int taskEscape = 2;

		static Font font = Font.MediumFont;
		static Action menuAction = null;
		static bool exit = false;
		static bool running = true;
		static Task t = new Find();
		static int taskID = taskFind;
		static Color enemyColor = Color.Red;

		private static bool Start(Lcd lcd, Buttons btns) {
			if (taskID == taskFind) {
				t = new Find ();
			} else if (taskID == taskEscape) {
				t = new Escape ();
			} else {
				//unknown task ID
				return false;
			}

			//clear previous log messages
			LcdConsole.Clear ();

			//set enemy color
			t.enemyColor = enemyColor;

			//init
			int oldLog = Log.level;
			Log.level = Log.LEVEL_ERROR;

			lcd.Clear ();
			Rectangle textRect = new Rectangle (new Point (0, Lcd.Height - (int)font.maxHeight - 2), new Point (Lcd.Width, Lcd.Height - 2));
			lcd.WriteTextBox (font, textRect, "initializing...", true, Lcd.Alignment.Center);
			lcd.Update ();

			t.Init ();

			InfoDialog InitFinishedDialog = new InfoDialog (font, lcd, btns, "Press ENTER to start", true, "Init finished");
			InitFinishedDialog.Show ();

			Log.level = oldLog;
			running = true;

			ButtonEvents be = new ButtonEvents ();
			be.EscapePressed += Stop;

			try {
				t.Start();
			}
			catch(Exception e) {
				InfoDialog dialog = new InfoDialog (font, lcd, btns, e + " Exception caught.", true, "Init finished");
				dialog.Show ();
				return false;
			}

			//wait for task to finish
			while (running)
				Thread.Sleep (50);

			be.EscapePressed -= delegate() {
				t.Stop();
				running = false;
			};

			//unregister event
			be.EscapePressed -= Stop;
			//let the GC free the memory
			t = null;

			return true;
		}

		private static void Stop() {
			t.Stop();
			running = false;
		}

		private static bool Exit(Lcd lcd, Buttons btns) {
			lcd.Clear ();
			exit = true;
			return true;
		}

		private static void ShowMainMenu(Lcd lcd, Buttons btns) {
			List<IMenuItem> items = new List<IMenuItem>();
			items.Add (new MenuItemWithAction(lcd, "Start", () => Start(lcd, btns), MenuItemSymbole.None));

			int[] tasks = { taskFind, taskEscape };
			int curTask = Array.IndexOf (tasks, taskID);
			var itemMode = new MenuItemWithOptions<string>(lcd,"Mode", new string[]{"FIND","ESCAPE"}, curTask);
			itemMode.OnOptionChanged += delegate(string selection) {
				if (selection == "FIND") {
					taskID = taskFind;
				}
				if (selection == "ESCAPE") {
					taskID = taskEscape;
				}
			};

			String red = Color.Red.ToString ().ToUpper ();
			String green = Color.Green.ToString ().ToUpper ();
			String blue = Color.Blue.ToString ().ToUpper ();
			String[] colors = { red, green, blue };
			int curColor = Array.IndexOf (colors, enemyColor.ToString ().ToUpper ());

			var itemEnemyColor = new MenuItemWithOptions<string>(lcd,"Enemy Color", colors, curColor);
			itemEnemyColor.OnOptionChanged += delegate(string selection) {
				if (selection == red) {
					enemyColor = Color.Red;
				}
				if (selection == green) {
					enemyColor = Color.Green;
				}
				if (selection == blue) {
					enemyColor = Color.Blue;
				}
			};

			items.Add (itemMode);
			items.Add (itemEnemyColor);

			items.Add (new MenuItemWithAction(lcd, "Exit", () => Exit(lcd, btns), MenuItemSymbole.None));
			Menu m = new Menu(font, lcd, btns ,"Robot", items);
			m.Show();
		}

		public static void Main (string[] args) {
			while (!exit) {
				menuAction = null;

				Lcd lcd = new Lcd ();
				Buttons btns = new Buttons ();

				ShowMainMenu (lcd, btns);
				if (menuAction != null) {
					menuAction ();
					menuAction = null;
				}
			} 
		}
	}
}