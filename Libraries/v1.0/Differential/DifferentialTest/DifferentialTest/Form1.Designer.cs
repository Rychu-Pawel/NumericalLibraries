namespace DifferentialTest
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
            this.gbConditionsFirstOrder = new System.Windows.Forms.GroupBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.txtPointFirstOrder = new System.Windows.Forms.TextBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtPointValueFirstOrder = new System.Windows.Forms.TextBox();
            this.gbFirstOrder = new System.Windows.Forms.GroupBox();
            this.txtDifferentialFirstOrder = new System.Windows.Forms.TextBox();
            this.gbResultFirstOrder = new System.Windows.Forms.GroupBox();
            this.txtResultFirstOrder = new System.Windows.Forms.TextBox();
            this.btnComputeFirstOrder = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbResultSecondOrder = new System.Windows.Forms.GroupBox();
            this.btnComputeSecondOrder = new System.Windows.Forms.Button();
            this.txtResultSecondOrder = new System.Windows.Forms.TextBox();
            this.txtDifferentialSecondOrder = new System.Windows.Forms.TextBox();
            this.gbConditionsSecondOrderI = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPointSecondOrder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPointValueSecondOrder = new System.Windows.Forms.TextBox();
            this.gbConditionsSecondOrderII = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPointSecondOrderII = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPointValueSecondOrderII = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLookingPointFirstOrder = new System.Windows.Forms.TextBox();
            this.txtLookingPointSecondOrder = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gbConditionsFirstOrder.SuspendLayout();
            this.gbFirstOrder.SuspendLayout();
            this.gbResultFirstOrder.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbResultSecondOrder.SuspendLayout();
            this.gbConditionsSecondOrderI.SuspendLayout();
            this.gbConditionsSecondOrderII.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbConditionsFirstOrder
            // 
            this.gbConditionsFirstOrder.Controls.Add(this.lblFrom);
            this.gbConditionsFirstOrder.Controls.Add(this.txtPointFirstOrder);
            this.gbConditionsFirstOrder.Controls.Add(this.lblTo);
            this.gbConditionsFirstOrder.Controls.Add(this.txtPointValueFirstOrder);
            this.gbConditionsFirstOrder.Location = new System.Drawing.Point(6, 81);
            this.gbConditionsFirstOrder.Name = "gbConditionsFirstOrder";
            this.gbConditionsFirstOrder.Size = new System.Drawing.Size(332, 44);
            this.gbConditionsFirstOrder.TabIndex = 5;
            this.gbConditionsFirstOrder.TabStop = false;
            this.gbConditionsFirstOrder.Text = "Conditions";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblFrom.Location = new System.Drawing.Point(40, 17);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(18, 16);
            this.lblFrom.TabIndex = 10;
            this.lblFrom.Text = "f (";
            // 
            // txtPointFirstOrder
            // 
            this.txtPointFirstOrder.Location = new System.Drawing.Point(64, 16);
            this.txtPointFirstOrder.Name = "txtPointFirstOrder";
            this.txtPointFirstOrder.Size = new System.Drawing.Size(96, 20);
            this.txtPointFirstOrder.TabIndex = 11;
            this.txtPointFirstOrder.Text = "2";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTo.Location = new System.Drawing.Point(166, 17);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(22, 16);
            this.lblTo.TabIndex = 12;
            this.lblTo.Text = ") =";
            // 
            // txtPointValueFirstOrder
            // 
            this.txtPointValueFirstOrder.Location = new System.Drawing.Point(194, 16);
            this.txtPointValueFirstOrder.Name = "txtPointValueFirstOrder";
            this.txtPointValueFirstOrder.Size = new System.Drawing.Size(96, 20);
            this.txtPointValueFirstOrder.TabIndex = 12;
            this.txtPointValueFirstOrder.Text = "5";
            // 
            // gbFirstOrder
            // 
            this.gbFirstOrder.Controls.Add(this.txtLookingPointFirstOrder);
            this.gbFirstOrder.Controls.Add(this.label5);
            this.gbFirstOrder.Controls.Add(this.gbResultFirstOrder);
            this.gbFirstOrder.Controls.Add(this.txtDifferentialFirstOrder);
            this.gbFirstOrder.Controls.Add(this.gbConditionsFirstOrder);
            this.gbFirstOrder.Location = new System.Drawing.Point(12, 12);
            this.gbFirstOrder.Name = "gbFirstOrder";
            this.gbFirstOrder.Size = new System.Drawing.Size(346, 232);
            this.gbFirstOrder.TabIndex = 6;
            this.gbFirstOrder.TabStop = false;
            this.gbFirstOrder.Text = "First order differential";
            // 
            // txtDifferentialFirstOrder
            // 
            this.txtDifferentialFirstOrder.Location = new System.Drawing.Point(6, 19);
            this.txtDifferentialFirstOrder.Name = "txtDifferentialFirstOrder";
            this.txtDifferentialFirstOrder.Size = new System.Drawing.Size(332, 20);
            this.txtDifferentialFirstOrder.TabIndex = 0;
            this.txtDifferentialFirstOrder.Text = "3*x^3/2+2*x-8*y";
            // 
            // gbResultFirstOrder
            // 
            this.gbResultFirstOrder.Controls.Add(this.btnComputeFirstOrder);
            this.gbResultFirstOrder.Controls.Add(this.txtResultFirstOrder);
            this.gbResultFirstOrder.Location = new System.Drawing.Point(6, 131);
            this.gbResultFirstOrder.Name = "gbResultFirstOrder";
            this.gbResultFirstOrder.Size = new System.Drawing.Size(332, 91);
            this.gbResultFirstOrder.TabIndex = 6;
            this.gbResultFirstOrder.TabStop = false;
            this.gbResultFirstOrder.Text = "Result";
            // 
            // txtResultFirstOrder
            // 
            this.txtResultFirstOrder.Location = new System.Drawing.Point(100, 23);
            this.txtResultFirstOrder.Name = "txtResultFirstOrder";
            this.txtResultFirstOrder.Size = new System.Drawing.Size(133, 20);
            this.txtResultFirstOrder.TabIndex = 0;
            // 
            // btnComputeFirstOrder
            // 
            this.btnComputeFirstOrder.Location = new System.Drawing.Point(129, 49);
            this.btnComputeFirstOrder.Name = "btnComputeFirstOrder";
            this.btnComputeFirstOrder.Size = new System.Drawing.Size(75, 23);
            this.btnComputeFirstOrder.TabIndex = 1;
            this.btnComputeFirstOrder.Text = "Compute";
            this.btnComputeFirstOrder.UseVisualStyleBackColor = true;
            this.btnComputeFirstOrder.Click += new System.EventHandler(this.btnComputeFirstOrder_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtLookingPointSecondOrder);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.gbConditionsSecondOrderII);
            this.groupBox1.Controls.Add(this.gbResultSecondOrder);
            this.groupBox1.Controls.Add(this.txtDifferentialSecondOrder);
            this.groupBox1.Controls.Add(this.gbConditionsSecondOrderI);
            this.groupBox1.Location = new System.Drawing.Point(12, 250);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(346, 280);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "First order differential";
            // 
            // gbResultSecondOrder
            // 
            this.gbResultSecondOrder.Controls.Add(this.btnComputeSecondOrder);
            this.gbResultSecondOrder.Controls.Add(this.txtResultSecondOrder);
            this.gbResultSecondOrder.Location = new System.Drawing.Point(6, 182);
            this.gbResultSecondOrder.Name = "gbResultSecondOrder";
            this.gbResultSecondOrder.Size = new System.Drawing.Size(332, 91);
            this.gbResultSecondOrder.TabIndex = 6;
            this.gbResultSecondOrder.TabStop = false;
            this.gbResultSecondOrder.Text = "Result";
            // 
            // btnComputeSecondOrder
            // 
            this.btnComputeSecondOrder.Location = new System.Drawing.Point(129, 49);
            this.btnComputeSecondOrder.Name = "btnComputeSecondOrder";
            this.btnComputeSecondOrder.Size = new System.Drawing.Size(75, 23);
            this.btnComputeSecondOrder.TabIndex = 1;
            this.btnComputeSecondOrder.Text = "Compute";
            this.btnComputeSecondOrder.UseVisualStyleBackColor = true;
            this.btnComputeSecondOrder.Click += new System.EventHandler(this.btnComputeSecondOrder_Click);
            // 
            // txtResultSecondOrder
            // 
            this.txtResultSecondOrder.Location = new System.Drawing.Point(100, 23);
            this.txtResultSecondOrder.Name = "txtResultSecondOrder";
            this.txtResultSecondOrder.Size = new System.Drawing.Size(133, 20);
            this.txtResultSecondOrder.TabIndex = 0;
            // 
            // txtDifferentialSecondOrder
            // 
            this.txtDifferentialSecondOrder.Location = new System.Drawing.Point(6, 19);
            this.txtDifferentialSecondOrder.Name = "txtDifferentialSecondOrder";
            this.txtDifferentialSecondOrder.Size = new System.Drawing.Size(332, 20);
            this.txtDifferentialSecondOrder.TabIndex = 0;
            this.txtDifferentialSecondOrder.Text = "3*x^3/2+2*x-8*y+y\'";
            // 
            // gbConditionsSecondOrderI
            // 
            this.gbConditionsSecondOrderI.Controls.Add(this.label1);
            this.gbConditionsSecondOrderI.Controls.Add(this.txtPointSecondOrder);
            this.gbConditionsSecondOrderI.Controls.Add(this.label2);
            this.gbConditionsSecondOrderI.Controls.Add(this.txtPointValueSecondOrder);
            this.gbConditionsSecondOrderI.Location = new System.Drawing.Point(6, 82);
            this.gbConditionsSecondOrderI.Name = "gbConditionsSecondOrderI";
            this.gbConditionsSecondOrderI.Size = new System.Drawing.Size(332, 44);
            this.gbConditionsSecondOrderI.TabIndex = 5;
            this.gbConditionsSecondOrderI.TabStop = false;
            this.gbConditionsSecondOrderI.Text = "Conditions";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(40, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "f (";
            // 
            // txtPointSecondOrder
            // 
            this.txtPointSecondOrder.Location = new System.Drawing.Point(64, 16);
            this.txtPointSecondOrder.Name = "txtPointSecondOrder";
            this.txtPointSecondOrder.Size = new System.Drawing.Size(96, 20);
            this.txtPointSecondOrder.TabIndex = 11;
            this.txtPointSecondOrder.Text = "3";
            this.txtPointSecondOrder.TextChanged += new System.EventHandler(this.txtPointSecondOrder_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(166, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = ") =";
            // 
            // txtPointValueSecondOrder
            // 
            this.txtPointValueSecondOrder.Location = new System.Drawing.Point(194, 16);
            this.txtPointValueSecondOrder.Name = "txtPointValueSecondOrder";
            this.txtPointValueSecondOrder.Size = new System.Drawing.Size(96, 20);
            this.txtPointValueSecondOrder.TabIndex = 12;
            this.txtPointValueSecondOrder.Text = "6";
            // 
            // gbConditionsSecondOrderII
            // 
            this.gbConditionsSecondOrderII.Controls.Add(this.label3);
            this.gbConditionsSecondOrderII.Controls.Add(this.txtPointSecondOrderII);
            this.gbConditionsSecondOrderII.Controls.Add(this.label4);
            this.gbConditionsSecondOrderII.Controls.Add(this.txtPointValueSecondOrderII);
            this.gbConditionsSecondOrderII.Location = new System.Drawing.Point(6, 132);
            this.gbConditionsSecondOrderII.Name = "gbConditionsSecondOrderII";
            this.gbConditionsSecondOrderII.Size = new System.Drawing.Size(332, 44);
            this.gbConditionsSecondOrderII.TabIndex = 13;
            this.gbConditionsSecondOrderII.TabStop = false;
            this.gbConditionsSecondOrderII.Text = "Conditions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(38, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "f\' (";
            // 
            // txtPointSecondOrderII
            // 
            this.txtPointSecondOrderII.Enabled = false;
            this.txtPointSecondOrderII.Location = new System.Drawing.Point(64, 16);
            this.txtPointSecondOrderII.Name = "txtPointSecondOrderII";
            this.txtPointSecondOrderII.Size = new System.Drawing.Size(96, 20);
            this.txtPointSecondOrderII.TabIndex = 11;
            this.txtPointSecondOrderII.Text = "3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(166, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = ") =";
            // 
            // txtPointValueSecondOrderII
            // 
            this.txtPointValueSecondOrderII.Location = new System.Drawing.Point(194, 16);
            this.txtPointValueSecondOrderII.Name = "txtPointValueSecondOrderII";
            this.txtPointValueSecondOrderII.Size = new System.Drawing.Size(96, 20);
            this.txtPointValueSecondOrderII.TabIndex = 12;
            this.txtPointValueSecondOrderII.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Point:";
            // 
            // txtLookingPointFirstOrder
            // 
            this.txtLookingPointFirstOrder.Location = new System.Drawing.Point(46, 48);
            this.txtLookingPointFirstOrder.Name = "txtLookingPointFirstOrder";
            this.txtLookingPointFirstOrder.Size = new System.Drawing.Size(100, 20);
            this.txtLookingPointFirstOrder.TabIndex = 8;
            this.txtLookingPointFirstOrder.Text = "5";
            // 
            // txtLookingPointSecondOrder
            // 
            this.txtLookingPointSecondOrder.Location = new System.Drawing.Point(46, 48);
            this.txtLookingPointSecondOrder.Name = "txtLookingPointSecondOrder";
            this.txtLookingPointSecondOrder.Size = new System.Drawing.Size(100, 20);
            this.txtLookingPointSecondOrder.TabIndex = 15;
            this.txtLookingPointSecondOrder.Text = "-2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Point:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 539);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbFirstOrder);
            this.Name = "Form1";
            this.Text = "DifferentialTest";
            this.gbConditionsFirstOrder.ResumeLayout(false);
            this.gbConditionsFirstOrder.PerformLayout();
            this.gbFirstOrder.ResumeLayout(false);
            this.gbFirstOrder.PerformLayout();
            this.gbResultFirstOrder.ResumeLayout(false);
            this.gbResultFirstOrder.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbResultSecondOrder.ResumeLayout(false);
            this.gbResultSecondOrder.PerformLayout();
            this.gbConditionsSecondOrderI.ResumeLayout(false);
            this.gbConditionsSecondOrderI.PerformLayout();
            this.gbConditionsSecondOrderII.ResumeLayout(false);
            this.gbConditionsSecondOrderII.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbConditionsFirstOrder;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.TextBox txtPointFirstOrder;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.TextBox txtPointValueFirstOrder;
        private System.Windows.Forms.GroupBox gbFirstOrder;
        private System.Windows.Forms.GroupBox gbResultFirstOrder;
        private System.Windows.Forms.Button btnComputeFirstOrder;
        private System.Windows.Forms.TextBox txtResultFirstOrder;
        private System.Windows.Forms.TextBox txtDifferentialFirstOrder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox gbConditionsSecondOrderII;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPointSecondOrderII;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPointValueSecondOrderII;
        private System.Windows.Forms.GroupBox gbResultSecondOrder;
        private System.Windows.Forms.Button btnComputeSecondOrder;
        private System.Windows.Forms.TextBox txtResultSecondOrder;
        private System.Windows.Forms.TextBox txtDifferentialSecondOrder;
        private System.Windows.Forms.GroupBox gbConditionsSecondOrderI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPointSecondOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPointValueSecondOrder;
        private System.Windows.Forms.TextBox txtLookingPointFirstOrder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLookingPointSecondOrder;
        private System.Windows.Forms.Label label6;
    }
}

