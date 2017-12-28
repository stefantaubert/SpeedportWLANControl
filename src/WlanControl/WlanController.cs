namespace WlanControl
{
    using System;
    using System.Threading;
    using System.Windows.Forms;
    using WlanControl.Properties;

    internal class WlanController
    {
        private System.Windows.Forms.Timer timerLogin;
        private System.Windows.Forms.Timer timerChangeState;
        private System.Windows.Forms.Timer timerLogOut;
        private System.Windows.Forms.Timer timerClose;

        private bool loggedIn = false;

        WebBrowser webBrowser1 = new WebBrowser();

        public WlanController()
        {
            this.timerLogin = new System.Windows.Forms.Timer();
            this.timerChangeState = new System.Windows.Forms.Timer();
            this.timerLogOut = new System.Windows.Forms.Timer();
            this.timerClose = new System.Windows.Forms.Timer();
            // 
            // timerLogin
            // 
            this.timerLogin.Interval = 2000;
            this.timerLogin.Tick += new System.EventHandler(this.timerLogin_Tick);
            // 
            // timerChangeState
            // 
            this.timerChangeState.Interval = 5000;
            this.timerChangeState.Tick += new System.EventHandler(this.timerChangeState_Tick);
            // 
            // timerLogOut
            // 
            this.timerLogOut.Interval = 1000;
            this.timerLogOut.Tick += new System.EventHandler(this.timerLogOut_Tick);
            // 
            // timerClose
            // 
            this.timerClose.Interval = 7000;
            this.timerClose.Tick += new System.EventHandler(this.timerClose_Tick);

            this.webBrowser1.Url = new System.Uri("http://speedport.ip/html/login/index.html?lang=de", System.UriKind.Absolute);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
        }

        private bool exit = false;

        public void WaitUntilFinished()
        {
            while (!exit)
            {
                Application.DoEvents();
                Thread.Sleep(500);
            }
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

        private void ChangeState()
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
            
            password.SetAttribute("value", Resources.RouterPw);
            password.Focus();

            this.timerLogin.Start();
        }

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
                    exit = true;
                    Application.Exit();
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
            Application.Exit();
            exit = true;
        }
    }
}
