using BrightIdeasSoftware;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zaku
{
    public partial class MainForm : MaterialForm
    {
        public MainForm()
        {
            InitializeComponent();
            //Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void InitAppSkin()
        {
            var materialSkinManager = GlobalVar.Instance.materialSkinManager;
            materialSkinManager.AddFormToManage(this);
        }

        private void InitAppTitle()
        {
            Text = $"ザクⅡ Ver:{Assembly.GetExecutingAssembly().GetName().Version.ToString()}    [ {EnumString.GetStringValue(GlobalVar.Instance.userInfo.userType)} {GlobalVar.Instance.userInfo.userName} ]";
        }

        private void InitUIControl()
        {
            //materialSingleLineTextField_ThreadNum.Text = Convert.ToString(Environment.ProcessorCount * 4);

            comboBox_SelectTaskGroup.Items.Clear();
            List<string> taskGroupNameList = GlobalVar.Instance.taskGroupManager.TaskGroupNameList();
            foreach( var name in taskGroupNameList){
                comboBox_SelectTaskGroup.Items.Add(name);
            }
            //if(comboBox_SelectTaskGroup.Items.Count >= 1)
            //{
            //    comboBox_SelectTaskGroup.SelectedIndex = 0;
            //}

            comboBox_UA.Items.Clear();
            List<string> uaTypeList = GlobalVar.Instance.uaList.uaTypeList;
            foreach (var name in uaTypeList)
            {
                comboBox_UA.Items.Add(name);
            }
            //if (comboBox_UA.Items.Count >= 1)
            //{
            //    comboBox_UA.SelectedIndex = 0;
            //}
            GlobalVar.Instance.logger.Debug($"加载UA成功 [{GlobalVar.Instance.uaList.Count()}] 个");


            fastObjectListView_main.UseTranslucentHotItem = false;
            fastObjectListView_main.UseHotItem = true;
            fastObjectListView_main.FullRowSelect = true;
            fastObjectListView_main.UseExplorerTheme = false;

            RowBorderDecoration rbd = new RowBorderDecoration();
            rbd.BorderPen = new Pen(Color.Purple, 2);
            rbd.FillBrush = null;
            rbd.CornerRounding = 4.0f;
            HotItemStyle hotItemStyle = new HotItemStyle();
            hotItemStyle.Decoration = rbd;
            fastObjectListView_main.HotItemStyle = hotItemStyle;
            fastObjectListView_main.View = View.Details;
            fastObjectListView_main.OwnerDraw = true;
            fastObjectListView_main.UseAlternatingBackColors = true;
            fastObjectListView_main.CheckBoxes = false;
            fastObjectListView_main.TriStateCheckBoxes = false;
            fastObjectListView_main.Invalidate();

            timer_Main.Enabled = true;
        }

        private void InitLogic()
        {
            GlobalVar.Instance.taskGroupManager.InitUIControls(this.fastObjectListView_main,
                this.usageThreadsInPool, this.usageHistorySTP, 
                this.chart_statistics, this.chart_timeEfficiency);
        }

        private Log2Console.MainLogForm mainLogForm;
        private void InitLogPanel()
        {
            if(GlobalVar.Instance.userInfo.userType < UserType.Manager)
            {
                return;
            }
            if (mainLogForm == null)
            {
                mainLogForm = new Log2Console.MainLogForm();
                mainLogForm.TopLevel = false;
                mainLogForm.Size = new Size(1005, 610);
                this.panel3.Controls.Add(mainLogForm);
            }
            mainLogForm.Show();
        }

        private void InitConfig()
        {
            GlobalVar.Instance.config = XMLConfig.ReadConfig(XMLConfig.GetAppConfigFilePath());

            materialSingleLineTextField_ThreadNum.Text = GlobalVar.Instance.config.ThreadNum.ToString();
            materialSingleLineTextField_Inteval.Text = GlobalVar.Instance.config.IntevalTime.ToString();
            comboBox_UA.Text = GlobalVar.Instance.config.UAType;
            materialCheckBox_AutoSaveResult.Checked = GlobalVar.Instance.config.IsAutoSaveResult;
            materialRadioButton_txt.Checked = GlobalVar.Instance.config.IsSaveAsTxt;
            materialRadioButton_csv.Checked = GlobalVar.Instance.config.IsSaveAsCsv;
            materialRadioButton_mysql.Checked = GlobalVar.Instance.config.IsSaveAsMySQL;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            InitAppSkin();
            InitAppTitle();
            InitUIControl();
            InitLogPanel();
            InitLogic();
            InitConfig();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainLogForm != null)
            {
                mainLogForm.Close();
            }
            XMLConfig.SaveConfig(XMLConfig.GetAppConfigFilePath(), GlobalVar.Instance.config);
        }

        private void SetTip(string str)
        {
            materialLabel_Tip.Text = str;
        }

        private void timer_Main_Tick(object sender, EventArgs e)
        {
            if (GlobalVar.Instance.taskGroupManager.IsNoticeStop)
            {
                materialRaisedButton_DoOrStop_Click(null, null);
                GlobalVar.Instance.taskGroupManager.IsNoticeStop = false;
            }
            if (!GlobalVar.Instance.taskGroupManager.IsAnyTaskActiving())
            {
                return;
            }

            GlobalVar.Instance.taskGroupManager.OnTick();
        }

        #region UI响应

        private void 浅色方案蓝色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalVar.Instance.materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            GlobalVar.Instance.materialSkinManager.ColorScheme = new ColorScheme(Primary.LightBlue800, Primary.LightBlue900, Primary.LightBlue500, Accent.LightBlue200, TextShade.BLACK);
        }

        private void 深色方案蓝色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalVar.Instance.materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            GlobalVar.Instance.materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.Blue200, TextShade.WHITE);
        }

        private void 浅色方案橙色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalVar.Instance.materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            GlobalVar.Instance.materialSkinManager.ColorScheme = new ColorScheme(Primary.Orange800, Primary.Orange900, Primary.Orange500, Accent.Orange200, TextShade.BLACK);
        }

        private void 深色方案橙色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalVar.Instance.materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            GlobalVar.Instance.materialSkinManager.ColorScheme = new ColorScheme(Primary.DeepOrange800, Primary.DeepOrange900, Primary.DeepOrange500, Accent.DeepOrange200, TextShade.WHITE);
        }

        private void materialRaisedButton_CreateTaskGroup_Click(object sender, EventArgs e)
        {
            var taskGroupName = comboBox_SelectTaskGroup.Text;
            var container = this.Controls.Find("groupBox_TaskSetting", true)[0];
            if (GlobalVar.Instance.taskGroupManager.CreateTaskGroupByName(taskGroupName, container) != null)
            {
                GlobalVar.Instance.logger.Info($"创建任务成功 [{taskGroupName}]");
                materialRaisedButton_LoadData.Visible = true;
                materialRaisedButton_CreateTaskGroup.Visible = false;
            }
            else
            {
                GlobalVar.Instance.logger.Error($"创建任务失败 [{taskGroupName}]");
                SetTip($"创建任务失败 [{taskGroupName}]");
            }
        }

        private void materialRaisedButton_LoadData_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择过滤文件";
            dialog.Filter = "所有文件(*.*)|*.*";
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string file = dialog.FileName;

            int nSrcCnt = GlobalVar.Instance.taskGroupManager.ImportData(file);
            if (nSrcCnt > 0)
            {
                GlobalVar.Instance.logger.Debug($"加载源数据成功，共 [{nSrcCnt}] 个");
                materialLabel3.Text = $"2:加载源数据文件 [{Path.GetFileName(file)}]";
                SetTip($"加载源数据成功，共 [{nSrcCnt}] 个");

                materialRaisedButton_LoadData.Visible = false;
                materialRaisedButton_DoOrStop.Visible = true;
            }
        }

        private void materialRaisedButton_DoOrStop_Click(object sender, EventArgs e)
        {
            if (!GlobalVar.Instance.taskGroupManager.IsAnyTaskActiving())
            {
                SetTip("当前没有活跃任务.");
                return;
            }
            if (materialRaisedButton_DoOrStop.Text == "停止")
            {
                if (!GlobalVar.Instance.taskGroupManager.Stop())
                {
                    SetTip("停止任务失败，请重置...");
                }
                else
                {
                    SetTip("任务已停止.");
                }
                materialRaisedButton_DoOrStop.Text = "执行";
                materialRaisedButton_DoOrStop.Visible = false;
                materialRaisedButton_Save.Visible = true;
                if (materialCheckBox_AutoSaveResult.Checked)
                {
                    materialRaisedButton_Save_Click(null, null);
                }
            }
            else if (materialRaisedButton_DoOrStop.Text == "执行")
            {
                TaskGroupSetting s = new TaskGroupSetting();
                s.IntervalTimeMS = GlobalVar.Instance.config.IntevalTime;
                s.ThreadNum = GlobalVar.Instance.config.ThreadNum;
                s.UAType = GlobalVar.Instance.config.UAType;
                s.IsAutoExport = materialCheckBox_AutoSaveResult.Checked;
                if (materialRadioButton_txt.Checked)
                    s.ExportType = ResourceType.eResourceType_Txt;
                if (materialRadioButton_csv.Checked)
                    s.ExportType = ResourceType.eResourceType_Csv;
                if (materialRadioButton_mysql.Checked)
                    s.ExportType = ResourceType.eResourceType_MySQL;

                GlobalVar.Instance.taskGroupManager.Do(s);

                SetTip("开始执行任务.");
                materialRaisedButton_DoOrStop.Text = "停止";
            }
            else
            {
                return;
            }
        }

        private void materialRaisedButton_Save_Click(object sender, EventArgs e)
        {
            SetTip("正在保存数据");
            GlobalVar.Instance.logger.Debug($"正在保存数据");

            ResourceType t = ResourceType.eResourceType_Csv;
            if (materialRadioButton_txt.Checked)
                t = ResourceType.eResourceType_Txt;
            if (materialRadioButton_csv.Checked)
                t = ResourceType.eResourceType_Csv;
            if (materialRadioButton_mysql.Checked)
                t = ResourceType.eResourceType_MySQL;

            string filename = GlobalVar.Instance.taskGroupManager.ExportData(t);

            materialRaisedButton_Save.Visible = false;
            SetTip($"数据保存完成 [{filename}]");
        }

        private void materialRaisedButton_Reset_Click(object sender, EventArgs e)
        {
            GlobalVar.Instance.logger.Debug($"任务状态重置");
            SetTip("任务状态已重置");
            materialRaisedButton_CreateTaskGroup.Visible = true;
            materialRaisedButton_LoadData.Visible = false;
            materialRaisedButton_DoOrStop.Visible = false;
            materialRaisedButton_Save.Visible = false;
            materialRaisedButton_DoOrStop.Text = "执行";
            materialLabel3.Text = "2:加载源数据文件";
            materialLabel5.Text = "4:保存结果数据";
            if (GlobalVar.Instance.taskGroupManager.IsAnyTaskActiving())
            {
                GlobalVar.Instance.taskGroupManager.Stop();
                GlobalVar.Instance.taskGroupManager.Clear();
            }
        }

        private void materialSingleLineTextField_ThreadNum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GlobalVar.Instance.config.ThreadNum = Convert.ToInt32(materialSingleLineTextField_ThreadNum.Text);
            }
            catch { }
        }

        private void materialSingleLineTextField_Inteval_TextChanged(object sender, EventArgs e)
        {
            GlobalVar.Instance.config.IntevalTime = Convert.ToInt32(materialSingleLineTextField_Inteval.Text);
        }

        private void comboBox_UA_SelectedIndexChanged(object sender, EventArgs e)
        {
            GlobalVar.Instance.config.UAType = comboBox_UA.Text;
        }

        private void materialRadioButton_txt_CheckedChanged(object sender, EventArgs e)
        {
            if (materialRadioButton_txt.Checked)
            {
                GlobalVar.Instance.config.IsSaveAsTxt = true;
            }else
            {
                GlobalVar.Instance.config.IsSaveAsTxt = false;
            }
        }

        private void materialRadioButton_csv_CheckedChanged(object sender, EventArgs e)
        {
            if (materialRadioButton_csv.Checked)
            {
                GlobalVar.Instance.config.IsSaveAsCsv = true;
            }
            else
            {
                GlobalVar.Instance.config.IsSaveAsCsv = false;
            }
        }

        private void materialRadioButton_mysql_CheckedChanged(object sender, EventArgs e)
        {
            if (materialRadioButton_mysql.Checked)
            {
                GlobalVar.Instance.config.IsSaveAsMySQL = true;
            }
            else
            {
                GlobalVar.Instance.config.IsSaveAsMySQL = false;
            }
        }

        private void fastObjectListView_main_IsHyperlink(object sender, IsHyperlinkEventArgs e)
        {
            if(e.Column.Text.Contains("链接"))
            {
                e.Url = e.Text;
            }
        }
        #endregion
    }
}
