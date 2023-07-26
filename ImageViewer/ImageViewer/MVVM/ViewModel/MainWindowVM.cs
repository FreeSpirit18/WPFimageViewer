using ImageViewer.MVVM.Model;
using ImageViewer.MVVM.ViewModel;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using WinForms = System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Windows.Media;
using System.Drawing;

namespace ImageViewer.MVVM.ViewModel
{
    internal class MainWindowVM : ViewModelBase
    {
        
        public MainWindowVM() {
            DisplayImage = new FolderImage("", emty);
            Images = new ObservableCollection<FolderImage>();
            

            SelectCommand = new RelayCommand(execute => Select_Click(), canExecute => { return true; });
            ClearCommand = new RelayCommand(execute => Clear_Click(), canExecute => { return true; });
        }
        public RelayCommand SelectCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }

        private readonly string emty = "C:\\Users\\admin\\Documents\\GitHub\\WPFimageViewer\\ImageViewer\\ImageViewer\\images\\No_Image_Available.jpg";

        private double blurValue;

        public double BlurValue
        {
            get { return blurValue; }
            set
            {
                blurValue = value;
                ApplyBlur();
                OnPropertyChange("BlurValue");
            }
        }


        private FolderImage displayImage;

        public FolderImage DisplayImage
        {
            get { return displayImage; }
            set
            {
                displayImage = value;
                filterImage = new Image<Bgr, byte>(displayImage.Path);
                OnPropertyChange("FilterImage");
                OnPropertyChange("DisplayImage");
            }
        }

        private Image<Bgr, byte> filterImage;

        public Image<Bgr, byte> FilterImage
        {
            get { return filterImage; }
            set
            {
                filterImage = value;
                OnPropertyChange("FilterImage");
            }
        }

        private ObservableCollection<FolderImage> images;
        public ObservableCollection<FolderImage> Images
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
                    Images.Add(new FolderImage(Path.GetFileName(ImagePaths[i]), ImagePaths[i]));
                }

            }
            else
            {
                //MessageBox.Show("Could not open image.", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private void Clear_Click()
        {
            DisplayImage = new FolderImage("", emty);
        }

        private void ApplyBlur()
        {
            if (DisplayImage != null)
            {
                
                using (Image<Bgr, byte> image = FilterImage)//needs to change!!
                {
                    double sigma = BlurValue; // Use the slider value as the sigma for the Gaussian blur
                    System.Drawing.Size blurSize = new System.Drawing.Size(5, 5); // You can adjust the blur size as needed
                    CvInvoke.GaussianBlur(image, image, blurSize, sigma, sigma);

                    // Convert the Emgu.CV Mat to a System.Drawing.Bitmap
                    Bitmap blurredBitmap = image.ToBitmap();

                    // Update the DisplayImage with the blurred image
                    FilterImage = blurredBitmap.ToImage<Bgr, byte>();
                }
            }
        }


    }
}
