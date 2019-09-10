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
// Create Time         :    2019/7/25 16:37:46
// Update Time         :    2019/7/25 16:37:46
// Class Version       :    v1.0.0.0
// Class Description   :    
// ===============================================================================
using BrightIdeasSoftware;
using System;
using System.ComponentModel;
using System.Drawing;
using TestAllInOne.Properties;
// ===============================================================================
namespace TestAllInOne
{
    enum MaritalStatus
    {
        Single,
        Married,
        Divorced,
        Partnered
    }

    class Person : INotifyPropertyChanged
    {
        public bool IsActive = true;

        public Person(string name)
        {
            this.name = name;
            this.YearOfBirth = 2019;
        }

        public Person(string name, string occupation, int culinaryRating, DateTime birthDate, double hourlyRate, bool canTellJokes, string photo, string comments)
        {
            this.name = name;
            this.Occupation = occupation;
            this.culinaryRating = culinaryRating;
            this.birthDate = birthDate;
            this.hourlyRate = hourlyRate;
            this.CanTellJokes = canTellJokes;
            this.Comments = comments;
            this.Photo = photo;
        }

        public Person(Person other)
        {
            this.name = other.Name;
            this.Occupation = other.Occupation;
            this.culinaryRating = other.CulinaryRating;
            this.birthDate = other.BirthDate;
            this.hourlyRate = other.GetRate();
            this.CanTellJokes = other.CanTellJokes;
            this.Photo = other.Photo;
            this.Comments = other.Comments;
            this.MaritalStatus = other.MaritalStatus;
        }

        [OLVIgnore]
        public Image ImageAspect
        {
            get
            {
                return Resources.folder16;
            }
        }

        [OLVIgnore]
        public string ImageName
        {
            get
            {
                return "user";
            }
        }

        // Allows tests for properties.
        [OLVColumn(ImageAspectName = "ImageName")]
        public string Name
        {
            get { return name; }
            set
            {
                if (name == value)
                    return;
                name = value;
                this.OnPropertyChanged("Name");
            }
        }
        private string name;

        [OLVColumn(ImageAspectName = "ImageName")]
        public string Occupation
        {
            get { return occupation; }
            set
            {
                if (occupation == value) return;
                occupation = value;
                this.OnPropertyChanged("Occupation");
            }
        }
        private string occupation;

        public int CulinaryRating
        {
            get { return culinaryRating; }
            set { culinaryRating = value; }
        }
        private int culinaryRating;

        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }
        private DateTime birthDate;

        public int YearOfBirth
        {
            get { return 2019 - this.BirthDate.Year; }
            set { this.BirthDate = new DateTime(value, birthDate.Month, birthDate.Day); }
        }

        // Allow tests for methods
        public double GetRate()
        {
            return hourlyRate;
        }
        private double hourlyRate;

        public void SetRate(double value)
        {
            hourlyRate = value;
        }

        // Allows tests for fields.
        public string Photo;
        public string Comments;
        public bool? CanTellJokes;

        // Allow tests for enums
        public MaritalStatus MaritalStatus = MaritalStatus.Single;

        #region 强制实现属性更变事件

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}