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
    public partial class TaskGroup_FileCheck_Form : Form
    {
        private string typeName;
        public TaskGroup_FileCheck_Form(string str)
        {
            InitializeComponent();
            typeName = str;
            GlobalVar.Instance.logger.Debug("创建Form: " + typeName + " 完成");
        }

        public void Reset()
        {

        }

        public Dictionary<String, List<String>> InitDictList()
        {
            String dicpath = "/Dic/FileCheck/";
            List<String> fileList = FileTool.ReadAllDic(dicpath);

            this.checkedListBox_dict.Items.Clear();
            Dictionary<String, List<String>> dirs = new Dictionary<String, List<String>>();
            dirs.Clear();
            foreach (String dir in fileList)
            {
                List<String> clist = FileTool.ReadFileToListByEncoding(dicpath + dir, "UTF-8");
                dirs[dir] = clist;
                this.checkedListBox_dict.Items.Add(dir + "|" + clist.Count + "条");
            }

            return dirs;
        }
    }
}
