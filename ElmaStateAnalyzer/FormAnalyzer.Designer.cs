namespace ElmaStateEditor
{
    partial class FormAnalyzer
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBoxFile1 = new System.Windows.Forms.CheckBox();
            this.checkBoxFile2 = new System.Windows.Forms.CheckBox();
            this.checkBoxDec = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "state.dat";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "File1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonFile_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 41);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "File2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonFile_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(12, 94);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(556, 369);
            this.textBox1.TabIndex = 6;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(65, 71);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(45, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Hex";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(116, 71);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(53, 17);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.Text = "String";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBoxFile1
            // 
            this.checkBoxFile1.AutoSize = true;
            this.checkBoxFile1.Enabled = false;
            this.checkBoxFile1.Location = new System.Drawing.Point(93, 17);
            this.checkBoxFile1.Name = "checkBoxFile1";
            this.checkBoxFile1.Size = new System.Drawing.Size(15, 14);
            this.checkBoxFile1.TabIndex = 1;
            this.checkBoxFile1.UseVisualStyleBackColor = true;
            this.checkBoxFile1.CheckedChanged += new System.EventHandler(this.checkBoxFile_CheckedChanged);
            // 
            // checkBoxFile2
            // 
            this.checkBoxFile2.AutoSize = true;
            this.checkBoxFile2.Enabled = false;
            this.checkBoxFile2.Location = new System.Drawing.Point(93, 45);
            this.checkBoxFile2.Name = "checkBoxFile2";
            this.checkBoxFile2.Size = new System.Drawing.Size(15, 14);
            this.checkBoxFile2.TabIndex = 3;
            this.checkBoxFile2.UseVisualStyleBackColor = true;
            this.checkBoxFile2.CheckedChanged += new System.EventHandler(this.checkBoxFile_CheckedChanged);
            // 
            // checkBoxDec
            // 
            this.checkBoxDec.AutoSize = true;
            this.checkBoxDec.Location = new System.Drawing.Point(13, 71);
            this.checkBoxDec.Name = "checkBoxDec";
            this.checkBoxDec.Size = new System.Drawing.Size(46, 17);
            this.checkBoxDec.TabIndex = 7;
            this.checkBoxDec.Text = "Dec";
            this.checkBoxDec.UseVisualStyleBackColor = true;
            this.checkBoxDec.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 476);
            this.Controls.Add(this.checkBoxDec);
            this.Controls.Add(this.checkBoxFile2);
            this.Controls.Add(this.checkBoxFile1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "State.dat Analyzer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBoxFile1;
        private System.Windows.Forms.CheckBox checkBoxFile2;
        private System.Windows.Forms.CheckBox checkBoxDec;
    }
}

