using ImageViewer.MVVM.ViewModel;
using System.Windows;

namespace ImageViewer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            MainWindowVM vm = new MainWindowVM();
            DataContext = vm;
        }
       
    }
}
