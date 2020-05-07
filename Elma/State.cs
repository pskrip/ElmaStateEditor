using System;
using System.Collections.Generic;
using System.Linq;

namespace Elma
{
	internal class State : IState
	{
		public const int MaxNumberOfPlayers = 50;
		public const int NumberOfLevels = 54;

		public const int StateSize = 67910;
		private const int OffsetNumberOfPlayers = 67724;
		private const int SizePlayer = 116;
		private static int OffsetPlayer(int player) => 61924 + SizePlayer * player;
		private const int OffsetPlayerSkipped = 16;
		private const int OffsetPlayerOpened = 108;
		private const int SizeLevel = 688;
		private static int OffsetLevel(int lev) => 4 + SizeLevel * lev;
		private static int OffsetRecordTime(int lev, int rec) => OffsetLevel(lev) + 4 + 4 * rec;
		private static int OffsetRecordPlayerA(int lev, int rec) => OffsetLevel(lev) + 44 + 15 * rec;
		private static int OffsetRecordPlayerB(int lev, int rec) => OffsetLevel(lev) + 194 + 15 * rec;
		private const int OffsetPlayerA = 67728;
		private const int OffsetPlayerB = 67743;

		private byte[] _data;
		private readonly List<IPlayer> _players;
		private readonly List<IRecord>[] _levelTimes;

		public State()
		{
			_data = new byte[StateSize];
			_players = new List<IPlayer>();
			_levelTimes = new List<IRecord>[NumberOfLevels];
		}

		#region IState

		public ICollection<IPlayer> Players => _players;

		public bool Load(IEnumerable<byte> state)
		{
			var data = XorWithEmptyState(state);
			if (data.Length != StateSize) return false;

			// read players
			_players.Clear();
			for (int i = 0; i < data[OffsetNumberOfPlayers]; i++)
				_players.Add(ReadPlayer(data.Skip(OffsetPlayer(i))));

			// read level times
			for (int lev = 0; lev < NumberOfLevels; lev++)
				_levelTimes[lev] = ReadLevelRecords(data, lev);

			_data = data;
			return true;
		}

		public byte[] Save()
		{
			_data[OffsetNumberOfPlayers] = (byte)_players.Count;
			if(_players.Any())
			{
				WriteName(_players.First().Name, OffsetPlayerA);
				WriteName(_players.First().Name, OffsetPlayerB);
			}

			foreach (var p in Enumerable.Range(0, Math.Min(_players.Count, MaxNumberOfPlayers)))
				WritePlayer(_players[p], OffsetPlayer(p));

			foreach (var lev in Enumerable.Range(0, NumberOfLevels))
				WriteLevelRecords(lev);

			return XorWithEmptyState(_data);
		}

		public ICollection<IRecord> Top10(int level)
		{
			return _levelTimes[level];
		}

		#endregion

		private byte[] XorWithEmptyState(IEnumerable<byte> data) =>
			Properties.Resources.EmptyState.Zip(data, (b1, b2) => (byte)(b1 ^ b2)).ToArray();

		private Player ReadPlayer(IEnumerable<byte> state)
		{
			var data = state.Take(SizePlayer).ToArray();
			var skipped = data.Skip(OffsetPlayerSkipped)
				.Take(NumberOfLevels)
				.Select((b, pos) => (b, pos))
				.Where(p => p.b == 1)
				.Select(p => p.pos);

			return new Player { Name = ReadName(data), OpenedLevels = data[OffsetPlayerOpened], SkippedLevels = skipped };
		}

		private void WritePlayer(IPlayer player, int offset)
		{
			WriteName(player.Name, offset);
			_data[offset + OffsetPlayerOpened] = (byte)((Player)player).OpenedLevels;
			foreach (int i in Enumerable.Range(0, NumberOfLevels))
				_data[offset + OffsetPlayerSkipped + i] = 0;
			foreach (int lev in player.SkippedLevels)
				_data[offset + OffsetPlayerSkipped + lev] = 1;
		}

		private List<IRecord> ReadLevelRecords(byte[] data, int lev)
		{
			var times = new List<IRecord>();
			for (int rec = 0; rec < data[OffsetLevel(lev)]; rec++)
			{
				var time = ReadTime(data.Skip(OffsetRecordTime(lev, rec)));
				var player = ReadName(data.Skip(OffsetRecordPlayerA(lev, rec)));
				times.Add(new Record { Time = time, Player = player });
			}

			return times;
		}

		private void WriteLevelRecords(int lev)
		{
			var times = _levelTimes[lev];
			_data[OffsetLevel(lev)] = (byte)times.Count;
			foreach (var rec in Enumerable.Range(0, Math.Min(times.Count, 10)))
			{
				WriteTime(times[rec].Time, OffsetRecordTime(lev, rec));
				WriteName(times[rec].Player, OffsetRecordPlayerA(lev, rec));
				WriteName(times[rec].Player, OffsetRecordPlayerB(lev, rec));
			}
		}

		private string ReadName(IEnumerable<byte> state) =>
			new string(state.Take(8).TakeWhile(b => b != 0).Select(b => (char)b).ToArray());

		private Time ReadTime(IEnumerable<byte> state)
		{
			var data = state.Take(2).ToArray();
			return new Time(data[0] + data[1] * 256);
		}

		private void WriteName(string name, int offset)
		{
			foreach (char c in name.Take(8))
				_data[offset++] = (byte)c;
			if(name.Length < 8)
				_data[offset] = 0;
		}

		private void WriteTime(ITime time, int offset)
		{
			_data[offset] = (byte)(time.ToHSeconds() % 256);
			_data[offset + 1] = (byte)(time.ToHSeconds() / 256);
		}

		private void RemovePlayerTimes(string playerName)
		{
			throw new System.NotImplementedException();
		}
	}
}