namespace WinFormsGreatAgain
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbParent = new System.Windows.Forms.GroupBox();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbParent.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbParent
            // 
            this.gbParent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbParent.Controls.Add(this.txtPhoneNumber);
            this.gbParent.Controls.Add(this.lblPhoneNumber);
            this.gbParent.Controls.Add(this.lblLastName);
            this.gbParent.Controls.Add(this.txtLastName);
            this.gbParent.Controls.Add(this.txtFirstName);
            this.gbParent.Controls.Add(this.lblFirstName);
            this.gbParent.Location = new System.Drawing.Point(29, 33);
            this.gbParent.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.gbParent.Name = "gbParent";
            this.gbParent.Padding = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.gbParent.Size = new System.Drawing.Size(1457, 569);
            this.gbParent.TabIndex = 0;
            this.gbParent.TabStop = false;
            this.gbParent.Text = "groupBox1";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhoneNumber.Location = new System.Drawing.Point(510, 221);
            this.txtPhoneNumber.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(927, 47);
            this.txtPhoneNumber.TabIndex = 5;
            this.txtPhoneNumber.Validated += new System.EventHandler(this.txtPhoneNumber_Validated);
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.AutoSize = true;
            this.lblPhoneNumber.Location = new System.Drawing.Point(15, 230);
            this.lblPhoneNumber.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(97, 41);
            this.lblPhoneNumber.TabIndex = 4;
            this.lblPhoneNumber.Text = "label1";
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(15, 148);
            this.lblLastName.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(97, 41);
            this.lblLastName.TabIndex = 3;
            this.lblLastName.Text = "label1";
            // 
            // txtLastName
            // 
            this.txtLastName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLastName.Location = new System.Drawing.Point(510, 139);
            this.txtLastName.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(927, 47);
            this.txtLastName.TabIndex = 2;
            this.txtLastName.Validated += new System.EventHandler(this.txtLastName_Validated);
            // 
            // txtFirstName
            // 
            this.txtFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFirstName.Location = new System.Drawing.Point(510, 60);
            this.txtFirstName.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(927, 47);
            this.txtFirstName.TabIndex = 1;
            this.txtFirstName.Validated += new System.EventHandler(this.txtFirstName_Validated);
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(15, 68);
            this.lblFirstName.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(97, 41);
            this.lblFirstName.TabIndex = 0;
            this.lblFirstName.Text = "label1";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(1304, 618);
            this.btnOk.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(182, 63);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "button1";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(1107, 618);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(182, 63);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "button1";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 41F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1515, 713);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gbParent);
            this.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.MinimumSize = new System.Drawing.Size(1509, 667);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.gbParent.ResumeLayout(false);
            this.gbParent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox gbParent;
        private Button btnOk;
        private Button btnCancel;
        private Label lblLastName;
        private TextBox txtLastName;
        private TextBox txtFirstName;
        private Label lblFirstName;
        private TextBox txtPhoneNumber;
        private Label lblPhoneNumber;
    }
}