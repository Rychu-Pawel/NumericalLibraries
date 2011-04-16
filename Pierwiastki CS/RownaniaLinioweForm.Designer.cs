namespace Pierwiastki_CS
{
    partial class RownaniaLinioweForm
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
            this.dgvRownania = new System.Windows.Forms.DataGridView();
            this.x1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.r = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.nudIloscZmiennych = new System.Windows.Forms.NumericUpDown();
            this.btnOblicz = new System.Windows.Forms.Button();
            this.dgvWyniki = new System.Windows.Forms.DataGridView();
            this.w1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.w2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblWyniki = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRownania)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIloscZmiennych)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWyniki)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRownania
            // 
            this.dgvRownania.AllowUserToAddRows = false;
            this.dgvRownania.AllowUserToDeleteRows = false;
            this.dgvRownania.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRownania.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.x1,
            this.x2,
            this.r});
            this.dgvRownania.Location = new System.Drawing.Point(12, 33);
            this.dgvRownania.MultiSelect = false;
            this.dgvRownania.Name = "dgvRownania";
            this.dgvRownania.Size = new System.Drawing.Size(558, 178);
            this.dgvRownania.TabIndex = 0;
            // 
            // x1
            // 
            this.x1.HeaderText = "x1";
            this.x1.Name = "x1";
            // 
            // x2
            // 
            this.x2.HeaderText = "x2";
            this.x2.Name = "x2";
            // 
            // r
            // 
            this.r.HeaderText = "r";
            this.r.Name = "r";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ilosc zmiennych:";
            // 
            // nudIloscZmiennych
            // 
            this.nudIloscZmiennych.Location = new System.Drawing.Point(103, 7);
            this.nudIloscZmiennych.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudIloscZmiennych.Name = "nudIloscZmiennych";
            this.nudIloscZmiennych.Size = new System.Drawing.Size(63, 20);
            this.nudIloscZmiennych.TabIndex = 2;
            this.nudIloscZmiennych.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudIloscZmiennych.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // btnOblicz
            // 
            this.btnOblicz.Location = new System.Drawing.Point(254, 331);
            this.btnOblicz.Name = "btnOblicz";
            this.btnOblicz.Size = new System.Drawing.Size(75, 23);
            this.btnOblicz.TabIndex = 3;
            this.btnOblicz.Text = "Oblicz";
            this.btnOblicz.UseVisualStyleBackColor = true;
            this.btnOblicz.Click += new System.EventHandler(this.btnOblicz_Click);
            // 
            // dgvWyniki
            // 
            this.dgvWyniki.AllowUserToAddRows = false;
            this.dgvWyniki.AllowUserToDeleteRows = false;
            this.dgvWyniki.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWyniki.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.w1,
            this.w2});
            this.dgvWyniki.Location = new System.Drawing.Point(12, 230);
            this.dgvWyniki.Name = "dgvWyniki";
            this.dgvWyniki.Size = new System.Drawing.Size(558, 62);
            this.dgvWyniki.TabIndex = 4;
            // 
            // w1
            // 
            this.w1.HeaderText = "x1";
            this.w1.Name = "w1";
            // 
            // w2
            // 
            this.w2.HeaderText = "x2";
            this.w2.Name = "w2";
            // 
            // lblWyniki
            // 
            this.lblWyniki.AutoSize = true;
            this.lblWyniki.Location = new System.Drawing.Point(12, 214);
            this.lblWyniki.Name = "lblWyniki";
            this.lblWyniki.Size = new System.Drawing.Size(42, 13);
            this.lblWyniki.TabIndex = 5;
            this.lblWyniki.Text = "Wyniki:";
            // 
            // RownaniaLinioweForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 366);
            this.Controls.Add(this.lblWyniki);
            this.Controls.Add(this.dgvWyniki);
            this.Controls.Add(this.btnOblicz);
            this.Controls.Add(this.nudIloscZmiennych);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvRownania);
            this.MinimumSize = new System.Drawing.Size(598, 404);
            this.Name = "RownaniaLinioweForm";
            this.Text = "Rownania Liniowe";
            this.Load += new System.EventHandler(this.RownaniaLinioweForm_Load);
            this.Resize += new System.EventHandler(this.RownaniaLinioweForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRownania)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIloscZmiennych)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWyniki)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRownania;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudIloscZmiennych;
        private System.Windows.Forms.DataGridViewTextBoxColumn x1;
        private System.Windows.Forms.DataGridViewTextBoxColumn x2;
        private System.Windows.Forms.DataGridViewTextBoxColumn r;
        private System.Windows.Forms.Button btnOblicz;
        private System.Windows.Forms.DataGridView dgvWyniki;
        private System.Windows.Forms.Label lblWyniki;
        private System.Windows.Forms.DataGridViewTextBoxColumn w1;
        private System.Windows.Forms.DataGridViewTextBoxColumn w2;
    }
}