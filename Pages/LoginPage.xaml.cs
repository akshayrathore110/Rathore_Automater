using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RathoreSearchAutomation.Pages
{
    public partial class LoginPage : Page
    {
        private readonly MainWindow mainWindow;
        
        public LoginPage(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
            
            // Subscribe to theme changes
            ThemeManager.Instance.OnThemeChanged += UpdateTheme;
            UpdateTheme(ThemeManager.Instance.IsDarkMode);
        }
        
        private void UpdateTheme(bool isDark)
        {
            if (isDark)
            {
                // Dark mode
                TitleText.Foreground = Brushes.White;
                WelcomeText.Foreground = Brushes.White;
                SubtitleText.Foreground = new SolidColorBrush(Color.FromArgb(179, 255, 255, 255));
                UserIdLabel.Foreground = new SolidColorBrush(Color.FromArgb(230, 255, 255, 255));
                KeyLabel.Foreground = new SolidColorBrush(Color.FromArgb(230, 255, 255, 255));
                
                UserIdTextBox.Foreground = Brushes.White;
                KeyPasswordBox.Foreground = Brushes.White;
            }
            else
            {
                // Light mode
                TitleText.Foreground = Brushes.Black;
                WelcomeText.Foreground = Brushes.Black;
                SubtitleText.Foreground = new SolidColorBrush(Color.FromArgb(153, 0, 0, 0));
                UserIdLabel.Foreground = new SolidColorBrush(Color.FromArgb(230, 0, 0, 0));
                KeyLabel.Foreground = new SolidColorBrush(Color.FromArgb(230, 0, 0, 0));
                
                UserIdTextBox.Foreground = Brushes.Black;
                KeyPasswordBox.Foreground = Brushes.Black;
            }
        }
        
        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            string userId = UserIdTextBox.Text.Trim();
            string key = KeyPasswordBox.Password;
            
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Please enter both User ID and Key.", 
                              "Login Error", 
                              MessageBoxButton.OK, 
                              MessageBoxImage.Warning);
                return;
            }
            
            // Navigate to config page
            mainWindow.NavigateToConfig();
        }
        
        private void TelegramButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://t.me/Rathore_Main",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open Telegram link: {ex.Message}", 
                              "Error", 
                              MessageBoxButton.OK, 
                              MessageBoxImage.Error);
            }
        }
    }
}
