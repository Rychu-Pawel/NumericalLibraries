namespace Pierwiastki_CS
{
    partial class InterpolacjaForm
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
            this.dgvInterpolacja = new System.Windows.Forms.DataGridView();
            this.x = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOblicz = new System.Windows.Forms.Button();
            this.txtFunkcja = new System.Windows.Forms.TextBox();
            this.lblWynik = new System.Windows.Forms.Label();
            this.btnZatwierdz = new System.Windows.Forms.Button();
            this.gbInterpolacja = new System.Windows.Forms.GroupBox();
            this.rbInterpolacja = new System.Windows.Forms.RadioButton();
            this.gbEkstrapolacja = new System.Windows.Forms.GroupBox();
            this.nudStopien = new System.Windows.Forms.NumericUpDown();
            this.rbAproksymacja = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInterpolacja)).BeginInit();
            this.gbInterpolacja.SuspendLayout();
            this.gbEkstrapolacja.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStopien)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvInterpolacja
            // 
            this.dgvInterpolacja.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInterpolacja.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.x,
            this.fx});
            this.dgvInterpolacja.Location = new System.Drawing.Point(19, 12);
            this.dgvInterpolacja.Name = "dgvInterpolacja";
            this.dgvInterpolacja.Size = new System.Drawing.Size(260, 199);
            this.dgvInterpolacja.TabIndex = 0;
            // 
            // x
            // 
            this.x.HeaderText = "x";
            this.x.Name = "x";
            // 
            // fx
            // 
            this.fx.HeaderText = "f(x)";
            this.fx.Name = "fx";
            // 
            // btnOblicz
            // 
            this.btnOblicz.Location = new System.Drawing.Point(68, 366);
            this.btnOblicz.Name = "btnOblicz";
            this.btnOblicz.Size = new System.Drawing.Size(75, 23);
            this.btnOblicz.TabIndex = 1;
            this.btnOblicz.Text = "Oblicz";
            this.btnOblicz.UseVisualStyleBackColor = true;
            this.btnOblicz.Click += new System.EventHandler(this.btnInterpoluj_Click);
            // 
            // txtFunkcja
            // 
            this.txtFunkcja.Location = new System.Drawing.Point(54, 339);
            this.txtFunkcja.Name = "txtFunkcja";
            this.txtFunkcja.Size = new System.Drawing.Size(227, 20);
            this.txtFunkcja.TabIndex = 2;
            // 
            // lblWynik
            // 
            this.lblWynik.AutoSize = true;
            this.lblWynik.Location = new System.Drawing.Point(18, 343);
            this.lblWynik.Name = "lblWynik";
            this.lblWynik.Size = new System.Drawing.Size(30, 13);
            this.lblWynik.TabIndex = 3;
            this.lblWynik.Text = "f(x) =";
            // 
            // btnZatwierdz
            // 
            this.btnZatwierdz.Location = new System.Drawing.Point(167, 366);
            this.btnZatwierdz.Name = "btnZatwierdz";
            this.btnZatwierdz.Size = new System.Drawing.Size(75, 23);
            this.btnZatwierdz.TabIndex = 4;
            this.btnZatwierdz.Text = "Zatwierdz";
            this.btnZatwierdz.UseVisualStyleBackColor = true;
            this.btnZatwierdz.Click += new System.EventHandler(this.btnZatwierdz_Click);
            // 
            // gbInterpolacja
            // 
            this.gbInterpolacja.Controls.Add(this.rbInterpolacja);
            this.gbInterpolacja.Location = new System.Drawing.Point(12, 217);
            this.gbInterpolacja.Name = "gbInterpolacja";
            this.gbInterpolacja.Size = new System.Drawing.Size(274, 50);
            this.gbInterpolacja.TabIndex = 5;
            this.gbInterpolacja.TabStop = false;
            this.gbInterpolacja.Text = "Interpolacja";
            // 
            // rbInterpolacja
            // 
            this.rbInterpolacja.AutoSize = true;
            this.rbInterpolacja.Checked = true;
            this.rbInterpolacja.Location = new System.Drawing.Point(69, 19);
            this.rbInterpolacja.Name = "rbInterpolacja";
            this.rbInterpolacja.Size = new System.Drawing.Size(136, 17);
            this.rbInterpolacja.TabIndex = 0;
            this.rbInterpolacja.TabStop = true;
            this.rbInterpolacja.Text = "Interpolacja Lagrange\'a";
            this.rbInterpolacja.UseVisualStyleBackColor = true;
            this.rbInterpolacja.CheckedChanged += new System.EventHandler(this.rbInterpolacja_CheckedChanged);
            // 
            // gbEkstrapolacja
            // 
            this.gbEkstrapolacja.Controls.Add(this.nudStopien);
            this.gbEkstrapolacja.Controls.Add(this.rbAproksymacja);
            this.gbEkstrapolacja.Location = new System.Drawing.Point(12, 273);
            this.gbEkstrapolacja.Name = "gbEkstrapolacja";
            this.gbEkstrapolacja.Size = new System.Drawing.Size(274, 50);
            this.gbEkstrapolacja.TabIndex = 6;
            this.gbEkstrapolacja.TabStop = false;
            this.gbEkstrapolacja.Text = "Aproksymacja";
            // 
            // nudStopien
            // 
            this.nudStopien.Location = new System.Drawing.Point(173, 19);
            this.nudStopien.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudStopien.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudStopien.Name = "nudStopien";
            this.nudStopien.Size = new System.Drawing.Size(61, 20);
            this.nudStopien.TabIndex = 1;
            this.nudStopien.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rbAproksymacja
            // 
            this.rbAproksymacja.AutoSize = true;
            this.rbAproksymacja.Location = new System.Drawing.Point(41, 19);
            this.rbAproksymacja.Name = "rbAproksymacja";
            this.rbAproksymacja.Size = new System.Drawing.Size(128, 17);
            this.rbAproksymacja.TabIndex = 0;
            this.rbAproksymacja.TabStop = true;
            this.rbAproksymacja.Text = "Aproksymacja stopnia";
            this.rbAproksymacja.UseVisualStyleBackColor = true;
            this.rbAproksymacja.CheckedChanged += new System.EventHandler(this.rbRegresjaLiniowa_CheckedChanged);
            // 
            // InterpolacjaForm
            // 
            this.AcceptButton = this.btnOblicz;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 402);
            this.Controls.Add(this.gbEkstrapolacja);
            this.Controls.Add(this.gbInterpolacja);
            this.Controls.Add(this.btnZatwierdz);
            this.Controls.Add(this.lblWynik);
            this.Controls.Add(this.txtFunkcja);
            this.Controls.Add(this.btnOblicz);
            this.Controls.Add(this.dgvInterpolacja);
            this.MaximumSize = new System.Drawing.Size(314, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(314, 440);
            this.Name = "InterpolacjaForm";
            this.Text = "Interpolacja";
            this.Resize += new System.EventHandler(this.InterpolacjaForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInterpolacja)).EndInit();
            this.gbInterpolacja.ResumeLayout(false);
            this.gbInterpolacja.PerformLayout();
            this.gbEkstrapolacja.ResumeLayout(false);
            this.gbEkstrapolacja.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStopien)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvInterpolacja;
        private System.Windows.Forms.DataGridViewTextBoxColumn x;
        private System.Windows.Forms.DataGridViewTextBoxColumn fx;
        private System.Windows.Forms.Button btnOblicz;
        private System.Windows.Forms.TextBox txtFunkcja;
        private System.Windows.Forms.Label lblWynik;
        private System.Windows.Forms.Button btnZatwierdz;
        private System.Windows.Forms.GroupBox gbInterpolacja;
        private System.Windows.Forms.RadioButton rbInterpolacja;
        private System.Windows.Forms.GroupBox gbEkstrapolacja;
        private System.Windows.Forms.RadioButton rbAproksymacja;
        private System.Windows.Forms.NumericUpDown nudStopien;
    }
}