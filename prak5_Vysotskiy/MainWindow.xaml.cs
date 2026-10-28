using System.Windows;
using prak5_Vysotskiy.Models;
using prak5_Vysotskiy.Pages;
using prak5_Vysotskiy.Services;

namespace prak5_Vysotskiy
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ProjectListPage(JsonService.Load()));
        }
    }
}
