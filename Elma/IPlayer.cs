using System.Collections.Generic;

namespace Elma
{
	public interface IPlayer
	{
		string Name { get; set; }
		int FinishedLevels { get; }
		IReadOnlyCollection<int> SkippedLevels { get; }
		void UnlockAllLevels();
	}
}
