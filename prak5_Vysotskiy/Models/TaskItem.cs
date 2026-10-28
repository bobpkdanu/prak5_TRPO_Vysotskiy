using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace prak5_Vysotskiy.Models
{
    public class TaskItem : INotifyPropertyChanged
    {
        private string title = "";
        private string executor = "";
        private int progress;

        public string Title
        {
            get => title;
            set { title = value; OnPropertyChanged(); }
        }

        public string Executor
        {
            get => executor;
            set { executor = value; OnPropertyChanged(); }
        }

        public int Progress
        {
            get => progress;
            set
            {
                progress = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
