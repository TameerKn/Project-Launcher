using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_C_
{
    class ThemeSwitch
    {

        public static void SwitchTheme(Uri theme)
        {
            ResourceDictionary Theme = new ResourceDictionary() { Source = theme };
            App.Current.Resources.MergedDictionaries.Remove(Theme);
            App.Current.Resources.MergedDictionaries.Add(Theme);
        }

    }
}
