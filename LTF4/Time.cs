using System;

namespace Robot
{
	public class Time
	{
		public static void Wait(int ms = 100)
		{
			System.Threading.Thread.Sleep (ms);
		}

		public static void WaitSec(int sec = 1)
		{
			System.Threading.Thread.Sleep (sec * 1000);
		}
	}
}
