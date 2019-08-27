using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZW.Common.Properties;
using ZW.Common.Encrypt;

namespace AutoSensor
{
    public partial class AuthWindow : Form
    {
        private static DesProperties prop = new DesProperties();
        public bool isExitAll = true;
        public AuthWindow()
        {
            InitializeComponent();
            prop.DesKey = Constants.mykey;
            prop.load("./data.db");
        }

        private void disableController()
        {
            this.Text = "授权登录(登录中...)";
            txt_passwd.Enabled = false;
            txt_username.Enabled = false;
            btn_login.Enabled = false;
        }

        private void enableController()
        {
            this.Text = "授权登录";
            txt_passwd.Enabled = true;
            txt_username.Enabled = true;
            btn_login.Enabled = true;
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (txt_passwd.Text.Length == 0 || txt_username.Text.Length == 0)
            {
                MessageBox.Show("用户名密码不能为空");
                return;
            }
            try
            {
                disableController();
                string pwd_md5 = MD5.UserMd5("wufei" + txt_passwd.Text);
                this.Text = "授权登录(登录中...)";
                if (BizHelper.login(txt_username.Text, pwd_md5) == true)
                {
                    isExitAll = false;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("登录错误:" + BizHelper.lastError);
                    enableController();
                }
            }
            catch (Exception ex)
            {
                enableController();
                MessageBox.Show("登录异常:" + ex.Message);
            }
        }

        private void AuthWindow_Shown(object sender, EventArgs e)
        {
            try
            {
                if (queryNewVersion() == true)
                {
                    return;
                }
                string username = (string)prop["username"];
                if (username != null)
                {
                    txt_username.Text = username;
                    disableController();
                    if (BizHelper.autoLogin() == true)
                    {
                        isExitAll = false;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("登录错误:" + BizHelper.lastError);
                        enableController();
                    }
                }
            }
            catch (Exception ex)
            {
                enableController();
                MessageBox.Show("登录异常:" + ex.Message);
            }
        }

        private bool queryNewVersion()
        {
            SoftUpdateResultData data = BizHelper.queryNewVersion();
            if (data != null && data.needUpdate == true)
            {
                NewVersionTips win = new NewVersionTips();
                win.data = data;
                win.ShowDialog();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
