using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace prak5_Vysotskiy.Models
{
    public class Project : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string name = "";
        private string manager = "";
        private DateTime startDate = DateTime.Today;

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        public string Manager
        {
            get => manager;
            set { manager = value; OnPropertyChanged(); }
        }

        public DateTime StartDate
        {
            get => startDate;
            set { startDate = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TaskItem> Tasks { get; set; } = new();

        [JsonIgnore]
        public double ProjectProgress => Tasks.Count == 0 ? 0 : Tasks.Average(t => t.Progress);

        public Project()
        {
            Tasks.CollectionChanged += Tasks_CollectionChanged;
        }

        private void Tasks_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null) 
                foreach (TaskItem task in e.NewItems)
                {
                    task.PropertyChanged += Task_PropertyChanged;
                }
            if (e.OldItems != null) 
                foreach (TaskItem task in e.OldItems)
                {
                    task.PropertyChanged -= Task_PropertyChanged;
                }
            OnPropertyChanged(nameof(ProjectProgress));
        }

        private void Task_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TaskItem.Progress)) OnPropertyChanged(nameof(ProjectProgress));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        public void RefreshTaskHandlers()
        {
            Tasks.CollectionChanged -= Tasks_CollectionChanged;
            Tasks.CollectionChanged += Tasks_CollectionChanged;

            foreach (TaskItem task in Tasks)
            {
                task.PropertyChanged -= Task_PropertyChanged;
                task.PropertyChanged += Task_PropertyChanged;
            }
        }
    }
}
