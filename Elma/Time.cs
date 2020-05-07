using System;
using System.Linq;

namespace Elma
{
	internal class Time : ITime
	{
		private int _totalHSec = 0;

		public Time() {}
		public Time(int hseconds) => FromHSeconds(hseconds);

		public new string ToString()
		{
			var mins = _totalHSec / 6000;
			var hsec = _totalHSec % 6000;
			var sec = hsec / 100;
			var ms = hsec % 100;
			return $"{mins:D2}:{sec:D2}.{ms:D2}";
		}

		public int ToHSeconds()
		{
			return _totalHSec;
		}

		public bool FromString(string time)
		{
			try
			{
				var str = time.Replace(':', '.');
				var split = str.Split('.');
				if (split.Last().Length < 2)
					split[split.Length - 1] = split.Last() + "0";
				int min = split.Length == 3 ? int.Parse(split.First()) : 0;
				int sec = int.Parse(split[split.Length - 2]);
				int hsec = int.Parse(split.Last());

				var totalHSec = hsec + sec * 100 + min * 6000;
				if (totalHSec < 0 || totalHSec > 0xFFFF)
					return false;
				_totalHSec = totalHSec;
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}

		public bool FromHSeconds(int hseconds)
		{
			if (hseconds < 0 || hseconds > 0xFFFF)
				return false;
			_totalHSec = hseconds;
			return true;
		}
	}
}
