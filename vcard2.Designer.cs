namespace XIMA_Beta
{
    partial class vcard2
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
            this.SuspendLayout();
            // 
            // jidlab
            // 
            this.jidlab.AutoSize = true;
            this.jidlab.Location = new System.Drawing.Point(0, 0);
            this.jidlab.Name = "jidlab";
            this.jidlab.Size = new System.Drawing.Size(35, 13);
            this.jidlab.TabIndex = 2;
            this.jidlab.Text = "label1";
            this.jidlab.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.jidlab_MouseDoubleClick);
            // 
            // vcard2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.jidlab);
            this.Name = "vcard2";
            this.Size = new System.Drawing.Size(239, 14);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label jidlab;
    }
}
