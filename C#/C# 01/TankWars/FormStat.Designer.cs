namespace TankWars
{
    partial class FormStat
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
            this.dgvStat = new System.Windows.Forms.DataGridView();
            this.ColumnObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStat)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvStat
            // 
            this.dgvStat.AllowUserToAddRows = false;
            this.dgvStat.AllowUserToDeleteRows = false;
            this.dgvStat.AllowUserToResizeRows = false;
            this.dgvStat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnObject,
            this.ColumnLocation});
            this.dgvStat.Location = new System.Drawing.Point(12, 12);
            this.dgvStat.MultiSelect = false;
            this.dgvStat.Name = "dgvStat";
            this.dgvStat.ReadOnly = true;
            this.dgvStat.RowHeadersVisible = false;
            this.dgvStat.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.dgvStat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStat.Size = new System.Drawing.Size(206, 238);
            this.dgvStat.TabIndex = 0;
            // 
            // ColumnObject
            // 
            this.ColumnObject.HeaderText = "Object";
            this.ColumnObject.Name = "ColumnObject";
            this.ColumnObject.ReadOnly = true;
            // 
            // ColumnLocation
            // 
            this.ColumnLocation.HeaderText = "Location";
            this.ColumnLocation.Name = "ColumnLocation";
            this.ColumnLocation.ReadOnly = true;
            // 
            // FormStat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 262);
            this.Controls.Add(this.dgvStat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormStat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Statistics";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormStat_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStat;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLocation;
    }
}