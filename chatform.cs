#region Namespaces
using System;
using System.Media;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//XMPP Namespace
using agsXMPP;
//AIML Namespace
using AIMLbot;
#endregion

namespace XIMA_Beta
{
    public partial class chatform1 : Form
    {
        #region Declaration

        public String m_Jid;
        private String m_path;
        private String halfid;
        private bool drag = false;
        private Point start_point = new Point(0, 0);
        private bool draggable = true;
        private string exclude_list = "";
        private User myUser;
        private System.Windows.Forms.ToolStripItem pinx;
        #endregion

        #region Construction
        public chatform1(String jid)
        {
            try
            {
                int halfindex;
                InitializeComponent();
                m_Jid = jid;
                halfindex = m_Jid.IndexOf('@');
                halfid = m_Jid.Remove(halfindex);
                m_path = "userdata/" + m_Jid + ".xml";

                //mouse events
                this.MouseDown += new MouseEventHandler(Form_MouseDown);
                this.MouseUp += new MouseEventHandler(Form_MouseUp);
                this.MouseMove += new MouseEventHandler(Form_MouseMove);
                label1.Text = m_Jid;

                myUser = new User(m_Jid, chatload.myBot);

                if (File.Exists(m_path))
                {
                    myUser.Predicates.loadSettings(m_path);
                }


                //background image
                switch (XIMA.backno)
                {
                    case 2:
                        this.BackgroundImage = global::XIMA_Beta.Properties.Resources._2;
                        break;
                    case 3:
                        this.BackgroundImage = global::XIMA_Beta.Properties.Resources._3;
                        break;
                    case 4:
                        this.BackgroundImage = global::XIMA_Beta.Properties.Resources._4;
                        break;
                    case 5:
                        this.BackgroundImage = global::XIMA_Beta.Properties.Resources._5;
                        break;
                    case 6:
                        this.BackgroundImage = global::XIMA_Beta.Properties.Resources._6;
                        break;

                    default:
                        this.BackgroundImage = global::XIMA_Beta.Properties.Resources._1;
                        break;


                }

                pinx = XIMA.Tray_menu.Items.Add(m_Jid);

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #region playsound
        private void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(global::XIMA_Beta.Properties.Resources.message);
            simpleSound.Play();
        }
        #endregion

        #region User Message Sending
        private void sendtext_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {

                    agsXMPP.protocol.client.Message msgs = new agsXMPP.protocol.client.Message();
                    msgs.Type = agsXMPP.protocol.client.MessageType.chat;
                    msgs.To = new Jid(m_Jid);
                    msgs.Body = sendtext.Text;


                    XIMA.xmppCon.Send(msgs);
                    chattext.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                    chattext.AppendText("Me: ");
                    chattext.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                    chattext.AppendText(sendtext.Text);
                    chattext.AppendText("\r\n");
                    chattext.AppendText("\r\n");
                    sendtext.Text = "";
                    chattext.SelectionStart = chattext.Text.Length;
                    chattext.ScrollToCaret();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #region Incoming
        public void incmoing(agsXMPP.protocol.client.Message msg)
        {
            try
            {
                if (msg.From.Bare == m_Jid)
                {
                    chattext.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                    chattext.AppendText(halfid + ": ");
                    chattext.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                    chattext.AppendText(msg.Body);
                    chattext.AppendText("\r\n");
                    chattext.AppendText("\r\n");
                    chattext.SelectionStart = chattext.Text.Length;
                    chattext.ScrollToCaret();
                    playSimpleSound();

                    //Bot Interaction
                    if (checkBox1.Checked == true)
                    {
                        Request r = new Request(msg.Body, myUser, chatload.myBot);
                        Result res = chatload.myBot.Chat(r);
                        String respond;
                        respond = res.Output;

                        agsXMPP.protocol.client.Message msgs = new agsXMPP.protocol.client.Message();
                        msgs.Type = agsXMPP.protocol.client.MessageType.chat;
                        msgs.To = new Jid(m_Jid);
                        msgs.Body = respond;
                        XIMA.xmppCon.Send(msgs);

                        chattext.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                        chattext.AppendText("AIIMA: ");
                        chattext.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                        chattext.AppendText(respond);
                        chattext.AppendText("\r\n");
                        chattext.AppendText("\r\n");
                        chattext.SelectionStart = chattext.Text.Length;
                        chattext.ScrollToCaret();
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #region Type Focus, No-title-bar
        private void chattext_Enter(object sender, EventArgs e)
        {
            sendtext.Focus();
        }

        private void chatform_Enter(object sender, EventArgs e)
        {
            sendtext.Focus();
        }

        //removing title bar
        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams cp = base.CreateParams;
                cp.Style &= ~0x00C00000; // WS_CAPTION
                return cp;
            }

        }


        #endregion

        #region Overriden Functions for Drag

        protected override void OnControlAdded(ControlEventArgs e)
        {
            try
            {
                //
                //Add Mouse Event Handlers for each control added into the form,
                //if Draggable property of the form is set to true and the control
                //name is not in the ExcludeList.Exclude list is the comma separated
                //list of the Controls for which you do not require the mouse handler 
                //to be added. For Example a button.  
                //
                if (this.Draggable && (this.ExcludeList.IndexOf(e.Control.Name) == -1))
                {
                    e.Control.MouseDown += new MouseEventHandler(Form_MouseDown);
                    e.Control.MouseUp += new MouseEventHandler(Form_MouseUp);
                    e.Control.MouseMove += new MouseEventHandler(Form_MouseMove);
                }
                base.OnControlAdded(e);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        #endregion      
        #region Control box
        private void button2_Click(object sender, EventArgs e)
        {
            //minimize
            this.Visible = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                //remove from home list
                for (int t = 0; t < chatload.chatwincount; t++)
                {
                    if (chatload.chatwinbase[t] == m_Jid)
                    {
                        chatload.chatwinbase[t] = ""; ;
                    }
                }
                //close
                myUser.Predicates.DictionaryAsXML.Save(m_path);
                XIMA.Tray_menu.Items.Remove(pinx);
                playSimpleSound1();
                this.Close();

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion


        #region Drag Event Handlers

        void Form_MouseDown(object sender, MouseEventArgs e)
        {
            //
            //On Mouse Down set the flag drag=true and 
            //Store the clicked point to the start_point variable
            //
            this.drag = true;
            this.start_point = new Point(e.X, e.Y);
        }

        void Form_MouseUp(object sender, MouseEventArgs e)
        {
            //
            //Set the drag flag = false;
            //
            this.drag = false;
        }

        void Form_MouseMove(object sender, MouseEventArgs e)
        {
            //
            //If drag = true, drag the form
            //
            if (this.drag)
            {
                Point p1 = new Point(e.X, e.Y);
                Point p2 = this.PointToScreen(p1);
                Point p3 = new Point(p2.X - this.start_point.X,
                                     p2.Y - this.start_point.Y);
                this.Location = p3;
            }
        }

        #endregion

        #region Drag Properties

        public string ExcludeList
        {
            set
            {
                this.exclude_list = value;
            }
            get
            {
                return this.exclude_list.Trim();
            }
        }

        public bool Draggable
        {
            set
            {
                this.draggable = value;
            }
            get
            {
                return this.draggable;
            }
        }

        #endregion


        #region playsound1
        private void playSimpleSound1()
        {
            SoundPlayer simpleSound = new SoundPlayer(global::XIMA_Beta.Properties.Resources.online);
            simpleSound.Play();
        }
        #endregion

        #region XIMA Loader
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            try
            {
                String respond;
                if (checkBox1.Checked == true)
                {
                    sendtext.Enabled = false;
                    respond = "AIIMA ON";

                    agsXMPP.protocol.client.Message msgs = new agsXMPP.protocol.client.Message();
                    msgs.Type = agsXMPP.protocol.client.MessageType.chat;
                    msgs.To = new Jid(m_Jid);
                    msgs.Body = respond;
                    XIMA.xmppCon.Send(msgs);

                    chattext.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                    chattext.AppendText("AIIMA: ");
                    chattext.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                    chattext.AppendText(respond);
                    chattext.AppendText("\r\n");
                    chattext.AppendText("\r\n");
                    chattext.SelectionStart = chattext.Text.Length;
                    chattext.ScrollToCaret();
                }
                else
                {
                    sendtext.Enabled = true;
                    respond = "AIIMA Signing Off...";

                    agsXMPP.protocol.client.Message msgs = new agsXMPP.protocol.client.Message();
                    msgs.Type = agsXMPP.protocol.client.MessageType.chat;
                    msgs.To = new Jid(m_Jid);
                    msgs.Body = respond;
                    XIMA.xmppCon.Send(msgs);

                    chattext.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
                    chattext.AppendText("AIIMA: ");
                    chattext.SelectionFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                    chattext.AppendText(respond);
                    chattext.AppendText("\r\n");
                    chattext.AppendText("\r\n");
                    chattext.SelectionStart = chattext.Text.Length;
                    chattext.ScrollToCaret();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #region Load
        private void chatform_Load(object sender, EventArgs e)
        {
            if (chatload.uniai == true)
            {
                checkBox1.Checked = true;

            }

        }

        private void chatform_VisibleChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region clear chat
        private void label2_Click_1(object sender, EventArgs e)
        {
            chattext.Text = null;
        }
        #endregion

       
    }
}
