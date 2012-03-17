﻿namespace NumericalCalculator
{
    partial class InterpolationForm
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
            this.btnCompute = new System.Windows.Forms.Button();
            this.txtFunction = new System.Windows.Forms.TextBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.gbInterpolation = new System.Windows.Forms.GroupBox();
            this.rbInterpolation = new System.Windows.Forms.RadioButton();
            this.gbApproximation = new System.Windows.Forms.GroupBox();
            this.nudLevel = new System.Windows.Forms.NumericUpDown();
            this.rbApproximation = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInterpolacja)).BeginInit();
            this.gbInterpolation.SuspendLayout();
            this.gbApproximation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel)).BeginInit();
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
            // btnCompute
            // 
            this.btnCompute.Location = new System.Drawing.Point(68, 366);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(75, 23);
            this.btnCompute.TabIndex = 1;
            this.btnCompute.Text = "Oblicz";
            this.btnCompute.UseVisualStyleBackColor = true;
            this.btnCompute.Click += new System.EventHandler(this.btnInterpoluj_Click);
            // 
            // txtFunction
            // 
            this.txtFunction.Location = new System.Drawing.Point(54, 339);
            this.txtFunction.Name = "txtFunction";
            this.txtFunction.Size = new System.Drawing.Size(227, 20);
            this.txtFunction.TabIndex = 2;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(18, 343);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(30, 13);
            this.lblResult.TabIndex = 3;
            this.lblResult.Text = "f(x) =";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(167, 366);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Zatwierdz";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnZatwierdz_Click);
            // 
            // gbInterpolation
            // 
            this.gbInterpolation.Controls.Add(this.rbInterpolation);
            this.gbInterpolation.Location = new System.Drawing.Point(12, 217);
            this.gbInterpolation.Name = "gbInterpolation";
            this.gbInterpolation.Size = new System.Drawing.Size(274, 50);
            this.gbInterpolation.TabIndex = 5;
            this.gbInterpolation.TabStop = false;
            this.gbInterpolation.Text = "Interpolacja";
            // 
            // rbInterpolation
            // 
            this.rbInterpolation.AutoSize = true;
            this.rbInterpolation.Checked = true;
            this.rbInterpolation.Location = new System.Drawing.Point(69, 19);
            this.rbInterpolation.Name = "rbInterpolation";
            this.rbInterpolation.Size = new System.Drawing.Size(136, 17);
            this.rbInterpolation.TabIndex = 0;
            this.rbInterpolation.TabStop = true;
            this.rbInterpolation.Text = "Interpolacja Lagrange\'a";
            this.rbInterpolation.UseVisualStyleBackColor = true;
            this.rbInterpolation.CheckedChanged += new System.EventHandler(this.rbInterpolacja_CheckedChanged);
            // 
            // gbApproximation
            // 
            this.gbApproximation.Controls.Add(this.nudLevel);
            this.gbApproximation.Controls.Add(this.rbApproximation);
            this.gbApproximation.Location = new System.Drawing.Point(12, 273);
            this.gbApproximation.Name = "gbApproximation";
            this.gbApproximation.Size = new System.Drawing.Size(274, 50);
            this.gbApproximation.TabIndex = 6;
            this.gbApproximation.TabStop = false;
            this.gbApproximation.Text = "Aproksymacja";
            // 
            // nudLevel
            // 
            this.nudLevel.Location = new System.Drawing.Point(173, 19);
            this.nudLevel.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLevel.Name = "nudLevel";
            this.nudLevel.Size = new System.Drawing.Size(61, 20);
            this.nudLevel.TabIndex = 1;
            this.nudLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rbApproximation
            // 
            this.rbApproximation.AutoSize = true;
            this.rbApproximation.Location = new System.Drawing.Point(41, 19);
            this.rbApproximation.Name = "rbApproximation";
            this.rbApproximation.Size = new System.Drawing.Size(128, 17);
            this.rbApproximation.TabIndex = 0;
            this.rbApproximation.TabStop = true;
            this.rbApproximation.Text = "Aproksymacja stopnia";
            this.rbApproximation.UseVisualStyleBackColor = true;
            this.rbApproximation.CheckedChanged += new System.EventHandler(this.rbRegresjaLiniowa_CheckedChanged);
            // 
            // InterpolationForm
            // 
            this.AcceptButton = this.btnCompute;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 402);
            this.Controls.Add(this.gbApproximation);
            this.Controls.Add(this.gbInterpolation);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.txtFunction);
            this.Controls.Add(this.btnCompute);
            this.Controls.Add(this.dgvInterpolacja);
            this.MaximumSize = new System.Drawing.Size(314, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(314, 440);
            this.Name = "InterpolationForm";
            this.Text = "Interpolacja";
            this.Resize += new System.EventHandler(this.InterpolacjaForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInterpolacja)).EndInit();
            this.gbInterpolation.ResumeLayout(false);
            this.gbInterpolation.PerformLayout();
            this.gbApproximation.ResumeLayout(false);
            this.gbApproximation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvInterpolacja;
        private System.Windows.Forms.DataGridViewTextBoxColumn x;
        private System.Windows.Forms.DataGridViewTextBoxColumn fx;
        private System.Windows.Forms.Button btnCompute;
        private System.Windows.Forms.TextBox txtFunction;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.GroupBox gbInterpolation;
        private System.Windows.Forms.RadioButton rbInterpolation;
        private System.Windows.Forms.GroupBox gbApproximation;
        private System.Windows.Forms.RadioButton rbApproximation;
        private System.Windows.Forms.NumericUpDown nudLevel;
    }
}