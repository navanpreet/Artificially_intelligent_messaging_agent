#region Namespaces
using System;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
#endregion

namespace XIMA_Beta
{
    public partial class Form2 : Form
    {

        #region Declarations
        MailMessage mail = new MailMessage();
        private RegistryKey key;
        // for ENCODING------DECODING
        UnicodeEncoding bytConvertor = new UnicodeEncoding();
        RSACryptoServiceProvider RSAServiceProvider = new RSACryptoServiceProvider();
        String fileName;

        
        #endregion

        public Form2()
        {
            InitializeComponent();
        }

        #region construction
        public void change()
        {
            //background image
            
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
                
        private void Form2_Load(object sender, EventArgs e)
        {
           
        }
       
        #region email button

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               
                    mail.From = new MailAddress("sahilchopra922@gmail.com");
                    mail.To.Add(new MailAddress(TO.Text.Trim()));
                    mail.Subject = Sub.Text.Trim();
                    mail.Body = Message.Text.Trim();
                    if (fileName != null && fileName != " ")
                    {

                        Attachment at = new Attachment(fileName);
                        mail.Attachments.Add(at);
                    }
                    SmtpClient smtp = new SmtpClient();
                    

                    smtp.Host = "smtp.gmail.com"; //You can add this in the webconfig
                    smtp.EnableSsl = true;

                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = "sahilchopra922@gmail.com";
                    NetworkCred.Password = "sahil@3553";
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587; //this is Gmail port for e-mail
                    smtp.Send(mail);//send an e-mail 
                    MessageBox.Show("E-mail Delivered");
                    TO.Text = null;
                    Sub.Text = null;
                    Message.Text = null;
               
               
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                MessageBox.Show("PLS CHECK THE EMAIL-ID");
            }
        }
        #endregion

        #region control buttons
        private void label1_Click(object sender, EventArgs e)
        {
            //XIMA.Tray_menu.Items.Remove(pinx);
            this.Close();                 
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            //minimize
            this.Visible = false;
        }
       

        private void T_Click(object sender, EventArgs e)
        {
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

        }
        #endregion

        private void label4_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse Text Files";

           // openFileDialog1.CheckFileExists = true;
            //openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
            }

        }

    }
}
