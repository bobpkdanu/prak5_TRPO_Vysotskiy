using System.Linq;
using System.Windows;
using System.Windows.Controls;
using prak5_Vysotskiy.Models;

namespace prak5_Vysotskiy.Pages
{
    public partial class ProjectFormPage : Page
    {
        private Project project;

        public ProjectFormPage(Project selectedProject)
        {
            InitializeComponent();
            project = selectedProject;
            DataContext = project;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new TaskFormPage(project));
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TasksList.SelectedItem is TaskItem task)
                project.Tasks.Remove(task);
            else
                MessageBox.Show("Сначала выберите задачу");
        }
        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (TasksList.SelectedItem is TaskItem task)
            {
                NavigationService?.Navigate(new TaskFormPage(task));
            }
            else
            {
                MessageBox.Show("Сначала выберите задачу");
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
