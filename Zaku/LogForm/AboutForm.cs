using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using Log2Console.Settings;


namespace Log2Console
{
  partial class AboutForm : Form
  {
    public AboutForm()
    {
      InitializeComponent();

      Font = UserSettings.Instance.DefaultFont ?? Font;

      //  Initialize the AboutBox to display the product information from the assembly information.
      //  Change assembly information settings for your application through either:
      //  - Project->Properties->Application->Assembly Information
      //  - AssemblyInfo.cs
      Text = String.Format("About {0}", AssemblyTitle);
      labelProductName.Text = AssemblyProduct;
      labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
      labelCopyright.Text = AssemblyCopyright;

      List<string> lines = new List<string>();
      lines.Add(AssemblyDescription);
      lines.Add("");
      lines.Add("这是一个开源软件 (duzhi5368@gmail.com).");
      lines.Add("");
      textBoxDescription.Lines = lines.ToArray();
    }


    #region Assembly Attribute Accessors

    public static string AssemblyTitle
    {
      get
      {
        // Get all Title attributes on this assembly
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
        // If there is at least one Title attribute
        if (attributes.Length > 0)
        {
          // Select the first one
          AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
          // If it is not an empty string, return it
          if (titleAttribute.Title != "")
            return titleAttribute.Title;
        }
        // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
        return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
      }
    }

    public static string AssemblyVersion
    {
      get
      {
        return Assembly.GetExecutingAssembly().GetName().Version.ToString();
      }
    }

    public static string AssemblyDescription
    {
      get
      {
        // Get all Description attributes on this assembly
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
        // If there aren't any Description attributes, return an empty string
        if (attributes.Length == 0)
          return "";
        // If there is a Description attribute, return its value
        return ((AssemblyDescriptionAttribute)attributes[0]).Description;
      }
    }

    public static string AssemblyProduct
    {
      get
      {
        // Get all Product attributes on this assembly
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
        // If there aren't any Product attributes, return an empty string
        if (attributes.Length == 0)
          return "";
        // If there is a Product attribute, return its value
        return ((AssemblyProductAttribute)attributes[0]).Product;
      }
    }

    public static string AssemblyCopyright
    {
      get
      {
        // Get all Copyright attributes on this assembly
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
        // If there aren't any Copyright attributes, return an empty string
        if (attributes.Length == 0)
          return "";
        // If there is a Copyright attribute, return its value
        return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
      }
    }

    public static string AssemblyCompany
    {
      get
      {
        // Get all Company attributes on this assembly
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
        // If there aren't any Company attributes, return an empty string
        if (attributes.Length == 0)
          return "";
        // If there is a Company attribute, return its value
        return ((AssemblyCompanyAttribute)attributes[0]).Company;
      }
    }

    #endregion
  }
}
