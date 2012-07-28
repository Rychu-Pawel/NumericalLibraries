namespace IntegralTest
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
            this.txtIntegral = new System.Windows.Forms.TextBox();
            this.txtLowerLimit = new System.Windows.Forms.TextBox();
            this.txtUpperLimit = new System.Windows.Forms.TextBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnCompute = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.gbResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIntegral
            // 
            this.txtIntegral.Location = new System.Drawing.Point(74, 12);
            this.txtIntegral.Name = "txtIntegral";
            this.txtIntegral.Size = new System.Drawing.Size(234, 20);
            this.txtIntegral.TabIndex = 0;
            this.txtIntegral.Text = "x^3-cos(x)";
            // 
            // txtLowerLimit
            // 
            this.txtLowerLimit.Location = new System.Drawing.Point(74, 64);
            this.txtLowerLimit.Name = "txtLowerLimit";
            this.txtLowerLimit.Size = new System.Drawing.Size(123, 20);
            this.txtLowerLimit.TabIndex = 1;
            this.txtLowerLimit.Text = "-5";
            // 
            // txtUpperLimit
            // 
            this.txtUpperLimit.Location = new System.Drawing.Point(74, 95);
            this.txtUpperLimit.Name = "txtUpperLimit";
            this.txtUpperLimit.Size = new System.Drawing.Size(123, 20);
            this.txtUpperLimit.TabIndex = 2;
            this.txtUpperLimit.Text = "5";
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(30, 57);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(100, 20);
            this.txtResult.TabIndex = 3;
            // 
            // btnCompute
            // 
            this.btnCompute.Location = new System.Drawing.Point(43, 24);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(75, 23);
            this.btnCompute.TabIndex = 4;
            this.btnCompute.Text = "Compute";
            this.btnCompute.UseVisualStyleBackColor = true;
            this.btnCompute.Click += new System.EventHandler(this.btnCompute_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Integral";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Lower limit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Upper limit";
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.txtResult);
            this.gbResult.Controls.Add(this.btnCompute);
            this.gbResult.Location = new System.Drawing.Point(227, 38);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(161, 100);
            this.gbResult.TabIndex = 8;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "Result";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 152);
            this.Controls.Add(this.gbResult);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUpperLimit);
            this.Controls.Add(this.txtLowerLimit);
            this.Controls.Add(this.txtIntegral);
            this.Name = "Form1";
            this.Text = "IntegralTest";
            this.gbResult.ResumeLayout(false);
            this.gbResult.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIntegral;
        private System.Windows.Forms.TextBox txtLowerLimit;
        private System.Windows.Forms.TextBox txtUpperLimit;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnCompute;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbResult;
    }
}

