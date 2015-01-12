namespace DatenLogger
{
    partial class FrmDatenlogger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDatenlogger));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.TXT_Watchdog = new Helper.TxtBox();
            this.listView1 = new Helper.ListViewNF();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Keyboard)).BeginInit();
            this.GB_Menu.SuspendLayout();
            this.GB_Datenausgabe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Menu)).BeginInit();
            this.SuspendLayout();
            // 
            // GB_Datenausgabe
            // 
            this.GB_Datenausgabe.Controls.Add(this.listView1);
            this.GB_Datenausgabe.Controls.Add(this.label6);
            this.GB_Datenausgabe.Controls.Add(this.TXT_Watchdog);
            this.GB_Datenausgabe.Enter += new System.EventHandler(this.GB_Datenausgabe_Enter);
            // 
            // BTN_Menu
            // 
            this.BTN_Menu.Click += new System.EventHandler(this.BTN_Menu_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(20, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 35);
            this.label6.TabIndex = 251;
            this.label6.Text = "Watchdog:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TXT_Watchdog
            // 
            this.TXT_Watchdog.BackColor = System.Drawing.Color.Black;
            this.TXT_Watchdog.Enabled = false;
            this.TXT_Watchdog.Error = false;
            this.TXT_Watchdog.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.TXT_Watchdog.ForeColor = System.Drawing.Color.Yellow;
            this.TXT_Watchdog.Format = null;
            this.TXT_Watchdog.Location = new System.Drawing.Point(153, 32);
            this.TXT_Watchdog.Margin = new System.Windows.Forms.Padding(4);
            this.TXT_Watchdog.Name = "TXT_Watchdog";
            this.TXT_Watchdog.Size = new System.Drawing.Size(79, 35);
            this.TXT_Watchdog.TabIndex = 250;
            this.TXT_Watchdog.Text = "1";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(6, 74);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(640, 437);
            this.listView1.TabIndex = 255;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // FrmDatenlogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(768, 528);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDatenlogger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Datenlogger";
            this.Load += new System.EventHandler(this.FrmDatenlogger_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Keyboard)).EndInit();
            this.GB_Menu.ResumeLayout(false);
            this.GB_Datenausgabe.ResumeLayout(false);
            this.GB_Datenausgabe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BTN_Menu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label6;
        private Helper.TxtBox TXT_Watchdog;
        private Helper.ListViewNF listView1;
    }
}

