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
using System;

namespace ImageViewer.MVVM.ViewModel
{
    internal class MainWindowVM : ViewModelBase
    {
        
        public MainWindowVM() {
            DisplayImage = new FolderImage("", emty);
            Images = new ObservableCollection<FolderImage>();
            thresholdType = new ObservableCollection<string>();//make dictionary <string, int>
            //treshSettings = new TreshFilterSettings(0,0, true);
            ImageColor = true;

            ThresholdType.Add("Binary");
            ThresholdType.Add("BinaryInv");
            ThresholdType.Add("Trunc");
            ThresholdType.Add("ToZero");
            ThresholdType.Add("ToZeroInv");
            ThresholdType.Add("Mask");
            ThresholdType.Add("Otsu");

            SelectCommand = new RelayCommand(execute => Select_Click(), canExecute => { return true; });
            ClearCommand = new RelayCommand(execute => Clear_Click(), canExecute => { return true; });
            ToggleColor = new RelayCommand(execute => Toggle_Color(), canExecute => { return true; });
        }
        public RelayCommand SelectCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public RelayCommand ToggleColor { get; private set; }

        private readonly string emty = "C:\\Users\\admin\\Documents\\GitHub\\WPFimageViewer\\ImageViewer\\ImageViewer\\images\\No_Image_Available.jpg";

        //---------Blur--------------------------
        private double blurValue;

        public double BlurValue
        {
            get { return blurValue; }
            set
            {
                blurValue = value;
                ApplyFilter();
                OnPropertyChange("BlurValue");
            }
        }
        //----------Tresholdin--------------------------------
        private TreshFilterSettings treshSettings;

        public TreshFilterSettings TreshSettings
        {
            get { return treshSettings; }
            set
            {
                treshSettings = value;
                ApplyFilter();
                OnPropertyChange("TreshSettings");
            }
        }

        private double treshValue;
        public double TreshValue
        {
            get { return treshValue; }
            set
            {
                treshValue = value;
                ApplyFilter();
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
                ApplyFilter();
                OnPropertyChange("MaxTreshValue");
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
        //-----------erode and dialate -----------------
        private int erodeIterations;
        public int ErodeIterations
        {
            get { return erodeIterations; }
            set
            {
                erodeIterations = value;
                ApplyFilter();
                OnPropertyChange("ErodeIterations");
            }
        }

        private int dilateIterations;
        public int DilateIterations
        {
            get { return dilateIterations; }
            set
            {
                dilateIterations = value;
                ApplyFilter();
                OnPropertyChange("DilateIterations");
            }
        }
        //-----------Edge-------------------------------
        private double edgeTresh1;
        public double EdgeTresh1
        {
            get { return edgeTresh1; }
            set
            {
                edgeTresh1 = value;
                ApplyFilter();
                OnPropertyChange("EdgeTresh1");
            }
        }

        private double edgeTresh2;
        public double EdgeTresh2
        {
            get { return edgeTresh2; }
            set
            {
                edgeTresh2 = value;
                ApplyFilter();
                OnPropertyChange("EdgeTresh2");
            }
        }
        //--------------------------------------------------
        private FolderImage displayImage;
        public FolderImage DisplayImage
        {
            get { return displayImage; }
            set
            {
                displayImage = value;
                
                BgrImage = new Image<Bgr, byte>(displayImage.Path);
                GrayImage = new Image<Gray, byte>(displayImage.Path);
                
                BlurValue = 0;
                OnPropertyChange("DisplayImage");
            }
        }

        //---------------------------------------------------
        private Image<Gray, byte> grayImage;
        public Image<Gray, byte> GrayImage
        {
            get { return grayImage; }
            set
            {
                grayImage = value;
                OnPropertyChange("GrayImage");
            }
        }

        private Image<Bgr, byte> bgrImage;
        public Image<Bgr, byte> BgrImage
        {
            get { return bgrImage; }
            set
            {
                bgrImage = value;
                OnPropertyChange("BgrImage");
            }
        }


        private bool imageColor;
        public bool ImageColor
        {
            get { return imageColor; }
            set
            {
                imageColor = value;
                OnPropertyChange("ImageColor");
            }
        }
        //---------------------------------------------------
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
        //-----------------------------------------------------
        
        //--------------------------------------------------
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

        private void Toggle_Color() { 
            // Toggle the ImageColor property between true and false
            ImageColor = !ImageColor;
        }

        private void Clear_Click()
        {
            DisplayImage = new FolderImage("", emty);
        }

        private void ApplyFilter()
        {
            if (DisplayImage != null)
            {
                
                using (Image<Gray, byte> image = new Image<Gray, byte>(DisplayImage.Path))//needs to change!!
                {

                    if(BlurValue > 0)
                    {
                        System.Drawing.Size blurSize = new System.Drawing.Size(31, 31); // You can adjust the blur size as needed
                        CvInvoke.GaussianBlur(image, image, blurSize, BlurValue, BlurValue);
                    }

                    if (TreshValue > 0)
                    {
                        CvInvoke.Threshold(image, image, TreshValue, MaxTreshValue, 0);
                    }
                    if (ErodeIterations > 1)
                    {
                        var erodeElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new System.Drawing.Size(3, 3), new System.Drawing.Point(-1, -1));
                        CvInvoke.Erode(image, image, erodeElement, new System.Drawing.Point(-1, -1), ErodeIterations, BorderType.Default, new MCvScalar(255, 255, 255));
                    }

                    if (DilateIterations > 1)
                    {
                        var dilateElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new System.Drawing.Size(3, 3), new System.Drawing.Point(-1, -1));
                        CvInvoke.Dilate(image, image, dilateElement, new System.Drawing.Point(-1, -1), DilateIterations, BorderType.Default, new MCvScalar(255, 255, 255));
                    }

                    if (EdgeTresh1 > 0 || EdgeTresh2 > 0)
                    {
                        Image<Gray, byte> grayImage = image.Convert<Gray, byte>();
                        //Image<Gray, byte> edgesImage = new Image<Gray, byte>(filterImage.Width, filterImage.Height);


                        CvInvoke.Canny(grayImage, image, 50, 100);

                    }

                    Bitmap blurredBitmap = image.ToBitmap();

                    //FilterImage = blurredBitmap.ToImage<Gray, byte>();
                }
            }
        }


    }
}
