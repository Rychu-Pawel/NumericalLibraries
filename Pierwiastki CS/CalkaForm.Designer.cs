namespace Pierwiastki_CS
{
    partial class CalkaForm
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
            this.btnOblicz = new System.Windows.Forms.Button();
            this.txtFunkcja = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudStopien = new System.Windows.Forms.NumericUpDown();
            this.dgvDane = new System.Windows.Forms.DataGridView();
            this.Zmienna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Od = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Do = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbWynik = new System.Windows.Forms.GroupBox();
            this.txtWynik = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudStopien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDane)).BeginInit();
            this.gbWynik.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOblicz
            // 
            this.btnOblicz.Location = new System.Drawing.Point(133, 45);
            this.btnOblicz.Name = "btnOblicz";
            this.btnOblicz.Size = new System.Drawing.Size(75, 23);
            this.btnOblicz.TabIndex = 0;
            this.btnOblicz.Text = "Oblicz";
            this.btnOblicz.UseVisualStyleBackColor = true;
            this.btnOblicz.Click += new System.EventHandler(this.btnOblicz_Click);
            // 
            // txtFunkcja
            // 
            this.txtFunkcja.Location = new System.Drawing.Point(48, 6);
            this.txtFunkcja.Name = "txtFunkcja";
            this.txtFunkcja.Size = new System.Drawing.Size(306, 20);
            this.txtFunkcja.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "f(x) =";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Stopien:";
            // 
            // nudStopien
            // 
            this.nudStopien.Location = new System.Drawing.Point(185, 33);
            this.nudStopien.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudStopien.Name = "nudStopien";
            this.nudStopien.Size = new System.Drawing.Size(48, 20);
            this.nudStopien.TabIndex = 4;
            this.nudStopien.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudStopien.ValueChanged += new System.EventHandler(this.nudStopien_ValueChanged);
            // 
            // dgvDane
            // 
            this.dgvDane.AllowUserToAddRows = false;
            this.dgvDane.AllowUserToDeleteRows = false;
            this.dgvDane.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDane.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Zmienna,
            this.Od,
            this.Do});
            this.dgvDane.Location = new System.Drawing.Point(12, 59);
            this.dgvDane.Name = "dgvDane";
            this.dgvDane.Size = new System.Drawing.Size(343, 195);
            this.dgvDane.TabIndex = 5;
            // 
            // Zmienna
            // 
            this.Zmienna.HeaderText = "Zmienna";
            this.Zmienna.Name = "Zmienna";
            // 
            // Od
            // 
            this.Od.HeaderText = "Od";
            this.Od.Name = "Od";
            // 
            // Do
            // 
            this.Do.HeaderText = "Do";
            this.Do.Name = "Do";
            // 
            // gbWynik
            // 
            this.gbWynik.Controls.Add(this.txtWynik);
            this.gbWynik.Controls.Add(this.btnOblicz);
            this.gbWynik.Location = new System.Drawing.Point(12, 260);
            this.gbWynik.Name = "gbWynik";
            this.gbWynik.Size = new System.Drawing.Size(342, 81);
            this.gbWynik.TabIndex = 6;
            this.gbWynik.TabStop = false;
            this.gbWynik.Text = "Wynik";
            // 
            // txtWynik
            // 
            this.txtWynik.Location = new System.Drawing.Point(93, 19);
            this.txtWynik.Name = "txtWynik";
            this.txtWynik.Size = new System.Drawing.Size(154, 20);
            this.txtWynik.TabIndex = 1;
            // 
            // CalkaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 355);
            this.Controls.Add(this.gbWynik);
            this.Controls.Add(this.dgvDane);
            this.Controls.Add(this.nudStopien);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFunkcja);
            this.Name = "CalkaForm";
            this.Text = "Calkowanie wielokrotne";
            ((System.ComponentModel.ISupportInitialize)(this.nudStopien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDane)).EndInit();
            this.gbWynik.ResumeLayout(false);
            this.gbWynik.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOblicz;
        private System.Windows.Forms.TextBox txtFunkcja;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudStopien;
        private System.Windows.Forms.DataGridView dgvDane;
        private System.Windows.Forms.DataGridViewTextBoxColumn Zmienna;
        private System.Windows.Forms.DataGridViewTextBoxColumn Od;
        private System.Windows.Forms.DataGridViewTextBoxColumn Do;
        private System.Windows.Forms.GroupBox gbWynik;
        private System.Windows.Forms.TextBox txtWynik;
    }
}