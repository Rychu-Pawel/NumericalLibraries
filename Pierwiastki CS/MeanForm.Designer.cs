namespace NumericalCalculator
{
    partial class MeanForm
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
            this.gbType = new System.Windows.Forms.GroupBox();
            this.rbWeighted = new System.Windows.Forms.RadioButton();
            this.rbArithmetic = new System.Windows.Forms.RadioButton();
            this.dgvValues = new System.Windows.Forms.DataGridView();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnCompute = new System.Windows.Forms.Button();
            this.gbType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValues)).BeginInit();
            this.gbResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbType
            // 
            this.gbType.Controls.Add(this.rbWeighted);
            this.gbType.Controls.Add(this.rbArithmetic);
            this.gbType.Location = new System.Drawing.Point(12, 12);
            this.gbType.Name = "gbType";
            this.gbType.Size = new System.Drawing.Size(261, 47);
            this.gbType.TabIndex = 0;
            this.gbType.TabStop = false;
            this.gbType.Text = "Type";
            // 
            // rbWeighted
            // 
            this.rbWeighted.AutoSize = true;
            this.rbWeighted.Location = new System.Drawing.Point(153, 18);
            this.rbWeighted.Name = "rbWeighted";
            this.rbWeighted.Size = new System.Drawing.Size(71, 17);
            this.rbWeighted.TabIndex = 1;
            this.rbWeighted.Text = "Weighted";
            this.rbWeighted.UseVisualStyleBackColor = true;
            // 
            // rbArithmetic
            // 
            this.rbArithmetic.AutoSize = true;
            this.rbArithmetic.Checked = true;
            this.rbArithmetic.Location = new System.Drawing.Point(36, 18);
            this.rbArithmetic.Name = "rbArithmetic";
            this.rbArithmetic.Size = new System.Drawing.Size(71, 17);
            this.rbArithmetic.TabIndex = 0;
            this.rbArithmetic.TabStop = true;
            this.rbArithmetic.Text = "Arithmetic";
            this.rbArithmetic.UseVisualStyleBackColor = true;
            this.rbArithmetic.CheckedChanged += new System.EventHandler(this.rbArithmetic_CheckedChanged);
            // 
            // dgvValues
            // 
            this.dgvValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Value,
            this.Weight});
            this.dgvValues.Location = new System.Drawing.Point(12, 65);
            this.dgvValues.Name = "dgvValues";
            this.dgvValues.Size = new System.Drawing.Size(261, 353);
            this.dgvValues.TabIndex = 1;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // Weight
            // 
            this.Weight.HeaderText = "Weight";
            this.Weight.Name = "Weight";
            this.Weight.Visible = false;
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.txtResult);
            this.gbResult.Controls.Add(this.btnCompute);
            this.gbResult.Location = new System.Drawing.Point(12, 424);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(261, 54);
            this.gbResult.TabIndex = 2;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "Result";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(6, 22);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(168, 20);
            this.txtResult.TabIndex = 3;
            // 
            // btnCompute
            // 
            this.btnCompute.Location = new System.Drawing.Point(180, 19);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(75, 23);
            this.btnCompute.TabIndex = 2;
            this.btnCompute.Text = "Compute";
            this.btnCompute.UseVisualStyleBackColor = true;
            this.btnCompute.Click += new System.EventHandler(this.btnCompute_Click);
            // 
            // MeanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 490);
            this.Controls.Add(this.gbResult);
            this.Controls.Add(this.dgvValues);
            this.Controls.Add(this.gbType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MeanForm";
            this.Text = "Mean";
            this.gbType.ResumeLayout(false);
            this.gbType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValues)).EndInit();
            this.gbResult.ResumeLayout(false);
            this.gbResult.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbType;
        private System.Windows.Forms.RadioButton rbWeighted;
        private System.Windows.Forms.RadioButton rbArithmetic;
        private System.Windows.Forms.DataGridView dgvValues;
        private System.Windows.Forms.GroupBox gbResult;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnCompute;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Weight;
    }
}