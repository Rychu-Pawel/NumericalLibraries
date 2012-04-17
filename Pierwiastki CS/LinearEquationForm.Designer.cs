namespace NumericalCalculator
{
    partial class LinearEquationForm
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
            this.dgvEquations = new System.Windows.Forms.DataGridView();
            this.x1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.r = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblNumberOfVariables = new System.Windows.Forms.Label();
            this.nudNumberOfVariables = new System.Windows.Forms.NumericUpDown();
            this.btnCompute = new System.Windows.Forms.Button();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.w1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.w2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblResults = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberOfVariables)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEquations
            // 
            this.dgvEquations.AllowUserToAddRows = false;
            this.dgvEquations.AllowUserToDeleteRows = false;
            this.dgvEquations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEquations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.x1,
            this.x2,
            this.r});
            this.dgvEquations.Location = new System.Drawing.Point(12, 33);
            this.dgvEquations.MultiSelect = false;
            this.dgvEquations.Name = "dgvEquations";
            this.dgvEquations.Size = new System.Drawing.Size(558, 178);
            this.dgvEquations.TabIndex = 0;
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
            // lblNumberOfVariables
            // 
            this.lblNumberOfVariables.AutoSize = true;
            this.lblNumberOfVariables.Location = new System.Drawing.Point(12, 9);
            this.lblNumberOfVariables.Name = "lblNumberOfVariables";
            this.lblNumberOfVariables.Size = new System.Drawing.Size(85, 13);
            this.lblNumberOfVariables.TabIndex = 1;
            this.lblNumberOfVariables.Text = "Ilość zmiennych:";
            // 
            // nudNumberOfVariables
            // 
            this.nudNumberOfVariables.Location = new System.Drawing.Point(122, 7);
            this.nudNumberOfVariables.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudNumberOfVariables.Name = "nudNumberOfVariables";
            this.nudNumberOfVariables.Size = new System.Drawing.Size(63, 20);
            this.nudNumberOfVariables.TabIndex = 2;
            this.nudNumberOfVariables.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudNumberOfVariables.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // btnCompute
            // 
            this.btnCompute.Location = new System.Drawing.Point(254, 331);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(75, 23);
            this.btnCompute.TabIndex = 3;
            this.btnCompute.Text = "Oblicz";
            this.btnCompute.UseVisualStyleBackColor = true;
            this.btnCompute.Click += new System.EventHandler(this.btnCompute_Click);
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.w1,
            this.w2});
            this.dgvResults.Location = new System.Drawing.Point(12, 230);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.Size = new System.Drawing.Size(558, 62);
            this.dgvResults.TabIndex = 4;
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
            // lblResults
            // 
            this.lblResults.AutoSize = true;
            this.lblResults.Location = new System.Drawing.Point(12, 214);
            this.lblResults.Name = "lblResults";
            this.lblResults.Size = new System.Drawing.Size(42, 13);
            this.lblResults.TabIndex = 5;
            this.lblResults.Text = "Wyniki:";
            // 
            // LinearEquationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 366);
            this.Controls.Add(this.lblResults);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.btnCompute);
            this.Controls.Add(this.nudNumberOfVariables);
            this.Controls.Add(this.lblNumberOfVariables);
            this.Controls.Add(this.dgvEquations);
            this.MinimumSize = new System.Drawing.Size(598, 404);
            this.Name = "LinearEquationForm";
            this.Text = "Rownania Liniowe";
            this.Load += new System.EventHandler(this.LinearEquationForm_Load);
            this.Resize += new System.EventHandler(this.LinearEquationForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberOfVariables)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEquations;
        private System.Windows.Forms.Label lblNumberOfVariables;
        private System.Windows.Forms.NumericUpDown nudNumberOfVariables;
        private System.Windows.Forms.DataGridViewTextBoxColumn x1;
        private System.Windows.Forms.DataGridViewTextBoxColumn x2;
        private System.Windows.Forms.DataGridViewTextBoxColumn r;
        private System.Windows.Forms.Button btnCompute;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Label lblResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn w1;
        private System.Windows.Forms.DataGridViewTextBoxColumn w2;
    }
}