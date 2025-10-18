using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using RathoreSearchAutomation.Helpers;

namespace RathoreSearchAutomation.Pages
{
    public partial class RunningPage : Page
    {
        private readonly MainWindow mainWindow;
        private readonly SearchAutomation automation;
        private bool isPaused = false;
        
        public RunningPage(MainWindow window, int totalSearches, int profileKey)
        {
            InitializeComponent();
            mainWindow = window;
            
            ProgressBar.Maximum = totalSearches;
            ProgressText.Text = $"0 / {totalSearches}";
            
            // Initialize automation
            automation = new SearchAutomation(totalSearches, profileKey);
            automation.OnProgressUpdate += OnProgressUpdate;
            automation.OnStatusUpdate += OnStatusUpdate;
            automation.OnCompleted += OnCompleted;
            automation.OnError += OnError;
            
            // Start automation
            automation.Start();
        }
        
        private void OnProgressUpdate(int current, int total)
        {
            Dispatcher.Invoke(() =>
            {
                ProgressBar.Value = current;
                ProgressText.Text = $"{current} / {total}";
            });
        }
        
        private void OnStatusUpdate(string status)
        {
            Dispatcher.Invoke(() =>
            {
                StatusText.Text = status;
            });
        }
        
        private void OnCompleted()
        {
            Dispatcher.Invoke(() =>
            {
                mainWindow.ShowCompletionPage((int)ProgressBar.Maximum);
            });
        }
        
        private void OnError(string error)
        {
            Dispatcher.Invoke(() =>
            {
                MessageBox.Show($"Automation Error: {error}",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            });
        }
        
        private void PauseResumeButton_Click(object sender, RoutedEventArgs e)
        {
            isPaused = !isPaused;
            
            if (isPaused)
            {
                automation.Pause();
                PauseIcon.Visibility = Visibility.Collapsed;
                PlayIcon.Visibility = Visibility.Visible;
                PauseResumeText.Text = "Resume";
                StatusHeaderText.Text = "Paused";
            }
            else
            {
                automation.Resume();
                PauseIcon.Visibility = Visibility.Visible;
                PlayIcon.Visibility = Visibility.Collapsed;
                PauseResumeText.Text = "Pause";
                StatusHeaderText.Text = "Running...";
            }
        }
        
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Do you want to close the browser when stopping?",
                "Stop Automation",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Cancel)
                return;
            
            bool closeBrowser = result == MessageBoxResult.Yes;
            
            automation.Stop(closeBrowser);
            mainWindow.HideRunningPage();
        }
    }
}
