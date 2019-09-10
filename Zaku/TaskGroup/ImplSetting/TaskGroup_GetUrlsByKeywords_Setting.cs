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
// Create Time         :    2019/8/14 14:33:53
// Update Time         :    2019/8/14 14:33:53
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
// ===============================================================================
namespace Zaku
{
    class TaskGroup_GetUrlsByKeywords_Setting
    {
        public bool IsTopLevelDomain;
        public bool IsGetBaiduRank;

        public bool IsUseBaiduSearch;
        public bool IsUse360Search;
        public bool IsUseSougouSearch;
        public bool IsUseBingSearch;
        public bool IsUseShenmaSearch;
        public bool IsUseGoogleSearch;

        public TaskGroup_GetUrlsByKeywords_Setting()
        {
            IsTopLevelDomain = true;
            IsGetBaiduRank = false;
            IsUse360Search = IsUseSougouSearch = IsUseSougouSearch = IsUseBingSearch = IsUseShenmaSearch = IsUseGoogleSearch = false;
            IsUseBaiduSearch = true;
        }
    }
}