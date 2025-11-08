using System;
using System.IO;
using System.Linq;
using System.Windows;
using RathoreSearchAutomation.Helpers;

namespace RathoreSearchAutomation
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Initialize application resources
            // Set default theme
            ThemeManager.Instance.IsDarkMode = false;

            // Early daily reset for all profile stores
            try
            {
                var dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
                if (Directory.Exists(dataDir))
                {
                    foreach (var file in Directory.EnumerateFiles(dataDir, "profile_*.json"))
                    {
                        var name = Path.GetFileNameWithoutExtension(file);
                        // profile_{key}.json -> extract key
                        var parts = name.Split('_');
                        if (parts.Length >= 2 && int.TryParse(parts[1], out int key))
                        {
                            var store = new ProfileDataStore(key);
                            store.ResetIfDateChanged();
                        }
                    }
                }
            }
            catch { }
        }
    }
    
    public class ThemeManager
    {
        private static ThemeManager? _instance;
        public static ThemeManager Instance => _instance ??= new ThemeManager();
        
        private bool _isDarkMode;
        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                _isDarkMode = value;
                OnThemeChanged?.Invoke(_isDarkMode);
            }
        }
        
        public event Action<bool>? OnThemeChanged;
    }
}
