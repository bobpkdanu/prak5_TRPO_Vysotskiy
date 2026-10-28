using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using prak5_Vysotskiy.Models;
using prak5_Vysotskiy.Services;

namespace prak5_Vysotskiy.Pages
{
    public partial class ProjectListPage : Page
    {
        private ObservableCollection<Project> projects;

        public ProjectListPage(ObservableCollection<Project> list)
        {
            InitializeComponent();
            projects = list;
            ProjectsList.ItemsSource = projects;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int id = projects.Count == 0 ? 1 : projects.Max(p => p.Id) + 1;
            var project = new Project { Id = id };
            projects.Add(project);
            NavigationService?.Navigate(new ProjectFormPage(project));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectsList.SelectedItem is Project project)
                NavigationService?.Navigate(new ProjectFormPage(project));
            else
                MessageBox.Show("Сначала выберите проект");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectsList.SelectedItem is Project project)
            {
                if (MessageBox.Show("Удалить выбранный проект?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    projects.Remove(project);
                }
            }
            else
                MessageBox.Show("Сначала выберите проект");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            JsonService.Save(projects);
            MessageBox.Show("Проекты сохранены в projects.json");
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            projects = JsonService.Load();
            ProjectsList.ItemsSource = projects;
            MessageBox.Show("Проекты загружены");
        }
    }
}
