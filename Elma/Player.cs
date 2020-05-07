using System.Collections.Generic;
using System.Linq;

namespace Elma
{
	internal class Player : IPlayer
	{
		private List<int> _skippedLevels;
		internal int OpenedLevels;

		public Player() => _skippedLevels = new List<int>();

		public string Name { get; set; }
		public int FinishedLevels => OpenedLevels - _skippedLevels.Count;
		public IEnumerable<int> SkippedLevels
		{
			get => _skippedLevels;
			internal set => _skippedLevels = value.ToList();
		}

		public void UnlockAllLevels()
		{
			OpenedLevels = State.NumberOfLevels;
			_skippedLevels.Clear();
		}
	}
}
