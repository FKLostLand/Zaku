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
// Create Time         :    2019/8/20 10:14:24
// Update Time         :    2019/8/20 10:14:24
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
// ===============================================================================
namespace Zaku
{
    class XMLConfig
    {
        public static void SaveConfig(string filename, Config config)
        {
            Stream fStream = null;
            try
            {
                fStream = new FileStream(filename, FileMode.Create, FileAccess.Write);
                XmlSerializer xmlFormat = new XmlSerializer(typeof(Config));
                xmlFormat.Serialize(fStream, config);
                fStream.Close();
            }
            catch
            {
            }
            finally
            {
                if (fStream != null)
                {
                    fStream.Close();
                }
            }
        }


        [NonSerialized]
        private const string AppConfigFileName = "AppConfig.dat";
        public static string GetAppConfigFilePath()
        {
            string userDir = System.Environment.CurrentDirectory;

            DirectoryInfo di = new DirectoryInfo(userDir);
            di = di.CreateSubdirectory("AppConfig");

            return di.FullName + Path.DirectorySeparatorChar + AppConfigFileName;
        }

        public static Config ReadConfig(string filename)
        {
            Stream fStream = null;
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(Config));
                fStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                XmlTextReader reader = new XmlTextReader(fStream);
                reader.Normalization = false;
                Config config = (Config)xml.Deserialize(reader);
                return config;
            }
            catch
            {
                return new Config();
            }
            finally
            {
                if (fStream != null)
                {
                    fStream.Close();
                }
            }
        }
    }
}