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
        private Process? pythonProcess;
        
        public event Action<int, int>? OnProgressUpdate;
        public event Action<string>? OnStatusUpdate;
        public event Action? OnCompleted;
        public event Action<string>? OnError;
        
        public SearchAutomation(int searches, int profile)
        {
            totalSearches = searches;
            profileKey = profile;
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
                var startInfo = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = $"\"{scriptPath}\" {totalSearches} {profileKey}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };
                
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
                    else if (output.Contains("COMPLETED"))
                    {
                        OnCompleted?.Invoke();
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
                pythonProcess.Start();
                pythonProcess.BeginOutputReadLine();
                pythonProcess.BeginErrorReadLine();
                
                pythonProcess.WaitForExit();
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Failed to start automation: {ex.Message}");
            }
        }
        
        public void Pause()
        {
            OnStatusUpdate?.Invoke("Paused - Automation stopped");
        }
        
        public void Resume()
        {
            OnStatusUpdate?.Invoke("Resuming...");
        }
        
        public void Stop(bool closeBrowser)
        {
            if (pythonProcess != null && !pythonProcess.HasExited)
            {
                try
                {
                    pythonProcess.Kill();
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
