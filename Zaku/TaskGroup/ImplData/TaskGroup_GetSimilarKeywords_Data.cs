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
// Create Time         :    2019/8/6 15:06:15
// Update Time         :    2019/8/6 15:06:15
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
// ===============================================================================
namespace Zaku
{
    public enum SimilarKeywordSource
    {
        [Description("未知")]
        eSimilarKeywordSource_Unknown = 0,
        [Description("百度")]
        eSimilarKeywordSource_Baidu = 1,
        [Description("360")]
        eSimilarKeywordSource_360 = 2,
        [Description("搜狗")]
        eSimilarKeywordSource_Sougou = 3,
        [Description("必应")]
        eSimilarKeywordSource_Bing = 4,
        [Description("神马")]
        eSimilarKeywordSource_Shenma = 5,
        [Description("谷歌")]
        eSimilarKeywordSource_Google = 6,
    }
    internal class TaskGroup_GetSimilarKeywords_Data : ITaskGroupResult {

        public List<TaskGroup_GetSimilarKeywords_OneData> Data
        {
            get { return data; }
        }

        private List<TaskGroup_GetSimilarKeywords_OneData> data;

        public TaskGroup_GetSimilarKeywords_Data()
        {
            data = new List<TaskGroup_GetSimilarKeywords_OneData>();
        }
        public void Add(TaskGroup_GetSimilarKeywords_OneData d)
        {
            if(data != null)
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

    internal class TaskGroup_GetSimilarKeywords_OneData
    {
        public TaskGroup_GetSimilarKeywords_OneData(string word)
        {
            Word = word;
        }

        public bool Successed
        {
            get { return successed; }
            set { successed = value; }
        }
        private bool successed;
        public string Word
        {
            get { return word; }
            set { word = value; }
        }
        private string word;

        public string BaseKeyword
        {
            get { return baseKeyword; }
            set { baseKeyword = value; }
        }
        private string baseKeyword;

        // 该近似词来源
        public SimilarKeywordSource eSource = SimilarKeywordSource.eSimilarKeywordSource_Unknown;

        public int BaiduPCIndex
        {
            get { return baiduPCIndex; }
            set { baiduPCIndex = value; }
        }
        private int baiduPCIndex;

        public int BaiduMobileIndex
        {
            get { return baiduMobileIndex; }
            set { baiduMobileIndex = value; }
        }
        private int baiduMobileIndex;

        public int ThreeSixZeroIndex
        {
            get { return threeSixZeroIndex; }
            set { threeSixZeroIndex = value; }
        }
        private int threeSixZeroIndex;

        public int SougouPCIndex
        {
            get { return sougouPCIndex; }
            set { sougouPCIndex = value; }
        }
        private int sougouPCIndex;

        public int SougouMobileIndex
        {
            get { return sougouMobileIndex; }
            set { sougouMobileIndex = value; }
        }
        private int sougouMobileIndex;

        public int ShenmaIndex
        {
            get { return shenmaIndex; }
            set { shenmaIndex = value; }
        }
        private int shenmaIndex;

        public string ToSuccessTxt()
        {
            return word;
        }

        public string ToFailedTxt()
        {
            return baseKeyword;
        }
    }
}