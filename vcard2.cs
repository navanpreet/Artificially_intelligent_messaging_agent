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
    public partial class vcard2 : UserControl
    {
        public vcard2()
        {
            InitializeComponent();
        }

        private void jidlab_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            chatload.loadchat(jidlab.Text);
        }
    }
}
