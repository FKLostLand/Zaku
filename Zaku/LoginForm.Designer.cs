namespace Zaku
{
    partial class LoginForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.materialRaisedButton_Login = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialSingleLineTextField_LoginPassword = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialSingleLineTextField_LoginAccout = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.SuspendLayout();
            // 
            // materialRaisedButton_Login
            // 
            this.materialRaisedButton_Login.AutoSize = true;
            this.materialRaisedButton_Login.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialRaisedButton_Login.Depth = 0;
            this.materialRaisedButton_Login.Icon = null;
            this.materialRaisedButton_Login.Location = new System.Drawing.Point(129, 152);
            this.materialRaisedButton_Login.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton_Login.Name = "materialRaisedButton_Login";
            this.materialRaisedButton_Login.Primary = true;
            this.materialRaisedButton_Login.Size = new System.Drawing.Size(51, 36);
            this.materialRaisedButton_Login.TabIndex = 9;
            this.materialRaisedButton_Login.Text = "登入";
            this.materialRaisedButton_Login.UseVisualStyleBackColor = true;
            this.materialRaisedButton_Login.Click += new System.EventHandler(this.materialRaisedButton_Login_Click);
            // 
            // materialSingleLineTextField_LoginPassword
            // 
            this.materialSingleLineTextField_LoginPassword.Depth = 0;
            this.materialSingleLineTextField_LoginPassword.Hint = "请输入密码";
            this.materialSingleLineTextField_LoginPassword.Location = new System.Drawing.Point(87, 106);
            this.materialSingleLineTextField_LoginPassword.MaxLength = 16;
            this.materialSingleLineTextField_LoginPassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSingleLineTextField_LoginPassword.Name = "materialSingleLineTextField_LoginPassword";
            this.materialSingleLineTextField_LoginPassword.PasswordChar = '*';
            this.materialSingleLineTextField_LoginPassword.SelectedText = "";
            this.materialSingleLineTextField_LoginPassword.SelectionLength = 0;
            this.materialSingleLineTextField_LoginPassword.SelectionStart = 0;
            this.materialSingleLineTextField_LoginPassword.Size = new System.Drawing.Size(133, 23);
            this.materialSingleLineTextField_LoginPassword.TabIndex = 8;
            this.materialSingleLineTextField_LoginPassword.TabStop = false;
            this.materialSingleLineTextField_LoginPassword.UseSystemPasswordChar = false;
            this.materialSingleLineTextField_LoginPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.materialSingleLineTextField_LoginPassword_KeyDown);
            // 
            // materialSingleLineTextField_LoginAccout
            // 
            this.materialSingleLineTextField_LoginAccout.Depth = 0;
            this.materialSingleLineTextField_LoginAccout.Hint = "请输入账号";
            this.materialSingleLineTextField_LoginAccout.Location = new System.Drawing.Point(87, 77);
            this.materialSingleLineTextField_LoginAccout.MaxLength = 16;
            this.materialSingleLineTextField_LoginAccout.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialSingleLineTextField_LoginAccout.Name = "materialSingleLineTextField_LoginAccout";
            this.materialSingleLineTextField_LoginAccout.PasswordChar = '\0';
            this.materialSingleLineTextField_LoginAccout.SelectedText = "";
            this.materialSingleLineTextField_LoginAccout.SelectionLength = 0;
            this.materialSingleLineTextField_LoginAccout.SelectionStart = 0;
            this.materialSingleLineTextField_LoginAccout.Size = new System.Drawing.Size(133, 23);
            this.materialSingleLineTextField_LoginAccout.TabIndex = 7;
            this.materialSingleLineTextField_LoginAccout.TabStop = false;
            this.materialSingleLineTextField_LoginAccout.UseSystemPasswordChar = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.materialSingleLineTextField_LoginAccout);
            this.Controls.Add(this.materialSingleLineTextField_LoginPassword);
            this.Controls.Add(this.materialRaisedButton_Login);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "[DK] MS-06F登入中...";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton_Login;
        private MaterialSkin.Controls.MaterialSingleLineTextField materialSingleLineTextField_LoginPassword;
        private MaterialSkin.Controls.MaterialSingleLineTextField materialSingleLineTextField_LoginAccout;
    }
}

