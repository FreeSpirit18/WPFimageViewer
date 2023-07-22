using ImageViewer.MVVM.Model;
using ImageViewer.MVVM.ViewModel;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using WinForms = System.Windows.Forms;

namespace ImageViewer.MVVM.ViewModel
{
    internal class MainWindowVM : ViewModelBase
    {
        
        public MainWindowVM() {
            DisplayImage = new Image("", emty);
            Images = new ObservableCollection<Image>();
            

            SelectCommand = new RelayCommand(execute => Select_Click(), canExecute => { return true; });
            ClearCommand = new RelayCommand(execute => Clear_Click(), canExecute => { return true; });
        }
        public RelayCommand SelectCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }

        private readonly string emty = "C:\\Users\\admin\\Documents\\GitHub\\WPFimageViewer\\ImageViewer\\ImageViewer\\images\\No_Image_Available.jpg";

        private Image displayImage;

        public Image DisplayImage
        {
            get { return displayImage; }
            set
            {
                displayImage = value;
                OnPropertyChange("DisplayImage");
            }
        }
        private ObservableCollection<Image> images;
        public ObservableCollection<Image> Images
        {
            get { return images; }
            set
            {
                images = value;
                OnPropertyChange("Images");
            }
        }

        private void Select_Click()
        {
            WinForms.FolderBrowserDialog dialog = new WinForms.FolderBrowserDialog();
            
            WinForms.DialogResult result = dialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                string folder = dialog.SelectedPath;

                string[] allFiles = Directory.GetFiles(folder);


                string[] imageExtensions = { ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".tiff" };

                string[] ImagePaths = allFiles.Where(file => imageExtensions.Contains(Path.GetExtension(file).ToLower())).ToArray();

                Images.Clear();
                for (int i = 0; i < ImagePaths.Length; i++)
                {
                    Images.Add(new Image(Path.GetFileName(ImagePaths[i]), ImagePaths[i]));
                }

            }
            else
            {
                //MessageBox.Show("Could not open image.", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void Clear_Click()
        {
            DisplayImage = new Image("", emty);
        }


        private void folderImages_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            /*int imageIndex = folderImages.SelectedIndex;
            if (imageIndex != -1)
            {
                DisplayImage = Images[imageIndex];
            }
            else
            {
                DisplayImage = new Image("", emty);
            }*/

        }
    }
}
