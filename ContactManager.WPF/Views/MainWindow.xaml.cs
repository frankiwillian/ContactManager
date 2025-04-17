using ContactManager.WPF.ViewModels;
using System.Windows;

namespace ContactManager.WPF.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }
}