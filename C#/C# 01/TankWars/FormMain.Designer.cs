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
            this.btPause = new System.Windows.Forms.Button();
            this.lbTanks = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btNewGame = new System.Windows.Forms.Button();
            this.lbApples = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbLifes = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnMap
            // 
            this.pnMap.BackColor = System.Drawing.Color.Black;
            this.pnMap.Location = new System.Drawing.Point(12, 12);
            this.pnMap.Name = "pnMap";
            this.pnMap.Size = new System.Drawing.Size(500, 500);
            this.pnMap.TabIndex = 0;
            // 
            // pnBottom
            // 
            this.pnBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnBottom.Controls.Add(this.btPause);
            this.pnBottom.Controls.Add(this.lbTanks);
            this.pnBottom.Controls.Add(this.label3);
            this.pnBottom.Controls.Add(this.btNewGame);
            this.pnBottom.Controls.Add(this.lbApples);
            this.pnBottom.Controls.Add(this.label2);
            this.pnBottom.Controls.Add(this.lbLifes);
            this.pnBottom.Controls.Add(this.label1);
            this.pnBottom.Location = new System.Drawing.Point(12, 528);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Size = new System.Drawing.Size(500, 31);
            this.pnBottom.TabIndex = 1;
            // 
            // btPause
            // 
            this.btPause.Location = new System.Drawing.Point(84, 3);
            this.btPause.Name = "btPause";
            this.btPause.Size = new System.Drawing.Size(75, 23);
            this.btPause.TabIndex = 8;
            this.btPause.Text = "Pause";
            this.btPause.UseVisualStyleBackColor = true;
            this.btPause.Click += new System.EventHandler(this.btPause_Click);
            this.btPause.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.bt_PreviewKeyDown);
            // 
            // lbTanks
            // 
            this.lbTanks.AutoSize = true;
            this.lbTanks.Location = new System.Drawing.Point(443, 8);
            this.lbTanks.Name = "lbTanks";
            this.lbTanks.Size = new System.Drawing.Size(22, 13);
            this.lbTanks.TabIndex = 7;
            this.lbTanks.Text = "_ _";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(400, 8);
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
            this.btNewGame.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.bt_PreviewKeyDown);
            // 
            // lbApples
            // 
            this.lbApples.AutoSize = true;
            this.lbApples.Location = new System.Drawing.Point(345, 8);
            this.lbApples.Name = "lbApples";
            this.lbApples.Size = new System.Drawing.Size(22, 13);
            this.lbApples.TabIndex = 4;
            this.lbApples.Text = "_ _";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(300, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Apples";
            // 
            // lbLifes
            // 
            this.lbLifes.AutoSize = true;
            this.lbLifes.Location = new System.Drawing.Point(235, 8);
            this.lbLifes.Name = "lbLifes";
            this.lbLifes.Size = new System.Drawing.Size(22, 13);
            this.lbLifes.TabIndex = 2;
            this.lbLifes.Text = "_ _";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(200, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lifes";
            // 
            // FormMain
            // 
            this.ClientSize = new System.Drawing.Size(524, 571);
            this.Controls.Add(this.pnBottom);
            this.Controls.Add(this.pnMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tank Wars";
            this.pnBottom.ResumeLayout(false);
            this.pnBottom.PerformLayout();
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
    }
}

