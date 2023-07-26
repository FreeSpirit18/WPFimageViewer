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
using Emgu.CV.CvEnum;

namespace ImageViewer.MVVM.ViewModel
{
    internal class MainWindowVM : ViewModelBase
    {
        
        public MainWindowVM() {
            DisplayImage = new FolderImage("", emty);
            Images = new ObservableCollection<FolderImage>();
            thresholdType = new ObservableCollection<string>();//make dictionary <string, int>

            ThresholdType.Add("Binary");
            ThresholdType.Add("BinaryInv");
            ThresholdType.Add("Trunc");
            ThresholdType.Add("ToZero");
            ThresholdType.Add("ToZeroInv");
            ThresholdType.Add("Mask");
            ThresholdType.Add("Otsu");

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

        private double treshValue;
        public double TreshValue
        {
            get { return treshValue; }
            set
            {
                treshValue = value;
                ApplyBlur();
                OnPropertyChange("TreshValue");
            }
        }

        private double maxTreshValue;
        public double MaxTreshValue
        {
            get { return maxTreshValue; }
            set
            {
                maxTreshValue = value;
                ApplyBlur();
                OnPropertyChange("MaxTreshValue");
            }
        }


        private FolderImage displayImage;

        public FolderImage DisplayImage
        {
            get { return displayImage; }
            set
            {
                displayImage = value;
                FilterImage = new Image<Bgr, byte>(displayImage.Path);
                BlurValue = 0;
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

        private ObservableCollection<string> thresholdType;
        public ObservableCollection<string> ThresholdType
        {
            get { return thresholdType; }
            set
            {
                thresholdType = value;
                OnPropertyChange("ThresholdType");
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
                
                using (Image<Bgr, byte> image = new Image<Bgr, byte>(DisplayImage.Path))//needs to change!!
                {
                    double sigma = BlurValue; // Use the slider value as the sigma for the Gaussian blur
                    
                    System.Drawing.Size blurSize = new System.Drawing.Size(31, 31); // You can adjust the blur size as needed

                    if(sigma > 0)
                    {
                        CvInvoke.GaussianBlur(image, image, blurSize, sigma, sigma);
                    }

                    if (TreshValue > 0)
                    {
                        CvInvoke.Threshold(image, image, TreshValue, MaxTreshValue, 0);
                    }


                    // Convert the Emgu.CV Mat to a System.Drawing.Bitmap
                    Bitmap blurredBitmap = image.ToBitmap();

                    // Update the DisplayImage with the blurred image
                    FilterImage = blurredBitmap.ToImage<Bgr, byte>();
                    
                    
                    
                }
            }
        }


    }
}
