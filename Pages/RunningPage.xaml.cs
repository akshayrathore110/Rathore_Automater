using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using RathoreSearchAutomation.Helpers;
using RathoreSearchAutomation.Helpers.Overlay;

namespace RathoreSearchAutomation.Pages
{
    public partial class RunningPage : Page
    {
        private readonly MainWindow mainWindow;
        private readonly SearchAutomation automation;
    private bool isPaused = false;
    private System.Windows.Threading.DispatcherTimer? pauseSoundTimer;
    private System.Media.SoundPlayer? pauseSoundPlayer;
    private WpfOverlayController? overlay;
        private readonly bool batchMode;
        private readonly System.Action? onSingleRunCompleted;
        
        public RunningPage(MainWindow window, int totalSearches, int profileKey, bool batchMode = false, System.Action? onSingleRunCompleted = null)
        {
            InitializeComponent();
            mainWindow = window;
            this.batchMode = batchMode;
            this.onSingleRunCompleted = onSingleRunCompleted;
            
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

            // Start overlay bound to Edge window; hotkeys control automation/app
            overlay = new WpfOverlayController(
                onPauseToggle: () => Dispatcher.Invoke(() => PauseResumeButton_Click(this, new RoutedEventArgs())),
                onStop: () => Dispatcher.Invoke(() => StopButton_Click(this, new RoutedEventArgs())),
                onExit: () => Dispatcher.Invoke(() => Application.Current.Shutdown())
            );
            overlay.Start();
        }
        
        private void OnProgressUpdate(int current, int total)
        {
            Dispatcher.Invoke(() =>
            {
                ProgressBar.Value = current;
                ProgressText.Text = $"{current} / {total}";
                overlay?.UpdateProgress(current, total);
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
                overlay?.Dispose();
                if (batchMode && onSingleRunCompleted != null)
                {
                    // Hide overlay frame and invoke next run
                    mainWindow.HideRunningPage();
                    onSingleRunCompleted.Invoke();
                }
                else
                {
                    mainWindow.ShowCompletionPage((int)ProgressBar.Maximum);
                }
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
                StartPauseSoundLoop();
            }
            else
            {
                automation.Resume();
                PauseIcon.Visibility = Visibility.Visible;
                PlayIcon.Visibility = Visibility.Collapsed;
                PauseResumeText.Text = "Pause";
                StatusHeaderText.Text = "Running...";
                StopPauseSoundLoop();
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
            overlay?.Dispose();
            mainWindow.HideRunningPage();
            StopPauseSoundLoop();
        }

        private void StartPauseSoundLoop()
        {
            if (pauseSoundTimer != null) return;
            // Expect user to drop file at Resources/audio/pause_notification.wav
            try
            {
                string soundPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "audio", "pause_notification.wav");
                if (System.IO.File.Exists(soundPath))
                {
                    pauseSoundPlayer = new System.Media.SoundPlayer(soundPath);
                }
            }
            catch { }
            pauseSoundTimer = new System.Windows.Threading.DispatcherTimer();
            pauseSoundTimer.Interval = System.TimeSpan.FromSeconds(3);
            pauseSoundTimer.Tick += (s, e) =>
            {
                try { pauseSoundPlayer?.Play(); } catch { }
            };
            pauseSoundTimer.Start();
        }

        private void StopPauseSoundLoop()
        {
            if (pauseSoundTimer != null)
            {
                pauseSoundTimer.Stop();
                pauseSoundTimer = null;
            }
            pauseSoundPlayer = null;
        }
    }
}
