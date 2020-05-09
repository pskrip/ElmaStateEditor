using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Elma;

namespace ElmaStateEditor
{
	public partial class FormEditor : Form
	{
		private const string Title = @"State.dat editor";

		private IState _state;
		private readonly string[] _levelNames = Info.LevelNames.ToArray();
		private int _currentLev;
		private string _openedFile;
		private bool _changed;
		private bool Changed
		{
			get => _changed;
			set { _changed = value; UpdateTitleAndMenu(); }
		}

		public FormEditor()
		{
			InitializeComponent();
			dataGridViewPlayers.Enabled = false;
			dataGridViewRecords.Enabled = false;
			dataGridViewTop10.Enabled = false;
			Player.MaxInputLength = Info.MaxPlayerNameLength;
			Players.MaxInputLength = Info.MaxPlayerNameLength;
			UpdateTitleAndMenu();
		}

		private void OpenFile()
		{
			if (openFileDialog1.ShowDialog() != DialogResult.OK)
				return;

			var file = File.ReadAllBytes(openFileDialog1.FileName);
			if (file.Length != Info.EmptyState.Count)
			{
				MessageBox.Show(@"Wrong file size");
				return;
			}

			_state = Elma.Container.NewState();
			_state.Load(file);

			_openedFile = openFileDialog1.FileName;
			Changed = false;

			DisplayPlayers();
			DisplayBestTimes();
		}

		private void SaveFile()
		{
			SavePlayers();
			SaveTop10();
			File.WriteAllBytes(_openedFile, _state.Save());
			Changed = false;
		}

		private void SavePlayers()
		{
			_state.Players.Clear();
			foreach (DataGridViewRow row in dataGridViewPlayers.Rows)
			{
				if (!row.IsNewRow)
				{
					var player = Elma.Container.NewPlayer();
					player.Name = row.Cells[0].Value as string ?? "";
					_state.Players.Add(player);
				}
			}
		}

		private void DisplayPlayers()
		{
			dataGridViewPlayers.CellValidating -= dataGridViewPlayers_CellValidating;
			dataGridViewPlayers.Rows.Clear();
			foreach (var player in _state.Players)
				dataGridViewPlayers.Rows.Add(player.Name, player.FinishedLevels, _state.CalculateTotalTime(player.Name));
			dataGridViewPlayers.CellValidating += dataGridViewPlayers_CellValidating;
			dataGridViewPlayers.Enabled = true;
		}

		private void DisplayBestTimes()
		{
			dataGridViewRecords.Rows.Clear();
			for (int lev = 0; lev < _levelNames.Length; lev++)
			{
				var times = _state.Top10(lev).ToArray();
				string firstTime = "";
				if (times.Any())
				{
					var first = times.First();
					firstTime = $"{first.Time.ToString()} {first.Player}";
				}
				dataGridViewRecords.Rows.Add($"{lev + 1}. {_levelNames[lev]}", firstTime);
			}
			dataGridViewRecords.Enabled = true;
			_currentLev = 0;
		}

		private void DisplayTop10(int lev)
		{
			dataGridViewTop10.CellValidating -= dataGridViewTop10_CellValidating;
			dataGridViewTop10.Rows.Clear();
			foreach (var rec in _state.Top10(lev))
				dataGridViewTop10.Rows.Add(rec.Time.ToString(), rec.Player);
			_currentLev = lev;
			dataGridViewTop10.CellValidating += dataGridViewTop10_CellValidating;
			dataGridViewTop10.Enabled = true;
		}

		private void SaveTop10()
		{
			var top10 = _state.Top10(_currentLev);
			top10.Clear();
			foreach (DataGridViewRow row in dataGridViewTop10.Rows)
			{
				var time = Elma.Container.NewTime();
				if (!(row.Cells[0].Value is string str) || !time.FromString(str))
					continue;

				var record = Elma.Container.NewRecord();
				record.Time = time;
				record.Player = row.Cells[1].Value as string ?? "";
				top10.Add(record);
			}
		}

		private void UpdateTitleAndMenu()
		{
			var text = Changed ? "* " : "";
			if (_state != null && _openedFile != null)
				text += $"{_openedFile} - ";
			text += Title;
			Text = text;

			saveToolStripMenuItem.Enabled = Changed;
			removeTimesExceptBestToolStripMenuItem.Enabled = _state != null;
			// not implemented yet
			//saveAsToolStripMenuItem.Enabled = _state != null;
		}

		private void RenamePlayerRecords(string oldName, string newName)
		{
			var playerRecords = Enumerable.Range(0, _levelNames.Length)
				.SelectMany(lev => _state.Top10(lev))
				.Where(rec => rec.Player == oldName);

			foreach (var record in playerRecords)
				record.Player = newName;
		}

		#region Event handlers

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFile();
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFile();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO
		}

		private void dataGridViewRecords_SelectionChanged(object sender, EventArgs e)
		{
			if (dataGridViewRecords.CurrentRow != null)
			{
				if (dataGridViewRecords.CurrentRow.Index != _currentLev)
					SaveTop10();
				DisplayTop10(dataGridViewRecords.CurrentRow.Index);
			}
		}

		private void dataGridViewTop10_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == -1 || e.RowIndex == -1)
				return;

			var cells = dataGridViewTop10.Rows[e.RowIndex].Cells;
			if (cells[0].Value != null || cells[1].Value != null)
				dataGridViewTop10.Sort(dataGridViewTop10.Columns[0], ListSortDirection.Ascending);

			Changed = true;
		}

		private void dataGridViewTop10_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			// validate time
			if (e.ColumnIndex == 0)
			{
				var time = Elma.Container.NewTime();
				if (e.FormattedValue.ToString().Any() && !time.FromString(e.FormattedValue.ToString()))
					e.Cancel = true;
			}
			// validate name
			else
			{
				if (e.FormattedValue.ToString().Length > Info.MaxPlayerNameLength)
					e.Cancel = true;
			}
		}

		private void dataGridViewTop10_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
		{
			// parse time
			if (e.ColumnIndex == 0)
			{
				var time = Elma.Container.NewTime();
				if (!time.FromString(e.Value.ToString()))
					return;
				e.Value = time.ToString();
				e.ParsingApplied = true;
			}
		}

		private void dataGridViewTop10_UserAddedRow(object sender, DataGridViewRowEventArgs e)
		{
			Changed = true;
		}

		private void dataGridViewTop10_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
		{
			Changed = true;
		}

		private void dataGridViewPlayers_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (e.RowIndex == -1 || e.ColumnIndex != 0)
				return;

			var cells = dataGridViewPlayers.Rows[e.RowIndex].Cells;
			var oldName = cells[0].Value as string;
			var newName = e.FormattedValue as string;
			var isNewRow = dataGridViewPlayers.Rows[e.RowIndex].IsNewRow;

			// allow empty bottom row
			if (isNewRow/* && newName == ""*/)
				return;

			if (newName == oldName)
				return;

			// check for same existing player name
			foreach (DataGridViewRow row in dataGridViewPlayers.Rows)
			{
				if (row.Index != e.RowIndex && !row.IsNewRow
					&& (row.Cells[0].Value as string ?? "") == newName)
				{
					e.Cancel = true;
					return;
				}
			}

			if (cells[1].Value is null)
				cells[1].Value = _levelNames.Length;

			// rename player
			if (oldName != null)
			{
				RenamePlayerRecords(oldName, newName);
				DisplayBestTimes();
			}

			Changed = true;
		}

		private void dataGridViewPlayers_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			var name = e.Row.Cells[0].Value as string ?? "";
			int times = Enumerable.Range(0, _levelNames.Length)
				.SelectMany(lev => _state.Top10(lev))
				.Count(rec => rec.Player == name);

			if (times == 0)
			{
				Changed = true;
				return;
			}

			switch (MessageBox.Show(
				@$"Player '{name}' has {times} records. Do you want to remove them'?",
				"Remove records",
				MessageBoxButtons.YesNoCancel))
			{
				case DialogResult.Cancel:
					e.Cancel = true;
					break;
				case DialogResult.No:
					Changed = true;
					break;
				case DialogResult.Yes:
					foreach (var top10 in Enumerable.Range(0, _levelNames.Length).Select(lev => _state.Top10(lev)))
						foreach (var rec in top10.Where(rec => rec.Player == name).ToList())
							top10.Remove(rec);

					DisplayBestTimes();
					Changed = true;
					break;
			}
		}

		private void removeTimesExceptBestToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool changed = false;
			foreach (var lev in Enumerable.Range(0, _levelNames.Length))
			{
				var top10 = _state.Top10(lev);
				changed |= top10.Count > 1;
				var best = top10.FirstOrDefault();
				top10.Clear();
				if (best != null)
					top10.Add(best);
			}
			if (changed)
				DisplayBestTimes();
			Changed |= changed;
		}

		#endregion
	}
}
