using System.Text.Json;
using TasksManager.PersistenceContracts;

namespace TasksManager.Services
{
    public class DatabasePathProvider : IDatabasePathProvider
    {
        private readonly string _dbPath;

        public DatabasePathProvider()
        {
            var settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");

            if (!File.Exists(settingsPath))
                throw new FileNotFoundException($"Configuration file not found: {settingsPath}");

            using var stream = File.OpenRead(settingsPath);
            var doc = JsonDocument.Parse(stream);
            var root = doc.RootElement;

            if (!root.TryGetProperty("Database", out var dbSection))
                throw new InvalidOperationException("Missing required section 'Database' in appsettings.json.");

            if (!dbSection.TryGetProperty("Directory", out var dirElement) || string.IsNullOrWhiteSpace(dirElement.GetString()))
                throw new InvalidOperationException("Missing or empty 'Database:Directory' in appsettings.json.");

            if (!dbSection.TryGetProperty("Filename", out var fileElement) || string.IsNullOrWhiteSpace(fileElement.GetString()))
                throw new InvalidOperationException("Missing or empty 'Database:Filename' in appsettings.json.");

            _dbPath = Path.Combine(AppContext.BaseDirectory, dirElement.GetString()!, fileElement.GetString()!);
        }

        public string GetDatabasePath() => _dbPath;
    }
}
