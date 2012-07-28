namespace CalculatorTest
{
    partial class ProportionsForm
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
            this.btnCompute = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtFirstValue = new System.Windows.Forms.TextBox();
            this.txtSecondValue = new System.Windows.Forms.TextBox();
            this.txtThirdValue = new System.Windows.Forms.TextBox();
            this.txtFourthValue = new System.Windows.Forms.TextBox();
            this.gbValues = new System.Windows.Forms.GroupBox();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbValues.SuspendLayout();
            this.gbResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCompute
            // 
            this.btnCompute.Location = new System.Drawing.Point(133, 18);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(75, 23);
            this.btnCompute.TabIndex = 0;
            this.btnCompute.Text = "Compute";
            this.btnCompute.UseVisualStyleBackColor = true;
            this.btnCompute.Click += new System.EventHandler(this.btnCompute_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 20);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(212, 13);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Put x only in one field you want to compute:";
            // 
            // txtFirstValue
            // 
            this.txtFirstValue.Location = new System.Drawing.Point(15, 47);
            this.txtFirstValue.Name = "txtFirstValue";
            this.txtFirstValue.Size = new System.Drawing.Size(100, 20);
            this.txtFirstValue.TabIndex = 2;
            this.txtFirstValue.Text = "100";
            // 
            // txtSecondValue
            // 
            this.txtSecondValue.Location = new System.Drawing.Point(121, 47);
            this.txtSecondValue.Name = "txtSecondValue";
            this.txtSecondValue.Size = new System.Drawing.Size(100, 20);
            this.txtSecondValue.TabIndex = 3;
            this.txtSecondValue.Text = "23";
            // 
            // txtThirdValue
            // 
            this.txtThirdValue.Location = new System.Drawing.Point(15, 73);
            this.txtThirdValue.Name = "txtThirdValue";
            this.txtThirdValue.Size = new System.Drawing.Size(100, 20);
            this.txtThirdValue.TabIndex = 4;
            this.txtThirdValue.Text = "39";
            // 
            // txtFourthValue
            // 
            this.txtFourthValue.Location = new System.Drawing.Point(121, 73);
            this.txtFourthValue.Name = "txtFourthValue";
            this.txtFourthValue.Size = new System.Drawing.Size(100, 20);
            this.txtFourthValue.TabIndex = 5;
            this.txtFourthValue.Text = "x";
            // 
            // gbValues
            // 
            this.gbValues.Controls.Add(this.txtFirstValue);
            this.gbValues.Controls.Add(this.lblDescription);
            this.gbValues.Controls.Add(this.txtFourthValue);
            this.gbValues.Controls.Add(this.txtSecondValue);
            this.gbValues.Controls.Add(this.txtThirdValue);
            this.gbValues.Location = new System.Drawing.Point(12, 12);
            this.gbValues.Name = "gbValues";
            this.gbValues.Size = new System.Drawing.Size(237, 105);
            this.gbValues.TabIndex = 6;
            this.gbValues.TabStop = false;
            this.gbValues.Text = "Values";
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.txtResult);
            this.gbResult.Controls.Add(this.btnCompute);
            this.gbResult.Location = new System.Drawing.Point(12, 123);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(237, 54);
            this.gbResult.TabIndex = 7;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "Result";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(15, 21);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(100, 20);
            this.txtResult.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(443, 108);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ProportionsForm
            // 
            this.AcceptButton = this.btnCompute;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(262, 189);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gbResult);
            this.Controls.Add(this.gbValues);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ProportionsForm";
            this.Text = "Proportions";
            this.gbValues.ResumeLayout(false);
            this.gbValues.PerformLayout();
            this.gbResult.ResumeLayout(false);
            this.gbResult.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCompute;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtFirstValue;
        private System.Windows.Forms.TextBox txtSecondValue;
        private System.Windows.Forms.TextBox txtThirdValue;
        private System.Windows.Forms.TextBox txtFourthValue;
        private System.Windows.Forms.GroupBox gbValues;
        private System.Windows.Forms.GroupBox gbResult;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnClose;
    }
}