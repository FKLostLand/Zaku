using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;
using System.Reflection;


namespace Zaku
{
    internal partial class LoginForm : MaterialForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitAppSkin()
        {
            var materialSkinManager = GlobalVar.Instance.materialSkinManager;
            materialSkinManager.AddFormToManage(this);
        }

        private void InitAppTitle()
        {
            Text = $"ザクⅡ Ver:{Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            InitAppSkin();
            InitAppTitle();
            GlobalVar.Instance.logger.Debug($"程序启动完成");
        }

        private void materialSingleLineTextField_LoginPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TryLogin();
            }
        }

        private void materialRaisedButton_Login_Click(object sender, EventArgs e)
        {
            TryLogin();
        }

        private void TryLogin()
        {
            var account = materialSingleLineTextField_LoginAccout.Text;
            var password = materialSingleLineTextField_LoginPassword.Text;
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("账号密码不可为空", MBType.MB_OK);
                return;
            }
            materialSingleLineTextField_LoginAccout.Text = "";
            materialSingleLineTextField_LoginPassword.Text = "";


            GlobalVar.Instance.Init(account, password);
            MainForm form = new MainForm();
            form.Show(this);
            this.Hide();
        }
    }
}
