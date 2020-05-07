using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace ElmaStateEditor
{
    public partial class FormAnalyzer : Form
    {
        private readonly byte[] _emptyState;

        private byte[] _state1;
        private byte[] _state2;

        public FormAnalyzer()
        {
            InitializeComponent();
            try
            {
                _emptyState = File.ReadAllBytes(@"empty.dat");
            }
            catch (Exception e)
            {
                MessageBox.Show(@"Не удается найти empty.dat");
                Load += (sender, args) => Close();
            }
        }

        private void buttonFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            var file = File.ReadAllBytes(openFileDialog1.FileName);
            if (file.Length != _emptyState.Length)
            {
                MessageBox.Show(@"Неверный размер файла");
                return;
            }

            var state = file.Zip(_emptyState, (b1, b2) => (byte)(b1 ^ b2)).ToArray();
            if (sender == button1)
            {
                _state1 = state;
                checkBoxFile1.CheckedChanged -= checkBoxFile_CheckedChanged;
                checkBoxFile1.Enabled = true;
                checkBoxFile1.Checked = true;
                checkBoxFile1.CheckedChanged += checkBoxFile_CheckedChanged;
                checkBoxFile1.Text = openFileDialog1.SafeFileName;
            }
            else
            {
                _state2 = state;
                checkBoxFile2.CheckedChanged -= checkBoxFile_CheckedChanged;
                checkBoxFile2.Enabled = true;
                checkBoxFile2.Checked = true;
                checkBoxFile2.CheckedChanged += checkBoxFile_CheckedChanged;
                checkBoxFile2.Text = openFileDialog1.SafeFileName;
            }

            CompareStates();
        }

        private IEnumerable<(int pos, List<byte> bytes)> GroupDiffBytes(IEnumerable<(byte b, int pos)> diffs)
        {
            if (!diffs.Any())
                yield break;

            int prev = diffs.First().pos;
            var group = new List<byte> { diffs.First().b };

            foreach (var (b, pos) in diffs.Skip(1))
            {
                if (pos == prev + 1)
                {
                    group.Add(b);
                }
                else
                {
                    yield return (prev - group.Count + 1, group);
                    group = new List<byte> { b };
                }

                prev = pos;
            }

            yield return (prev - group.Count + 1, group);
        }

        private IEnumerable<(int pos, List<byte> bytes1, List<byte> bytes2)> GroupDiffBytes(IEnumerable<(int pos, byte b1, byte b2)> diffs)
        {
            if (!diffs.Any())
                yield break;

            int prev = diffs.First().pos;
            var group1 = new List<byte> { diffs.First().b1 };
            var group2 = new List<byte> { diffs.First().b2 };

            foreach (var (pos, b1, b2) in diffs.Skip(1))
            {
                if (pos == prev + 1)
                {
                    group1.Add(b1);
                    group2.Add(b2);
                }
                else
                {
                    yield return (prev - group1.Count + 1, group1, group2);
                    group1 = new List<byte> { b1 };
                    group2 = new List<byte> { b2 };
                }

                prev = pos;
            }

            yield return (prev - group1.Count + 1, group1, group2);
        }

        private string CharsToString(List<byte> chars)
        {
            return string.Join("", chars.Select(c => (char)c).Select(c => char.IsControl(c) ? '?' : c));
        }

        private void CompareStates()
        {
            textBox1.Text = "";

            if (_state1 is null && _state2 is null)
                return;

            string text = "";

            bool dec = checkBoxDec.Checked;
            bool hex = checkBox1.Checked;
            bool strOn = checkBox2.Checked;

            if (_state1 is null || _state2 is null)
            {
                var state = _state1 ?? _state2;
                var diffs = state.Select((b, pos) => (b, pos))
                    .Where(p => p.b != 0);

                foreach (var (pos, bytes) in GroupDiffBytes(diffs))
                {
                    var line = $"{pos,5}";
                    if (bytes.Count > 1)
                        line += $" ({bytes.Count})";
                    line = line.PadRight(9);
                    if(dec)
                        line += $" | {string.Join(" ", bytes.Select(b => $"{b}"))}";
                    if (hex)
                        line += $" | {string.Join(" ", bytes.Select(b => $"{b:X2}"))}";
                    if (strOn)
                        line += $" | '{CharsToString(bytes)}'";
                    text += line + Environment.NewLine;
                }
            }
            else
            {
                var diffs2 = _state1.Select((b, pos) => (b, pos))
                    .Zip(_state2, (bp1, b2) => (bp1.pos, bp1.b, b2))
                    .Where(t => t.b != t.b2);

                foreach (var (pos, bytes1, bytes2) in GroupDiffBytes(diffs2))
                {
                    var line = $"{pos,5}";
                    if (bytes1.Count > 1)
                        line += $" ({bytes1.Count})";
                    line = line.PadRight(9);
                    if (dec)
                    {
                        line += $" | {string.Join(" ", bytes1.Select(b => $"{b}"))}";
                        line += $" => {string.Join(" ", bytes2.Select(b => $"{b}"))}";
                    }
                    if (hex)
                    {
                        line += $" | {string.Join(" ", bytes1.Select(b => $"{b:X2}"))}";
                        line += $" => {string.Join(" ", bytes2.Select(b => $"{b:X2}"))}";
                    }
                    if (strOn)
                    {
                        line += $" | '{CharsToString(bytes1)}'";
                        line += $" => '{CharsToString(bytes2)}'";
                    }
                    text += line + Environment.NewLine;
                }
            }

            textBox1.Text += text;
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CompareStates();
        }

        private void checkBoxFile_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox) sender;
            checkBox.Enabled = checkBox.Checked;
            if (!checkBox.Checked)
            {
                checkBox.Text = "";
                if (checkBox == checkBoxFile1)
                    _state1 = null;
                else
                    _state2 = null;
            }

            CompareStates();
        }
    }
}
