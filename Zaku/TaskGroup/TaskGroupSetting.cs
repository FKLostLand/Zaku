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
// Create Time         :    2019/8/8 10:32:33
// Update Time         :    2019/8/8 10:32:33
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace Zaku
{
    public class TaskGroupSetting
    {
        public int ThreadNum;
        public int IntervalTimeMS;
        public string UAType;
        public bool IsAutoExport;
        public ResourceType ExportType;

        public TaskGroupSetting()
        {
            ThreadNum = Environment.ProcessorCount * 4;
            IntervalTimeMS = 50;
            UAType = "";
            IsAutoExport = true;
            ExportType = ResourceType.eResourceType_Txt;
        }

        public void HardCopy(TaskGroupSetting other)
        {
            this.ThreadNum = other.ThreadNum;
            this.IntervalTimeMS = other.IntervalTimeMS;
            this.UAType = other.UAType;
            this.IsAutoExport = other.IsAutoExport;
            this.ExportType = other.ExportType;
        }
    }
}