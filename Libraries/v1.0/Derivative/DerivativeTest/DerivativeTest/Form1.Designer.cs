namespace DerivativeTest
{
    partial class Form1
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
            this.txtDerivative = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPoint = new System.Windows.Forms.TextBox();
            this.gbFirstDerivative = new System.Windows.Forms.GroupBox();
            this.txtResultFirst = new System.Windows.Forms.TextBox();
            this.btnComputeFirst = new System.Windows.Forms.Button();
            this.gbSecondDerivative = new System.Windows.Forms.GroupBox();
            this.btnComputeSecond = new System.Windows.Forms.Button();
            this.txtResultSecond = new System.Windows.Forms.TextBox();
            this.gbFunctionAtPoint = new System.Windows.Forms.GroupBox();
            this.btnComputeFunctionAtPoint = new System.Windows.Forms.Button();
            this.txtResultFunctionAtPoint = new System.Windows.Forms.TextBox();
            this.gbFirstDerivative.SuspendLayout();
            this.gbSecondDerivative.SuspendLayout();
            this.gbFunctionAtPoint.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDerivative
            // 
            this.txtDerivative.Location = new System.Drawing.Point(12, 12);
            this.txtDerivative.Name = "txtDerivative";
            this.txtDerivative.Size = new System.Drawing.Size(230, 20);
            this.txtDerivative.TabIndex = 0;
            this.txtDerivative.Text = "x^3-cos(x)+3*x-sqrt(2)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Point:";
            // 
            // txtPoint
            // 
            this.txtPoint.Location = new System.Drawing.Point(49, 45);
            this.txtPoint.Name = "txtPoint";
            this.txtPoint.Size = new System.Drawing.Size(100, 20);
            this.txtPoint.TabIndex = 2;
            this.txtPoint.Text = "6";
            // 
            // gbFirstDerivative
            // 
            this.gbFirstDerivative.Controls.Add(this.btnComputeFirst);
            this.gbFirstDerivative.Controls.Add(this.txtResultFirst);
            this.gbFirstDerivative.Location = new System.Drawing.Point(12, 80);
            this.gbFirstDerivative.Name = "gbFirstDerivative";
            this.gbFirstDerivative.Size = new System.Drawing.Size(230, 107);
            this.gbFirstDerivative.TabIndex = 3;
            this.gbFirstDerivative.TabStop = false;
            this.gbFirstDerivative.Text = "First derivative";
            // 
            // txtResultFirst
            // 
            this.txtResultFirst.Location = new System.Drawing.Point(54, 31);
            this.txtResultFirst.Name = "txtResultFirst";
            this.txtResultFirst.Size = new System.Drawing.Size(122, 20);
            this.txtResultFirst.TabIndex = 0;
            // 
            // btnComputeFirst
            // 
            this.btnComputeFirst.Location = new System.Drawing.Point(74, 57);
            this.btnComputeFirst.Name = "btnComputeFirst";
            this.btnComputeFirst.Size = new System.Drawing.Size(75, 23);
            this.btnComputeFirst.TabIndex = 1;
            this.btnComputeFirst.Text = "Compute";
            this.btnComputeFirst.UseVisualStyleBackColor = true;
            this.btnComputeFirst.Click += new System.EventHandler(this.btnComputeFirst_Click);
            // 
            // gbSecondDerivative
            // 
            this.gbSecondDerivative.Controls.Add(this.btnComputeSecond);
            this.gbSecondDerivative.Controls.Add(this.txtResultSecond);
            this.gbSecondDerivative.Location = new System.Drawing.Point(12, 202);
            this.gbSecondDerivative.Name = "gbSecondDerivative";
            this.gbSecondDerivative.Size = new System.Drawing.Size(230, 107);
            this.gbSecondDerivative.TabIndex = 4;
            this.gbSecondDerivative.TabStop = false;
            this.gbSecondDerivative.Text = "Second derivative";
            // 
            // btnComputeSecond
            // 
            this.btnComputeSecond.Location = new System.Drawing.Point(74, 57);
            this.btnComputeSecond.Name = "btnComputeSecond";
            this.btnComputeSecond.Size = new System.Drawing.Size(75, 23);
            this.btnComputeSecond.TabIndex = 1;
            this.btnComputeSecond.Text = "Compute";
            this.btnComputeSecond.UseVisualStyleBackColor = true;
            this.btnComputeSecond.Click += new System.EventHandler(this.btnComputeSecond_Click);
            // 
            // txtResultSecond
            // 
            this.txtResultSecond.Location = new System.Drawing.Point(54, 31);
            this.txtResultSecond.Name = "txtResultSecond";
            this.txtResultSecond.Size = new System.Drawing.Size(122, 20);
            this.txtResultSecond.TabIndex = 0;
            // 
            // gbFunctionAtPoint
            // 
            this.gbFunctionAtPoint.Controls.Add(this.btnComputeFunctionAtPoint);
            this.gbFunctionAtPoint.Controls.Add(this.txtResultFunctionAtPoint);
            this.gbFunctionAtPoint.Location = new System.Drawing.Point(12, 324);
            this.gbFunctionAtPoint.Name = "gbFunctionAtPoint";
            this.gbFunctionAtPoint.Size = new System.Drawing.Size(230, 107);
            this.gbFunctionAtPoint.TabIndex = 5;
            this.gbFunctionAtPoint.TabStop = false;
            this.gbFunctionAtPoint.Text = "Function value at point";
            // 
            // btnComputeFunctionAtPoint
            // 
            this.btnComputeFunctionAtPoint.Location = new System.Drawing.Point(74, 57);
            this.btnComputeFunctionAtPoint.Name = "btnComputeFunctionAtPoint";
            this.btnComputeFunctionAtPoint.Size = new System.Drawing.Size(75, 23);
            this.btnComputeFunctionAtPoint.TabIndex = 1;
            this.btnComputeFunctionAtPoint.Text = "Compute";
            this.btnComputeFunctionAtPoint.UseVisualStyleBackColor = true;
            this.btnComputeFunctionAtPoint.Click += new System.EventHandler(this.btnComputeFunctionAtPoint_Click);
            // 
            // txtResultFunctionAtPoint
            // 
            this.txtResultFunctionAtPoint.Location = new System.Drawing.Point(54, 31);
            this.txtResultFunctionAtPoint.Name = "txtResultFunctionAtPoint";
            this.txtResultFunctionAtPoint.Size = new System.Drawing.Size(122, 20);
            this.txtResultFunctionAtPoint.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 446);
            this.Controls.Add(this.gbFunctionAtPoint);
            this.Controls.Add(this.gbSecondDerivative);
            this.Controls.Add(this.gbFirstDerivative);
            this.Controls.Add(this.txtPoint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDerivative);
            this.Name = "Form1";
            this.Text = "DerivativeTest";
            this.gbFirstDerivative.ResumeLayout(false);
            this.gbFirstDerivative.PerformLayout();
            this.gbSecondDerivative.ResumeLayout(false);
            this.gbSecondDerivative.PerformLayout();
            this.gbFunctionAtPoint.ResumeLayout(false);
            this.gbFunctionAtPoint.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDerivative;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPoint;
        private System.Windows.Forms.GroupBox gbFirstDerivative;
        private System.Windows.Forms.Button btnComputeFirst;
        private System.Windows.Forms.TextBox txtResultFirst;
        private System.Windows.Forms.GroupBox gbSecondDerivative;
        private System.Windows.Forms.Button btnComputeSecond;
        private System.Windows.Forms.TextBox txtResultSecond;
        private System.Windows.Forms.GroupBox gbFunctionAtPoint;
        private System.Windows.Forms.Button btnComputeFunctionAtPoint;
        private System.Windows.Forms.TextBox txtResultFunctionAtPoint;
    }
}

