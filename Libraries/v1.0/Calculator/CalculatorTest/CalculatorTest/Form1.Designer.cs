namespace CalculatorTest
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
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.btnCompute = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.btnMean = new System.Windows.Forms.Button();
            this.btnProportions = new System.Windows.Forms.Button();
            this.gbResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFunction
            // 
            this.txtFunction.Location = new System.Drawing.Point(12, 12);
            this.txtFunction.Name = "txtFunction";
            this.txtFunction.Size = new System.Drawing.Size(396, 20);
            this.txtFunction.TabIndex = 0;
            this.txtFunction.Text = "5^3/4+cos(20)*(tgh(10)-exp(3))";
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.btnCompute);
            this.gbResult.Controls.Add(this.txtResult);
            this.gbResult.Location = new System.Drawing.Point(81, 38);
            this.gbResult.Name = "gbResult";
            this.gbResult.Size = new System.Drawing.Size(251, 88);
            this.gbResult.TabIndex = 1;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "Result";
            // 
            // btnCompute
            // 
            this.btnCompute.Location = new System.Drawing.Point(87, 48);
            this.btnCompute.Name = "btnCompute";
            this.btnCompute.Size = new System.Drawing.Size(75, 23);
            this.btnCompute.TabIndex = 1;
            this.btnCompute.Text = "Compute";
            this.btnCompute.UseVisualStyleBackColor = true;
            this.btnCompute.Click += new System.EventHandler(this.btnCompute_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(66, 22);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(119, 20);
            this.txtResult.TabIndex = 0;
            // 
            // btnMean
            // 
            this.btnMean.Location = new System.Drawing.Point(12, 267);
            this.btnMean.Name = "btnMean";
            this.btnMean.Size = new System.Drawing.Size(75, 23);
            this.btnMean.TabIndex = 2;
            this.btnMean.Text = "Mean";
            this.btnMean.UseVisualStyleBackColor = true;
            this.btnMean.Click += new System.EventHandler(this.btnMean_Click);
            // 
            // btnProportions
            // 
            this.btnProportions.Location = new System.Drawing.Point(333, 267);
            this.btnProportions.Name = "btnProportions";
            this.btnProportions.Size = new System.Drawing.Size(75, 23);
            this.btnProportions.TabIndex = 3;
            this.btnProportions.Text = "Proportions";
            this.btnProportions.UseVisualStyleBackColor = true;
            this.btnProportions.Click += new System.EventHandler(this.btnProportions_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnCompute;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 302);
            this.Controls.Add(this.btnProportions);
            this.Controls.Add(this.btnMean);
            this.Controls.Add(this.gbResult);
            this.Controls.Add(this.txtFunction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "CalculatorTest";
            this.gbResult.ResumeLayout(false);
            this.gbResult.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtFunction;
        private System.Windows.Forms.GroupBox gbResult;
        private System.Windows.Forms.Button btnCompute;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Button btnMean;
        private System.Windows.Forms.Button btnProportions;
    }
}

