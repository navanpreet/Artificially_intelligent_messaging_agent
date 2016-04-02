

#region Namespaces

//namespaces related to the CLR winfroms
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Media;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//Namespace for implementing Registry
using Microsoft.Win32;

//XMPP namespaces
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.Xml.Dom;

//Encryption Namespace
using System.Security.Cryptography;
using System.Reflection;


#endregion

#region XIMA Namespace

namespace XIMA_Beta
{
    public partial class XIMA : Form
    {
        #region Declarations
        //Initialization of variables for Form Drag
        private bool drag = false;
        private Point start_point = new Point(0, 0);
        private bool draggable = true;
        private string exclude_list = "";
        static public int backno;
        private RegistryKey key;
        private int rem;
        private String[,] pr = new String[100, 4];
        private String[] rs = new String[500];
        private int rsindex = 0;
        private int prindex = 0;
        ListBox drop1 = new ListBox();
        ListBox drop2 = new ListBox();
        public static System.Windows.Forms.ContextMenuStrip Tray_menu = new ContextMenuStrip();
        private System.Windows.Forms.ToolStripMenuItem show_xima = new System.Windows.Forms.ToolStripMenuItem();
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

        static public XmppClientConnection xmppCon = new XmppClientConnection();
        // for ENCODING------DECODING
        UnicodeEncoding bytConvertor = new UnicodeEncoding();
        RSACryptoServiceProvider RSAServiceProvider = new RSACryptoServiceProvider();

       
        


        #endregion

        #region Construction

        public XIMA()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(Form_MouseDown);
            this.MouseUp += new MouseEventHandler(Form_MouseUp);
            this.MouseMove += new MouseEventHandler(Form_MouseMove);
            drop1.MouseClick += new MouseEventHandler(drop1_click);
            drop2.MouseClick += new MouseEventHandler(drop2_click);
            drop2.Visible = false;
            drop1.Visible = false;
            this.HelpRequested += new HelpEventHandler(call_help);

            // show_xima
            // 
            this.show_xima.Name = "show_xima";
            this.show_xima.Size = new System.Drawing.Size(152, 22);
            this.show_xima.Text = "Show XIMA";
            this.show_xima.Visible = false;
            this.show_xima.Click += new System.EventHandler(this.show_xima_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);

            XIMA.Tray_menu.Items.Add(show_xima);
            XIMA.Tray_menu.Items.Add(exitToolStripMenuItem);

            XIMA.Tray_menu.Name = "Tray_menu";
            XIMA.Tray_menu.Size = new System.Drawing.Size(153, 70);
            XIMA.Tray_menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(chatload.trayevent);

            this.Vconnect_Tray.ContextMenuStrip = XIMA.Tray_menu;

            Init();
        }
        public void Init()
        {
            xmppCon.OnLogin += new ObjectHandler(xmppCon_OnLogin);
            xmppCon.OnRosterStart += new ObjectHandler(xmppCon_OnRosterStart);
            xmppCon.OnRosterEnd += new ObjectHandler(xmppCon_OnRosterEnd);
            xmppCon.OnRosterItem += new XmppClientConnection.RosterHandler(xmppCon_OnRosterItem);
            xmppCon.OnPresence += new agsXMPP.protocol.client.PresenceHandler(xmppCon_OnPresence);
            xmppCon.OnAuthError += new XmppElementHandler(xmppCon_OnAuthError);
            xmppCon.OnError += new ErrorHandler(xmppCon_OnError);
            xmppCon.OnClose += new ObjectHandler(xmppCon_OnClose);
            xmppCon.OnMessage += new agsXMPP.protocol.client.MessageHandler(xmppCon_OnMessage);


        }

        #endregion

        #region XMPP event handlers

        #region login

        #region login event
        void xmppCon_OnLogin(object sender)
        {
            try
            {
                if (InvokeRequired)
                {
                    // Windows Forms are not Thread Safe, we need to invoke this :(
                    // We're not in the UI thread, so we need to call BeginInvoke				
                    BeginInvoke(new ObjectHandler(xmppCon_OnLogin), new object[] { sender });
                    return;
                }
                Login.Visible = false;
                chatarea.Visible = true;
                button4.Visible = true;
                button4.Text = "Logout";
                jidlab.Text = usernamebox.Text;


                showbar.SelectedItem = "Available";
                statusbar.Text = "Status: Using Vconnect";
                xmppCon.Show = ShowType.chat;
                xmppCon.Status = statusbar.Text;
                xmppCon.SendMyPresence();
                loginlabel.Visible = false;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }
        #endregion

        #region logout event
        void xmppCon_OnClose(object sender)
        {
            try
            {
                if (InvokeRequired)
                {

                    BeginInvoke(new ObjectHandler(xmppCon_OnClose), new object[] { sender });
                    return;
                }
                Login.Visible = true;
                Login.Enabled = true;
                chatarea.Visible = false;
                button4.Visible = false;
                chatarea.Enabled = true;
                button4.Text = "Logout";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #region login error
        void xmppCon_OnAuthError(object sender, agsXMPP.Xml.Dom.Element e)
        {
            try
            {
                if (InvokeRequired)
                {

                    BeginInvoke(new XmppElementHandler(xmppCon_OnAuthError), new object[] { sender, e });
                    return;
                }
                Login.Enabled = true;
                loginlabel.Visible = true;
                loginlabel.Text = "Credential Error - Please try again";
                button4.Visible = false;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #endregion

        #region roster

        #region roster start/end
        void xmppCon_OnRosterStart(object sender)
        {
            try
            {
                if (InvokeRequired)
                {

                    BeginInvoke(new ObjectHandler(xmppCon_OnRosterStart), new object[] { sender });
                    return;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        void xmppCon_OnRosterEnd(object sender)
        {
            try
            {
                if (InvokeRequired)
                {

                    BeginInvoke(new ObjectHandler(xmppCon_OnRosterEnd), new object[] { sender });
                    return;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #region roster item
        void xmppCon_OnRosterItem(object sender, agsXMPP.protocol.iq.roster.RosterItem item)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new XmppClientConnection.RosterHandler(xmppCon_OnRosterItem), new object[] { sender, item });
                    return;
                }


                rostpan3.addrost(item.Jid.Bare);
                rs[rsindex] = item.Jid.Bare;
                rsindex++;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #endregion

        #region presence
        void xmppCon_OnPresence(object sender, agsXMPP.protocol.client.Presence pres)
        {
            try
            {
                bool up = false;
                int i = 0;
                if (InvokeRequired)
                {

                    BeginInvoke(new agsXMPP.protocol.client.PresenceHandler(xmppCon_OnPresence), new object[] { sender, pres });
                    return;
                }

                //listBox1.Items.Add(String.Format("Received Presence from:{0} show:{1} status:{2} type:{3} nick:{4}", pres.From.Bare.ToString(), pres.Show.ToString(), pres.Status, pres.Type.ToString(), pres.Nickname));

                if (pres.Type == PresenceType.subscribe)
                {
                    reqlist.Items.Add(pres.From.Bare);
                }
                else
                {
                    if (pres.Type.ToString() == "available")
                    {
                        try
                        {
                            while (i < prindex)
                            {
                                if (pr[i, 1] == pres.From.Bare)
                                {
                                    rostpan1.itemup(pres.From.Bare, pres.Show.ToString(), pres.Status, i);
                                    pr[i, 1] = pres.From.Bare;
                                    pr[i, 2] = pres.Show.ToString();
                                    pr[i, 3] = pres.Status;
                                    up = true;
                                    break;
                                }
                                i++;
                            }
                        }
                        catch { }
                        if (up == false)
                        {
                            rostpan1.additems(pres.Status, pres.From.Bare, pres.Show.ToString());
                            pr[prindex, 1] = pres.From.Bare;
                            pr[prindex, 2] = pres.Show.ToString();
                            pr[prindex, 3] = pres.Status;
                            prindex++;
                        }
                    }
                    else
                    {
                        try
                        {
                            for (i = 0; i < prindex; i++)
                            {
                                if (pr[i, 1] == pres.From.Bare)
                                {
                                    prindex--;
                                    pr[i, 1] = pr[prindex, 1];
                                    pr[i, 2] = pr[prindex, 2];
                                    pr[i, 3] = pr[prindex, 3];

                                    rostpan1.flush();
                                    for (int j = 0; j < prindex; j++)
                                    {
                                        rostpan1.additems(pr[j, 3], pr[j, 1], pr[j, 2]);

                                    }
                                    break;
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #region error
        void xmppCon_OnError(object sender, Exception ex)
        {
            try
            {
                if (InvokeRequired)
                {
                    // Windows Forms are not Thread Safe, we need to invoke this :(
                    // We're not in the UI thread, so we need to call BeginInvoke				
                    BeginInvoke(new ErrorHandler(xmppCon_OnError), new object[] { sender, ex });
                    return;
                }
                //status.Text = "OnError";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #region message
        void xmppCon_OnMessage(object sender, agsXMPP.protocol.client.Message msg)
        {
            try
            {
                // ignore empty messages (events)
                if (msg.Body == null)
                    return;

                if (InvokeRequired)
                {
                    // Windows Forms are not Thread Safe, we need to invoke this :(
                    // We're not in the UI thread, so we need to call BeginInvoke				
                    BeginInvoke(new agsXMPP.protocol.client.MessageHandler(xmppCon_OnMessage), new object[] { sender, msg });
                    return;
                }
                bool naya = true;
                for (int t = 0; t < chatload.chatwincount; t++)
                {
                    if (chatload.chatwinbase[t] == msg.From.Bare)
                    {
                        chatload.chatwin[t].incmoing(msg);
                        naya = false;
                        break;
                    }
                }
                if (naya == true)
                {
                    chatload.loadchat(msg.From.Bare);
                    chatload.chatwin[(chatload.chatwincount) - 1].incmoing(msg);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

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

        #region Title Bar removing code
        //Code to remove default title bar
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

        #region control buttons code

        private void buttonx_Click(object sender, EventArgs e)
        {
            try
            {
                key = Registry.CurrentUser.OpenSubKey("Software\\Vconnect\\WindowPos", true);
                Point finalloc = new Point();
                finalloc = this.Location;
                key.SetValue("X", finalloc.X);
                key.SetValue("Y", finalloc.Y);
                key.Close();
                key = Registry.CurrentUser.OpenSubKey("Software\\Vconnect\\tindex", true);
                int backsave = backno - 1;
                key.SetValue("X", backsave);
                key.Close();
                this.Close();

                if (autologincheck.Checked == true)
                {
                    key = Registry.CurrentUser.OpenSubKey("Software\\Vconnect\\autologin", true);

                    key.SetValue("set", 1);
                    key.Close();

                }
                else
                {
                    key = Registry.CurrentUser.OpenSubKey("Software\\Vconnect\\autologin", true);

                    key.SetValue("set", 0);
                    key.Close();
                }
                if (invisiblecheck.Checked == true)
                {
                    key = Registry.CurrentUser.OpenSubKey("Software\\Vconnect\\invisible", true);

                    key.SetValue("set", 1);
                    key.Close();

                }
                else
                {
                    key = Registry.CurrentUser.OpenSubKey("Software\\Vconnect\\invisible", true);

                    key.SetValue("set", 0);
                    key.Close();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            show_xima.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                switch (backno)
                {
                    case 1:
                        this.BackgroundImage = global::XIMA_Beta.Properties.Resources._2;
                        backno = 2;
                        break;
                    case 2:
                        this.BackgroundImage = global::XIMA_Beta.Properties.Resources._3;
                        backno = 3;
                        break;
                    case 3:
                        this.BackgroundImage = global::XIMA_Beta.Properties.Resources._4;
                        backno = 4;
                        break;
                    case 4:
                        this.BackgroundImage = global::XIMA_Beta.Properties.Resources._5;
                        backno = 5;
                        break;
                    case 5:
                        this.BackgroundImage = global::XIMA_Beta.Properties.Resources._6;
                        backno = 6;
                        break;
                    default:
                        this.BackgroundImage = global::XIMA_Beta.Properties.Resources._1;
                        backno = 1;
                        break;             

                }
                for (int q = 0; q < chatload.chatwincount; q++)
                {
                    chatload.chatwin[q].BackgroundImage = this.BackgroundImage;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }

        }


        private void button7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("help.html");
        }
        public void call_help(object sender, HelpEventArgs e)
        {
            System.Diagnostics.Process.Start("help.html");
        }
        private void textBox2_Enter(object sender, EventArgs e)
        {
            label5.Focus();
        }
        #endregion

        #region resize event handler
        private void XIMA_Resize(object sender, EventArgs e)
        {
            Point loginloc = new Point();
            loginloc.X = (this.Size.Width - Login.Size.Width) / 2;
            loginloc.Y = Login.Location.Y;
            Login.Location = loginloc;

            chatarea.Width = this.Width - 11;
            chatarea.Height = this.Height - 88;
        }

        private void chatarea_Resize(object sender, EventArgs e)
        {
            statusbar.Width = chatarea.Width - 31;
            tabControl1.Width = chatarea.Width - 10;
            tabControl1.Height = chatarea.Height - 88;
            searchbar.Width = chatarea.Width - 52;
        }


        #endregion

        #region Form Load Event
        private void XIMA_Load(object sender, EventArgs e)
        {

            try
            {
                

                chatload.myBot.loadSettings();
                chatload.myBot.isAcceptingUserInput = false;
                chatload.myBot.loadAIMLFromFiles();
                chatload.myBot.isAcceptingUserInput = true;

                chatload.uniai = false;

                key = Registry.CurrentUser.OpenSubKey("Software\\Vconnect\\WindowPos");

                // If the return value is null, the key doesn't exist
                if (key == null)
                {
                    // The key doesn't exist; create it / open it
                    key = Registry.CurrentUser.CreateSubKey("Software\\Vconnect\\WindowPos");
                }

                // Attempt to retrieve the value X; if null is returned, the value
                // doesn't exist in the registry.
                if (key.GetValue("X") != null)
                {
                    // The value exists; move the form to the coordinates stored in the
                    // registry.
                    this.Location = new Point((int)key.GetValue("X"), (int)key.GetValue("Y"));
                }
                key.Close();

                key = Registry.CurrentUser.OpenSubKey("Software\\Vconnect\\tindex");

                // If the return value is null, the key doesn't exist
                if (key == null)
                {
                    // The key doesn't exist; create it / open it
                    key = Registry.CurrentUser.CreateSubKey("Software\\Vconnect\\tindex");
                }

                // Attempt to retrieve the value X; if null is returned, the value
                // doesn't exist in the registry.
                if (key.GetValue("X") != null)
                {
                    // The value exists; move the form to the coordinates stored in the
                    // registry.
                    backno = (int)key.GetValue("X");
                    switch (backno)
                    {
                        case 1:
                            this.BackgroundImage = global::XIMA_Beta.Properties.Resources._2;
                            backno = 2;
                            break;
                        case 2:
                            this.BackgroundImage = global::XIMA_Beta.Properties.Resources._3;
                            backno = 3;
                            break;
                        case 3:
                            this.BackgroundImage = global::XIMA_Beta.Properties.Resources._4;
                            backno = 4;
                            break;
                        case 4:
                            this.BackgroundImage = global::XIMA_Beta.Properties.Resources._5;
                            backno = 5;
                            break;
                        case 5:
                            this.BackgroundImage = global::XIMA_Beta.Properties.Resources._6;
                            backno = 6;
                            break;
                        default:
                            this.BackgroundImage = global::XIMA_Beta.Properties.Resources._1;
                            backno = 1;
                            break;

                    }

                }
                key.Close();

                ///**

                key = Registry.CurrentUser.OpenSubKey("Software\\Vconnect\\rem");

                // If the return value is null, the key doesn't exist
                if (key == null)
                {
                    // The key doesn't exist; create it / open it
                    key = Registry.CurrentUser.CreateSubKey("Software\\Vconnect\\rem");
                }

                // Attempt to retrieve the value X; if null is returned, the value
                // doesn't exist in the registry.
                if (key.GetValue("set") != null)
                {
                    // The value exists; move the form to the coordinates stored in the
                    // registry.
                    rem = (int)key.GetValue("set");
                    if (rem == 1)
                    {

                        byte[] tempuser1 = (byte[])key.GetValue("username");
                        string str = System.Text.Encoding.Unicode.GetString(tempuser1);
                        string tempuser = bytConvertor.GetString(tempuser1);
                        byte[] tempass1 = (byte[])key.GetValue("pass");
                        string strpass = System.Text.Encoding.Unicode.GetString(tempass1);
                        string temppass = bytConvertor.GetString(tempass1);


                        if (key.GetValue("username") != null)
                        {
                            usernamebox.Text = str;
                        }
                        if (key.GetValue("pass") != null)
                        {
                            passbox.Text = strpass;
                        }
                        remembercheck.Checked = true;
                    }

                }
                key.Close();


                key = Registry.CurrentUser.OpenSubKey("Software\\Vconnect\\invisible");

                // If the return value is null, the key doesn't exist
                if (key == null)
                {
                    // The key doesn't exist; create it / open it
                    key = Registry.CurrentUser.CreateSubKey("Software\\Vconnect\\invisible");
                }

                // Attempt to retrieve the value X; if null is returned, the value
                // doesn't exist in the registry.
                if (key.GetValue("set") != null)
                {
                    // The value exists; move the form to the coordinates stored in the
                    // registry.

                    if ((int)key.GetValue("set") == 1)
                    {
                        invisiblecheck.Checked = true;
                    }

                }
                key.Close();

                key = Registry.CurrentUser.OpenSubKey("Software\\Vconnect\\autologin");

                // If the return value is null, the key doesn't exist
                if (key == null)
                {
                    // The key doesn't exist; create it / open it
                    key = Registry.CurrentUser.CreateSubKey("Software\\Vconnect\\autologin");
                }

                // Attempt to retrieve the value X; if null is returned, the value
                // doesn't exist in the registry.
                if (key.GetValue("set") != null)
                {
                    // The value exists; move the form to the coordinates stored in the
                    // registry.

                    if ((int)key.GetValue("set") == 1)
                    {
                        autologincheck.Checked = true;
                        logincall();
                    }

                }
                key.Close();

                drop1.Visible = false;
                drop2.Visible = false;
                //**/
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        #endregion

        #region login box links code

        private void newuserlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.com/accounts/NewAccount?continue=http%3A%2F%2Fwww.google.co.in%2F&hl=en");
        }

        private void forgotpasslink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.google.com/support/accounts/bin/answer.py?answer=48598&hl=en&ctx=ch_Login&fpUrl=https%3A%2F%2Fwww.google.com%2Faccounts%2FForgotPasswd%3FfpOnly%3D1%26continue%3Dhttp%253A%252F%252Fwww.google.co.in%252F%26hl%3Den");
        }

        private void googlelink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.com/accounts/Login?hl=en&continue=http://www.google.co.in/");
        }

        #endregion

        #region login code

        //login button
        private void loginbut_Click(object sender, EventArgs e)
        {

            check_loginid(); 
        }

        #region Encrypt function
        static private byte[] Encrypt(byte[] DataToEncrypt, RSAParameters keyInfo)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSA.ImportParameters(keyInfo);
            return RSA.Encrypt(DataToEncrypt, false);
        }
        #endregion

        //#region Decrypt function
        //static private byte[] Decrypt(byte[] DataToDecrypt, RSAParameters keyInfo)
        //{
        //    RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        //    RSA.ImportParameters(keyInfo);
        //    return RSA.Decrypt(DataToDecrypt, false);

        //}
        //#endregion



        //login function
        private void logincall()
        {
            try
            {

                byte[] plainData = bytConvertor.GetBytes(usernamebox.Text);
                byte[] passplaindata = bytConvertor.GetBytes(passbox.Text);
                byte[] enData = Encrypt(plainData, RSAServiceProvider.ExportParameters(false));//encryption for username
                byte[] enData1 = Encrypt(passplaindata, RSAServiceProvider.ExportParameters(false));// encryption for password
                

                if (remembercheck.Checked == true)
                {
                    rem = 1;
                    key = Registry.CurrentUser.OpenSubKey("Software\\Vconnect\\rem", true);
                    key.SetValue("username", plainData);
                    key.SetValue("pass",passplaindata);
                    key.SetValue("set", rem);
                    key.Close();
                }
                else
                {
                    rem = 0;
                    key = Registry.CurrentUser.OpenSubKey("Software\\Vconnect\\rem", true);
                    key.SetValue("username", "");
                    key.SetValue("pass", "");
                    key.SetValue("set", rem);
                    key.Close();
                }

                Login.Enabled = false;
                Jid jidUser = new Jid(usernamebox.Text);
                xmppCon.Username = jidUser.User;
                xmppCon.Server = jidUser.Server;
                xmppCon.Password = passbox.Text;
                xmppCon.AutoResolveConnectServer = true;
                xmppCon.Open();
                button4.Visible = true;
                button4.Text = "Abort";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        #endregion

        #region Logout Code
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                chatarea.Enabled = false;
                button4.Text = ".....";
                for (int q = 0; q < chatload.chatwincount; q++)
                {
                    try
                    {
                        chatload.chatwin[q].Close();
                    }
                    catch
                    {

                    }
                }
                rostpan1.flush();
                rostpan3.flush();
                xmppCon.Close();

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #region Show Code
        private void showbar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (showbar.SelectedIndex == 0)
                {
                    showimg.Image = chat.Image;
                    xmppCon.Show = ShowType.chat;

                }
                else
                {
                    if (showbar.SelectedIndex == 1)
                    {
                        showimg.Image = dnd.Image;
                        xmppCon.Show = ShowType.dnd;

                    }
                    else
                    {
                        showimg.Image = away.Image;
                        xmppCon.Show = ShowType.away;

                    }
                }
                xmppCon.SendMyPresence();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #region Status Code
        private void statusbar_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    xmppCon.Status = statusbar.Text;
                    xmppCon.SendMyPresence();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                xmppCon.Status = statusbar.Text;
                xmppCon.SendMyPresence();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #region friendlist add/remove code
        private void button3_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectTab(addfrnd);
        }

        private void frndadd_Click(object sender, EventArgs e)
        {
            try
            {
                Jid strfrnd = new Jid(addrem.Text);
                xmppCon.RosterManager.AddRosterItem(strfrnd);
                xmppCon.PresenceManager.Subscribe(strfrnd);
                addrem.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void delfrnd_Click(object sender, EventArgs e)
        {
            try
            {
                Jid strfrnd = new Jid(addrem.Text);
                xmppCon.RosterManager.RemoveRosterItem(strfrnd);
                xmppCon.PresenceManager.Unsubscribe(strfrnd);
                addrem.Text = "";
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void allow_Click(object sender, EventArgs e)
        {
            try
            {
                int reqno = reqlist.SelectedIndex;
                Jid reqit = new Jid(reqlist.SelectedItem.ToString());
                xmppCon.PresenceManager.ApproveSubscriptionRequest(reqit);
                reqlist.Items.RemoveAt(reqno);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void reject_Click(object sender, EventArgs e)
        {
            try
            {
                int reqno = reqlist.SelectedIndex;
                Jid reqit = new Jid(reqlist.SelectedItem.ToString());
                xmppCon.PresenceManager.RefuseSubscriptionRequest(reqit);
                reqlist.Items.RemoveAt(reqno);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #region search online
        private void searchbar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                drop1.Visible = true;
                searchbar.Parent.Controls.Add(drop1);
                drop1.BringToFront();
                drop1.Location = new Point(searchbar.Location.X, searchbar.Location.Y + searchbar.Size.Height);
                drop1.Items.Clear();
                for (int j = 0; j < prindex; j++)
                {
                    if (pr[j, 1].StartsWith(searchbar.Text))
                    {
                        drop1.Items.Add(pr[j, 1]);
                    }
                }
                drop1.Size = new Size(searchbar.Size.Width, drop1.Items.Count * 20);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void drop1_click(Object sender, MouseEventArgs e)
        {

            try
            {
                searchbar.Text = ((ListBox)sender).SelectedItem.ToString();
                searchbut.Enabled = true;
                drop1.Visible = false;
                searchbar.Focus();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void searchbar_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (searchbut.Enabled == true)
                    {
                        chatload.loadchat(searchbar.Text);
                        searchbut.Enabled = false;
                        searchbar.Text = "";
                        drop1.Visible = false;
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }


        private void searchbar_Leave(object sender, EventArgs e)
        {
            //
        }

        private void searchbut_Click(object sender, EventArgs e)
        {
            try
            {
                chatload.loadchat(searchbar.Text);
                searchbut.Enabled = false;
                searchbar.Text = "";
                drop1.Visible = false;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        #region search roster

        private void searchbar2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                drop2.Visible = true;
                searchbar2.Parent.Controls.Add(drop2);
                drop2.BringToFront();
                drop2.Location = new Point(searchbar2.Location.X, searchbar2.Location.Y + searchbar2.Size.Height);
                drop2.Items.Clear();
                for (int j = 0; j < rsindex; j++)
                {
                    if (rs[j].StartsWith(searchbar2.Text))
                    {
                        drop2.Items.Add(rs[j]);
                    }
                }
                drop2.Size = new Size(searchbar2.Size.Width, drop2.Items.Count * 20);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void drop2_click(Object sender, MouseEventArgs e)
        {
            try
            {
                searchbar2.Text = ((ListBox)sender).SelectedItem.ToString();
                searchbut2.Enabled = true;
                drop2.Visible = false;
                searchbar2.Focus();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private void searchbar2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (searchbut2.Enabled == true)
                    {
                        chatload.loadchat(searchbar2.Text);
                        searchbut2.Enabled = false;
                        searchbar2.Text = "";
                        drop2.Visible = false;
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void searchbut2_Click(object sender, EventArgs e)
        {
            try
            {
                chatload.loadchat(searchbar2.Text);
                searchbut2.Enabled = false;
                searchbar2.Text = "";
                drop2.Visible = false;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

       /* #region Universal AI
        private void ximaon_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ximaon.Checked == true)
                {
                    this.Visible = false;
                    show_xima.Visible = true;
                }
                chatload.uniai = ximaon.Checked;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion */

        #region Tray
        private void show_xima_Click(object sender, EventArgs e)
        {
            try
            {
                this.Visible = true;
                show_xima.Visible = false;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonx_Click(null, null);
        }

        private void XIMA_Tray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            show_xima.Visible = false;
        }

        private void XIMA_Tray_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(Vconnect_Tray, null);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        #endregion

        # region checking id
        void check_loginid()
        {
            string va = "";
            char[] chk = new char[usernamebox.Text.Length];
            chk = usernamebox.Text.ToCharArray();
            int len = chk.Length;

            for (int i = 0; i <= len - 1; i++)
            {
                if (chk[i] == '@')
                {
                    for (int j = i + 1; j <= len - 1; j++)
                    {
                        va = va + chk[j];

                    }

                }
            }

            if (va == "gmail.com")
            {
                logincall();
            }
            else
            {
                MessageBox.Show("Invalid id- need Gmail id");
                usernamebox.Focus();
            }



        }
        #endregion

        #region email_click
        private void button8_Click(object sender, EventArgs e)
        {
            Form2 obj1 = new Form2();
            obj1.Show();
        }
        #endregion

        private void creditlab2_Click(object sender, EventArgs e)
        {

        }

        private void chat_Click(object sender, EventArgs e)
        {

        }
    }
}

#endregion