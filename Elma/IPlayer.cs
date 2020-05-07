using System.Collections.Generic;

namespace Elma
{
	public interface IPlayer
	{
		string Name { get; set; }
		int FinishedLevels { get; }
		IEnumerable<int> SkippedLevels { get; }
		void UnlockAllLevels();
	}
}
