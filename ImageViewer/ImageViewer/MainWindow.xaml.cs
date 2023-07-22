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
        

        /*

        private void folderImages_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int imageIndex = folderImages.SelectedIndex;
            if (imageIndex != -1)
            {
                Display = imagePaths[imageIndex];
            }
            else
            {
                Display = emty;
            }
            
        }*/
    }
}
