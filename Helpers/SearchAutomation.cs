using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace RathoreSearchAutomation.Helpers
{
    public class SearchAutomation
    {
    private readonly int totalSearches;
    private readonly int profileKey;
    private readonly ProfileDataStore dataStore;
        private Process? pythonProcess;
        private readonly string controlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "automation_control.json");
        private volatile bool completedSignaled;
        
        public event Action<int, int>? OnProgressUpdate;
        public event Action<string>? OnStatusUpdate;
        public event Action? OnCompleted;
        public event Action<string>? OnError;
        
        public SearchAutomation(int searches, int profile)
        {
            totalSearches = searches;
            profileKey = profile;
            dataStore = new ProfileDataStore(profileKey);
        }
        
        public void Start()
        {
            Task.Run(() => RunAutomation());
        }
        
        private void RunAutomation()
        {
            try
            {
                OnStatusUpdate?.Invoke("Starting Edge browser...");
                
                // Get the path to the Python script
                string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "search_runner.py");
                
                if (!File.Exists(scriptPath))
                {
                    OnError?.Invoke("Python script 'search_runner.py' not found!");
                    return;
                }
                
                // Create process start info
                // Initialize control file (not paused, not stopped)
                File.WriteAllText(controlFilePath, "{\"paused\": false, \"stop\": false}");

                // Prepare used keys file for Python exclusion
                dataStore.ResetIfDateChanged();
                string usedKeysFile = dataStore.WriteUsedKeysFile();

                var startInfo = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = $"\"{scriptPath}\" {totalSearches} {profileKey}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };
                // Provide control path to Python
                startInfo.EnvironmentVariables["RSA_CONTROL_FILE"] = controlFilePath;
                startInfo.EnvironmentVariables["RSA_USED_KEYS_FILE"] = usedKeysFile;
                
                pythonProcess = new Process { StartInfo = startInfo };
                
                // Handle output
                pythonProcess.OutputDataReceived += (sender, e) =>
                {
                    if (string.IsNullOrEmpty(e.Data)) return;
                    
                    string output = e.Data.Trim();
                    
                    if (output.StartsWith("STATUS:"))
                    {
                        string status = output.Substring(7).Trim();
                        OnStatusUpdate?.Invoke(status);
                    }
                    else if (output.StartsWith("PROGRESS:"))
                    {
                        // Parse "PROGRESS: X/Y"
                        string progress = output.Substring(9).Trim();
                        var parts = progress.Split('/');
                        if (parts.Length == 2 && 
                            int.TryParse(parts[0], out int current) && 
                            int.TryParse(parts[1], out int total))
                        {
                            OnProgressUpdate?.Invoke(current, total);
                        }
                    }
                    else if (output.StartsWith("QUERY:"))
                    {
                        string query = output.Substring(6).Trim();
                        dataStore.AddKey(query);
                    }
                    else if (output.Contains("COMPLETED"))
                    {
                        if (!completedSignaled)
                        {
                            completedSignaled = true;
                            OnCompleted?.Invoke();
                        }
                    }
                    else if (output.StartsWith("ERROR:"))
                    {
                        string error = output.Substring(6).Trim();
                        OnError?.Invoke(error);
                    }
                };
                
                // Handle errors
                pythonProcess.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        OnError?.Invoke(e.Data);
                    }
                };
                
                // Start the process
                pythonProcess.EnableRaisingEvents = true;
                pythonProcess.Exited += (s, e) =>
                {
                    // If script ends without emitting COMPLETED, still move forward
                    OnStatusUpdate?.Invoke("Python script exited");
                    if (!completedSignaled)
                    {
                        completedSignaled = true;
                        OnCompleted?.Invoke();
                    }
                };

                pythonProcess.Start();
                pythonProcess.BeginOutputReadLine();
                pythonProcess.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Failed to start automation: {ex.Message}");
            }
        }
        
        public void Pause()
        {
            try { File.WriteAllText(controlFilePath, "{\"paused\": true, \"stop\": false}"); } catch { }
            OnStatusUpdate?.Invoke("Paused - Automation stopped");
        }
        
        public void Resume()
        {
            try { File.WriteAllText(controlFilePath, "{\"paused\": false, \"stop\": false}"); } catch { }
            OnStatusUpdate?.Invoke("Resuming...");
        }
        
        public void Stop(bool closeBrowser)
        {
            try { File.WriteAllText(controlFilePath, "{\"paused\": false, \"stop\": true}"); } catch { }
            if (pythonProcess != null && !pythonProcess.HasExited)
            {
                try
                {
                    // Give the script up to 2s to exit gracefully, then kill
                    if (!pythonProcess.WaitForExit(2000))
                    {
                        pythonProcess.Kill();
                    }
                }
                catch { }
            }
            
            if (closeBrowser)
            {
                try
                {
                    foreach (var process in Process.GetProcessesByName("msedge"))
                    {
                        process.Kill();
                    }
                }
                catch { }
            }
            
            OnStatusUpdate?.Invoke(closeBrowser ? 
                "Stopping automation and closing browser..." : 
                "Stopping automation (browser remains open)...");
        }
    }
}
