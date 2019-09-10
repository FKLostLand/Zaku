/* 
 * WRANING: These codes below is far away from bugs with the god and his animal protecting
 *                  _oo0oo_                   ┏┓　　　┏┓
 *                 o8888888o                ┏┛┻━━━┛┻┓
 *                 88" . "88                ┃　　　　　　　┃ 　
 *                 (| -_- |)                ┃　　　━　　　┃
 *                 0\  =  /0                ┃　┳┛　┗┳　┃
 *               ___/`---'\___              ┃　　　　　　　┃
 *             .' \\|     |# '.             ┃　　　┻　　　┃
 *            / \\|||  :  |||# \            ┃　　　　　　　┃
 *           / _||||| -:- |||||- \          ┗━┓　　　┏━┛
 *          |   | \\\  -  #/ |   |          　　┃　　　┃神兽保佑
 *          | \_|  ''\---/''  |_/ |         　　┃　　　┃永无BUG
 *          \  .-\__  '-'  ___/-. /         　　┃　　　┗━━━┓
 *        ___'. .'  /--.--\  `. .'___       　　┃　　　　　　　┣┓
 *     ."" '<  `.___\_<|>_/___.' >' "".     　　┃　　　　　　　┏┛
 *    | | :  `- \`.;`\ _ /`;.`/ - ` : | |   　　┗┓┓┏━┳┓┏┛
 *    \  \ `_.   \_ __\ /__ _/   .-` /  /   　　　┃┫┫　┃┫┫
 *=====`-.____`.___ \_____/___.-`___.-'=====　　　┗┻┛　┗┻┛ 
 *                  `=---='　　　
 *          佛祖保佑       永无BUG
 */
// =============================================================================== 
// Author              :    Frankie.W
// Create Time         :    2019/8/20 15:38:58
// Update Time         :    2019/8/20 15:38:58
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using BrightIdeasSoftware;
using HttpRequest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
// ===============================================================================
namespace Zaku
{
    class TaskGroup_FileCheck : ITaskGroup
    {
        public List<string> domains { get; private set; }
        public TaskGroupSetting globalSetting { get; set; }
        public Dictionary<String, List<String>> dirs { get; set; }
        public TaskGroup_FileCheck_Setting setting { get; set; }

        public HashSet<String> suffixDirs = new HashSet<String>();

        private int productIndex = 0;

        public TaskGroup_FileCheck()
        {
            this.Name = "网站页面扫描";
            this.taskGroupStatus = TaskGroupStatus.eTaskGroupStatus_Idle;
        }
        public override void Clear()
        {
            if (this.uiPanel != null && this.uiPanel is TaskGroup_FileCheck_Form)
            {
                ((TaskGroup_FileCheck_Form)uiPanel).Reset();
            }
            if (this.uiPanel != null && this.uiPanel.Parent != null)
            {
                this.uiPanel.Parent.Controls.Clear();
            }
            this.productIndex = 0;
            this.taskGroupStatus = TaskGroupStatus.eTaskGroupStatus_Idle;
        }

        public override string ExportData(object data, ResourceType type)
        {
            string strSuccessedFile = string.Empty;
            string strFailedFile = string.Empty;
            string strWorkdir = Directory.GetCurrentDirectory();
            ArrayList listViewData = (ArrayList)data;
            switch (type)
            {
                case ResourceType.eResourceType_Txt:
                    strSuccessedFile = strWorkdir + "\\Data\\" + this.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_成功.txt";
                    strFailedFile = strWorkdir + "\\Data\\" + this.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_失败.txt";

                    FileStream fs = new FileStream(strSuccessedFile, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);

                    FileStream fsf = new FileStream(strFailedFile, FileMode.Create);
                    StreamWriter swf = new StreamWriter(fsf);

                    {
                        foreach (var oneData in listViewData)
                        {
                            if (((TaskGroup_FileCheck_OneData)oneData).Successed)
                            {
                                sw.WriteLine(((TaskGroup_FileCheck_OneData)oneData).ToSuccessTxt());
                            }
                            else
                            {
                                swf.WriteLine(((TaskGroup_FileCheck_OneData)oneData).ToFailedTxt());
                            }
                        }
                    }
                    sw.Flush();
                    sw.Close();
                    swf.Flush();
                    swf.Close();
                    fs.Close();
                    fsf.Close();
                    break;
                case ResourceType.eResourceType_Csv:
                    strSuccessedFile = strWorkdir + "\\Data\\" + this.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".csv";
                    var csvHelper = new CsvHelper(",", false);
                    foreach (var oneData in listViewData)
                    {
                        csvHelper.AddRow();
                        csvHelper["Web路径"] = ((TaskGroup_FileCheck_OneData)oneData).url;
                        csvHelper["Http响应"] = ((TaskGroup_FileCheck_OneData)oneData).code;
                        csvHelper["文档类型"] = ((TaskGroup_FileCheck_OneData)oneData).contentType;
                        csvHelper["服务器"] = ((TaskGroup_FileCheck_OneData)oneData).server;
                        csvHelper["中间件"] = ((TaskGroup_FileCheck_OneData)oneData).powerBy;
                    }
                    File.WriteAllBytes(strSuccessedFile, csvHelper.ExportToBytes(true));
                    break;
                case ResourceType.eResourceType_MySQL:
                    strSuccessedFile = string.Empty;
                    // TODO:
                    break;
            }
            this.taskGroupStatus = TaskGroupStatus.eTaskGroupStatus_Idle;
            if (strSuccessedFile != string.Empty)
            {
                return Path.GetFileName(strSuccessedFile);
            }
            return string.Empty;
        }

        public override int ImportData(object data, ResourceType type)
        {
            if (this.taskGroupStatus != TaskGroupStatus.eTaskGroupStatus_InitComplete)
            {
                return 0;
            }
            int nSuccessedDataCnt = 0;
            switch (type)
            {
                case ResourceType.eResourceType_Txt:
                    if (data is string)
                    {
                        nSuccessedDataCnt = LoadUrlsFromTxtFile((string)data);
                    }
                    break;
                case ResourceType.eResourceType_Csv:
                    if (data is string)
                    {
                        nSuccessedDataCnt = LoadUrlsFromCsvFile((string)data);
                    }
                    break;
            }

            if (nSuccessedDataCnt > 0)
            {
                this.taskGroupStatus = TaskGroupStatus.eTaskGroupStatus_ImportDataComplete;
            }
            return nSuccessedDataCnt;
        }

        public override bool Init(Control container)
        {
            if (this.taskGroupStatus != TaskGroupStatus.eTaskGroupStatus_Idle)
            {
                GlobalVar.Instance.logger.Warn("当前任务状态为: " + taskGroupStatus);
                return false;
            }
            try
            {
                container.Controls.Clear();
                if (container == null)
                {
                    return false;
                }
                this.uiPanel = new TaskGroup_FileCheck_Form(this.Name);
                if (uiPanel is Form)
                {
                    ((Form)uiPanel).TopLevel = false;
                    ((Form)uiPanel).FormBorderStyle = FormBorderStyle.None;
                }

                uiPanel.Dock = DockStyle.Fill;
                container.Controls.Add(uiPanel);
                uiPanel.Show();

                this.domains = new List<string>();
                //this.dirs = new Dictionary<String, List<String>>();
                this.taskGroupStatus = TaskGroupStatus.eTaskGroupStatus_InitComplete;
            }
            catch (Exception e)
            {
                GlobalVar.Instance.logger.Error("创建Panel失败.", e);
                return false;
            }

            // 加载词典
            LoadDir();

            return true;
        }

        private void LoadDir()
        {
            if(this.uiPanel == null)
            {
                return;
            }

            Dictionary<String, List<String>> tmpDirs = ((TaskGroup_FileCheck_Form)uiPanel).InitDictList();
            this.dirs = new Dictionary<String, List<String>>(tmpDirs);
        }

        public override bool IsShow()
        {
            return true;
        }

        public override object OneTask_Do(object data)
        {
            if (data == null || !(data is string))
            {
                return null;
            }
            string strDomain = CommonTool.formatDomain((string)data);
            string strUA = GlobalVar.Instance.uaList.GetUA(globalSetting.UAType);
            TaskGroup_FileCheck_Data list = new TaskGroup_FileCheck_Data();

            foreach (String scanSuffix in suffixDirs)
            {
                string url = strDomain + scanSuffix;
                //GlobalVar.Instance.logger.Debug("checking: " + url);
                switch (this.setting.ScanType)
                {
                    case ENUM_ScanType.eScanType_Head:
                        TaskGroup_FileCheck_OneData oHeader = ScanRequestGetHeader(url);
                        if(oHeader.code == 404 || oHeader.code == 0)
                        {
                            continue;
                            //list.Add(oHeader);
                        }
                        else if (oHeader.code == 301 || oHeader.code == 302)
                        {
                            if (this.setting.IsShow301302)
                            {
                                list.Add(oHeader);
                            }
                        }
                        else if(oHeader.code == 500)
                        {
                            if (this.setting.IsShow500)
                            {
                                list.Add(oHeader);
                            }
                        }else
                        {
                            list.Add(oHeader);
                        }
                        break;
                    case ENUM_ScanType.eScanType_Get:
                        TaskGroup_FileCheck_OneData oBody = ScanRequestGetBody(url, strUA);
                        if (oBody.code == 404 || oBody.code == 0)
                        {
                            continue;
                            //list.Add(oBody);
                        }
                        else if (oBody.code == 301 || oBody.code == 302)
                        {
                            if (this.setting.IsShow301302)
                            {
                                list.Add(oBody);
                            }
                        }
                        else if (oBody.code == 500)
                        {
                            if (this.setting.IsShow500)
                            {
                                list.Add(oBody);
                            }
                        }
                        else
                        {
                            list.Add(oBody);
                        }
                        break;
                }
            }

            Thread.Sleep(globalSetting.IntervalTimeMS);
            return list;
        }

        public override void OneTask_Finished(Control listView, object dataList, TaskStatus reason)
        {
            if (dataList == null || !(dataList is TaskGroup_FileCheck_Data))
            {
                return;
            }
            if (listView == null || !(listView is BrightIdeasSoftware.FastObjectListView))
            {
                return;
            }
            var result = (TaskGroup_FileCheck_Data)dataList;
            if (reason != TaskStatus.eTaskStatus_Finish_Suceessed)
            {
                result.Status = reason;
                foreach (var i in result.Data)
                {
                    i.Successed = false;
                }
            }

            // 最终显示
            AddObjectToListViewDelegate dl = new AddObjectToListViewDelegate(AddObjectToListView);
            ((BrightIdeasSoftware.FastObjectListView)listView).BeginInvoke(dl, new Object[] { ((BrightIdeasSoftware.FastObjectListView)listView), result.Data });

            this.taskGroupStatus = TaskGroupStatus.eTaskGroupStatus_WaitingExportData;
        }

        public delegate void AddObjectToListViewDelegate(FastObjectListView lv, List<TaskGroup_FileCheck_OneData> l);
        private void AddObjectToListView(FastObjectListView lv, List<TaskGroup_FileCheck_OneData> l)
        {
            lv.AddObjects(l);
        }

        public override object OneTask_Produce()
        {
            string s = string.Empty;
            if (productIndex < domains.Count)
            {
                s = domains[productIndex];
                productIndex++;
                return s;
            }
            return null;
        }
        public override bool PrepareDo(Control listView, TaskGroupSetting s)
        {
            if (listView == null || !(listView is FastObjectListView))
            {
                return false;
            }
            if (this.uiPanel == null || !(uiPanel is TaskGroup_FileCheck_Form))
            {
                return false;
            }
            this.taskGroupStatus = TaskGroupStatus.eTaskGroupStatus_Doing;
            initPrivateSetting();
            initPublicSetting(s);
            initListView(listView);

            return true;
        }
        public override bool Stop()
        {
            return true;
        }

        private int LoadUrlsFromTxtFile(string file)
        {
            int nSuccessedCnt = 0;
            FileStream fileStream = null;
            StreamReader streamReader = null;
            try
            {
                fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
                streamReader = new StreamReader(fileStream, Encoding.Default);
                fileStream.Seek(0, SeekOrigin.Begin);

                string content = streamReader.ReadLine();
                while (content != null)
                {
                    if (AddUrl(content))
                    {
                        nSuccessedCnt++;
                    }
                    content = streamReader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                GlobalVar.Instance.logger.Error("读取文件失败 ", ex);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
                if (streamReader != null)
                {
                    streamReader.Close();
                }
            }
            return nSuccessedCnt;
        }
        private bool AddUrl(string url)
        {
            url = url.Replace(" ", "");
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }
            int oldCnt = domains.Count;
            domains.Add(url);
            domains = domains.Distinct().ToList();
            int newCnt = domains.Count;
            return !(oldCnt == newCnt);
        }
        private int LoadUrlsFromCsvFile(string file)
        {
            List<string> row = new List<string>();
            using (var reader = new CsvFileReader(file))
            {
                // 第一行头部
                if (!reader.ReadRow(row))
                {
                    return 0;
                }
                int pos = -1;
                foreach (string cell in row)
                {
                    pos++;
                    if (string.Equals(cell, "域名"))
                    {
                        break;
                    }
                }
                if (pos < 0)
                {
                    return 0;
                }

                int nSuccessedCnt = 0;
                while (reader.ReadRow(row))
                {
                    if (row.Count <= pos)
                    {
                        break;
                    }
                    if (AddUrl(row[pos]))
                    {
                        nSuccessedCnt++;
                    }
                }
                return nSuccessedCnt;
            }
        }

        private bool initPrivateSetting()
        {
            if (setting == null)
                setting = new TaskGroup_FileCheck_Setting();

            MaterialSkin.Controls.MaterialCheckBox t = ((TaskGroup_FileCheck_Form)uiPanel).Controls.Find("materialCheckBox1", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsShow301302 = t.Checked;
            t = ((TaskGroup_FileCheck_Form)uiPanel).Controls.Find("materialCheckBox2", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsShow500 = t.Checked;
            t = ((TaskGroup_FileCheck_Form)uiPanel).Controls.Find("materialCheckBox3", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsGetServerType = t.Checked;
            t = ((TaskGroup_FileCheck_Form)uiPanel).Controls.Find("materialCheckBox4", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsGetMiddleWare = t.Checked;

            MaterialSkin.Controls.MaterialRadioButton rd = ((TaskGroup_FileCheck_Form)uiPanel).Controls.Find("materialRadioButton1", true).FirstOrDefault() as MaterialSkin.Controls.MaterialRadioButton;
            if (rd != null && rd.Checked)
                setting.ScanType = ENUM_ScanType.eScanType_Head;
            rd = ((TaskGroup_FileCheck_Form)uiPanel).Controls.Find("materialRadioButton2", true).FirstOrDefault() as MaterialSkin.Controls.MaterialRadioButton;
            if (rd != null && rd.Checked)
                setting.ScanType = ENUM_ScanType.eScanType_Get;

            CheckedListBox clb = ((TaskGroup_FileCheck_Form)uiPanel).Controls.Find("checkedListBox_dict", true).FirstOrDefault() as CheckedListBox;
            if (clb.SelectedItems.Count <= 0)
            {
                MessageBox.Show("请选择扫描字典！");
                return false;
            }
            // 合并字典
            suffixDirs.Clear();
            foreach (String dir in clb.CheckedItems)
            {
                String cdir = dir.Split('|')[0];
                List<String> clist = null;
                Boolean isget = dirs.TryGetValue(cdir, out clist);
                foreach (String scanDir in clist)
                {
                    if (!suffixDirs.Contains(scanDir))
                    {
                        suffixDirs.Add(scanDir);
                    }
                }
            }
            return true;
        }
        private bool initPublicSetting(TaskGroupSetting s)
        {
            if (globalSetting == null)
                globalSetting = new TaskGroupSetting();
            globalSetting.HardCopy(s);
            return true;
        }
        private bool initListView(Control listView)
        {
            ((FastObjectListView)listView).ClearObjects();
            ((FastObjectListView)listView).Columns.Clear();

            OLVColumn col1 = new OLVColumn();
            col1.Text = "Web路径[链接]";
            col1.UseInitialLetterForGroup = true;
            col1.Hyperlink = true;
            col1.Width = 300;
            col1.AspectGetter = delegate (object x) { return ((TaskGroup_FileCheck_OneData)x).url; };
            ((FastObjectListView)listView).Columns.Add(col1);

            OLVColumn col2 = new OLVColumn();
            col2.Text = "Http响应";
            col2.UseInitialLetterForGroup = true;
            col2.Width = 80;
            col2.AspectGetter = delegate (object x) { return ((TaskGroup_FileCheck_OneData)x).code; };
            ((FastObjectListView)listView).Columns.Add(col2);

            OLVColumn col3 = new OLVColumn();
            col3.Text = "文档类型";
            col3.UseInitialLetterForGroup = true;
            col3.Width = 80;
            col3.AspectGetter = delegate (object x) { return ((TaskGroup_FileCheck_OneData)x).contentType; };
            ((FastObjectListView)listView).Columns.Add(col3);

            OLVColumn col4 = new OLVColumn();
            col4.Text = "长度";
            col4.UseInitialLetterForGroup = true;
            col4.Width = 60;
            col4.AspectGetter = delegate (object x) { return ((TaskGroup_FileCheck_OneData)x).length; };
            ((FastObjectListView)listView).Columns.Add(col4);

            OLVColumn col5 = new OLVColumn();
            col5.Text = "服务器";
            col5.UseInitialLetterForGroup = true;
            col5.Width = 120;
            col5.AspectGetter = delegate (object x) { return ((TaskGroup_FileCheck_OneData)x).server; };
            ((FastObjectListView)listView).Columns.Add(col5);

            OLVColumn col6 = new OLVColumn();
            col6.Text = "中间件";
            col6.UseInitialLetterForGroup = true;
            col6.Width = 120;
            col6.AspectGetter = delegate (object x) { return ((TaskGroup_FileCheck_OneData)x).powerBy; };
            ((FastObjectListView)listView).Columns.Add(col6);

            OLVColumn col7 = new OLVColumn();
            col7.Text = "响应时间";
            col7.UseInitialLetterForGroup = true;
            col7.Width = 120;
            col7.AspectGetter = delegate (object x) { return ((TaskGroup_FileCheck_OneData)x).time; };
            ((FastObjectListView)listView).Columns.Add(col7);

            return true;
        }


        private TaskGroup_FileCheck_OneData ScanRequestGetHeader(string url)
        {
            Stopwatch st = new Stopwatch();
            st.Start();

            TaskGroup_FileCheck_OneData result = new TaskGroup_FileCheck_OneData(url);

            HttpHelper.HttpResult hr = HttpHelper.HeadInfo(url);
            if (hr.Result != null && hr.IsCompleted)
            {
                result.contentType = hr.ContentType;
                result.length = hr.ContentLength;
                result.server = hr.Server;
                result.code = (int)hr.StatusCode ;
                result.powerBy = hr.Header.Get("X-Powered-By");
            }
            result.time = st.ElapsedMilliseconds;
            return result;
        }
        private TaskGroup_FileCheck_OneData ScanRequestGetBody(string url, string userAgent)
        {
            Stopwatch st = new Stopwatch();
            st.Start();

            TaskGroup_FileCheck_OneData result = new TaskGroup_FileCheck_OneData(url);

            HttpHelper.HttpParam hp = new HttpHelper.HttpParam()
            {
                URL = url,
                UserAgent = userAgent,
                ContentType = "application/json;charset=unicode",
                Timeout = TimeSpan.FromSeconds(1),
                Method = HttpHelper.HttpVerb.Get,
                AllowAutoRedirect = false,
                Accept = "*/*",
                Referer = "http://www.baidu.com/",
            };

            HttpHelper.HttpResult hr = HttpHelper.Request(hp);
            if (hr.Result != null)
            {
                result.contentType = hr.ContentType;
                result.length = hr.ContentLength;
                result.server = hr.Server;
                result.code = (int)hr.StatusCode;
                result.powerBy = hr.Header.Get("X-Powered-By");
            }

            result.time = st.ElapsedMilliseconds;
            return result;
        }
    }
}