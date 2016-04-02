namespace XIMA_Beta
{
    partial class vcard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.jidlab = new System.Windows.Forms.Label();
            this.statuslab = new System.Windows.Forms.Label();
            this.shimg = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.shimg)).BeginInit();
            this.SuspendLayout();
            // 
            // jidlab
            // 
            this.jidlab.AutoSize = true;
            this.jidlab.Location = new System.Drawing.Point(20, 0);
            this.jidlab.Name = "jidlab";
            this.jidlab.Size = new System.Drawing.Size(35, 13);
            this.jidlab.TabIndex = 1;
            this.jidlab.Text = "label1";
            this.jidlab.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.jidlab_MouseDoubleClick);
            // 
            // statuslab
            // 
            this.statuslab.AutoSize = true;
            this.statuslab.Location = new System.Drawing.Point(0, 14);
            this.statuslab.Name = "statuslab";
            this.statuslab.Size = new System.Drawing.Size(35, 13);
            this.statuslab.TabIndex = 2;
            this.statuslab.Text = "label1";
            this.statuslab.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.statuslab_MouseDoubleClick);
            // 
            // shimg
            // 
            this.shimg.Location = new System.Drawing.Point(0, 0);
            this.shimg.Name = "shimg";
            this.shimg.Size = new System.Drawing.Size(13, 13);
            this.shimg.TabIndex = 0;
            this.shimg.TabStop = false;
            this.shimg.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.shimg_MouseDoubleClick);
            // 
            // vcard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.statuslab);
            this.Controls.Add(this.jidlab);
            this.Controls.Add(this.shimg);
            this.Name = "vcard";
            this.Size = new System.Drawing.Size(250, 27);
            ((System.ComponentModel.ISupportInitialize)(this.shimg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public  System.Windows.Forms.PictureBox shimg;
        public  System.Windows.Forms.Label jidlab;
        public  System.Windows.Forms.Label statuslab;
    }
}
