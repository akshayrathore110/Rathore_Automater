using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RathoreSearchAutomation.Pages;

namespace RathoreSearchAutomation
{
    public partial class MainWindow : Window
    {
        private bool isDarkMode = false;
        
        public MainWindow()
        {
            InitializeComponent();
            
            // Navigate to login page
            MainFrame.Navigate(new LoginPage(this));
            
            // Subscribe to theme changes
            ThemeManager.Instance.OnThemeChanged += OnThemeChanged;
            
            // Set initial theme
            UpdateTheme();
        }
        
        private void ThemeToggleButton_Click(object sender, RoutedEventArgs e)
        {
            isDarkMode = !isDarkMode;
            ThemeManager.Instance.IsDarkMode = isDarkMode;
            UpdateTheme();
        }
        
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        
        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                var maxText = FindVisualChild<TextBlock>(MaximizeButton, "MaximizeText");
                if (maxText != null) maxText.Text = "□";
            }
            else
            {
                WindowState = WindowState.Maximized;
                var maxText = FindVisualChild<TextBlock>(MaximizeButton, "MaximizeText");
                if (maxText != null) maxText.Text = "❐";
            }
        }
        
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private void OnThemeChanged(bool isDark)
        {
            isDarkMode = isDark;
            UpdateTheme();
        }
        
        private void UpdateTheme()
        {
            try
            {
                // Find icon TextBlocks in the button template
                var sunIcon = FindVisualChild<TextBlock>(ThemeToggleButton, "SunIcon");
                var moonIcon = FindVisualChild<TextBlock>(ThemeToggleButton, "MoonIcon");
                
                if (isDarkMode)
                {
                    // Dark mode - use dark-bg.jpg
                    BackgroundBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/dark-bg.jpg"));
                    
                    // Show moon icon, hide sun icon
                    if (sunIcon != null) sunIcon.Visibility = Visibility.Collapsed;
                    if (moonIcon != null) moonIcon.Visibility = Visibility.Visible;
                    
                    // Update title bar colors for dark mode
                    TitleBarText.Foreground = new SolidColorBrush(Color.FromArgb(230, 255, 255, 255)); // #E6FFFFFF
                    
                    // Update window button colors
                    UpdateButtonTextColor(MinimizeButton, "MinimizeText", "#E6FFFFFF");
                    UpdateButtonTextColor(MaximizeButton, "MaximizeText", "#E6FFFFFF");
                    UpdateButtonTextColor(CloseButton, "CloseText", "#E6FFFFFF");
                }
                else
                {
                    // Light mode - use light-bg.jpg
                    BackgroundBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resources/light-bg.jpg"));
                    
                    // Show sun icon, hide moon icon
                    if (sunIcon != null) sunIcon.Visibility = Visibility.Visible;
                    if (moonIcon != null) moonIcon.Visibility = Visibility.Collapsed;
                    
                    // Update title bar colors for light mode
                    TitleBarText.Foreground = new SolidColorBrush(Color.FromArgb(230, 0, 0, 0)); // #E6000000
                    
                    // Update window button colors
                    UpdateButtonTextColor(MinimizeButton, "MinimizeText", "#E6000000");
                    UpdateButtonTextColor(MaximizeButton, "MaximizeText", "#E6000000");
                    UpdateButtonTextColor(CloseButton, "CloseText", "#E6000000");
                }
            }
            catch (Exception ex)
            {
                // Fallback to solid colors if images not found
                if (isDarkMode)
                {
                    RootGrid.Background = new SolidColorBrush(Color.FromRgb(17, 24, 39));
                }
                else
                {
                    RootGrid.Background = new LinearGradientBrush(
                        Color.FromRgb(59, 130, 246),
                        Color.FromRgb(147, 51, 234),
                        90);
                }
                
                System.Diagnostics.Debug.WriteLine($"Theme update error: {ex.Message}");
            }
        }
        
        private void UpdateButtonTextColor(Button button, string textBlockName, string colorHex)
        {
            var textBlock = FindVisualChild<TextBlock>(button, textBlockName);
            if (textBlock != null)
            {
                var color = (Color)ColorConverter.ConvertFromString(colorHex);
                textBlock.Foreground = new SolidColorBrush(color);
            }
        }
        
        // Helper method to find child controls in template
        private T FindVisualChild<T>(DependencyObject parent, string name) where T : FrameworkElement
        {
            if (parent == null) return null;
            
            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                
                if (child is T typedChild && (string.IsNullOrEmpty(name) || typedChild.Name == name))
                {
                    return typedChild;
                }
                
                var result = FindVisualChild<T>(child, name);
                if (result != null)
                {
                    return result;
                }
            }
            
            return null;
        }
        
        public void NavigateToConfig()
        {
            MainFrame.Navigate(new ConfigPage(this));
        }
        
        public void NavigateToLogin()
        {
            MainFrame.Navigate(new LoginPage(this));
        }
        
        public void ShowRunningPage(int totalSearches, int profileKey)
        {
            var runningPage = new RunningPage(this, totalSearches, profileKey);
            OverlayFrame.Navigate(runningPage);
            RunningOverlay.Visibility = Visibility.Visible;
        }
        
        public void HideRunningPage()
        {
            RunningOverlay.Visibility = Visibility.Collapsed;
            OverlayFrame.Content = null;
        }
        
        public void ShowCompletionPage(int totalSearches)
        {
            HideRunningPage();
            MainFrame.Navigate(new CompletionPage(this, totalSearches));
        }
    }
}
