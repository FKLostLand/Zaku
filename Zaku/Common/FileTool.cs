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
// Create Time         :    2019/8/20 17:15:46
// Update Time         :    2019/8/20 17:15:46
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
// ===============================================================================
namespace Zaku
{
    class FileTool
    {
        public static List<string> ReadAllDic(String dic)
        {
            List<string> fs = new List<string>();
            try
            {
                DirectoryInfo din = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "/" + dic);
                FileInfo[] files = din.GetFiles();
                foreach (FileInfo f in files)
                {
                    fs.Add(f.Name);
                }
            }
            catch (Exception re)
            {
                GlobalVar.Instance.logger.Error(dic + "读取错误！" + re.Message);
            }
            return fs;
        }

        public static List<String> ReadFileToListByEncoding(String path, String encode)
        {

            List<String> list = new List<String>();
            FileStream fs_dir = null;
            StreamReader reader = null;
            try
            {
                fs_dir = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/" + path, FileMode.Open, FileAccess.Read);

                reader = new StreamReader(fs_dir, Encoding.GetEncoding(encode));

                String lineStr;

                while ((lineStr = reader.ReadLine()) != null)
                {
                    if (!lineStr.Equals(""))
                    {
                        int index = 0;
                        if ((index = lineStr.IndexOf("?")) != -1)
                        {
                            String cpath = lineStr.Substring(0, index);
                            String data = lineStr.Substring(index + 1, lineStr.Length - index - 1);
                            list.Add(CommonTool.EncodeURIPath(CommonTool.UrlEncode(cpath) + "?" + data));
                        }
                        else
                        {
                            list.Add(CommonTool.UrlEncode(lineStr));
                        }

                    }

                }
            }
            catch (Exception e)
            {
                GlobalVar.Instance.logger.Error(e.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (fs_dir != null)
                {
                    fs_dir.Close();
                }
            }
            return list;
        }
    }
}