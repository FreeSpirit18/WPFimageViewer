using ImageViewer.MVVM.ViewModel;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using WinForms = System.Windows.Forms;

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
