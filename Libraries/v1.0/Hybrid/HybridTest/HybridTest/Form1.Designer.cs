namespace HybridTest
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
            this.txtFunction = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.btnCompute = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.gbResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFunction
            // 
            this.txtFunction.Location = new System.Drawing.Point(12, 12);
            this.txtFunction.Name = "txtFunction";
            this.txtFunction.Size = new System.Drawing.Size(283, 20);
            this.txtFunction.TabIndex = 0;
            this.txtFunction.Text = "(x-3/2)^2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Look for function root from ";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(149, 38);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(59, 20);
            this.txtFrom.TabIndex = 2;
            this.txtFrom.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "to";
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(236, 38);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(59, 20);
            this.txtTo.TabIndex = 4;
            this.txtTo.Text = "2";
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.txtResult);
            this.gbResult.Controls.Add(this.btnCompute);
            this.gbResult.Location = new System.Drawing.Point(12, 64);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(283, 100);
            this.gbResult.TabIndex = 5;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "Result";
            // 
            // btnCompute
            // 
            this.btnCompute.Location = new System.Drawing.Point(104, 28);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(75, 23);
            this.btnCompute.TabIndex = 0;
            this.btnCompute.Text = "Compute";
            this.btnCompute.UseVisualStyleBackColor = true;
            this.btnCompute.Click += new System.EventHandler(this.btnCompute_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(91, 57);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(100, 20);
            this.txtResult.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(307, 176);
            this.Controls.Add(this.gbResult);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFunction);
            this.Name = "Form1";
            this.Text = "HybridTest";
            this.gbResult.ResumeLayout(false);
            this.gbResult.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFunction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.GroupBox gbResult;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnCompute;
    }
}

