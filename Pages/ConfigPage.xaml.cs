using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using RathoreSearchAutomation.Helpers;

namespace RathoreSearchAutomation.Pages
{
    public partial class ConfigPage : Page
    {
        private readonly MainWindow mainWindow;
        
        public ConfigPage(MainWindow window)
        {
            InitializeComponent();
            mainWindow = window;
            
            LoadEdgeProfiles();
            
            // Subscribe to theme changes
            ThemeManager.Instance.OnThemeChanged += UpdateTheme;
            UpdateTheme(ThemeManager.Instance.IsDarkMode);
        }
        
        private void LoadEdgeProfiles()
        {
            var profiles = EdgeProfileDetector.DetectEdgeProfiles();
            
            ProfileComboBox.ItemsSource = profiles;
            
            // Set default selection
            if (profiles.Count > 0)
            {
                ProfileComboBox.SelectedIndex = 0;
            }
        }
        
        private void UpdateTheme(bool isDark)
        {
            if (isDark)
            {
                // Dark mode
                TitleText.Foreground = Brushes.White;
                ConfigText.Foreground = Brushes.White;
                SubtitleText.Foreground = new SolidColorBrush(Color.FromArgb(179, 255, 255, 255));
                SearchCountLabel.Foreground = new SolidColorBrush(Color.FromArgb(230, 255, 255, 255));
                ProfileLabel.Foreground = new SolidColorBrush(Color.FromArgb(230, 255, 255, 255));
                
                SearchCountTextBox.Foreground = Brushes.White;
                ProfileComboBox.Foreground = Brushes.White;
            }
            else
            {
                // Light mode
                TitleText.Foreground = Brushes.Black;
                ConfigText.Foreground = Brushes.Black;
                SubtitleText.Foreground = new SolidColorBrush(Color.FromArgb(153, 0, 0, 0));
                SearchCountLabel.Foreground = new SolidColorBrush(Color.FromArgb(230, 0, 0, 0));
                ProfileLabel.Foreground = new SolidColorBrush(Color.FromArgb(230, 0, 0, 0));
                
                SearchCountTextBox.Foreground = Brushes.Black;
                ProfileComboBox.Foreground = Brushes.Black;
            }
        }
        
        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(SearchCountTextBox.Text, out int searchCount) || searchCount < 1)
            {
                MessageBox.Show("Please enter a valid number of searches (minimum 1).",
                              "Invalid Input",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return;
            }
            
            if (ProfileComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select an Edge profile.",
                              "Invalid Input",
                              MessageBoxButton.OK,
                              MessageBoxImage.Warning);
                return;
            }
            
            int profileKey = (int)ProfileComboBox.SelectedValue;
            
            // Navigate to running page
            mainWindow.ShowRunningPage(searchCount, profileKey);
        }
        
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.NavigateToLogin();
        }
    }
}
