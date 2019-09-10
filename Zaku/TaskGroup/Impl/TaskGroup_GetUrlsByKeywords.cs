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
// Create Time         :    2019/8/2 10:48:19
// Update Time         :    2019/8/2 10:48:19
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using BrightIdeasSoftware;
using HttpRequest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Zaku.TaskGroup.ImplForm;
using HtmlAgilityPack;
using System.Net.Http;
// ===============================================================================
namespace Zaku
{
    class TaskGroup_GetUrlsByKeywords : ITaskGroup
    {
        public List<string> keywords { get; private set; }
        public TaskGroup_GetUrlsByKeywords_Setting setting { get; set; }
        public TaskGroupSetting globalSetting { get; set; }
        private int productIndex = 0;
        public TaskGroup_GetUrlsByKeywords()
        {
            this.Name = "关键词采集域名";
            this.taskGroupStatus = TaskGroupStatus.eTaskGroupStatus_Idle;
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
            string strKeyword = (string)data;
            string strUA = GlobalVar.Instance.uaList.GetUA(globalSetting.UAType);
            TaskGroup_GetUrlsByKeywords_Data list = new TaskGroup_GetUrlsByKeywords_Data();
            //GlobalVar.Instance.logger.Debug("DO BEGIN");
            if (this.setting.IsUseBaiduSearch)
            {
                List<string> result = BaiduSearch(strKeyword, strUA);
                result = result.Distinct().ToList();
                if (result != null && result.Count > 0)
                {
                    list.Status = TaskStatus.eTaskStatus_Finish_Suceessed;
                    foreach (string url in result)
                    {
                        TaskGroup_GetUrlsByKeywords_OneData d = new TaskGroup_GetUrlsByKeywords_OneData(url);
                        d.Keyword = strKeyword;
                        d.Successed = true;
                        d.eSource = GetUrlsSource.eGetUrlsSource_Baidu;
                        list.Add(d);
                    }
                }
            }
            //GlobalVar.Instance.logger.Debug($"DO END [{list.Count}]");
            Thread.Sleep(globalSetting.IntervalTimeMS);

            // 数据保护
            if (list.Count() <= 0)
            {
                TaskGroup_GetUrlsByKeywords_OneData d = new TaskGroup_GetUrlsByKeywords_OneData("");
                d.Keyword = strKeyword;
                d.eSource = GetUrlsSource.eGetUrlsSource_Unknown;
                d.Successed = false;
                list.Add(d);

                list.Status = TaskStatus.eTaskStatus_Finish_Error;
            }

            return list;
        }
        public override object OneTask_Produce()
        {
            string s = string.Empty;
            if (productIndex < keywords.Count)
            {
                s = keywords[productIndex];
                productIndex++;
                return s;
            }
            return null;
        }
        public override void OneTask_Finished(Control listView, object dataList, TaskStatus reason)
        {
            if (dataList == null || !(dataList is TaskGroup_GetUrlsByKeywords_Data))
            {
                return;
            }
            if (listView == null || !(listView is BrightIdeasSoftware.FastObjectListView))
            {
                return;
            }
            var result = (TaskGroup_GetUrlsByKeywords_Data)dataList;
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
        public delegate void AddObjectToListViewDelegate(FastObjectListView lv, List<TaskGroup_GetUrlsByKeywords_OneData> l);
        private void AddObjectToListView(FastObjectListView lv, List<TaskGroup_GetUrlsByKeywords_OneData> l)
        {
            lv.AddObjects(l);
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
                            if (((TaskGroup_GetUrlsByKeywords_OneData)oneData).Successed)
                            {
                                sw.WriteLine(((TaskGroup_GetUrlsByKeywords_OneData)oneData).ToSuccessedTxt());
                            }
                            else
                            {
                                swf.WriteLine(((TaskGroup_GetSimilarKeywords_OneData)oneData).ToFailedTxt());
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
                        csvHelper["域名"] = ((TaskGroup_GetUrlsByKeywords_OneData)oneData).Url;
                        csvHelper["关键词"] = ((TaskGroup_GetUrlsByKeywords_OneData)oneData).Keyword;
                        csvHelper["状态"] = ((TaskGroup_GetUrlsByKeywords_OneData)oneData).Successed;
                        csvHelper["来源"] = EnumString.GetStringValue(((TaskGroup_GetUrlsByKeywords_OneData)oneData).eSource);
                        csvHelper["百度PC指数"] = ((TaskGroup_GetUrlsByKeywords_OneData)oneData).BaiduPCIndex;
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
                        nSuccessedDataCnt = LoadKeywordsFromTxtFile((string)data);
                    }
                    break;
                case ResourceType.eResourceType_Csv:
                    if (data is string)
                    {
                        nSuccessedDataCnt = LoadKeywordsFromCsvFile((string)data);
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
                this.uiPanel = new TaskGroup_GetUrlsByKeywords_Form(this.Name);
                if (uiPanel is Form)
                {
                    ((Form)uiPanel).TopLevel = false;
                    ((Form)uiPanel).FormBorderStyle = FormBorderStyle.None;
                }

                uiPanel.Dock = DockStyle.Fill;
                container.Controls.Add(uiPanel);
                uiPanel.Show();

                this.keywords = new List<string>();

                this.taskGroupStatus = TaskGroupStatus.eTaskGroupStatus_InitComplete;
            }
            catch (Exception e)
            {
                GlobalVar.Instance.logger.Error("创建Panel失败.", e);
                return false;
            }
            return true;
        }

        public override void Clear()
        {
            if (this.uiPanel != null && this.uiPanel is TaskGroup_GetUrlsByKeywords_Form)
            {
                ((TaskGroup_GetUrlsByKeywords_Form)uiPanel).Reset();
            }
            if (this.uiPanel != null && this.uiPanel.Parent != null)
            {
                this.uiPanel.Parent.Controls.Clear();
            }
            this.productIndex = 0;
            this.taskGroupStatus = TaskGroupStatus.eTaskGroupStatus_Idle;
        }

        public override bool Stop()
        {
            return true;
        }

        public override bool PrepareDo(Control listView, TaskGroupSetting s)
        {
            if (listView == null || !(listView is FastObjectListView))
            {
                return false;
            }
            if (this.uiPanel == null || !(uiPanel is TaskGroup_GetUrlsByKeywords_Form))
            {
                return false;
            }
            this.taskGroupStatus = TaskGroupStatus.eTaskGroupStatus_Doing;
            initPrivateSetting();
            initPublicSetting(s);
            initListView(listView);

            return true;
        }

        private bool initPrivateSetting()
        {
            if (setting == null)
                setting = new TaskGroup_GetUrlsByKeywords_Setting();

            MaterialSkin.Controls.MaterialCheckBox t = ((TaskGroup_GetUrlsByKeywords_Form)uiPanel).Controls.Find("materialCheckBox_GetRank", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsGetBaiduRank = t.Checked;
            t = ((TaskGroup_GetUrlsByKeywords_Form)uiPanel).Controls.Find("materialCheckBox_TopLevelDomain", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsTopLevelDomain = t.Checked;

            t = ((TaskGroup_GetUrlsByKeywords_Form)uiPanel).Controls.Find("materialCheckBox5", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsUseBaiduSearch = t.Checked;
            t = ((TaskGroup_GetUrlsByKeywords_Form)uiPanel).Controls.Find("materialCheckBox6", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsUse360Search = t.Checked;
            t = ((TaskGroup_GetUrlsByKeywords_Form)uiPanel).Controls.Find("materialCheckBox7", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsUseSougouSearch = t.Checked;
            t = ((TaskGroup_GetUrlsByKeywords_Form)uiPanel).Controls.Find("materialCheckBox8", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsUseBingSearch = t.Checked;
            t = ((TaskGroup_GetUrlsByKeywords_Form)uiPanel).Controls.Find("materialCheckBox9", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsUseShenmaSearch = t.Checked;
            t = ((TaskGroup_GetUrlsByKeywords_Form)uiPanel).Controls.Find("materialCheckBox10", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsUseGoogleSearch = t.Checked;

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
            col1.Text = "域名";
            col1.UseInitialLetterForGroup = true;
            col1.Width = 180;
            col1.AspectGetter = delegate (object x) { return ((TaskGroup_GetUrlsByKeywords_OneData)x).Url; };
            ((FastObjectListView)listView).Columns.Add(col1);

            OLVColumn col2 = new OLVColumn();
            col2.Text = "关键词";
            col2.UseInitialLetterForGroup = true;
            col2.Width = 140;
            col2.AspectGetter = delegate (object x) { return ((TaskGroup_GetUrlsByKeywords_OneData)x).Keyword; };
            ((FastObjectListView)listView).Columns.Add(col2);

            OLVColumn col3 = new OLVColumn();
            col3.Text = "状态";
            col3.UseInitialLetterForGroup = false;
            col3.Width = 80;
            col3.AspectGetter = delegate (object x) {
                if (((TaskGroup_GetUrlsByKeywords_OneData)x).Successed)
                {
                    return "成功";
                }
                else { return "失败"; }
            };
            ((FastObjectListView)listView).Columns.Add(col3);

            OLVColumn col4 = new OLVColumn();
            col4.Width = 68;
            col4.Text = "来源";
            col4.TextAlign = HorizontalAlignment.Center;
            col4.AspectGetter = delegate (object x) { return (EnumString.GetStringValue(((TaskGroup_GetUrlsByKeywords_OneData)x).eSource)); };
            ((FastObjectListView)listView).Columns.Add(col4);

            OLVColumn col5 = new OLVColumn();
            col5.Text = "百度PC指数";
            col5.Width = 100;
            col5.TextAlign = HorizontalAlignment.Center;
            col5.Renderer = new MultiImageRenderer(Properties.Resources.star16, 5, 0, 10);
            col5.AspectGetter = delegate (object x) { return ((TaskGroup_GetUrlsByKeywords_OneData)x).BaiduPCIndex.ToString(); };
            ((FastObjectListView)listView).Columns.Add(col5);

            return true;
        }


        private List<string> BaiduSearch(string keyword, string userAgent)
        {
            string url = "http://www.baidu.com/s?ie=utf-8&wd=" + keyword + "&rn=50";
            //GlobalVar.Instance.logger.Debug("Ready to search : " + url);
            List<string> result = null;

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
                var sr = new StreamReader(hr.Result, Encoding.Default);
                if (sr != null)
                {
                    string html = sr.ReadToEnd();
                    if (!string.IsNullOrEmpty(html))
                    {
                        //GlobalVar.Instance.logger.Debug("HTML length = " + html.Length);
                        ParserBaiduSearchResult(html, out result);
                    }
                }
                hr.Result.Close();
            }
            return result;
        }

        private int LoadKeywordsFromTxtFile(string file)
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
                    if (AddKeyword(content))
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
        private bool AddKeyword(string word)
        {
            word = word.Replace(" ", "");
            if (word.Length > 8 || string.IsNullOrEmpty(word))
            {
                return false;
            }
            int oldCnt = keywords.Count;
            keywords.Add(word);
            keywords = keywords.Distinct().ToList();
            int newCnt = keywords.Count;
            return !(oldCnt == newCnt);
        }
        private int LoadKeywordsFromCsvFile(string file)
        {
            List<string> row = new List<string>();
            using (var reader = new CsvFileReader(file))
            {
                // 第一行头部
                if (!reader.ReadRow(row)){
                    return 0;
                }
                int pos = -1;
                foreach (string cell in row)
                {
                    pos++;
                    if (string.Equals(cell, "关键词"))
                    {
                        break;
                    }
                }
                if(pos < 0)
                {
                    return 0;
                }

                int nSuccessedCnt = 0;
                while (reader.ReadRow(row))
                {
                    if(row.Count <= pos)
                    {
                        break;
                    }
                    if (AddKeyword(row[pos]))
                    {
                        nSuccessedCnt++;
                    }
                }
                return nSuccessedCnt;
            }
        }

        private string GetLocation(string URL)
        {
            try
            {
                HttpClientHandler hander = new HttpClientHandler();
                hander.AllowAutoRedirect = false;
                HttpClient client = new HttpClient(hander);
                return client.GetAsync(URL).Result.Headers.Location.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        private void ParserBaiduSearchResult(string strHtml, out List<string> urls)
        {
            urls = new List<string>();

            try
            {
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(strHtml);

                for (int i = 1; i <= 50; i++)
                {
                    HtmlNode titleNode = doc.DocumentNode.SelectSingleNode("//div[@id='" + i.ToString() + "']/h3/a");
                    if (titleNode != null)
                    {
                        string tmp = GetLocation(titleNode.Attributes["href"].Value);
                        //GlobalVar.Instance.logger.Debug("href length = " + tmp.Length);
                        if (!string.IsNullOrEmpty(tmp))
                        {
                            Uri u = new Uri(tmp);
                            if (!string.IsNullOrEmpty(u.Host))
                            {
                                if (this.setting.IsTopLevelDomain)
                                {
                                    urls.Add(GetTopLevelDomain(u.Host));
                                }
                                else
                                {
                                    urls.Add(u.Host);
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public static bool IsNumeric(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            int len = value.Length;
            if ('-' != value[0] && '+' != value[0] && !char.IsNumber(value[0]))
            {
                return false;
            }
            for (int i = 1; i < len; i++)
            {
                if (!char.IsNumber(value[i]))
                {
                    return false;
                }
            }
            return true;
        }
        public string GetTopLevelDomain(string domain)
        {
            string str = domain;
            if (str.IndexOf(".") > 0)
            {
                string[] strArr = str.Split(':')[0].Split('.');
                if (IsNumeric(strArr[strArr.Length - 1]))
                {
                    return str;
                }
                else
                {
                    string domainRules = "||com.cn|net.cn|org.cn|gov.cn|com.hk|公司|中国|网络|com|net|org|int|edu|gov|mil|arpa|Asia|biz|info|name|pro|coop|aero|museum|ac|ad|ae|af|ag|ai|al|am|an|ao|aq|ar|as|at|au|aw|az|ba|bb|bd|be|bf|bg|bh|bi|bj|bm|bn|bo|br|bs|bt|bv|bw|by|bz|ca|cc|cf|cg|ch|ci|ck|cl|cm|cn|co|cq|cr|cu|cv|cx|cy|cz|de|dj|dk|dm|do|dz|ec|ee|eg|eh|es|et|ev|fi|fj|fk|fm|fo|fr|ga|gb|gd|ge|gf|gh|gi|gl|gm|gn|gp|gr|gt|gu|gw|gy|hk|hm|hn|hr|ht|hu|id|ie|il|in|io|iq|ir|is|it|jm|jo|jp|ke|kg|kh|ki|km|kn|kp|kr|kw|ky|kz|la|lb|lc|li|lk|lr|ls|lt|lu|lv|ly|ma|mc|md|me|mg|mh|ml|mm|mn|mo|mp|mq|mr|ms|mt|mv|mw|mx|my|mz|na|nc|ne|nf|ng|ni|nl|no|np|nr|nt|nu|nz|om|pa|pe|pf|pg|ph|pk|pl|pm|pn|pr|pt|pw|py|qa|re|ro|ru|rw|sa|sb|sc|sd|se|sg|sh|si|sj|sk|sl|sm|sn|so|sr|st|su|sy|sz|tc|td|tf|tg|th|tj|tk|tm|tn|to|tp|tr|tt|tv|tw|tz|ua|ug|uk|us|uy|va|vc|ve|vg|vn|vu|wf|ws|ye|yu|za|zm|zr|zw|";
                    string tempDomain;
                    if (strArr.Length >= 4)
                    {
                        tempDomain = strArr[strArr.Length - 3] + "." + strArr[strArr.Length - 2] + "." + strArr[strArr.Length - 1];
                        if (domainRules.IndexOf("|" + tempDomain + "|") > 0)
                        {
                            return strArr[strArr.Length - 4] + "." + tempDomain;
                        }
                    }
                    if (strArr.Length >= 3)
                    {
                        tempDomain = strArr[strArr.Length - 2] + "." + strArr[strArr.Length - 1];
                        if (domainRules.IndexOf("|" + tempDomain + "|") > 0)
                        {
                            return strArr[strArr.Length - 3] + "." + tempDomain;
                        }
                    }
                    if (strArr.Length >= 2)
                    {
                        tempDomain = strArr[strArr.Length - 1];
                        if (domainRules.IndexOf("|" + tempDomain + "|") > 0)
                        {
                            return strArr[strArr.Length - 2] + "." + tempDomain;
                        }
                    }
                }
            }
            return str;
        }

    }
}