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
// Create Time         :    2019/8/22 10:21:37
// Update Time         :    2019/8/22 10:21:37
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
// ===============================================================================
namespace Zaku
{
    internal class TaskGroup_FileCheck_Data : ITaskGroupResult
    {
        public List<TaskGroup_FileCheck_OneData> Data
        {
            get { return data; }
        }

        private List<TaskGroup_FileCheck_OneData> data;

        public TaskGroup_FileCheck_Data()
        {
            data = new List<TaskGroup_FileCheck_OneData>();
        }
        public void Add(TaskGroup_FileCheck_OneData d)
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

    internal class TaskGroup_FileCheck_OneData
    {
        public TaskGroup_FileCheck_OneData(string path)
        {
            url = path;
        }

        public bool Successed
        {
            get { return successed; }
            set { successed = value; }
        }
        private bool successed;

        public String url = "";
        public String server = "";
        public String contentType = "";
        public long length = 0;
        public int code = 0;
        public String powerBy = "";
        public long time = 0;

        public string ToSuccessTxt()
        {
            return url;
        }

        public string ToFailedTxt()
        {
            return url;
        }
    }
}