using Amib.Threading;
using BrightIdeasSoftware;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TestAllInOne.Properties;

namespace TestAllInOne
{
    public partial class MainForm : MaterialForm
    {

        public MainForm()
        {
            InitializeComponent();
            UpdateTitle();
            InitSkin();
            InitializeListViewExamples();
            InitializeLogForm();
            InitializeGUIPerformanceCounters();
        }

        #region 第一页

        private MaterialSkinManager materialSkinManager;
        public void InitSkin()
        {
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.LightBlue800, Primary.LightBlue900, Primary.LightBlue500, Accent.LightBlue200, TextShade.WHITE);
        }

        public void UpdateTitle()
        {
            Text = $"ザク by DarkKnight    Ver:{Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
        }

        private void toolStripMenuItem_深色方案蓝色_Click(object sender, EventArgs e)
        {
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void toolStripMenuItem_浅色方案蓝色_Click(object sender, EventArgs e)
        {
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.LightBlue800, Primary.LightBlue900, Primary.LightBlue500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void toolStripMenuItem_深色方案橙色_Click(object sender, EventArgs e)
        {
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.DeepOrange800, Primary.DeepOrange900, Primary.DeepOrange500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void toolStripMenuItem_浅色方案橙色_Click(object sender, EventArgs e)
        {
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Orange800, Primary.Orange900, Primary.Orange500, Accent.LightBlue200, TextShade.BLACK);
        }

        private void AddTestInfoToListView()
        {
            var data = new[]
            {
                new []{"1", "刘德华", "392"},
                new []{"2", "张三丰", "518"},
                new []{"4", "Peter", "237"},
                new []{"5", "Jelly Bean", "375"},
                new []{"88", "德玛西亚", "408"}
            };

            foreach (string[] version in data)
            {
                var item = new ListViewItem(version);
                materialListView1.Items.Add(item);
            }
        }

        private void materialFlatButton_FlatButton_Click(object sender, EventArgs e)
        {
            AddTestInfoToListView();
            materialFlatButton_FlatButton.Text = $"当前数据 {materialListView1.Items.Count.ToString()} 个";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var value = materialProgressBar1.Value;
            value += 5;
            if (value > 100)
            {
                value = 10;
            }
            materialProgressBar1.Value = value;
        }

        #endregion 第一页

        #region 第二页

        private List<Person> masterList;
        private bool isTakeNoticeOfSelectionEvent = true;
        private bool isShouldSelectAll = true;
        private TextOverlay nagOverlay;
        private void InitializeListViewExamples()
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 2;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;

            if (ObjectListView.IsVistaOrLater)
                this.Font = new Font("Segoe UI", 9);

            masterList = new List<Person>();
            masterList.Add(new Person("王尼玛", "物理学家", 21, new DateTime(1984, 9, 23), 45.67, false, "ak", "大胸, 蠢货"));
            masterList.Add(new Person("刘二傻", "物理学家", 21, new DateTime(1974, 9, 23), 245.67, false, "gp", "美女, 机智"));
            masterList.Add(new Person("张三", "舞者", 30, new DateTime(1965, 11, 1), 75.5, false, "ns", "爱吹牛, 狡猾"));
            masterList.Add(new Person("李四", "待业人员", 1, new DateTime(1966, 10, 12), 12.25, true, "cp", "穷, 爱吹牛"));
            masterList.Add(new Person("孙五", "护士", 42, new DateTime(1965, 10, 29), 1245.7, false, "np", "优秀, 可爱, 恋父狂"));
            masterList.Add(new Person("赵六", "老师", 21, new DateTime(1964, 9, 23), 145.67, false, "jr", "机智, 可爱"));
            masterList.Add(new Person("钱七", "老师", 21, new DateTime(1960, 1, 23), 145.67, false, "gab", "机智, 帅, 老头子, 狡猾"));
            masterList.Add(new Person("宋思聪", "房地产商", 30, new DateTime(1975, 1, 12), 175.5, false, "sp", "土豪, 高, 帅"));
            masterList.Add(new Person("宋健林", "房地产商", 41, new DateTime(1956, 9, 24), 212.25, true, "cr", "土豪, 老头子"));
            masterList.Add(new Person("马风", "演员", 35, new DateTime(1970, 9, 29), 1145, false, "mb", "样貌丑陋, 爱吹牛"));
            masterList.Add(new Person("于大神", "程序员", 27, new DateTime(1974, 8, 28), 245.7, false, "sj", "天才"));
            masterList.Add(new Person("杨无名"));

            List<Person> list = new List<Person>();
            foreach (Person p in masterList)
                list.Add(new Person(p));

            // 性能填充检查
            /*
            while (list.Count < 50000) {
                foreach (Person p in masterList)
                    list.Add(new Person(p));
            }
            */

            InitializeFastListExample(list);

            // 打印机
            this.printPreviewControl1.Zoom = 1;
            this.printPreviewControl1.AutoZoom = true;
            UpdatePrintPreview();
        }

        private void UpdatePrintPreview()
        {
            this.listViewPrinter1.IsShrinkToFit = true;
            this.listViewPrinter1.IsPrintSelectionOnly = this.materialCheckBox9.Checked;
            this.listViewPrinter1.FirstPage = 1;
            this.listViewPrinter1.LastPage = 0x00ffffff;
            this.printPreviewControl1.InvalidatePreview();
        }

        private void InitializeFastListExample(List<Person> list)
        {
            // 拖拽
            this.fastObjectListView1.DragSource = new SimpleDragSource();
            this.fastObjectListView2.DragSource = new SimpleDragSource();
            this.fastObjectListView1.DropSink = new RearrangingDropSink(true);
            this.fastObjectListView2.DropSink = new RearrangingDropSink(true);


            this.fastObjectListView1.CheckBoxes = true;
            this.fastObjectListView1.TriStateCheckBoxes = false;

            this.olvColumn1.ImageGetter = delegate (object row) {
                Person person = ((Person)row);
                string personNameMD5 = CreateMD5(person.Name);
                if (personNameMD5.Contains("9"))
                    return 0; // 男
                else
                    return 1; // 女
            };

            this.olvColumn2.AspectGetter = delegate (object x) { return ((Person)x).Occupation; };
            this.olvColumn7.AspectGetter = delegate (object row) {
                if (((Person)row).GetRate() < 100)
                    return "很少";
                if (((Person)row).GetRate() > 1000)
                    return "超多";
                return "普通";
            };
            this.olvColumn4.AspectGetter = delegate (object x) { return ((Person)x).YearOfBirth; };
            this.olvColumn7.Renderer = new MappedImageRenderer(new Object[] { "很少", Resources.down16, "普通", Resources.tick16, "超多", Resources.star16 });
            this.olvColumn3.Renderer = new MultiImageRenderer(Resources.star16, 5, 0, 40);
            this.olvColumn3.MakeGroupies(
                new object[] { 10, 20, 30, 40 },
                new string[] { "描述", "描述", "描述", "描述", "描述" },
                new string[] { "图片", "图片", "图片", "图片", "图片" },
                new string[] { "说明", "说明", "说明", "说明", "说明" },
                new string[] { "任务", "任务", "任务", "任务", "任务" }
            );


            this.fastObjectListView1.SetObjects(list);
        }

        private string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private void materialCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            fastObjectListView1.OwnerDraw = materialCheckBox4.Checked;
            fastObjectListView1.BuildList();
        }

        private void materialCheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (ObjectListView.IsVistaOrLater)
            {
                fastObjectListView1.ShowGroups = materialCheckBox5.Checked;
                fastObjectListView1.BuildList();
            }
            else
            {
                MessageBox.Show("虚拟列表机制只能在 Vista以及以上的系统 运行",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void materialCheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            fastObjectListView1.CheckBoxes = materialCheckBox6.Checked;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "禁止修改")
                fastObjectListView1.CellEditActivation = ObjectListView.CellEditActivateMode.None;
            //else if (comboBox1.Text == "单击修改")
            //    fastObjectListView1.CellEditActivation = ObjectListView.CellEditActivateMode.SingleClick;
            else if (comboBox1.Text == "双击修改")
                fastObjectListView1.CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick;
            else
                fastObjectListView1.CellEditActivation = ObjectListView.CellEditActivateMode.F2Only;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (comboBox2.SelectedIndex == 3)
            {
                if (fastObjectListView1.VirtualMode)
                {
                    MessageBox.Show("虚拟列表机制不支持标题模式", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (fastObjectListView1.CheckBoxes)
                {
                    MessageBox.Show("标题模式下不支持前置复选框，所以自动关闭", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fastObjectListView1.CheckBoxes = false;
                }
            }
            */

            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    fastObjectListView1.View = View.SmallIcon;
                    break;
                case 1:
                    fastObjectListView1.View = View.LargeIcon;
                    break;
                    /*
                case 2:
                    fastObjectListView1.View = View.List;
                    break;
                case 3:
                    fastObjectListView1.View = View.Tile;
                    break;
                    */
                default:
                    fastObjectListView1.View = View.Details;
                    break;
            }
        }


        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            fastObjectListView1.CopyObjectsToClipboard(fastObjectListView1.CheckedObjects);
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            fastObjectListView1.ClearObjects();
            HandleSelectionChanged();
        }

        private void HandleSelectionChanged()
        {
            string msg;
            Person p = (Person)fastObjectListView1.SelectedObject;
            if (p == null)
                msg = fastObjectListView1.SelectedIndices.Count.ToString();
            else
                msg = String.Format("'{0}'", p.Name);
            Person focused = fastObjectListView1.FocusedItem == null ? null : (((OLVListItem)fastObjectListView1.FocusedItem).RowObject) as Person;
            SetTips(String.Format("总共 {1} 个元素，当前选中了元素 {0}", msg, fastObjectListView1.GetItemCount()));
        }

        private void fastObjectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            HandleSelectionChanged();
        }

        private void fastObjectListView1_SelectionChanged(object sender, EventArgs e)
        {
            if (this.isTakeNoticeOfSelectionEvent)
                HandleSelectionChanged();

            this.isTakeNoticeOfSelectionEvent = true;
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            fastObjectListView1.RemoveObjects(fastObjectListView1.SelectedObjects);
        }

        private void materialRaisedButton4_Click(object sender, EventArgs e)
        {
            ArrayList l = new ArrayList();
            foreach (Person x in this.masterList)
                l.Add(new Person(x));

            Stopwatch stopWatch = new Stopwatch();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                stopWatch.Start();
                fastObjectListView1.AddObjects(l);
            }
            finally
            {
                stopWatch.Stop();
                this.Cursor = Cursors.Default;
            }

            this.isTakeNoticeOfSelectionEvent = false;
            SetTips(String.Format("创建元素 {0} 个，花费 {1} 毫秒, 平均单个元素花费 {2:F} 毫秒",
                              this.fastObjectListView1.Items.Count,
                              stopWatch.ElapsedMilliseconds,
                              (float)stopWatch.ElapsedMilliseconds / this.fastObjectListView1.Items.Count));
        }

        private void materialRaisedButton5_Click(object sender, EventArgs e)
        {
            ArrayList l = new ArrayList();
            while (l.Count < 10000)
            {
                foreach (Person x in this.masterList)
                    l.Add(new Person(x));
            }

            Stopwatch stopWatch = new Stopwatch();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                stopWatch.Start();
                fastObjectListView1.AddObjects(l);
            }
            finally
            {
                stopWatch.Stop();
                this.Cursor = Cursors.Default;
            }

            this.isTakeNoticeOfSelectionEvent = false;
            SetTips(String.Format("创建元素 {0} 个，花费 {1} 毫秒, 平均单个元素花费 {2:F} 毫秒",
                              this.fastObjectListView1.Items.Count,
                              stopWatch.ElapsedMilliseconds,
                              (float)stopWatch.ElapsedMilliseconds / this.fastObjectListView1.Items.Count));
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            fastObjectListView1.UseTranslucentHotItem = false;
            fastObjectListView1.UseHotItem = true;
            fastObjectListView1.FullRowSelect = true;
            fastObjectListView1.UseExplorerTheme = false;

            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    fastObjectListView1.UseHotItem = false;
                    break;
                case 1:
                    HotItemStyle hotItemStyle = new HotItemStyle();
                    hotItemStyle.ForeColor = Color.AliceBlue;
                    hotItemStyle.BackColor = Color.FromArgb(255, 64, 64, 64);
                    fastObjectListView1.HotItemStyle = hotItemStyle;
                    break;
                case 2:
                    RowBorderDecoration rbd = new RowBorderDecoration();
                    rbd.BorderPen = new Pen(Color.Purple, 2);
                    rbd.FillBrush = null;
                    rbd.CornerRounding = 4.0f;
                    HotItemStyle hotItemStyle2 = new HotItemStyle();
                    hotItemStyle2.Decoration = rbd;
                    fastObjectListView1.HotItemStyle = hotItemStyle2;
                    break;
                case 3:
                    HotItemStyle hotItemStyle3 = new HotItemStyle();
                    hotItemStyle3.Decoration = new LightBoxDecoration();
                    fastObjectListView1.HotItemStyle = hotItemStyle3;
                    break;
                default:
                    fastObjectListView1.UseHotItem = true;
                    break;
            }
            fastObjectListView1.Invalidate();
        }

        private void materialSingleLineTextField3_TextChanged(object sender, EventArgs e)
        {

            string txt = materialSingleLineTextField3.Text;
            //Debug.WriteLine("过滤： " + txt);
            int matchKind = comboBox4.SelectedIndex;
            TextMatchFilter filter = null;
            if (!String.IsNullOrEmpty(txt))
            {
                switch (matchKind)
                {
                    case 0:
                    default:
                        filter = TextMatchFilter.Contains(fastObjectListView1, txt);
                        break;
                    case 1:
                        filter = TextMatchFilter.Prefix(fastObjectListView1, txt);
                        break;
                    case 2:
                        filter = TextMatchFilter.Regex(fastObjectListView1, txt);
                        break;
                }
            }

            if (filter == null)
                fastObjectListView1.DefaultRenderer = null;
            else
            {
                fastObjectListView1.DefaultRenderer = new HighlightTextRenderer(filter);

                // 可以选择GDI渲染高亮
                //fastObjectListView1.DefaultRenderer = new HighlightTextRenderer { Filter = filter, UseGdiTextRendering = false };
            }

            // 如果使用了其他过滤器
            HighlightTextRenderer highlightingRenderer = fastObjectListView1.GetColumn(0).Renderer as HighlightTextRenderer;
            if (highlightingRenderer != null)
            {
                highlightingRenderer.Filter = filter;
            }

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            fastObjectListView1.AdditionalFilter = filter;
            stopWatch.Stop();

            IList objects = fastObjectListView1.Objects as IList;
            if (objects == null)
                SetTips(String.Format("过滤完成, 消耗 {0} 毫秒", stopWatch.ElapsedMilliseconds));
            else
                SetTips(String.Format("过滤完成, 总元素数量 {0} , 过滤后元素数量 {1} ,消耗 {2} 毫秒",
                                  objects.Count,
                                  fastObjectListView1.Items.Count,
                                  stopWatch.ElapsedMilliseconds));

            fastObjectListView1.Invalidate();

            //Debug.WriteLine("过滤： " + txt + " 完毕.");
        }

        private void fastObjectListView1_IsHyperlink(object sender, IsHyperlinkEventArgs e)
        {
            if (e.Text.Contains("员"))
                e.Url = "http://www.baidu.com";
            else
                e.Url = "http://www.google.com";
        }

        private void materialCheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            fastObjectListView1.UseAlternatingBackColors = materialCheckBox7.Checked;
            fastObjectListView1.Invalidate();
        }

        private void materialRaisedButton6_Click(object sender, EventArgs e)
        {
            if (isShouldSelectAll)
            {
                fastObjectListView1.SelectAll();
                isShouldSelectAll = false;
            }
            else
            {
                fastObjectListView1.DeselectAll();
                isShouldSelectAll = true;
            }
        }

        private void materialCheckBox8_CheckedChanged(object sender, EventArgs e)
        {
            this.fastObjectListView1.RemoveOverlay(this.nagOverlay);
            if (materialCheckBox8.Checked)
            {
                this.nagOverlay = new TextOverlay();
                this.nagOverlay.Alignment = ContentAlignment.MiddleCenter;
                this.nagOverlay.Text = "  俺がザクだ.  ";
                this.nagOverlay.TextColor = Color.Red;
                this.nagOverlay.BorderWidth = 4.0f;
                this.nagOverlay.BorderColor = Color.Red;
                this.nagOverlay.Rotation = -30;
                this.nagOverlay.Font = new Font("Stencil", 36);
                this.fastObjectListView1.OverlayTransparency = 192;
                this.fastObjectListView1.AddOverlay(this.nagOverlay);
            }
        }


        private void fastObjectListView1_CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            //if (Control.ModifierKeys != Keys.Control)
            //    return;

            OLVColumn col = e.Column ?? e.ListView.GetColumn(0);
            if (col.Text != "岁数")
            {
                return;
            }
            string stringValue = col.GetStringValue(e.Model);
            if (stringValue.StartsWith("3", StringComparison.InvariantCultureIgnoreCase))
            {
                e.IsBalloon = !ObjectListView.IsVistaOrLater;
                e.ToolTipControl.SetMaxWidth(400);
                e.Title = "警告";
                e.StandardIcon = ToolTipControl.StandardIcons.InfoLarge;
                e.BackColor = Color.AliceBlue;
                e.ForeColor = Color.IndianRed;
                e.AutoPopDelay = 15000;
                e.Font = new Font("Tahoma", 12.0f);
                e.Text = "30+岁是个危险年龄!\r\n\r\n" +
                    "注意锻炼身体，不要挂!";
            }
            else
            {
                e.Title = "小标签";
                e.Text = String.Format("用户 '{0}', 列 '{1}'\r\n值为: '{2}'",
                    ((Person)e.Model).Name, col.Text, stringValue);
            }
        }

        private void materialRaisedButton9_Click(object sender, EventArgs e)
        {
            this.UpdatePrintPreview();
        }

        private void materialRaisedButton7_Click(object sender, EventArgs e)
        {
            this.listViewPrinter1.PageSetup();
            this.UpdatePrintPreview();
        }

        private void materialRaisedButton8_Click(object sender, EventArgs e)
        {
            this.listViewPrinter1.PrintPreview();
        }

        private void materialRaisedButton10_Click(object sender, EventArgs e)
        {
            this.listViewPrinter1.PrintWithDialog();
        }

        #endregion 第二页

        #region 第三页

        private Log2Console.MainLogForm mainLogForm;
        private void InitializeLogForm()
        {
            mainLogForm = new Log2Console.MainLogForm();
            mainLogForm.TopLevel = false;
            mainLogForm.Size = new Size(890, 570);
            this.panel3.Controls.Add(mainLogForm);
            mainLogForm.Show();
        }

        #endregion

        #region 第四页

        private bool _useWindowsPerformanceCounters;
        private bool running;
        private Thread workItemsProducerThread;
        private SmartThreadPool _smartThreadPool;
        private IWorkItemsGroup _workItemsGroup;
        private PerformanceCounter _pcActiveThreads;
        private PerformanceCounter _pcInUseThreads;
        private PerformanceCounter _pcQueuedWorkItems;
        private PerformanceCounter _pcCompletedWorkItems;
        private Amib.Threading.Func<long> _getActiveThreads;
        private Amib.Threading.Func<long> _getInUseThreads;
        private Amib.Threading.Func<long> _getQueuedWorkItems;
        private Amib.Threading.Func<long> _getCompletedWorkItems;
        private long workItemsGenerated;
        private long workItemsCompleted;
        private DateTime startTime;
        private DateTime stopTime;

        private void InitializeGUIPerformanceCounters()
        {
            running = false;
            startTime = DateTime.MinValue;
            stopTime = DateTime.MaxValue;
            _useWindowsPerformanceCounters = InitializePerformanceCounters();
            if (_useWindowsPerformanceCounters)
            {
                InitializeWindowsPerformanceCounters();
            }
            else
            {
                InitializeLocalPerformanceCounters();
            }
        }

        private bool InitializePerformanceCounters()
        {
            if (!PerformanceCounterCategory.Exists("测试线程池"))
            {
                STPStartInfo stpStartInfo = new STPStartInfo();
                stpStartInfo.PerformanceCounterInstanceName = "测试线程池工具";

                SmartThreadPool stp = new SmartThreadPool(stpStartInfo);
                stp.Shutdown();

                if (!PerformanceCounterCategory.Exists("测试线程池"))
                {
                    //MessageBox.Show("创建性能监视器失败.\r\n如果你使用Windows Vista 或者Windows 7，请使用管理员权限执行本程序.\r\n\r\n将使用内置性能监视器.", "测试线程池工具", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                Process process = new Process();
                process.StartInfo.FileName = Application.ExecutablePath;

                try
                {
                    process.Start();
                }
                catch (Exception e)
                {
                    e.GetHashCode();
                    MessageBox.Show("未知错误.", "测试线程池工具");
                }

                return false;
            }

            return true;
        }

        private void InitializeWindowsPerformanceCounters()
        {
            this._pcActiveThreads = new System.Diagnostics.PerformanceCounter();
            this._pcInUseThreads = new System.Diagnostics.PerformanceCounter();
            this._pcQueuedWorkItems = new System.Diagnostics.PerformanceCounter();
            this._pcCompletedWorkItems = new System.Diagnostics.PerformanceCounter();
            this._pcActiveThreads.CategoryName = "测试线程池";
            this._pcActiveThreads.CounterName = "活跃线程";
            this._pcActiveThreads.InstanceName = "测试线程池工具";
            this._pcInUseThreads.CategoryName = "测试线程池";
            this._pcInUseThreads.CounterName = "使用中线程";
            this._pcInUseThreads.InstanceName = "测试线程池工具";
            this._pcQueuedWorkItems.CategoryName = "测试线程池";
            this._pcQueuedWorkItems.CounterName = "队列中线程";
            this._pcQueuedWorkItems.InstanceName = "测试线程池工具";
            this._pcCompletedWorkItems.CategoryName = "测试线程池";
            this._pcCompletedWorkItems.CounterName = "任务完成线程";
            this._pcCompletedWorkItems.InstanceName = "测试线程池工具";
            _getActiveThreads = () => (long)_pcActiveThreads.NextValue();
            _getInUseThreads = () => (long)_pcInUseThreads.NextValue();
            _getQueuedWorkItems = () => (long)_pcQueuedWorkItems.NextValue();
            _getCompletedWorkItems = () => (long)_pcCompletedWorkItems.NextValue();
        }

        private void InitializeLocalPerformanceCounters()
        {
            _getActiveThreads = () => _smartThreadPool.PerformanceCountersReader.ActiveThreads;
            _getInUseThreads = () => _smartThreadPool.PerformanceCountersReader.InUseThreads;
            _getQueuedWorkItems = () => _smartThreadPool.PerformanceCountersReader.WorkItemsQueued;
            _getCompletedWorkItems = () => _smartThreadPool.PerformanceCountersReader.WorkItemsProcessed;
        }
        private void materialRaisedButton11_Click(object sender, EventArgs e)
        {
            startTime = System.DateTime.Now;
            running = true;
            UpdateControls(true);

            workItemsCompleted = 0;
            workItemsGenerated = 0;

            STPStartInfo stpStartInfo = new STPStartInfo();
            stpStartInfo.IdleTimeout = Convert.ToInt32(materialSingleLineTextField4.Text) * 1000;
            stpStartInfo.MaxWorkerThreads = Convert.ToInt32(materialSingleLineTextField5.Text);
            stpStartInfo.MinWorkerThreads = 0;
            if (_useWindowsPerformanceCounters)
            {
                stpStartInfo.PerformanceCounterInstanceName = "测试线程池工具";
            }
            else
            {
                stpStartInfo.EnableLocalPerformanceCounters = true;
            }
            _smartThreadPool = new SmartThreadPool(stpStartInfo);
            _workItemsGroup = _smartThreadPool;

            workItemsProducerThread = new Thread(new ThreadStart(this.WorkItemsProducer));
            workItemsProducerThread.IsBackground = true;
            workItemsProducerThread.Start();
        }

        private void UpdateControls(bool bStart)
        {
            materialRaisedButton11.Visible = !bStart;
            materialRaisedButton12.Visible = bStart;
            materialFlatButton1.Visible = !bStart;
            timer2.Enabled = bStart;
            usageHistorySTP.Reset();
        }

        private void materialRaisedButton12_Click(object sender, EventArgs e)
        {
            stopTime = System.DateTime.Now;
            running = false;

            workItemsProducerThread.Join();
            _smartThreadPool.Shutdown(true, 1000);
            _smartThreadPool.Dispose();
            _smartThreadPool = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            UpdateControls(false);
        }

        private void WorkItemsProducer()
        {
            WorkItemCallback workItemCallback = new WorkItemCallback(this.DoWork);
            while (running)
            {
                IWorkItemsGroup workItemsGroup = _workItemsGroup;
                if (null == workItemsGroup)
                {
                    return;
                }

                try
                {
                    workItemCallback = new WorkItemCallback(this.DoWork);
                    workItemsGroup.QueueWorkItem(workItemCallback);
                }
                catch (ObjectDisposedException e)
                {
                    e.GetHashCode();
                    break;
                }
                workItemsGenerated++;
                Thread.Sleep(Convert.ToInt32(10));
            }
        }

        private object DoWork(object obj)
        {
            string url = materialSingleLineTextField7.Text;
            if (materialRadioButton9.Checked)
            {
                // DO NOTHING
            }
            else if (materialRadioButton8.Checked)
            {
                DoHttpTest("SimpleTest", url);
            }else if (materialRadioButton4.Checked)
            {
                DoHttpTest("NormalGet", url);
            }else if (materialRadioButton6.Checked)
            {
                DoHttpTest("FastGet", url);
            }else if (materialRadioButton5.Checked)
            {
                DoHttpTest("NormalHead", url);
            }
            else if (materialRadioButton7.Checked)
            {
                DoHttpTest("FastHead", url);
            }

            int nSleepTime = Convert.ToInt32(materialSingleLineTextField6.Text);
            if(nSleepTime <= 1)
            {
                nSleepTime = 1;
            }
            if(nSleepTime >= 10000)
            {
                nSleepTime = 10000;
            }
            Thread.Sleep(nSleepTime);
            Interlocked.Increment(ref workItemsCompleted);
            return null;
        }

        private void CloseThreadPool()
        {
            if (null != _smartThreadPool)
            {
                _smartThreadPool.Shutdown(true, 1000);
                _smartThreadPool = null;
                _workItemsGroup = null;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            SmartThreadPool stp = _smartThreadPool;
            if (null == stp)
            {
                return;
            }

            int threadsInUse = (int)_getInUseThreads();
            int threadsInPool = (int)_getActiveThreads();
            string workedTime = " 0 ";
            string workedPerfermance = " 0 ";
            long completedWorkItems = _getCompletedWorkItems();
            if (running)
            {
                TimeSpan ts = System.DateTime.Now.Subtract(startTime);
                workedTime = string.Format("{0} 分 {1} 秒 {2} 毫秒", ts.Minutes, ts.Seconds, ts.Milliseconds);
                workedPerfermance =  string.Format("{0:0.00}", ((double)completedWorkItems / ts.TotalSeconds));
            }
            else
            {
                TimeSpan ts = stopTime.Subtract(startTime);
                workedTime = string.Format("{0} 分 {1} 秒 {2} 毫秒", ts.Minutes, ts.Seconds, ts.Milliseconds);
                workedPerfermance = string.Format("{0:0.00}", ((double)completedWorkItems / ts.TotalSeconds));
            }

            string s = string.Format("任务池执行时间 {0} \r\n\r\n执行任务中进程 {1} 个\r\n线程池中总进程 {2} 个\r\n\r\n已发布任务 {3} 个，平均每秒 100 个\r\n已完成任务 {4} 个，平均每秒 {5} 个\r\n待执行任务 {6} 个",
                workedTime,
                threadsInUse.ToString(),
                threadsInPool.ToString(),
                workItemsGenerated.ToString(),
                completedWorkItems.ToString(),
                workedPerfermance,
                _getQueuedWorkItems().ToString());
            richTextBox1.Text = s;

            usageThreadsInPool.Value1 = threadsInUse;
            usageThreadsInPool.Value2 = threadsInPool;
            usageHistorySTP.AddValues(threadsInUse, threadsInPool);
        }

        #endregion

        #region 第四页半

        private void DoHttpTest(string strType, string url)
        {
            HttpTest.RunTest(typeof(MainForm), this, strType, url);
        }

        private delegate void setRichTexBox(string s);
        public void SetRichTextBox2Text(string txt)
        {
            if (this.richTextBox2.InvokeRequired)//等待异步
            {
                setRichTexBox fc = new setRichTexBox(SetRichTextBox2Text);
                this.Invoke(fc, new object[] { txt });
            }
            else
            {
                if (richTextBox2.Lines.Length > 9999)
                {
                    richTextBox2.Clear();
                }
                this.richTextBox2.AppendText(txt);
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            // 太影响性能了。。。
            //richTextBox2.SelectionStart = richTextBox2.TextLength;
            //richTextBox2.ScrollToCaret();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
        }

        #endregion

        private void SetTips(string str)
        {
            materialLabel2.Text = str;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(mainLogForm != null)
            {
                mainLogForm.Close();
            }
            CloseThreadPool();
        }
    }
}
