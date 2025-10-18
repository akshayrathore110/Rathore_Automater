using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RathoreSearchAutomation.Helpers
{
    public static class EdgeProfileDetector
    {
        public static Dictionary<int, string> DetectEdgeProfiles()
        {
            var profiles = new Dictionary<int, string>();
            
            try
            {
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string userDataDir = Path.Combine(localAppData, "Microsoft", "Edge", "User Data");
                
                if (!Directory.Exists(userDataDir))
                {
                    // Return default profile if Edge is not installed
                    profiles.Add(0, "Default");
                    return profiles;
                }
                
                var directories = Directory.GetDirectories(userDataDir);
                
                foreach (var dir in directories)
                {
                    string dirName = Path.GetFileName(dir);
                    
                    if (dirName == "Default")
                    {
                        profiles.Add(0, "Default");
                    }
                    else if (dirName.StartsWith("Profile "))
                    {
                        try
                        {
                            // Extract profile number from "Profile X"
                            string numberPart = dirName.Substring(8); // After "Profile "
                            if (int.TryParse(numberPart, out int profileNum))
                            {
                                profiles.Add(profileNum, dirName);
                            }
                        }
                        catch
                        {
                            // Skip invalid profile names
                            continue;
                        }
                    }
                }
                
                // Ensure at least default profile exists
                if (profiles.Count == 0)
                {
                    profiles.Add(0, "Default");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error detecting Edge profiles: {ex.Message}");
                profiles.Add(0, "Default");
            }
            
            return profiles.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value);
        }
    }
}
