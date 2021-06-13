using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private bool darkTheme=false;

        public bool DarkTheme
        {
            get { return darkTheme; }
            set { darkTheme = value; }
        }


        public ResourceDictionary ThemeDictionary
        {
            get { return Resources.MergedDictionaries[0]; }
        }
        

        public void ChangeLanguage(string currLang)
        {
            if (currLang.Equals("en-US"))
            {
                TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            }
            else
            {
                TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("sr-LATN");
            }
        }
        public void ChangeTheme(Uri uri)
        {
            DarkTheme = !DarkTheme;
            ThemeDictionary.MergedDictionaries.Clear();
            ThemeDictionary.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
        }
    }
}
