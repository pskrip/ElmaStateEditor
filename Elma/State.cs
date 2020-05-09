using System;
using System.Collections.Generic;
using System.Linq;

namespace Elma
{
	internal class State : IState
	{
		public const int MaxNumberOfPlayers = 50;
		public const int NumberOfLevels = 54;
		public const int MaxNameLength = 8;
		public const int MaxRecordsInLev = 10;

		public const int StateSize = 67910;
		private const int OffsetNumberOfPlayers = 67724;
		private static int OffsetPlayer(int player) => 61924 + 116 * player;
		private static int OffsetPlayerSkipped(int player) => OffsetPlayer(player) + 16;
		private static int OffsetPlayerOpened(int player) => OffsetPlayer(player) + 108;
		private const int SizeLevel = 688;
		private static int OffsetLevel(int lev) => 4 + SizeLevel * lev;
		private static int OffsetRecordTime(int lev, int rec) => OffsetLevel(lev) + 4 + 4 * rec;
		private static int OffsetRecordPlayerA(int lev, int rec) => OffsetLevel(lev) + 44 + 15 * rec;
		private static int OffsetRecordPlayerB(int lev, int rec) => OffsetLevel(lev) + 194 + 15 * rec;
		private const int OffsetPlayerA = 67728;
		private const int OffsetPlayerB = 67743;

		private byte[] _data;
		private readonly List<IPlayer> _players;
		private readonly List<IRecord>[] _levelRecords;

		public State()
		{
			_data = new byte[StateSize];
			_players = new List<IPlayer>();
			_levelRecords = new List<IRecord>[NumberOfLevels];
		}

		#region IState

		public ICollection<IPlayer> Players => _players;

		public bool Load(IEnumerable<byte> state)
		{
			var data = XorWithEmptyState(state);
			if (data.Length != StateSize)
				return false;

			// Should do more checks actually
			// Or not because able to recover damaged file
			_data = data;

			// read players
			_players.Clear();
			int numberOfPlayers = Math.Min((int)data[OffsetNumberOfPlayers], MaxNumberOfPlayers);
			foreach (int p in Enumerable.Range(0, numberOfPlayers))
				_players.Add(ReadPlayer(p));

			// read level times
			foreach (int lev in Enumerable.Range(0, NumberOfLevels))
				_levelRecords[lev] = ReadLevelRecords(lev);

			return true;
		}

		public byte[] Save()
		{
			var players = _players.Take(MaxNumberOfPlayers).Select((player, pos) => (player, pos)).ToList();
			_data[OffsetNumberOfPlayers] = (byte)players.Count;
			WriteName(_players.FirstOrDefault()?.Name ?? "", OffsetPlayerA);
			WriteName(_players.FirstOrDefault()?.Name ?? "", OffsetPlayerB);

			foreach (var (player, pos) in players)
				WritePlayer(player, pos);

			foreach (var lev in Enumerable.Range(0, NumberOfLevels))
				WriteLevelRecords(lev);

			return XorWithEmptyState(_data);
		}

		public ICollection<IRecord> Top10(int level) => _levelRecords[level];

		public string CalculateTotalTime(string playerName)
		{
			int totalHSec = 0;
			foreach (var top10 in _levelRecords)
			{
				var rec = top10.FirstOrDefault(r => r.Player == playerName);
				if (rec is null)
					return "";

				totalHSec += rec.Time.ToHSeconds();
			}

			int hour = totalHSec / 360000;
			totalHSec %= 360000;
			int min = totalHSec / 6000;
			totalHSec %= 6000;
			int sec = totalHSec / 100;
			int hsec = totalHSec % 100;
			string tt = hour > 0 ? $"{hour}h" : "";
			return tt + $"{min:D2}:{sec:D2}.{hsec:D2}";
		}

		#endregion

		private byte[] XorWithEmptyState(IEnumerable<byte> data) =>
			Properties.Resources.EmptyState.Zip(data, (b1, b2) => (byte)(b1 ^ b2)).ToArray();

		private Player ReadPlayer(int player)
		{
			var skipped = _data
				.Skip(OffsetPlayerSkipped(player))
				.Take(NumberOfLevels)
				.Select((b, lev) => (b, lev))
				.Where(p => p.b == 1)
				.Select(p => p.lev);

			return new Player
			{
				Name = ReadName(OffsetPlayer(player)),
				OpenedLevels = _data[OffsetPlayerOpened(player)],
				SkippedLevels = skipped.ToList()
			};
		}

		private void WritePlayer(IPlayer player, int number)
		{
			WriteName(player.Name, OffsetPlayer(number));
			_data[OffsetPlayerOpened(number)] = (byte)((Player)player).OpenedLevels;

			foreach (int i in Enumerable.Range(0, NumberOfLevels))
				_data[OffsetPlayerSkipped(number) + i] = 0;

			foreach (int lev in player.SkippedLevels)
				_data[OffsetPlayerSkipped(number) + lev] = 1;
		}

		private List<IRecord> ReadLevelRecords(int lev) =>
			Enumerable.Range(0, Math.Min((int)_data[OffsetLevel(lev)], MaxRecordsInLev))
				.Select(rec => new Record
				{
					Time = ReadTime(OffsetRecordTime(lev, rec)),
					Player = ReadName(OffsetRecordPlayerA(lev, rec))
				})
				.Cast<IRecord>()
				.ToList();

		private void WriteLevelRecords(int lev)
		{
			var top10 = _levelRecords[lev]
				.OrderBy(rec => rec.Time.ToHSeconds())
				.Take(MaxRecordsInLev)
				.Select((rec, pos) => (rec, pos))
				.ToList();

			_data[OffsetLevel(lev)] = (byte)top10.Count;
			foreach (var (rec, pos) in top10)
			{
				WriteTime(rec.Time, OffsetRecordTime(lev, pos));
				WriteName(rec.Player, OffsetRecordPlayerA(lev, pos));
				WriteName(rec.Player, OffsetRecordPlayerB(lev, pos));
			}
		}

		private string ReadName(int offset) =>
			new string(_data
				.Skip(offset)
				.Take(MaxNameLength)
				.TakeWhile(b => b != 0)
				.Select(b => (char)b)
				.ToArray());

		private Time ReadTime(int offset) =>
			new Time(_data[offset] + _data[offset + 1] * 256);

		private void WriteName(string name, int offset)
		{
			foreach (char c in name.Take(MaxNameLength))
				_data[offset++] = (byte)c;
			if (name.Length < MaxNameLength)
				_data[offset] = 0;
		}

		private void WriteTime(ITime time, int offset)
		{
			_data[offset] = (byte)(time.ToHSeconds() % 256);
			_data[offset + 1] = (byte)(time.ToHSeconds() / 256);
		}

		private void RemovePlayerRecords(string playerName)
		{
			foreach (var top10 in _levelRecords)
				top10.RemoveAll(rec => rec.Player == playerName);
		}
	}
}