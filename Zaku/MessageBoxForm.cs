using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zaku
{
    public enum MBType
    {
        MB_OK = 0,
        MB_OKCANCEL = 1,
    }
    internal partial class MessageBoxForm : MaterialForm
    {
        public MessageBoxForm()
        {
            InitializeComponent();
        }

        public MessageBoxForm(string description)
        {
            InitializeComponent();

            this.materialLabel_Content.Text = description;
            this.materialRaisedButton_Cancel.Visible = false;
        }

        public MessageBoxForm( string description, MBType type)
        {
            InitializeComponent();

            this.materialLabel_Content.Text = description;
            if(type == MBType.MB_OK)
                this.materialRaisedButton_Cancel.Visible = false;
        }

        private void MessageBoxForm_Load(object sender, EventArgs e)
        {
            InitAppSkin();
            InitAppTitle();
        }

        private void InitAppSkin()
        {
            var materialSkinManager = GlobalVar.Instance.materialSkinManager;
            materialSkinManager.AddFormToManage(this);
        }

        private void InitAppTitle()
        {
            Text = "SYSTEM WARNING!!!";
        }

        private void materialFlatButton_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void materialRaisedButton_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public static class MessageBox
    {
        public static void Show(string description)
        {
            using (var form = new MessageBoxForm(description))
            {
                form.ShowDialog();
            }
        }

        public static void Show(string description, MBType type)
        {
            using (var form = new MessageBoxForm(description, type))
            {
                form.ShowDialog();
            }
        }
    }
}
