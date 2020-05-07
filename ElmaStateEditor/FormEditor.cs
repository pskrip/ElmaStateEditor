using System;
using System.Collections.Generic;
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
			UpdateTitleAndMenu();
		}

		private void OpenFile()
		{
			if (openFileDialog1.ShowDialog() != DialogResult.OK)
				return;

			var file = File.ReadAllBytes(openFileDialog1.FileName);
			if (file.Length != Info.StateFileSize)
			{
				MessageBox.Show(@"Неверный размер файла");
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
			Changed = false;

			if (_openedFile is null || _state is null)
				return;

			SaveTop10();
			File.WriteAllBytes(_openedFile, _state.Save());
		}

		private void DisplayPlayers()
		{
			dataGridViewPlayers.CellValidating -= dataGridViewPlayers_CellValidating;
			dataGridViewPlayers.Rows.Clear();
			foreach (var player in _state.Players)
			{
				int totalHSec = 0;
				for (int lev = 0; lev < Elma.Info.NumberOfLevels; lev++)
				{
					var time = _state.Top10(lev).FirstOrDefault(rec => rec.Player == player.Name);
					if (time is null)
					{
						totalHSec = 0;
						break;
					}

					totalHSec += time.Time.ToHSeconds();
				}

				string tt = "";
				if (totalHSec > 0)
				{
					int hour = totalHSec / 360000;
					totalHSec %= 360000;
					int min = totalHSec / 6000;
					totalHSec %= 6000;
					int sec = totalHSec / 100;
					int hsec = totalHSec % 100;
					tt = $"{min:D2}:{sec:D2}.{hsec:D2}";
					if (hour > 0)
						tt = $"{hour}h" + tt;
				}

				dataGridViewPlayers.Rows.Add(player.Name, player.FinishedLevels, tt);
			}
			dataGridViewPlayers.CellValidating += dataGridViewPlayers_CellValidating;
		}

		private void DisplayBestTimes()
		{
			dataGridViewRecords.Rows.Clear();
			for (int lev = 0; lev < Info.NumberOfLevels; lev++)
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
			saveAsToolStripMenuItem.Enabled = _state != null;
		}

		private void RenamePlayerRecords(string oldName, string newName)
		{
			var playerRecords = Enumerable.Range(0, Info.NumberOfLevels)
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

		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFile();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{

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
			if (cells[e.ColumnIndex == 0 ? 1 : 0].Value != null)
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
				if (e.FormattedValue.ToString().Length > 8)
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

			var oldName = dataGridViewPlayers.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string;
			var newName = e.FormattedValue as string;

			if (newName == oldName || string.IsNullOrEmpty(newName) && string.IsNullOrEmpty(oldName))
				return;

			// check existing player names
			foreach(DataGridViewRow row in dataGridViewPlayers.Rows)
			{
				if (row.Index != e.RowIndex && row.Cells[0].Value as string == newName)
				{
					e.Cancel = true;
					return;
				}
			}

			// rename player
			if (oldName != null)
			{
				var oldPlayer = _state.Players.FirstOrDefault(player => player.Name == oldName);
				if (oldPlayer != null)
					oldPlayer.Name = newName;
				RenamePlayerRecords(oldName, newName);
				DisplayBestTimes();
			}

			Changed = true;
		}

		private void dataGridViewPlayers_UserAddedRow(object sender, DataGridViewRowEventArgs e)
		{
			Changed = true;
		}

		private void dataGridViewPlayers_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
		{
			Changed = true;
		}

		private void removeTimesExceptBestToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_state is null)
				return;
			bool changed = false;
			foreach(var lev in Enumerable.Range(0, Info.NumberOfLevels))
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
