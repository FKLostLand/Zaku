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
// Create Time         :    2019/8/14 14:37:08
// Update Time         :    2019/8/14 14:37:08
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
// ===============================================================================
namespace Zaku
{
    public enum GetUrlsSource
    {
        [Description("未知")]
        eGetUrlsSource_Unknown = 0,
        [Description("百度")]
        eGetUrlsSource_Baidu = 1,
        [Description("360")]
        eGetUrlsSource_360 = 2,
        [Description("搜狗")]
        eGetUrlsSource_Sougou = 3,
        [Description("必应")]
        eGetUrlsSource_Bing = 4,
        [Description("神马")]
        eGetUrlsSource_Shenma = 5,
        [Description("谷歌")]
        eGetUrlsSource_Google = 6,
    }
    internal class TaskGroup_GetUrlsByKeywords_Data : ITaskGroupResult
    {
        private List<TaskGroup_GetUrlsByKeywords_OneData> data;
        public List<TaskGroup_GetUrlsByKeywords_OneData> Data
        {
            get { return data; }
        }

        public TaskGroup_GetUrlsByKeywords_Data()
        {
            data = new List<TaskGroup_GetUrlsByKeywords_OneData>();
        }
        public void Add(TaskGroup_GetUrlsByKeywords_OneData d)
        {
            if (data != null)
            {
                data.Add(d);
            }
        }
        public int Count()
        {
            if (data == null)
                return 0;
            return data.Count;
        }

        #region 强制实现属性更变事件

        public override event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

    internal class TaskGroup_GetUrlsByKeywords_OneData
    {
        public TaskGroup_GetUrlsByKeywords_OneData(string url)
        {
            Url = url;
        }

        private string url;
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        private string keyword;
        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }

        public GetUrlsSource eSource = GetUrlsSource.eGetUrlsSource_Unknown;

        public int BaiduPCIndex
        {
            get { return baiduPCIndex; }
            set { baiduPCIndex = value; }
        }
        private int baiduPCIndex;

        public bool Successed
        {
            get { return successed; }
            set { successed = value; }
        }
        private bool successed;

        public string ToSuccessedTxt()
        {
            return url;
        }

        public string ToFailedTxt()
        {
            return keyword;
        }
    }
}