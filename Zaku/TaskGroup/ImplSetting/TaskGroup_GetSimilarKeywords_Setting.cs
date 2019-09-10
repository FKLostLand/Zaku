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
// Create Time         :    2019/8/7 15:39:22
// Update Time         :    2019/8/7 15:39:22
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Collections.Generic;
// ===============================================================================
namespace Zaku
{
    class TaskGroup_GetSimilarKeywords_Setting
    {
        public bool IsGetBaiduRank;
        public bool IsForceInclude;
        public bool IsForceExclude;
        public bool IsStopUntilCount;

        public bool IsUseBaiduSearch;
        public bool IsUse360Search;
        public bool IsUseSougouSearch;
        public bool IsUseBingSearch;
        public bool IsUseShenmaSearch;
        public bool IsUseGoogleSearch;

        public List<string> ForceIncludeFilter;
        public List<string> ForceExcludeFilter;
        public int StopCount;

        public TaskGroup_GetSimilarKeywords_Setting()
        {
            IsStopUntilCount = true;
            IsForceInclude = IsForceExclude = IsGetBaiduRank = false;
            StopCount = 1000;

            IsUseBaiduSearch = true;
            IsUse360Search = IsUseSougouSearch = IsUseSougouSearch = IsUseBingSearch = IsUseShenmaSearch = IsUseGoogleSearch = false;
            ForceIncludeFilter = ForceExcludeFilter = null;

            ForceIncludeFilter = new List<string>();
            ForceExcludeFilter = new List<string>();
        }
    }
}