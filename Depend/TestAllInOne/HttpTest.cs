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
// Create Time         :    2019/7/30 17:55:19
// Update Time         :    2019/7/30 17:55:19
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;
// ===============================================================================
namespace TestAllInOne
{
    public abstract class HttpTest
    {
        public abstract void Execute(MainForm r, string url);

        public virtual bool Off
        {
            get
            {
                return false;
            }
        }

        public static void RunTest(Type t, MainForm r, string strType, string url)
        {
            System.Reflection.Assembly dataAccess = System.Reflection.Assembly.GetAssembly(t);

            foreach (var type in dataAccess.GetTypes())
            {
                if (!type.IsAbstract && 
                    typeof(HttpTest).IsAssignableFrom(type) && 
                    string.Equals(type.Name, strType, StringComparison.OrdinalIgnoreCase))
                {
                    HttpTest exe = Activator.CreateInstance(type) as HttpTest;
                    if (!exe.Off)
                    {
                        r.SetRichTextBox2Text("线程ID: " + Thread.CurrentThread.ManagedThreadId.ToString() + "   开始测试: " + type.Name + "\r\n");
                        exe.Execute(r, url);
                    }
                }
            }
        }

    }
}