using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace RathoreSearchAutomation.Helpers
{
    /// <summary>
    /// Per-profile persistent store of searched keys. JSON file format:
    /// {
    ///   "date": "YYYY-MM-DD",
    ///   "keys": ["query1", "query2", ...],
    ///   "resolution": "optional", // reserved for future use
    ///   "coordinate": {"x": 0, "y": 0} // reserved for future use
    /// }
    /// Automatically resets keys when stored date != today.
    /// </summary>
    public class ProfileDataStore
    {
        private readonly int _profileKey;
        private readonly string _path;
        private DataModel _data = new();

        private class DataModel
        {
            public string date { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd");
            public List<string> keys { get; set; } = new();
            public string? resolution { get; set; }
            public Coordinate? coordinate { get; set; }
        }

        public class Coordinate
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        public ProfileDataStore(int profileKey)
        {
            _profileKey = profileKey;
            var dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
            Directory.CreateDirectory(dataDir);
            _path = Path.Combine(dataDir, $"profile_{_profileKey}.json");
            Load();
            ResetIfDateChanged();
        }

        private void Load()
        {
            try
            {
                if (File.Exists(_path))
                {
                    var json = File.ReadAllText(_path);
                    var model = JsonSerializer.Deserialize<DataModel>(json);
                    if (model != null)
                    {
                        _data = model;
                        return;
                    }
                }
            }
            catch { }
            _data = new DataModel();
            Save();
        }

        private void Save()
        {
            try
            {
                var json = JsonSerializer.Serialize(_data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_path, json);
            }
            catch { }
        }

        public void ResetIfDateChanged()
        {
            var today = DateTime.UtcNow.ToString("yyyy-MM-dd");
            if (!string.Equals(_data.date, today, StringComparison.Ordinal))
            {
                _data.date = today;
                _data.keys.Clear();
                Save();
            }
        }

        public bool Contains(string key) => _data.keys.Contains(key);

        public void AddKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return;
            if (_data.keys.Contains(key)) return;
            _data.keys.Add(key);
            Save();
        }

        /// <summary>
        /// Writes used keys to a temporary file for Python to read and exclude.
        /// </summary>
        public string WriteUsedKeysFile()
        {
            var tempPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"used_keys_{_profileKey}.txt");
            try
            {
                File.WriteAllLines(tempPath, _data.keys);
            }
            catch { }
            return tempPath;
        }
    }
}
