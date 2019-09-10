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
// Create Time         :    2019/7/31 14:55:42
// Update Time         :    2019/7/31 14:55:42
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Net;
using System.Threading;
// ===============================================================================
namespace TestAllInOne
{
    public class NormalHead : HttpTest
    {
        public override bool Off
        {
            get
            {
                return false;
            }
        }

        public override void Execute(MainForm r, string url)
        {
            testNormalHead(r, url);
        }

        public void testNormalHead(MainForm form, string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 3000;
            request.Method = "HEAD";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    form.SetRichTextBox2Text("线程ID: " + Thread.CurrentThread.ManagedThreadId.ToString() + "   HEAD请求成功. " + "\r\n");
                }
                else
                {
                    form.SetRichTextBox2Text("线程ID: " + Thread.CurrentThread.ManagedThreadId.ToString() + "   HEAD请求失败: " + response.StatusCode.ToString() + "\r\n");
                }
            }
        }
    }
}