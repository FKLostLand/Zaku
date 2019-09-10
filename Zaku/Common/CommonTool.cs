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
// Create Time         :    2019/8/20 17:31:57
// Update Time         :    2019/8/20 17:31:57
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Text;
// ===============================================================================
namespace Zaku
{
    class CommonTool
    {
        public static String EncodeURIPath(String path)
        {
            return Uri.EscapeUriString(path).Replace("#", "%23");
        }

        public static String UrlEncode(String str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '-' || c == '.' || c == ':' || c == '(' || c == ')' || c == '*' || c == '_' || c == '!' || c == '/')
                {
                    stringBuilder.Append(c);
                }
                else
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(new char[] { c });
                    byte[] array = bytes;
                    for (int j = 0; j < array.Length; j++)
                    {
                        byte b = array[j];
                        stringBuilder.Append("%");
                        stringBuilder.Append(b.ToString("x2"));
                    }
                }
            }
            return stringBuilder.ToString();
        }

        public static String formatDomain(String weburl)
        {
            if ("".Equals(weburl.Trim()))
            {
                return "";
            }
            if (!weburl.StartsWith("http://") && !weburl.StartsWith("https://"))
            {
                weburl = "http://" + weburl;
            }
            if (weburl.EndsWith("/"))
            {
                weburl = weburl.Substring(0, weburl.Length - 1);
            }

            return weburl;
        }

    }
}