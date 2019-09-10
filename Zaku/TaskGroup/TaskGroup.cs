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
// Create Time         :    2019/8/1 16:29:52
// Update Time         :    2019/8/1 16:29:52
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using MaterialSkin.Controls;
using System;
using System.ComponentModel;
using System.Windows.Forms;
// ===============================================================================
namespace Zaku
{
    public enum TaskGroupStatus
    {
        [Description("闲置中")]
        eTaskGroupStatus_Idle = 0,
        [Description("初始化完成")]
        eTaskGroupStatus_InitComplete = 1,
        [Description("导入数据完成")]
        eTaskGroupStatus_ImportDataComplete = 2,
        [Description("处理中")]
        eTaskGroupStatus_Doing = 3,
        [Description("等待导出数据")]
        eTaskGroupStatus_WaitingExportData = 4,
    }

    public enum TaskStatus
    {
        [Description("")]
        eTaskStatus_Unknown = 0,
        [Description("处理中")]
        eTaskStatus_Doing = 1,
        [Description("成功")]
        eTaskStatus_Finish_Suceessed = 2,
        [Description("用户取消")]
        eTaskStatus_Finish_Cancelled = 3,
        [Description("失败")]
        eTaskStatus_Finish_Error = 4,
    }

    public abstract class ITaskGroupResult : INotifyPropertyChanged
    {
        // 当前任务状态
        public TaskStatus Status
        {
            get { return status; }
            set { status = value; }
        }
        private TaskStatus status;

        public abstract event PropertyChangedEventHandler PropertyChanged;
    }
    public abstract class ITaskGroup
    {
        /// <summary>
        /// 任务名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 子类型设置面板
        /// </summary>
        public Control uiPanel { get; set; }
        /// <summary>
        /// 当前任务的进行状态
        /// </summary>
        public TaskGroupStatus taskGroupStatus { get; set; }

        /// <summary>
        /// 是否显示（是否可用）
        /// </summary>
        /// <returns></returns>
        public abstract bool IsShow();
        /// <summary>
        /// 初始化【点击 创建  按钮时触发】
        /// </summary>
        /// <param name="container">子类型设置面板</param>
        /// <returns></returns>
        public abstract bool Init(Control container);
        /// <summary>
        /// 导入输入数据
        /// </summary>
        /// <param name="data">文件路径. string格式</param>
        /// <param name="type">输入文件类型</param>
        /// <returns></returns>
        public abstract int ImportData(object data, ResourceType type);
        /// <summary>
        /// 导出结果文件
        /// </summary>
        /// <param name="data">文件路径 string格式</param>
        /// <param name="type">导出文件类型</param>
        /// <returns></returns>
        public abstract string ExportData(object data, ResourceType type);
        /// <summary>
        /// 开始准备【点击 执行 按钮时触发】
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public abstract bool PrepareDo(Control listView, TaskGroupSetting s);
        /// <summary>
        /// 停止任务
        /// </summary>
        /// <returns></returns>
        public abstract bool Stop();
        /// <summary>
        /// 最终清除
        /// </summary>
        public abstract void Clear();

        public abstract object OneTask_Do(object data);
        public abstract object OneTask_Produce();
        public abstract void OneTask_Finished(Control listView, object data, TaskStatus reason);
    }
}