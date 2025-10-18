using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace RathoreSearchAutomation.Pages
{
    public partial class CompletionPage : Page
    {
        private readonly MainWindow mainWindow;
        private readonly int totalSearches;
        
        public CompletionPage(MainWindow window, int searches)
        {
            InitializeComponent();
            mainWindow = window;
            totalSearches = searches;
            
            CompletionDetails.Text = $"Successfully completed {totalSearches} searches";
        }
        
        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.NavigateToConfig();
        }
        
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Do you want to close the browser when exiting?",
                "Exit Program",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Cancel)
                return;
            
            if (result == MessageBoxResult.Yes)
            {
                // Close Edge browser
                try
                {
                    foreach (var process in Process.GetProcessesByName("msedge"))
                    {
                        process.Kill();
                    }
                }
                catch { }
            }
            
            Application.Current.Shutdown();
        }
    }
}
