using System;
using MonoBrickFirmware.Display;

namespace Robot
{
	public class Log
	{
		public const int LEVEL_DEBUG = 1;
		public const int LEVEL_INFO = 2;
		public const int LEVEL_WARNING = 3;
		public const int LEVEL_ERROR = 4;

		public static int level = LEVEL_DEBUG;

		public static void Debug(String message) {
			if (level <= LEVEL_DEBUG) {
				LcdConsole.WriteLine ("[D] " + message);
			}
		}

		public static void Info(String message) {
			if (level <= LEVEL_INFO) {
				LcdConsole.WriteLine ("[I] " + message);
			}
		}

		public static void Warning(String message) {
			if (level <= LEVEL_WARNING) {
				LcdConsole.WriteLine ("[W] " + message);
			}
		}

		public static void Error(String message) {
			LcdConsole.WriteLine ("[E] " + message);
		}
	}
}
