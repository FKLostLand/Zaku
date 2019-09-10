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
    public partial class TaskGroup_GetSimilarKeywords_Form : Form //Panel
    {
        private string typeName;
        public TaskGroup_GetSimilarKeywords_Form(string str)
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
