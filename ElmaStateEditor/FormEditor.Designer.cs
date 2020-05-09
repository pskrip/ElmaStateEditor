namespace ElmaStateEditor
{
    partial class FormEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeTimesExceptBestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.dataGridViewRecords = new System.Windows.Forms.DataGridView();
			this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Times = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTop10 = new System.Windows.Forms.DataGridView();
			this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Player = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.dataGridViewPlayers = new System.Windows.Forms.DataGridView();
			this.Players = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Unlocked = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TotalTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecords)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewTop10)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlayers)).BeginInit();
			this.SuspendLayout();
			// 
			// fileToolStripMenuItem
			// 
			fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			fileToolStripMenuItem.Text = "File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Enabled = false;
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Enabled = false;
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.saveAsToolStripMenuItem.Text = "Save as...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            fileToolStripMenuItem,
            this.editToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(981, 24);
			this.menuStrip.TabIndex = 0;
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeTimesExceptBestToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// removeTimesExceptBestToolStripMenuItem
			// 
			this.removeTimesExceptBestToolStripMenuItem.Name = "removeTimesExceptBestToolStripMenuItem";
			this.removeTimesExceptBestToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.removeTimesExceptBestToolStripMenuItem.Text = "Remove times except best";
			this.removeTimesExceptBestToolStripMenuItem.Click += new System.EventHandler(this.removeTimesExceptBestToolStripMenuItem_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "state.dat";
			// 
			// dataGridViewRecords
			// 
			this.dataGridViewRecords.AllowUserToAddRows = false;
			this.dataGridViewRecords.AllowUserToDeleteRows = false;
			this.dataGridViewRecords.AllowUserToResizeRows = false;
			this.dataGridViewRecords.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dataGridViewRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewRecords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Level,
            this.Times});
			this.dataGridViewRecords.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewRecords.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
			this.dataGridViewRecords.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewRecords.MultiSelect = false;
			this.dataGridViewRecords.Name = "dataGridViewRecords";
			this.dataGridViewRecords.ReadOnly = true;
			this.dataGridViewRecords.RowHeadersVisible = false;
			this.dataGridViewRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewRecords.Size = new System.Drawing.Size(439, 593);
			this.dataGridViewRecords.TabIndex = 3;
			this.dataGridViewRecords.SelectionChanged += new System.EventHandler(this.dataGridViewRecords_SelectionChanged);
			// 
			// Level
			// 
			this.Level.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Level.HeaderText = "Level";
			this.Level.Name = "Level";
			this.Level.ReadOnly = true;
			this.Level.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.Level.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// Times
			// 
			this.Times.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.Times.HeaderText = "Best time";
			this.Times.Name = "Times";
			this.Times.ReadOnly = true;
			this.Times.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.Times.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Times.Width = 56;
			// 
			// dataGridViewTop10
			// 
			this.dataGridViewTop10.AllowUserToResizeRows = false;
			this.dataGridViewTop10.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dataGridViewTop10.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewTop10.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.Player});
			this.dataGridViewTop10.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewTop10.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
			this.dataGridViewTop10.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewTop10.Name = "dataGridViewTop10";
			this.dataGridViewTop10.Size = new System.Drawing.Size(538, 593);
			this.dataGridViewTop10.TabIndex = 8;
			this.dataGridViewTop10.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.dataGridViewTop10_CellParsing);
			this.dataGridViewTop10.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewTop10_CellValidating);
			this.dataGridViewTop10.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTop10_CellValueChanged);
			this.dataGridViewTop10.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewTop10_UserAddedRow);
			this.dataGridViewTop10.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewTop10_UserDeletedRow);
			// 
			// Time
			// 
			this.Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.Time.HeaderText = "Time";
			this.Time.Name = "Time";
			this.Time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Time.Width = 36;
			// 
			// Player
			// 
			this.Player.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Player.HeaderText = "Player";
			this.Player.MaxInputLength = 8;
			this.Player.Name = "Player";
			this.Player.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.dataGridViewRecords);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.dataGridViewTop10);
			this.splitContainer1.Size = new System.Drawing.Size(981, 593);
			this.splitContainer1.SplitterDistance = 439;
			this.splitContainer1.TabIndex = 9;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 24);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.dataGridViewPlayers);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
			this.splitContainer2.Size = new System.Drawing.Size(981, 732);
			this.splitContainer2.SplitterDistance = 135;
			this.splitContainer2.TabIndex = 10;
			// 
			// dataGridViewPlayers
			// 
			this.dataGridViewPlayers.AllowUserToResizeRows = false;
			this.dataGridViewPlayers.BackgroundColor = System.Drawing.SystemColors.Control;
			this.dataGridViewPlayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewPlayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Players,
            this.Unlocked,
            this.TotalTime});
			this.dataGridViewPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewPlayers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
			this.dataGridViewPlayers.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewPlayers.Name = "dataGridViewPlayers";
			this.dataGridViewPlayers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewPlayers.Size = new System.Drawing.Size(981, 135);
			this.dataGridViewPlayers.TabIndex = 0;
			this.dataGridViewPlayers.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridViewPlayers_CellValidating);
			this.dataGridViewPlayers.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridViewPlayers_UserDeletingRow);
			// 
			// Players
			// 
			this.Players.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Players.HeaderText = "Player";
			this.Players.MaxInputLength = 8;
			this.Players.Name = "Players";
			// 
			// Unlocked
			// 
			this.Unlocked.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.Unlocked.HeaderText = "Unlocked";
			this.Unlocked.Name = "Unlocked";
			this.Unlocked.ReadOnly = true;
			this.Unlocked.Width = 78;
			// 
			// TotalTime
			// 
			this.TotalTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.TotalTime.HeaderText = "Total time";
			this.TotalTime.Name = "TotalTime";
			this.TotalTime.ReadOnly = true;
			// 
			// FormEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(981, 756);
			this.Controls.Add(this.splitContainer2);
			this.Controls.Add(this.menuStrip);
			this.MainMenuStrip = this.menuStrip;
			this.Name = "FormEditor";
			this.Text = "State.dat editor";
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewRecords)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewTop10)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlayers)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dataGridViewRecords;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.DataGridView dataGridViewTop10;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Time;
		private System.Windows.Forms.DataGridViewTextBoxColumn Player;
		private System.Windows.Forms.DataGridViewTextBoxColumn Level;
		private System.Windows.Forms.DataGridViewTextBoxColumn Times;
		private System.Windows.Forms.DataGridView dataGridViewPlayers;
		private System.Windows.Forms.DataGridViewTextBoxColumn Players;
		private System.Windows.Forms.DataGridViewTextBoxColumn Unlocked;
		private System.Windows.Forms.DataGridViewTextBoxColumn TotalTime;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem removeTimesExceptBestToolStripMenuItem;
	}
}

