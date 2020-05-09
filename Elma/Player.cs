using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Elma
{
	internal class Player : IPlayer
	{
		internal int OpenedLevels;

		// Unlocking internals by default
		public Player() => UnlockAllLevels();

		public string Name { get; set; }
		public int FinishedLevels => OpenedLevels - SkippedLevels.Count;
		public IReadOnlyCollection<int> SkippedLevels { get; internal set; }
		public void UnlockAllLevels()
		{
			OpenedLevels = State.NumberOfLevels;
			SkippedLevels = new List<int>();
		}
	}
}
