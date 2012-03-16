namespace NumericalCalculator
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.lnkPawelRychlicki = new System.Windows.Forms.LinkLabel();
            this.lblVersionValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.AutoSize = true;
            this.lblAppTitle.Location = new System.Drawing.Point(17, 17);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(104, 13);
            this.lblAppTitle.TabIndex = 0;
            this.lblAppTitle.Text = "Numerical Calculator";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(17, 45);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(40, 13);
            this.lblVersion.TabIndex = 1;
            this.lblVersion.Text = "Wersja";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Copyright © 2009 - 2010";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(17, 101);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(253, 174);
            this.txtDescription.TabIndex = 4;
            this.txtDescription.Text = resources.GetString("txtDescription.Text");
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(106, 294);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lnkPawelRychlicki
            // 
            this.lnkPawelRychlicki.ActiveLinkColor = System.Drawing.Color.Black;
            this.lnkPawelRychlicki.AutoSize = true;
            this.lnkPawelRychlicki.LinkColor = System.Drawing.Color.Black;
            this.lnkPawelRychlicki.Location = new System.Drawing.Point(139, 73);
            this.lnkPawelRychlicki.Name = "lnkPawelRychlicki";
            this.lnkPawelRychlicki.Size = new System.Drawing.Size(84, 13);
            this.lnkPawelRychlicki.TabIndex = 6;
            this.lnkPawelRychlicki.TabStop = true;
            this.lnkPawelRychlicki.Text = "Paweł Rychlicki";
            this.lnkPawelRychlicki.VisitedLinkColor = System.Drawing.Color.Black;
            this.lnkPawelRychlicki.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPawelRychlicki_LinkClicked);
            // 
            // lblVersionValue
            // 
            this.lblVersionValue.AutoSize = true;
            this.lblVersionValue.Location = new System.Drawing.Point(53, 45);
            this.lblVersionValue.Name = "lblVersionValue";
            this.lblVersionValue.Size = new System.Drawing.Size(31, 13);
            this.lblVersionValue.TabIndex = 7;
            this.lblVersionValue.Text = "1.0.3";
            // 
            // AboutForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(286, 330);
            this.Controls.Add(this.lblVersionValue);
            this.Controls.Add(this.lnkPawelRychlicki);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblAppTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AboutForm";
            this.Text = "O programie";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.LinkLabel lnkPawelRychlicki;
        private System.Windows.Forms.Label lblVersionValue;
    }
}