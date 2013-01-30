using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiRemoteDesktopClient
{
    public partial class ConfigurationWindow : Form
    {
        public ConfigurationWindow()
        {
            InitializeComponent();
            InitializeControls();
            InitializeControlEvents();
        }

        public void InitializeControls()
        {
            txPassword.Text = GlobalHelper.appSettings.Settings.Password;
            cbHideWhenMinimized.Checked = GlobalHelper.appSettings.Settings.HideWhenMinimized;
            cbNotificationWindow.Checked = GlobalHelper.appSettings.Settings.HideInformationPopupWindow;
        }

        public void InitializeControlEvents()
        {
            this.btnSave.Click += new EventHandler(DefaultButtons_Click);
            this.btnClose.Click += new EventHandler(DefaultButtons_Click);

            this.lblShowPass.Click += new EventHandler(lblShowPass_Click);
        }

        void lblShowPass_Click(object sender, EventArgs e)
        {
            lblShowPass = (Label)sender;

            if (int.Parse(lblShowPass.Tag.ToString()) == 0)
            {
                lblShowPass.Text = "암호숨김";
                txPassword.UseSystemPasswordChar = false;
                lblShowPass.Tag = 1;
            }
            else
            {
                lblShowPass.Text = "암호표시";
                txPassword.UseSystemPasswordChar = true;
                lblShowPass.Tag = 0;
            }
        }

        void DefaultButtons_Click(object sender, EventArgs e)
        {
            if (sender == btnSave)
            {
                GlobalHelper.appSettings.Settings.Password = txPassword.Text;
                GlobalHelper.appSettings.Settings.HideWhenMinimized = cbHideWhenMinimized.Checked;
                GlobalHelper.appSettings.Settings.HideInformationPopupWindow = cbNotificationWindow.Checked;

                if (GlobalHelper.appSettings.Save())
                {
                    MessageBox.Show("변경 사항이 저장되었습니다.\r\n창을 닫을수 있습니다.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("변경을 하지 못했습니다.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (sender == btnClose)
            {
            }
        }
    }
}
