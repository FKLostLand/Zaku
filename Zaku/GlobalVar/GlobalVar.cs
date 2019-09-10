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
// Create Time         :    2019/8/1 11:34:38
// Update Time         :    2019/8/1 11:34:38
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using MaterialSkin;
using System;
// ===============================================================================
namespace Zaku
{
    internal class GlobalVar : Singleton<GlobalVar>
    {
        public GlobalVar()
        {
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.LightBlue800, Primary.LightBlue900, Primary.LightBlue500, Accent.LightBlue200, TextShade.WHITE);

            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            taskGroupManager = new TaskGroupManager();

            uaList = new UAList();
        }
        internal MaterialSkinManager materialSkinManager { get; private set; }
        internal UserInfo userInfo { get; private set; }
        internal log4net.ILog logger { get; private set; }
        internal TaskGroupManager taskGroupManager { get; private set; }
        internal UAList uaList { get; private set; }
        internal Config config { get; set; }

        internal void Init(string account, string password)
        {
            userInfo = new UserInfo(account, password);
            logger.Info($"{EnumString.GetStringValue(userInfo.userType)} {userInfo.userName} 登录");
            if (taskGroupManager.Init(typeof(MainForm)))
            {
                logger.Debug($"初始化任务管理器完成，当前任务类型 {taskGroupManager.TaskGroupTypeCount()} 种");
            }
        }
    }
}