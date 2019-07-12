namespace Bus_Management_System
{
    partial class LOGIN
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
            this.btn_login = new System.Windows.Forms.Button();
            this.txt_box_password = new System.Windows.Forms.TextBox();
            this.user_label = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.combo_user_type = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_box_userid = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_login
            // 
            this.btn_login.Location = new System.Drawing.Point(163, 210);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(76, 35);
            this.btn_login.TabIndex = 0;
            this.btn_login.Text = "Login";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // txt_box_password
            // 
            this.txt_box_password.Location = new System.Drawing.Point(139, 155);
            this.txt_box_password.Name = "txt_box_password";
            this.txt_box_password.PasswordChar = '*';
            this.txt_box_password.Size = new System.Drawing.Size(132, 20);
            this.txt_box_password.TabIndex = 2;
            // 
            // user_label
            // 
            this.user_label.AutoSize = true;
            this.user_label.Location = new System.Drawing.Point(81, 85);
            this.user_label.Name = "user_label";
            this.user_label.Size = new System.Drawing.Size(46, 13);
            this.user_label.TabIndex = 3;
            this.user_label.Text = "User ID:";
            this.user_label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(32, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Bus Ticket Managment System";
            // 
            // combo_user_type
            // 
            this.combo_user_type.FormattingEnabled = true;
            this.combo_user_type.Items.AddRange(new object[] {
            "Administrator",
            "Ticket Seller"});
            this.combo_user_type.Location = new System.Drawing.Point(139, 118);
            this.combo_user_type.Name = "combo_user_type";
            this.combo_user_type.Size = new System.Drawing.Size(132, 21);
            this.combo_user_type.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "User Type:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txt_box_userid
            // 
            this.txt_box_userid.Location = new System.Drawing.Point(139, 82);
            this.txt_box_userid.Name = "txt_box_userid";
            this.txt_box_userid.Size = new System.Drawing.Size(132, 20);
            this.txt_box_userid.TabIndex = 8;
            // 
            // LOGIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 318);
            this.Controls.Add(this.txt_box_userid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.combo_user_type);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.user_label);
            this.Controls.Add(this.txt_box_password);
            this.Controls.Add(this.btn_login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LOGIN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.TextBox txt_box_password;
        private System.Windows.Forms.Label user_label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combo_user_type;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_box_userid;
    }
}

