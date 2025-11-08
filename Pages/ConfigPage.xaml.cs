using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using RathoreSearchAutomation.Helpers;
using System.Linq;
using System.Collections.Generic;

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

        // --- Command line handling ---
        private void CommandTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                RunCommand();
            }
        }

        private void RunCommandButton_Click(object sender, RoutedEventArgs e)
        {
            RunCommand();
        }

    private void RunCommand()
        {
            var cmd = (CommandTextBox.Text ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(cmd))
            {
                MessageBox.Show("Please enter a command.", "Command", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (!int.TryParse(SearchCountTextBox.Text, out int searchCount) || searchCount < 1)
            {
                MessageBox.Show("Please enter a valid number of searches (minimum 1).", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!TryParseCommand(cmd, out var options, out string? error))
            {
                MessageBox.Show(error ?? "Invalid command.", "Command Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!options.RunAll)
            {
                MessageBox.Show("This command requires -run_all. For single runs, use Start Search.", "Command", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var allProfiles = EdgeProfileDetector.DetectEdgeProfiles();

            // Build exclusion set from tokens (names or numeric keys)
            var excludeKeys = new HashSet<int>();
            var notFound = new List<string>();
            if (options.ExcludeTokens != null && options.ExcludeTokens.Count > 0)
            {
                foreach (var raw in options.ExcludeTokens)
                {
                    var token = (raw ?? string.Empty).Trim().Trim('"');
                    if (string.IsNullOrWhiteSpace(token)) continue;

                    // Numeric key?
                    if (int.TryParse(token, out int k))
                    {
                        if (allProfiles.ContainsKey(k)) excludeKeys.Add(k);
                        else notFound.Add(token);
                        continue;
                    }

                    // Named match (case-insensitive). Special-case "default" -> key 0
                    if (string.Equals(token, "default", System.StringComparison.OrdinalIgnoreCase) && allProfiles.ContainsKey(0))
                    {
                        excludeKeys.Add(0);
                        continue;
                    }

                    var match = allProfiles.FirstOrDefault(p => string.Equals(p.Value, token, System.StringComparison.OrdinalIgnoreCase));
                    if (!match.Equals(default(KeyValuePair<int, string>)))
                    {
                        excludeKeys.Add(match.Key);
                    }
                    else
                    {
                        notFound.Add(token);
                    }
                }
            }

            IEnumerable<KeyValuePair<int, string>> profiles = allProfiles.Where(p => !excludeKeys.Contains(p.Key));

            var profileKeys = profiles.Select(p => p.Key).ToList();
            if (profileKeys.Count == 0)
            {
                MessageBox.Show("No profiles to run after applying exclusions.", "Command", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            int repeats = options.Repeats > 0 ? options.Repeats : 1;

            if (notFound.Count > 0)
            {
                MessageBox.Show($"Some excludes did not match any profile: {string.Join(", ", notFound)}\nContinuing with matched exclusions.", "Command Warning", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // Confirm and start batch runs
            mainWindow.StartBatchRuns(searchCount, profileKeys, repeats);
        }

        private class CommandOptions
        {
            public bool RunAll { get; set; }
            public List<string> ExcludeTokens { get; set; } = new List<string>();
            public int Repeats { get; set; } = 1;
        }

        private bool TryParseCommand(string command, out CommandOptions options, out string? error)
        {
            options = new CommandOptions();
            error = null;

            try
            {
                // Tokenize by spaces but keep braces content intact: we'll regex specific flags
                // Patterns: -run_all, -e{"name1,name2"} or -e{name1,name2}, t{n}
                var runAll = Regex.IsMatch(command, "(^|\\s)-run_all(\\s|$)", RegexOptions.IgnoreCase);
                options.RunAll = runAll;

                var eMatch = Regex.Match(command, "-e\\{(.*?)\\}", RegexOptions.IgnoreCase);
                if (eMatch.Success)
                {
                    var list = eMatch.Groups[1].Value;
                    foreach (var part in list.Split(','))
                    {
                        var token = part.Trim();
                        if (!string.IsNullOrWhiteSpace(token)) options.ExcludeTokens.Add(token);
                    }
                }

                var tMatch = Regex.Match(command, "t\\{(\\d+)\\}", RegexOptions.IgnoreCase);
                if (tMatch.Success && int.TryParse(tMatch.Groups[1].Value, out int t))
                {
                    if (t < 1 || t > 1000)
                    {
                        error = "Repeat count t{n} must be between 1 and 1000.";
                        return false;
                    }
                    options.Repeats = t;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                error = $"Failed to parse command: {ex.Message}";
                return false;
            }
        }
    }
}
