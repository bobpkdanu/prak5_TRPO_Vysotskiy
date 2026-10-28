using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using prak5_Vysotskiy.Models;

namespace prak5_Vysotskiy.Services
{
    public static class JsonService
    {
        private static readonly string FileName = Path.Combine(AppContext.BaseDirectory, "projects.json");

        public static void Save(ObservableCollection<Project> projects)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(FileName, JsonSerializer.Serialize(projects, options));
        }

        public static ObservableCollection<Project> Load()
        {
            if (!File.Exists(FileName)) return new ObservableCollection<Project>();
            try
            {
                var projects = JsonSerializer.Deserialize<ObservableCollection<Project>>(File.ReadAllText(FileName));

                projects ??= new ObservableCollection<Project>();

                foreach (Project project in projects)
                {
                    project.RefreshTaskHandlers();
                }
                return projects;
            }
            catch
            {
                return new ObservableCollection<Project>();
            }
        }
    }
}
