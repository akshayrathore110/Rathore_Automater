using System;
using System.Windows;

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
