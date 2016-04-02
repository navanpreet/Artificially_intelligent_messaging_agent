namespace XIMA_Beta
{
    partial class Form2
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
            this.button1 = new System.Windows.Forms.Button();
            this.Too = new System.Windows.Forms.Label();
            this.Subject = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TO = new System.Windows.Forms.TextBox();
            this.Sub = new System.Windows.Forms.TextBox();
            this.Message = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.T = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(233, 337);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Too
            // 
            this.Too.AutoSize = true;
            this.Too.BackColor = System.Drawing.Color.Transparent;
            this.Too.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Too.Location = new System.Drawing.Point(10, 52);
            this.Too.Name = "Too";
            this.Too.Size = new System.Drawing.Size(26, 18);
            this.Too.TabIndex = 1;
            this.Too.Text = "To";
            // 
            // Subject
            // 
            this.Subject.AutoSize = true;
            this.Subject.BackColor = System.Drawing.Color.Transparent;
            this.Subject.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Subject.Location = new System.Drawing.Point(10, 83);
            this.Subject.Name = "Subject";
            this.Subject.Size = new System.Drawing.Size(57, 18);
            this.Subject.TabIndex = 2;
            this.Subject.Text = "Subject";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Message";
            // 
            // TO
            // 
            this.TO.Location = new System.Drawing.Point(97, 50);
            this.TO.Name = "TO";
            this.TO.Size = new System.Drawing.Size(326, 20);
            this.TO.TabIndex = 4;
            // 
            // Sub
            // 
            this.Sub.Location = new System.Drawing.Point(97, 81);
            this.Sub.Name = "Sub";
            this.Sub.Size = new System.Drawing.Size(325, 20);
            this.Sub.TabIndex = 5;
            // 
            // Message
            // 
            this.Message.Location = new System.Drawing.Point(13, 134);
            this.Message.Multiline = true;
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(540, 192);
            this.Message.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "X";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 24);
            this.label2.TabIndex = 8;
            this.label2.Text = "-";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // T
            // 
            this.T.AutoSize = true;
            this.T.BackColor = System.Drawing.Color.Transparent;
            this.T.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.T.Location = new System.Drawing.Point(5, 3);
            this.T.Name = "T";
            this.T.Size = new System.Drawing.Size(23, 24);
            this.T.TabIndex = 9;
            this.T.Text = "T";
            this.T.Click += new System.EventHandler(this.T_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(488, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Attach";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(567, 377);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.T);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Message);
            this.Controls.Add(this.Sub);
            this.Controls.Add(this.TO);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Subject);
            this.Controls.Add(this.Too);
            this.Controls.Add(this.button1);
            this.Name = "Form2";
            this.Text = "email";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label Too;
        private System.Windows.Forms.Label Subject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TO;
        private System.Windows.Forms.TextBox Sub;
        private System.Windows.Forms.TextBox Message;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label T;
        private System.Windows.Forms.Label label4;
    }
}