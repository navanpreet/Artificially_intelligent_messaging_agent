using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using agsXMPP;
using AIMLbot;
using System.Windows.Forms;

namespace XIMA_Beta
{
    public static class chatload
    {
        static public int chatwincount;//basecount
        static public chatform1[] chatwin = new chatform1[100];//chatwindow
        static public String[] chatwinbase = new String[100];//Home list
        static public Bot myBot = new Bot();//XIMA Bot

        static public bool uniai;

        //Chat Window loader
        public static void loadchat(String jidq)
        {
            try
            {
                chatwin[chatwincount] = new chatform1(jidq);
                chatwin[chatwincount].Text = jidq;

                chatwin[chatwincount].Show();
                if (uniai == true)
                {
                    chatwin[chatwincount].Visible = false;
                }
                chatwinbase[chatwincount] = jidq;
                chatwincount++;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        public static void trayevent(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                for (int i = 0; i < 100; i++)
                {

                    if (chatwinbase[i] == e.ClickedItem.Text)
                    {

                        chatwin[i].Visible = true;
                    }

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }
}
