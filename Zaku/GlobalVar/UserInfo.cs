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
// Create Time         :    2019/8/1 11:52:31
// Update Time         :    2019/8/1 11:52:31
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace Zaku
{
    enum UserType
    {
        [Description("游客")]
        Guest = 0,
        [Description("用户")]
        User = 1,
        [Description("管理员")]
        Manager = 2,
        [Description("大佐")]
        Master = 3,
    }

    internal class UserInfo
    {
        public UserType userType { get; private set; }
        public string userName { get; private set; }
        public UserInfo()
        {
            userType = UserType.Guest;
            userName = "Unknown";
        }
        public UserInfo(string account, string password)
        {
            CheckUserInfoOnLocal(account, password);
        }

        private void CheckUserInfoOnLocal(string account, string password)
        {
            if (string.Equals(account, "FK", StringComparison.OrdinalIgnoreCase) &&
                string.Equals(account, "FK", StringComparison.OrdinalIgnoreCase))
            {
                userType = UserType.Master;
                userName = "シャア・アズナブル";
            }
            else if (string.Equals(account, "A03", StringComparison.OrdinalIgnoreCase) &&
                string.Equals(account, "A03", StringComparison.OrdinalIgnoreCase))
            {
                userType = UserType.Manager;
                userName = account;
            }
            else if (string.Equals(account, "User", StringComparison.OrdinalIgnoreCase) &&
                string.Equals(account, "User", StringComparison.OrdinalIgnoreCase))
            {
                userType = UserType.User;
                userName = account;
            }
            else
            {
                userType = UserType.Guest;
                userName = account;
            }
        }
    }
}