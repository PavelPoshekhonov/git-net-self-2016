namespace TankWars
{
    partial class FormMain
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
            this.pnMap = new System.Windows.Forms.Panel();
            this.pnBottom = new System.Windows.Forms.Panel();
            this.btStat = new System.Windows.Forms.Button();
            this.btPause = new System.Windows.Forms.Button();
            this.lbTanks = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btNewGame = new System.Windows.Forms.Button();
            this.lbApples = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbLifes = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnHelp = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pnBottom.SuspendLayout();
            this.pnHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnMap
            // 
            this.pnMap.BackColor = System.Drawing.Color.Black;
            this.pnMap.Location = new System.Drawing.Point(12, 12);
            this.pnMap.Name = "pnMap";
            this.pnMap.Size = new System.Drawing.Size(513, 318);
            this.pnMap.TabIndex = 0;
            // 
            // pnBottom
            // 
            this.pnBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnBottom.Controls.Add(this.btStat);
            this.pnBottom.Controls.Add(this.btPause);
            this.pnBottom.Controls.Add(this.lbTanks);
            this.pnBottom.Controls.Add(this.label3);
            this.pnBottom.Controls.Add(this.btNewGame);
            this.pnBottom.Controls.Add(this.lbApples);
            this.pnBottom.Controls.Add(this.label2);
            this.pnBottom.Controls.Add(this.lbLifes);
            this.pnBottom.Controls.Add(this.label1);
            this.pnBottom.Location = new System.Drawing.Point(12, 346);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Size = new System.Drawing.Size(513, 31);
            this.pnBottom.TabIndex = 1;
            // 
            // btStat
            // 
            this.btStat.Location = new System.Drawing.Point(165, 3);
            this.btStat.Name = "btStat";
            this.btStat.Size = new System.Drawing.Size(75, 23);
            this.btStat.TabIndex = 9;
            this.btStat.TabStop = false;
            this.btStat.Text = "Statistics";
            this.btStat.UseVisualStyleBackColor = true;
            this.btStat.Click += new System.EventHandler(this.btStat_Click);
            // 
            // btPause
            // 
            this.btPause.Location = new System.Drawing.Point(84, 3);
            this.btPause.Name = "btPause";
            this.btPause.Size = new System.Drawing.Size(75, 23);
            this.btPause.TabIndex = 8;
            this.btPause.TabStop = false;
            this.btPause.Text = "Pause";
            this.btPause.UseVisualStyleBackColor = true;
            this.btPause.Click += new System.EventHandler(this.btPause_Click);
            // 
            // lbTanks
            // 
            this.lbTanks.AutoSize = true;
            this.lbTanks.Location = new System.Drawing.Point(436, 8);
            this.lbTanks.Name = "lbTanks";
            this.lbTanks.Size = new System.Drawing.Size(22, 13);
            this.lbTanks.TabIndex = 7;
            this.lbTanks.Text = "_ _";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(397, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tanks";
            // 
            // btNewGame
            // 
            this.btNewGame.Location = new System.Drawing.Point(3, 3);
            this.btNewGame.Name = "btNewGame";
            this.btNewGame.Size = new System.Drawing.Size(75, 23);
            this.btNewGame.TabIndex = 5;
            this.btNewGame.TabStop = false;
            this.btNewGame.Text = "New Game";
            this.btNewGame.UseVisualStyleBackColor = true;
            this.btNewGame.Click += new System.EventHandler(this.btNewGame_Click);
            // 
            // lbApples
            // 
            this.lbApples.AutoSize = true;
            this.lbApples.Location = new System.Drawing.Point(362, 8);
            this.lbApples.Name = "lbApples";
            this.lbApples.Size = new System.Drawing.Size(22, 13);
            this.lbApples.TabIndex = 4;
            this.lbApples.Text = "_ _";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(321, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Apples";
            // 
            // lbLifes
            // 
            this.lbLifes.AutoSize = true;
            this.lbLifes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbLifes.Location = new System.Drawing.Point(286, 8);
            this.lbLifes.Name = "lbLifes";
            this.lbLifes.Size = new System.Drawing.Size(22, 13);
            this.lbLifes.TabIndex = 2;
            this.lbLifes.Text = "_ _";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(255, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lifes";
            // 
            // pnHelp
            // 
            this.pnHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnHelp.Controls.Add(this.label4);
            this.pnHelp.Location = new System.Drawing.Point(12, 383);
            this.pnHelp.Name = "pnHelp";
            this.pnHelp.Size = new System.Drawing.Size(513, 31);
            this.pnHelp.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(308, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "<Arrows> - Change direction. <Space> - Fire. <F2> - New game.";
            // 
            // FormMain
            // 
            this.ClientSize = new System.Drawing.Size(537, 424);
            this.Controls.Add(this.pnHelp);
            this.Controls.Add(this.pnBottom);
            this.Controls.Add(this.pnMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tank Wars";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.pnBottom.ResumeLayout(false);
            this.pnBottom.PerformLayout();
            this.pnHelp.ResumeLayout(false);
            this.pnHelp.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel pnMap;
        private System.Windows.Forms.Panel pnBottom;
        private System.Windows.Forms.Label lbApples;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbLifes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btNewGame;
        private System.Windows.Forms.Label lbTanks;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btPause;
        private System.Windows.Forms.Panel pnHelp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btStat;
    }
}

