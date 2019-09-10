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
// Create Time         :    2019/8/21 10:36:46
// Update Time         :    2019/8/21 10:36:46
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Collections.Generic;
// ===============================================================================
namespace Zaku
{
    public enum ENUM_ScanType
    {
        eScanType_Get = 0,
        eScanType_Head,
    }
    class TaskGroup_FileCheck_Setting
    {
        public Dictionary<String, List<String>> suffixes;
        public bool IsShow301302;
        public bool IsShow500;
        public bool IsGetServerType;
        public bool IsGetMiddleWare;
        public ENUM_ScanType ScanType;

        public TaskGroup_FileCheck_Setting()
        {
            ScanType = ENUM_ScanType.eScanType_Get;
            IsGetMiddleWare = true;
            IsGetServerType = false;
            IsShow301302 = true;
            IsShow500 = true;
            suffixes = new Dictionary<String, List<String>>();
        }
    }
}