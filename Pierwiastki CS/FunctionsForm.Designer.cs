namespace NumericalCalculator
{
    partial class FunctionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FunctionForm));
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblOperators = new System.Windows.Forms.Label();
            this.lblBody = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Location = new System.Drawing.Point(116, 19);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(249, 13);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "W programie można używać następujących funkcji:";
            // 
            // lblOperators
            // 
            this.lblOperators.AutoSize = true;
            this.lblOperators.Location = new System.Drawing.Point(12, 48);
            this.lblOperators.Name = "lblOperators";
            this.lblOperators.Size = new System.Drawing.Size(119, 13);
            this.lblOperators.TabIndex = 1;
            this.lblOperators.Text = "Operatory:  +, -, *, /, ^, !";
            // 
            // lblBody
            // 
            this.lblBody.AutoSize = true;
            this.lblBody.Location = new System.Drawing.Point(12, 77);
            this.lblBody.Name = "lblBody";
            this.lblBody.Size = new System.Drawing.Size(470, 546);
            this.lblBody.TabIndex = 2;
            this.lblBody.Text = resources.GetString("lblBody.Text");
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(208, 643);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // FunctionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(494, 678);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblBody);
            this.Controls.Add(this.lblOperators);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(510, 717);
            this.MinimumSize = new System.Drawing.Size(510, 717);
            this.Name = "FunctionForm";
            this.Text = "Funkcje";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblOperators;
        private System.Windows.Forms.Label lblBody;
        private System.Windows.Forms.Button btnOK;
    }
}