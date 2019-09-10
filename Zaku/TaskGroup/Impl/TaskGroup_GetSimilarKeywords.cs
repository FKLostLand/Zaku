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
// Create Time         :    2019/8/1 16:50:06
// Update Time         :    2019/8/1 16:50:06
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using BrightIdeasSoftware;
using HttpRequest;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
// ===============================================================================
namespace Zaku
{
    class TaskGroup_GetSimilarKeywords : ITaskGroup
    {
        public List<string> baseKeywords { get; private set; }
        public TaskGroup_GetSimilarKeywords_Setting setting { get; set; }
        public TaskGroupSetting globalSetting { get; set; }

        private int productIndex = 0;


        public TaskGroup_GetSimilarKeywords()
        {
            this.Name = "获取相似关联词";
            this.taskGroupStatus = TaskGroupStatus.eTaskGroupStatus_Idle;
        }

        public override bool IsShow()
        {
            return true;
        }
        private bool initPrivateSetting()
        {
            if (setting == null)
                setting = new TaskGroup_GetSimilarKeywords_Setting();

            MaterialSkin.Controls.MaterialCheckBox t = ((TaskGroup_GetSimilarKeywords_Form)uiPanel).Controls.Find("materialCheckBox_GetRank", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsGetBaiduRank = t.Checked;
            t = ((TaskGroup_GetSimilarKeywords_Form)uiPanel).Controls.Find("materialCheckBox_StopUntilCount", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsStopUntilCount = t.Checked;

            MaterialSkin.Controls.MaterialSingleLineTextField l = ((TaskGroup_GetSimilarKeywords_Form)uiPanel).Controls.Find("materialSingleLineTextField_StopUntilCount", true).FirstOrDefault() as MaterialSkin.Controls.MaterialSingleLineTextField;
            if (l != null)
            {
                setting.StopCount = Convert.ToInt32(l.Text);
                if (setting.StopCount <= 0)
                {
                    setting.IsStopUntilCount = false;
                }
            }

            t = ((TaskGroup_GetSimilarKeywords_Form)uiPanel).Controls.Find("materialCheckBox5", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsUseBaiduSearch = t.Checked;
            t = ((TaskGroup_GetSimilarKeywords_Form)uiPanel).Controls.Find("materialCheckBox6", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsUse360Search = t.Checked;
            t = ((TaskGroup_GetSimilarKeywords_Form)uiPanel).Controls.Find("materialCheckBox7", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsUseSougouSearch = t.Checked;
            t = ((TaskGroup_GetSimilarKeywords_Form)uiPanel).Controls.Find("materialCheckBox8", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsUseBingSearch = t.Checked;
            t = ((TaskGroup_GetSimilarKeywords_Form)uiPanel).Controls.Find("materialCheckBox9", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsUseShenmaSearch = t.Checked;
            t = ((TaskGroup_GetSimilarKeywords_Form)uiPanel).Controls.Find("materialCheckBox10", true).FirstOrDefault() as MaterialSkin.Controls.MaterialCheckBox;
            if (t != null)
                setting.IsUseGoogleSearch = t.Checked;

            return true;
        }
        private bool initListView(Control listView)
        {
            ((FastObjectListView)listView).ClearObjects();
            ((FastObjectListView)listView).Columns.Clear();

            OLVColumn col1 = new OLVColumn();
            col1.Text = "关键词";
            col1.UseInitialLetterForGroup = true;
            col1.Width = 140;
            col1.AspectGetter = delegate (object x) { return ((TaskGroup_GetSimilarKeywords_OneData)x).Word; };
            ((FastObjectListView)listView).Columns.Add(col1);

            OLVColumn col2 = new OLVColumn();
            col2.Text = "基础词";
            col2.UseInitialLetterForGroup = true;
            col2.Width = 140;
            col2.AspectGetter = delegate (object x) { return ((TaskGroup_GetSimilarKeywords_OneData)x).BaseKeyword; };
            ((FastObjectListView)listView).Columns.Add(col2);

            OLVColumn col3 = new OLVColumn();
            col3.Text = "状态";
            col3.UseInitialLetterForGroup = false;
            col3.Width = 80;
            col3.AspectGetter = delegate (object x) {
                if (((TaskGroup_GetSimilarKeywords_OneData)x).Successed){
                    return "成功";
                }
                else{ return "失败"; }
            };
            ((FastObjectListView)listView).Columns.Add(col3);

            OLVColumn col4 = new OLVColumn();
            col4.Width = 68;
            col4.Text = "来源";
            col4.TextAlign = HorizontalAlignment.Center;
            col4.AspectGetter = delegate (object x) { return (EnumString.GetStringValue(((TaskGroup_GetSimilarKeywords_OneData)x).eSource)); };
            ((FastObjectListView)listView).Columns.Add(col4);

            OLVColumn col5 = new OLVColumn();
            col5.Text = "百度PC指数";
            col5.Width = 100;
            col5.TextAlign = HorizontalAlignment.Center;
            col5.Renderer = new MultiImageRenderer(Properties.Resources.star16, 5, 0, 10);
            col5.AspectGetter = delegate (object x) { return ((TaskGroup_GetSimilarKeywords_OneData)x).BaiduPCIndex.ToString(); };
            ((FastObjectListView)listView).Columns.Add(col5);

            return true;
        }
        private bool initPublicSetting(TaskGroupSetting s)
        {
            if(globalSetting == null)
                globalSetting = new TaskGroupSetting();
            globalSetting.HardCopy(s);
            return true;
        }
        public override bool PrepareDo(Control listView, TaskGroupSetting s)
        {
            if (listView == null || !(listView is FastObjectListView))
            {
                return false;
            }
            if(this.uiPanel == null || !(uiPanel is TaskGroup_GetSimilarKeywords_Form))
            {
                return false;
            }

            this.taskGroupStatus = TaskGroupStatus.eTaskGroupStatus_Doing;
            initPrivateSetting();
            initPublicSetting(s);
            initListView(listView);

            return true;
        }
        public override object OneTask_Produce()
        {
            string s = string.Empty;
            if(productIndex < baseKeywords.Count)
            {
                s = baseKeywords[productIndex];
                productIndex++;
                return s;
            }
            return null;
        }
        public override object OneTask_Do(object data)
        {
            if (data == null || !(data is string))
            {
                return null;
            }
            string strBaseKeyword = (string)data;
            string strUA = GlobalVar.Instance.uaList.GetUA(globalSetting.UAType);
            TaskGroup_GetSimilarKeywords_Data list = new TaskGroup_GetSimilarKeywords_Data();

            //GlobalVar.Instance.logger.Debug("DO BEGIN");
            if (this.setting.IsUseBaiduSearch)
            {
                List<string> result = BaiduSearch(strBaseKeyword, strUA);
                result = result.Distinct().ToList();
                if (result != null && result.Count > 0)
                {
                    list.Status = TaskStatus.eTaskStatus_Finish_Suceessed;
                    foreach (string similarWord in result)
                    {
                        TaskGroup_GetSimilarKeywords_OneData d = new TaskGroup_GetSimilarKeywords_OneData(similarWord);
                        d.BaseKeyword = strBaseKeyword;
                        d.Successed = true;
                        d.eSource = SimilarKeywordSource.eSimilarKeywordSource_Baidu;
                        list.Add(d);
                    }
                }
            }
            //GlobalVar.Instance.logger.Debug($"DO END [{list.Count}]");
            Thread.Sleep(globalSetting.IntervalTimeMS);

            // 数据保护
            if(list.Count() <= 0)
            {
                TaskGroup_GetSimilarKeywords_OneData d = new TaskGroup_GetSimilarKeywords_OneData(strBaseKeyword);
                d.BaseKeyword = "";
                d.eSource = SimilarKeywordSource.eSimilarKeywordSource_Unknown;
                d.Successed = false;
                list.Add(d);

                list.Status = TaskStatus.eTaskStatus_Finish_Error;
            }

            return list;
        }
        public override void OneTask_Finished(Control listView, object dataList, TaskStatus reason)
        {
            if (dataList == null || !(dataList is TaskGroup_GetSimilarKeywords_Data))
            {
                return;
            }
            if(listView == null || !(listView is BrightIdeasSoftware.FastObjectListView))
            {
                return;
            }
            var result = (TaskGroup_GetSimilarKeywords_Data)dataList;
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
            ((BrightIdeasSoftware.FastObjectListView)listView).BeginInvoke(dl, new Object[] { ((BrightIdeasSoftware.FastObjectListView)listView) , result.Data });

            this.taskGroupStatus = TaskGroupStatus.eTaskGroupStatus_WaitingExportData;
        }

        public delegate void AddObjectToListViewDelegate(FastObjectListView lv, List<TaskGroup_GetSimilarKeywords_OneData> l);
        private void AddObjectToListView(FastObjectListView lv, List<TaskGroup_GetSimilarKeywords_OneData> l)
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
                            if (((TaskGroup_GetSimilarKeywords_OneData)oneData).Successed)
                            {
                                sw.WriteLine(((TaskGroup_GetSimilarKeywords_OneData)oneData).ToSuccessTxt());
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
                        csvHelper["关键词"] = ((TaskGroup_GetSimilarKeywords_OneData)oneData).Word;
                        csvHelper["基础词"] = ((TaskGroup_GetSimilarKeywords_OneData)oneData).BaseKeyword;
                        csvHelper["状态"] = ((TaskGroup_GetSimilarKeywords_OneData)oneData).Successed;
                        csvHelper["来源"] = EnumString.GetStringValue(((TaskGroup_GetSimilarKeywords_OneData)oneData).eSource);
                        csvHelper["百度PC指数"] = ((TaskGroup_GetSimilarKeywords_OneData)oneData).BaiduPCIndex;
                    }
                    File.WriteAllBytes(strSuccessedFile, csvHelper.ExportToBytes(true));
                    break;
                case ResourceType.eResourceType_MySQL:
                    strSuccessedFile = string.Empty;
                    // TODO:
                    break;
            }
            this.taskGroupStatus = TaskGroupStatus.eTaskGroupStatus_Idle;
            if(strSuccessedFile != string.Empty)
            {
                return Path.GetFileName(strSuccessedFile);
            }
            return string.Empty;
        }
        public override int ImportData(object data, ResourceType type)
        {
            if(this.taskGroupStatus != TaskGroupStatus.eTaskGroupStatus_InitComplete)
            {
                return 0;
            }
            int nSuccessedDataCnt = 0;
            switch (type)
            {
                case ResourceType.eResourceType_Txt:
                    if(data is string)
                    {
                        nSuccessedDataCnt = LoadBaseKeywordsFromFile((string)data);
                    }
                    break;
            }

            if(nSuccessedDataCnt > 0)
            {
                this.taskGroupStatus = TaskGroupStatus.eTaskGroupStatus_ImportDataComplete;
            }
            return nSuccessedDataCnt;
        }
        public override bool Init(Control container)
        {
            if(this.taskGroupStatus != TaskGroupStatus.eTaskGroupStatus_Idle)
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
                this.uiPanel = new TaskGroup_GetSimilarKeywords_Form(this.Name);
                if (uiPanel is Form)
                {
                    ((Form)uiPanel).TopLevel = false;
                    ((Form)uiPanel).FormBorderStyle = FormBorderStyle.None;
                }

                uiPanel.Dock = DockStyle.Fill;
                container.Controls.Add(uiPanel);
                uiPanel.Show();

                this.baseKeywords = new List<string>();

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
            if(this.uiPanel != null && this.uiPanel is TaskGroup_GetSimilarKeywords_Form)
            {
                ((TaskGroup_GetSimilarKeywords_Form)uiPanel).Reset();
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

        private int LoadBaseKeywordsFromFile(string file)
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
                    if (AddBaseKeyword(content))
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
        private bool AddBaseKeyword(string word)
        {
            word = word.Replace(" ", "");
            if (word.Length > 8 || string.IsNullOrEmpty(word))
            {
                return false;
            }
            int oldCnt = baseKeywords.Count;
            baseKeywords.Add(word);
            baseKeywords = baseKeywords.Distinct().ToList();
            int newCnt = baseKeywords.Count;
            return !(oldCnt == newCnt);
        }
        /*
        private int LoadForceIncludeWordsFromFile(string file)
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
                    if (AddForceIncludeWord(content))
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
        private bool AddForceIncludeWord(string word)
        {
            word = word.Replace(" ", "");
            if (word.Length > 8 || string.IsNullOrEmpty(word))
            {
                return false;
            }
            int oldCnt = setting.ForceIncludeFilter.Count;
            setting.ForceIncludeFilter.Add(word);
            setting.ForceIncludeFilter = setting.ForceIncludeFilter.Distinct().ToList();
            int newCnt = setting.ForceIncludeFilter.Count;
            return !(oldCnt == newCnt);
        }
        private int LoadForceExcludeWordsFromFile(string file)
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
                    if (AddForceExcludeWord(content))
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
        private bool AddForceExcludeWord(string word)
        {
            word = word.Replace(" ", "");
            if (word.Length > 8 || string.IsNullOrEmpty(word))
            {
                return false;
            }
            int oldCnt = setting.ForceExcludeFilter.Count;
            setting.ForceExcludeFilter.Add(word);
            setting.ForceExcludeFilter = setting.ForceExcludeFilter.Distinct().ToList();
            int newCnt = setting.ForceExcludeFilter.Count;
            return !(oldCnt == newCnt);
        }
        */


        public class SuggestionResponse
        {
            public SuggestionResponse(string json)
            {
                JObject obj = JObject.Parse(json);
                JToken jSR = obj["sr"];
                baseWord = (string)jSR["q"];
                isFailed = (bool)jSR["p"];
                similarWords = jSR["s"].ToArray();
            }

            public string baseWord { get; set; }
            public bool isFailed { get; set; }
            public Array similarWords { get; set; }
        }
        private List<string> BaiduSearch(string baseKeyword, string userAgent)
        {
            string suggestionUrl = "http://suggestion.baidu.com/su?wd=" + baseKeyword + "&cb=window.bdsug.sug&from=superpage&t=1335581987353";
            List<string> result = null;

            HttpHelper.HttpParam hp = new HttpHelper.HttpParam()
            {
                URL = suggestionUrl,
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
                        ParserBaiduSearchResult(html, out result);
                    }
                }
                hr.Result.Close();
            }
            return result;
        }
        private void ParserBaiduSearchResult(string str, out List<string> similarWords)
        {
            similarWords = new List<string>();

            if (str.Length < 19)
            {
                return;
            }
            str = str.Substring(17);
            str = str.Substring(0, str.Length - 2);
            str = "{\"sr\":" + str + "}";

            SuggestionResponse sr = new SuggestionResponse(str);
            foreach (var i in sr.similarWords)
            {
                similarWords.Add(i.ToString());
            }
        }
    }
}