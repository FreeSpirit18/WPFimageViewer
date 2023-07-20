using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using WinForms = System.Windows.Forms;

namespace ImageViewer
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            DataContext = this;
            Display = emty;
            _folderImages = new ObservableCollection<string>();


            InitializeComponent();
        }
        

        

        private readonly string emty = "C:\\Users\\admin\\Documents\\GitHub\\WPFimageViewer\\ImageViewer\\ImageViewer\\images\\No_Image_Available.jpg";

        public event PropertyChangedEventHandler? PropertyChanged;
        


        
        private string _display;
        public string Display
        {
            get { return _display; }
            set { 
                _display = value;
                OnPropertyChange("Display");
            }
        }

        private string[] imagePaths;
        public string[] ImagePaths
        {
            get { return imagePaths; }
            set
            {
                imagePaths = value;
                OnPropertyChange("ImagePaths");
            }
        }

        private ObservableCollection<string> _folderImages;
        public ObservableCollection<string> FolderImages
        {
            get { return _folderImages; }
            set
            {
                _folderImages = value;
                OnPropertyChange("FolderImages");
            }
        }
        private void Select_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog dialog = new WinForms.FolderBrowserDialog();
            WinForms.DialogResult result = dialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                string folder = dialog.SelectedPath;

                string[] allFiles = Directory.GetFiles(folder);


                string[] imageExtensions = { ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".tiff" };
                //FolderImages.Add("");
                ImagePaths = allFiles.Where(file => imageExtensions.Contains(Path.GetExtension(file).ToLower())).ToArray();

                FolderImages.Clear();
                foreach(string image in ImagePaths.Select(file => Path.GetFileName(file)).ToArray())
                    FolderImages.Add(image);

            }
            else
            {
                //MessageBox.Show("Could not open image.", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /*private void Select_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.tiff";

            bool? sucsess = fileDialog.ShowDialog();
            if (sucsess == true)
            {
                string path = fileDialog.FileName;
                display = path;
            }
            else
            {
                MessageBox.Show("Could not open image.", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }*/

        

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Display = emty;
        }

        private void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


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
            
        }
    }
}
