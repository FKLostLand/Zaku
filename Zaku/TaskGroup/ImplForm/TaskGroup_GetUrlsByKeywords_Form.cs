using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zaku.TaskGroup.ImplForm
{
    public partial class TaskGroup_GetUrlsByKeywords_Form : Form
    {
        private string typeName;
        public TaskGroup_GetUrlsByKeywords_Form(string str)
        {
            InitializeComponent();
            typeName = str;
            GlobalVar.Instance.logger.Debug("创建Form: " + typeName + " 完成");
        }

        public void Reset()
        {

        }
    }
}
