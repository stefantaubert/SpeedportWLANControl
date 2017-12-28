namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerLogin = new System.Windows.Forms.Timer(this.components);
            this.timerChangeState = new System.Windows.Forms.Timer(this.components);
            this.timerLogOut = new System.Windows.Forms.Timer(this.components);
            this.timerClose = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
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
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 514);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerLogin;
        private System.Windows.Forms.Timer timerChangeState;
        private System.Windows.Forms.Timer timerLogOut;
        private System.Windows.Forms.Timer timerClose;
    }
}

