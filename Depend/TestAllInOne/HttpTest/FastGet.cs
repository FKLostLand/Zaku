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
// Create Time         :    2019/7/31 14:43:49
// Update Time         :    2019/7/31 14:43:49
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using HttpRequest;
using System;
using System.IO;
using System.Threading;
// ===============================================================================
namespace TestAllInOne
{
    public class FastGet : HttpTest
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
            testFastGet(r, url);
        }

        void testFastGet(MainForm form, string url)
        {
            HttpHelper.Request(
               new HttpHelper.HttpParam()
               {
                   URL = url,
                   ContentType = "application/x-www-form-urlencoded",
                   Timeout = TimeSpan.FromSeconds(3),
                   Method = HttpHelper.HttpVerb.Get,
                   //Parameters = new { canshu1 = "Hello", canshu2 = 23 }

               },
                callback: (r) =>
                {
                    if (r.Result != null)
                    {
                        var sr = new StreamReader(r.Result);
                        if (sr != null)
                        {
                            string html = sr.ReadToEnd();
                            if (!string.IsNullOrEmpty(html))
                            {
                                form.SetRichTextBox2Text("线程ID: " + Thread.CurrentThread.ManagedThreadId.ToString() + "   收到数据: " + html.Length + " 字节." + "\r\n");
                            }
                        }
                        r.Result.Close();
                    }
                }
            );
        }
    }
}