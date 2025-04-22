using System.Windows;

namespace ApkInstaller.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void SelectFilesButton_Click(object sender, RoutedEventArgs e)
        {
            var installWindow = new InstallWindow();
            installWindow.Show();
            this.Hide(); 
        }
    }
}