using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;

namespace ImageViewer
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            DataContext = this;
            display = emty;
            InitializeComponent();
        }
        private string _display;

        private readonly string emty = "C:\\Users\\admin\\Documents\\GitHub\\WPFimageViewer\\ImageViewer\\ImageViewer\\images\\No_Image_Available.jpg";

        public event PropertyChangedEventHandler? PropertyChanged;

        public string display
        {
            get { return _display; }
            set { 
                _display = value;
                OnPropertyChange("display");
            }
        }

        private void Select_Click(object sender, RoutedEventArgs e)
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

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            display = emty;
        }

        private void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
