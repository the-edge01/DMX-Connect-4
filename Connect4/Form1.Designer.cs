namespace Connect4
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.P1ColorButton = new System.Windows.Forms.Button();
            this.P2ColorButton = new System.Windows.Forms.Button();
            this.NeutralColorButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.turnButton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // P1ColorButton
            // 
            this.P1ColorButton.Location = new System.Drawing.Point(8, 69);
            this.P1ColorButton.Margin = new System.Windows.Forms.Padding(2);
            this.P1ColorButton.Name = "P1ColorButton";
            this.P1ColorButton.Size = new System.Drawing.Size(81, 42);
            this.P1ColorButton.TabIndex = 1;
            this.P1ColorButton.Text = "Player 1 Color";
            this.P1ColorButton.UseVisualStyleBackColor = true;
            this.P1ColorButton.Click += new System.EventHandler(this.P1ColorButton_Click);
            // 
            // P2ColorButton
            // 
            this.P2ColorButton.Location = new System.Drawing.Point(9, 137);
            this.P2ColorButton.Margin = new System.Windows.Forms.Padding(2);
            this.P2ColorButton.Name = "P2ColorButton";
            this.P2ColorButton.Size = new System.Drawing.Size(81, 42);
            this.P2ColorButton.TabIndex = 2;
            this.P2ColorButton.Text = "Player 2 Color";
            this.P2ColorButton.UseVisualStyleBackColor = true;
            this.P2ColorButton.Click += new System.EventHandler(this.P2ColorButton_Click);
            // 
            // NeutralColorButton
            // 
            this.NeutralColorButton.Location = new System.Drawing.Point(9, 207);
            this.NeutralColorButton.Margin = new System.Windows.Forms.Padding(2);
            this.NeutralColorButton.Name = "NeutralColorButton";
            this.NeutralColorButton.Size = new System.Drawing.Size(81, 42);
            this.NeutralColorButton.TabIndex = 3;
            this.NeutralColorButton.Text = "Neutral Color";
            this.NeutralColorButton.UseVisualStyleBackColor = true;
            this.NeutralColorButton.Click += new System.EventHandler(this.NeutralColorButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(9, 293);
            this.ResetButton.Margin = new System.Windows.Forms.Padding(2);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(81, 42);
            this.ResetButton.TabIndex = 4;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // turnButton
            // 
            this.turnButton.Location = new System.Drawing.Point(8, 12);
            this.turnButton.Margin = new System.Windows.Forms.Padding(2);
            this.turnButton.Name = "turnButton";
            this.turnButton.Size = new System.Drawing.Size(81, 28);
            this.turnButton.TabIndex = 5;
            this.turnButton.Text = "Turn:";
            this.turnButton.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(16, 360);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(103, 17);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Random Pattern";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(16, 390);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(98, 17);
            this.checkBox2.TabIndex = 7;
            this.checkBox2.Text = "Connect 4 SLS";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(16, 443);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 104);
            this.trackBar1.TabIndex = 8;
            this.trackBar1.Value = 80;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(16, 411);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(108, 17);
            this.checkBox3.TabIndex = 9;
            this.checkBox3.Text = "Spring Fling 2018";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(16, 432);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(48, 17);
            this.checkBox4.TabIndex = 10;
            this.checkBox4.Text = "Both";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 603);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.turnButton);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.NeutralColorButton);
            this.Controls.Add(this.P2ColorButton);
            this.Controls.Add(this.P1ColorButton);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button P1ColorButton;
        private System.Windows.Forms.Button P2ColorButton;
        private System.Windows.Forms.Button NeutralColorButton;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button turnButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
    }
}

