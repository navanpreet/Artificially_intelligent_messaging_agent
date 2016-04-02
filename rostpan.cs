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
    public partial class rostpan : UserControl
    {
        #region Declaration
        public vcard[] name;
        public vcard2[] name2;
        public int z = 0;
        #endregion

        #region Construction
        public rostpan()
        {
            InitializeComponent();
            name = new vcard[100];
            name2 = new vcard2[500];
        }
        #endregion

        #region Presence Array Creation
        public void additems(String status, String jid, String show)
        {
            try
            {
                name[z] = new vcard();
                this.Controls.Add(name[z]);
                name[z].Location = new Point(name[z].Location.X, name[z].Location.Y + (27 * z));
                name[z].Size = new Size(250, 27);
                name[z].jidlab.Text = jid;
                name[z].Tag = jid;
                name[z].statuslab.Text = status;
                if (show == "away")
                {
                    name[z].shimg.Image = global::XIMA_Beta.Properties.Resources.aw;
                }
                else
                {
                    if (show == "dnd")
                    {
                        name[z].shimg.Image = global::XIMA_Beta.Properties.Resources.dnd;
                    }
                    else
                    {
                        if (show == "off")
                        {
                            name[z].shimg.Image = global::XIMA_Beta.Properties.Resources.off;
                        }
                        else
                        {
                            name[z].shimg.Image = global::XIMA_Beta.Properties.Resources.chat;
                        }
                    }
                }
                name[z].MouseDoubleClick += new MouseEventHandler(x_MouseDoubleClick);
                z++;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #region Roster Array Creation
        public void addrost(String jid)
        {
            try
            {
                name2[z] = new vcard2();
                this.Controls.Add(name2[z]);
                name2[z].Location = new Point(name2[z].Location.X, name2[z].Location.Y + (14 * z));
                name2[z].jidlab.Text = jid;
                name2[z].MouseDoubleClick += new MouseEventHandler(x_MouseDoubleClick2);
                z++;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #region Presence Update
        public void itemup(String jid, String show, String status, int i)
        {
            try
            {
                if (jid == name[i].jidlab.Text)
                {
                    name[i].statuslab.Text = status;
                    if (show == "away")
                    {
                        name[i].shimg.Image = global::XIMA_Beta.Properties.Resources.aw;
                    }
                    else
                    {
                        if (show == "dnd")
                        {
                            name[i].shimg.Image = global::XIMA_Beta.Properties.Resources.dnd;
                        }
                        else
                        {
                            if (show == "off")
                            {
                                name[i].shimg.Image = global::XIMA_Beta.Properties.Resources.off;
                            }
                            else
                            {
                                name[i].shimg.Image = global::XIMA_Beta.Properties.Resources.chat;
                            }
                        }
                    }


                }

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #region Mouse Event Handlers
        public void x_MouseDoubleClick(object n, EventArgs e)
        {

            chatload.loadchat(((vcard)n).jidlab.Text);

        }
        public void x_MouseDoubleClick2(object n, EventArgs e)
        {

            chatload.loadchat(((vcard2)n).jidlab.Text);

        }
        #endregion

        #region Flush
        public void flush()
        {
            this.Controls.Clear();
            z = 0;
        }
        #endregion
    }
}
