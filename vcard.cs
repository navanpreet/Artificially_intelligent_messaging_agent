using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XIMA_Beta
{
    public partial class vcard : UserControl
    {
        public vcard()
        {
            InitializeComponent();
        }

        private void jidlab_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            chatload.loadchat(jidlab.Text);
        }
       

        private void statuslab_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            chatload.loadchat(jidlab.Text);
        }

        private void shimg_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            chatload.loadchat(jidlab.Text);
        }

    }
}
