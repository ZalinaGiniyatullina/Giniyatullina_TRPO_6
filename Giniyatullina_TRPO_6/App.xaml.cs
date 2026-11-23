using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Giniyatullina_TRPO_6
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        //применяет сохраненную тему. И она устанавливается при старте приложения
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ThemeHelper.ApplySaved();
        }
    }
    public static class ThemeHelper
    {
        private static readonly string[] _themePath =
        {
            "Styles/Colors/LightTheme.xaml",
            "Styles/Colors/DarkTheme.xaml"
        };

        public static string Current
        {
            get => string.IsNullOrEmpty(Properties.Settings.Default.ThemePath)
                ? _themePath[0]
                : Properties.Settings.Default.ThemePath;
            set
            {
                Properties.Settings.Default.ThemePath = value;
                Properties.Settings.Default.Save();
            }
        }

        public static void Apply(string themePath)
        {
            var newTheme = new ResourceDictionary
            {
                Source = new Uri(themePath, UriKind.Relative)
            };

            var oldTheme = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null &&
                    _themePath.Any(p => d.Source.OriginalString
                        .EndsWith(p, StringComparison.OrdinalIgnoreCase)));

            if (oldTheme != null)
            {
                int index = Application.Current.Resources.MergedDictionaries.IndexOf(oldTheme);
                Application.Current.Resources.MergedDictionaries[index] = newTheme;
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(newTheme);
            }

            Current = themePath;
        }

        public static void ApplySaved() => Apply(Current);

        public static void Toggle()
        {
            var newTheme = Current == _themePath[0] ? _themePath[1] : _themePath[0];
            Apply(newTheme);
        }
    }
}
