using System.Collections.Generic;

namespace Elma
{
	public interface IState
	{
		ICollection<IPlayer> Players { get; }

		bool Load(IEnumerable<byte> state);
		byte[] Save();

		ICollection<IRecord> Top10(int level);

		string CalculateTotalTime(string playerName);
	}
}
