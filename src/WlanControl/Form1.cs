using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        WebBrowser webBrowser1 = new WebBrowser();
        public Form1()
        {
            InitializeComponent();

            this.webBrowser1.Url = new System.Uri("http://speedport.ip/html/login/index.html?lang=de", System.UriKind.Absolute);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Login();
        }

        private void Login()
        {
            HtmlDocument doc = webBrowser1.Document;
            HtmlElement submit = doc.GetElementById("loginbutton");
            submit.InvokeMember("click");
            this.loggedIn = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChangeState();
        }

        private void LogOut()
        {
            foreach (System.Windows.Forms.HtmlElement html in webBrowser1.Document.GetElementsByTagName("a"))
            {
                if (html.InnerText == "Logout")
                {
                    html.InvokeMember("click");
                    this.timerClose.Start();
                    break;
                }
            }
        }

        void ChangeState()
        {
            foreach (System.Windows.Forms.HtmlElement html in webBrowser1.Document.GetElementsByTagName("label"))
            {
                if (html.InnerText == "WLAN im 2,4-GHz-Frequenzband einschalten")
                {
                    html.InvokeMember("click");
                    timerLogOut.Start();
                    break;
                }
            }
        }

        private void SetPassword()
        {
            HtmlDocument doc = webBrowser1.Document;
            HtmlElement password = doc.GetElementById("router_password");
            HtmlElement submit = doc.GetElementById("loginbutton");

            password.SetAttribute("value", "80711489");
            password.Focus();

            this.timerLogin.Start();
        }

        private bool loggedIn = false;

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.AbsoluteUri == "http://speedport.ip/html/login/index.html?lang=de")
            {
                if (!loggedIn)
                {
                    this.SetPassword();
                }
                else
                {
                    this.Close();
                }
            }
            else if (e.Url.AbsoluteUri == "http://speedport.ip/html/content/overview/index.html?lang=de")
            {
                this.webBrowser1.Navigate("http://speedport.ip/html/content/network/wlan_basic.html?lang=de");
            }
            else if (e.Url.AbsoluteUri == "http://speedport.ip/html/content/network/wlan_basic.html?lang=de")
            {
                this.timerChangeState.Start();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.LogOut();
        }

        private void timerLogin_Tick(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Timer)sender).Stop();

            this.Login();
        }

        private void timerChangeState_Tick(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Timer)sender).Stop();

            this.ChangeState();
        }

        private void timerLogOut_Tick(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Timer)sender).Stop();

            this.LogOut();
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
