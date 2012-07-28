namespace FourierTransformTest
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
            this.btnDrawFT = new System.Windows.Forms.Button();
            this.txtFunction = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSamplingRate = new System.Windows.Forms.TextBox();
            this.pbChart = new System.Windows.Forms.PictureBox();
            this.btnDraw = new System.Windows.Forms.Button();
            this.gbFT = new System.Windows.Forms.GroupBox();
            this.gbIFT = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCutoff = new System.Windows.Forms.TextBox();
            this.btnDrawIFT = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExportFT = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbChart)).BeginInit();
            this.gbFT.SuspendLayout();
            this.gbIFT.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDrawFT
            // 
            this.btnDrawFT.Location = new System.Drawing.Point(18, 24);
            this.btnDrawFT.Name = "btnDrawFT";
            this.btnDrawFT.Size = new System.Drawing.Size(98, 23);
            this.btnDrawFT.TabIndex = 0;
            this.btnDrawFT.Text = "Draw FT";
            this.btnDrawFT.UseVisualStyleBackColor = true;
            this.btnDrawFT.Click += new System.EventHandler(this.btnDrawFT_Click);
            // 
            // txtFunction
            // 
            this.txtFunction.Location = new System.Drawing.Point(12, 12);
            this.txtFunction.Name = "txtFunction";
            this.txtFunction.Size = new System.Drawing.Size(347, 20);
            this.txtFunction.TabIndex = 1;
            this.txtFunction.Text = "exp(x/20)*(sin(1/2*x)+cos(3*x)+1/5*sin(4*x)*cos(40*x))";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sampling rate:";
            // 
            // txtSamplingRate
            // 
            this.txtSamplingRate.Location = new System.Drawing.Point(92, 43);
            this.txtSamplingRate.Name = "txtSamplingRate";
            this.txtSamplingRate.Size = new System.Drawing.Size(139, 20);
            this.txtSamplingRate.TabIndex = 3;
            this.txtSamplingRate.Text = "256";
            // 
            // pbChart
            // 
            this.pbChart.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbChart.Location = new System.Drawing.Point(446, 12);
            this.pbChart.Name = "pbChart";
            this.pbChart.Size = new System.Drawing.Size(480, 373);
            this.pbChart.TabIndex = 4;
            this.pbChart.TabStop = false;
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(365, 10);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(75, 23);
            this.btnDraw.TabIndex = 5;
            this.btnDraw.Text = "Draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // gbFT
            // 
            this.gbFT.Controls.Add(this.btnExportFT);
            this.gbFT.Controls.Add(this.btnDrawFT);
            this.gbFT.Location = new System.Drawing.Point(15, 79);
            this.gbFT.Name = "gbFT";
            this.gbFT.Size = new System.Drawing.Size(135, 94);
            this.gbFT.TabIndex = 6;
            this.gbFT.TabStop = false;
            this.gbFT.Text = "Fourier transform";
            // 
            // gbIFT
            // 
            this.gbIFT.Controls.Add(this.label3);
            this.gbIFT.Controls.Add(this.txtCutoff);
            this.gbIFT.Controls.Add(this.btnDrawIFT);
            this.gbIFT.Location = new System.Drawing.Point(159, 79);
            this.gbIFT.Name = "gbIFT";
            this.gbIFT.Size = new System.Drawing.Size(281, 94);
            this.gbIFT.TabIndex = 7;
            this.gbIFT.TabStop = false;
            this.gbIFT.Text = "Inverse fourier transform";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Cutoff: (noise reduction)";
            // 
            // txtCutoff
            // 
            this.txtCutoff.Location = new System.Drawing.Point(153, 61);
            this.txtCutoff.Name = "txtCutoff";
            this.txtCutoff.Size = new System.Drawing.Size(100, 20);
            this.txtCutoff.TabIndex = 1;
            this.txtCutoff.Text = "10";
            // 
            // btnDrawIFT
            // 
            this.btnDrawIFT.Location = new System.Drawing.Point(85, 32);
            this.btnDrawIFT.Name = "btnDrawIFT";
            this.btnDrawIFT.Size = new System.Drawing.Size(75, 23);
            this.btnDrawIFT.TabIndex = 0;
            this.btnDrawIFT.Text = "Draw IFT";
            this.btnDrawIFT.UseVisualStyleBackColor = true;
            this.btnDrawIFT.Click += new System.EventHandler(this.btnDrawIFT_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(453, 388);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(463, 26);
            this.label2.TabIndex = 8;
            this.label2.Text = "Note: For functions and IFT this chart is fixed from 0 to 6 for abscissa and from" +
                " -1 to 3 for ordinate\r\n           For FT this values are (0, 6) and (-1, 150)";
            // 
            // btnExportFT
            // 
            this.btnExportFT.Location = new System.Drawing.Point(18, 53);
            this.btnExportFT.Name = "btnExportFT";
            this.btnExportFT.Size = new System.Drawing.Size(98, 23);
            this.btnExportFT.TabIndex = 1;
            this.btnExportFT.Text = "Save to TXT";
            this.btnExportFT.UseVisualStyleBackColor = true;
            this.btnExportFT.Click += new System.EventHandler(this.btnExportFT_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 420);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gbIFT);
            this.Controls.Add(this.gbFT);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.pbChart);
            this.Controls.Add(this.txtSamplingRate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFunction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "FourierTransformTest";
            ((System.ComponentModel.ISupportInitialize)(this.pbChart)).EndInit();
            this.gbFT.ResumeLayout(false);
            this.gbIFT.ResumeLayout(false);
            this.gbIFT.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDrawFT;
        private System.Windows.Forms.TextBox txtFunction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSamplingRate;
        private System.Windows.Forms.PictureBox pbChart;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.GroupBox gbFT;
        private System.Windows.Forms.GroupBox gbIFT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCutoff;
        private System.Windows.Forms.Button btnDrawIFT;
        private System.Windows.Forms.Button btnExportFT;
    }
}

