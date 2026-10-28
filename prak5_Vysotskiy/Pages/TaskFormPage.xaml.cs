using prak5_Vysotskiy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prak5_Vysotskiy.Pages
{
    public partial class TaskFormPage : Page
    {
        private Project project;
        private TaskItem? task;
        private bool isNew;

        public TaskFormPage(Project selectedProject)
        {
            InitializeComponent();

            project = selectedProject;
            isNew = true;
        }

        public TaskFormPage(TaskItem selectedTask)
        {
            InitializeComponent();

            task = selectedTask;
            isNew = false;

            TitleBox.Text = task.Title;
            ExecutorBox.Text = task.Executor;
            ProgressBox.Text = task.Progress.ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(ProgressBox.Text, out int progress)
                || progress < 0 || progress > 100)
            {
                MessageBox.Show("Введите процент от 0 до 100");
                return;
            }

            if (string.IsNullOrWhiteSpace(TitleBox.Text))
            {
                MessageBox.Show("Введите название задачи");
                return;
            }

            if (isNew)
            {
                project.Tasks.Add(new TaskItem
                {
                    Title = TitleBox.Text,
                    Executor = ExecutorBox.Text,
                    Progress = progress
                });
            }
            else if (task != null)
            {
                task.Title = TitleBox.Text;
                task.Executor = ExecutorBox.Text;
                task.Progress = progress;
            }

            NavigationService?.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
