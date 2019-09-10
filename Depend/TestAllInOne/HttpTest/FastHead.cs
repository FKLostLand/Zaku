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
// Create Time         :    2019/7/31 15:00:56
// Update Time         :    2019/7/31 15:00:56
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using HttpRequest;
using System;
using System.Threading;
// ===============================================================================
namespace TestAllInOne
{
    public class FastHead : HttpTest
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
            testFastHead(r, url);
        }

        void testFastHead(MainForm form, string url)
        {
            var result = HttpHelper.HeadInfo(url);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                form.SetRichTextBox2Text("线程ID: " + Thread.CurrentThread.ManagedThreadId.ToString() + "   HEAD请求成功. " + "\r\n");
            }else
            {
                form.SetRichTextBox2Text("线程ID: " + Thread.CurrentThread.ManagedThreadId.ToString() + "   HEAD请求失败: " + result.StatusCode.ToString() + "\r\n");
            }
        }
    }
}