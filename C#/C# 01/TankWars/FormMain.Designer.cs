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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbApples = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbLifes = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btNewGame = new System.Windows.Forms.Button();
            this.lbTanks = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
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
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbTanks);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btNewGame);
            this.panel1.Controls.Add(this.lbApples);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbLifes);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 528);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 31);
            this.panel1.TabIndex = 1;
            // 
            // lbApples
            // 
            this.lbApples.AutoSize = true;
            this.lbApples.Location = new System.Drawing.Point(255, 8);
            this.lbApples.Name = "lbApples";
            this.lbApples.Size = new System.Drawing.Size(22, 13);
            this.lbApples.TabIndex = 4;
            this.lbApples.Text = "_ _";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Apples";
            // 
            // lbLifes
            // 
            this.lbLifes.AutoSize = true;
            this.lbLifes.Location = new System.Drawing.Point(145, 8);
            this.lbLifes.Name = "lbLifes";
            this.lbLifes.Size = new System.Drawing.Size(22, 13);
            this.lbLifes.TabIndex = 2;
            this.lbLifes.Text = "_ _";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lifes";
            // 
            // btNewGame
            // 
            this.btNewGame.Location = new System.Drawing.Point(3, 3);
            this.btNewGame.Name = "btNewGame";
            this.btNewGame.Size = new System.Drawing.Size(75, 23);
            this.btNewGame.TabIndex = 5;
            this.btNewGame.Text = "New Game";
            this.btNewGame.UseVisualStyleBackColor = true;
            this.btNewGame.Click += new System.EventHandler(this.btNewGame_Click);
            // 
            // lbTanks
            // 
            this.lbTanks.AutoSize = true;
            this.lbTanks.Location = new System.Drawing.Point(353, 8);
            this.lbTanks.Name = "lbTanks";
            this.lbTanks.Size = new System.Drawing.Size(22, 13);
            this.lbTanks.TabIndex = 7;
            this.lbTanks.Text = "_ _";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(310, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Tanks";
            // 
            // FormMain
            // 
            this.ClientSize = new System.Drawing.Size(524, 571);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel pnMap;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbApples;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbLifes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btNewGame;
        private System.Windows.Forms.Label lbTanks;
        private System.Windows.Forms.Label label4;
    }
}

