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

            BlurActive = true;
            TreshActive = true;
            ErodeDilateActive = true;
            EdgeActive = true;

            /*
            ThresholdType.Add("Binary");
            ThresholdType.Add("BinaryInv");
            ThresholdType.Add("Trunc");
            ThresholdType.Add("ToZero");
            ThresholdType.Add("ToZeroInv");
            ThresholdType.Add("Mask");
            ThresholdType.Add("Otsu");
            */

            SelectCommand = new RelayCommand(execute => Select_Click(), canExecute => { return true; });
            ClearCommand = new RelayCommand(execute => Clear_Click(), canExecute => { return true; });
            ToggleColor = new RelayCommand(execute => Toggle_Color(), canExecute => { return true; });
            SaveCommand = new RelayCommand(execute => SaveImage(), canExecute => { return true; });
        }
        public RelayCommand SelectCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public RelayCommand ToggleColor { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        private readonly string emty = "C:\\Users\\admin\\Documents\\GitHub\\WPFimageViewer\\ImageViewer\\ImageViewer\\images\\No_Image_Available.jpg";

        private string folder { get; set; }

        //---------Blur--------------------------
        private bool blurActive;
        public bool BlurActive
        {
            get { return blurActive; }
            set
            {
                blurActive = value;
                ApplyFilter();
                OnPropertyChange("BlurActive");
            }
        }

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
        private bool treshActive;
        public bool TreshActive
        {
            get { return treshActive; }
            set
            {
                treshActive = value;
                ApplyFilter();
                OnPropertyChange("TreshActive");
            }
        }

        /*private TreshFilterSettings treshSettings;
        public TreshFilterSettings TreshSettings
        {
            get { return treshSettings; }
            set
            {
                treshSettings = value;
                ApplyFilter();
                OnPropertyChange("TreshSettings");
            }
        }*/

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
        private bool erodeDilateActive;
        public bool ErodeDilateActive
        {
            get { return erodeDilateActive; }
            set
            {
                erodeDilateActive = value;
                ApplyFilter();
                OnPropertyChange("ErodeDilateActive");
            }
        }
        
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
        private bool edgeActive;
        public bool EdgeActive
        {
            get { return edgeActive; }
            set
            {
                edgeActive = value;
                ApplyFilter();
                OnPropertyChange("EdgeActive");
            }
        }
        
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
                if (displayImage != null)
                {
                    BgrImage = new Image<Bgr, byte>(value.Path);
                    GrayImage = new Image<Gray, byte>(value.Path);
                }
                EdgeTresh1 = 0;
                EdgeTresh2 = 0;
                BlurValue = 0;
                TreshValue = 0;
                MaxTreshValue = 0;
                ErodeIterations = 0;
                DilateIterations = 0;


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
                folder = dialog.SelectedPath;

                Select_Images();
            }
            else
            {
                //MessageBox.Show("Could not open image.", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Select_Images()
        {
            string[] allFiles = Directory.GetFiles(folder);


            string[] imageExtensions = { ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".tiff" };

            string[] ImagePaths = allFiles.Where(file => imageExtensions.Contains(Path.GetExtension(file).ToLower())).ToArray();

            Images.Clear();
            for (int i = 0; i < ImagePaths.Length; i++)
            {
                Images.Add(new FolderImage(Path.GetFileName(ImagePaths[i]), ImagePaths[i]));

            }
            DisplayImage = new FolderImage("", emty);

        }

        private void Toggle_Color() { 
            // Toggle the ImageColor property between true and false
            ImageColor = !ImageColor;
            ApplyFilter();
        }

        private void Clear_Click()
        {
            DisplayImage = new FolderImage("", emty);
            //Images.Clear();
        }

        // ... Your other code ...

        public void SaveImage()
        {
            // Show the SaveFileDialog to get the file path and name
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                if (ImageColor)
                {

                    var bgrImageToSave = BgrImage.ToBitmap();

                    using (bgrImageToSave)
                    {
                        bgrImageToSave.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
                else
                {
                    var grayImageToSave = GrayImage.ToBitmap();

                    using (grayImageToSave)
                    {
                        grayImageToSave.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
                Select_Images();
            }
        }

        private void ApplyFilter()
        {
            if (DisplayImage != null)
            {

                using (Image<Gray, byte> grayImage = ImageColor ? null : new Image<Gray, byte>(DisplayImage.Path))
                using (Image<Bgr, byte> bgrImage = ImageColor ? new Image<Bgr, byte>(DisplayImage.Path) : null)
                {

                    if (BlurValue > 0 && BlurActive)
                    {
                        System.Drawing.Size blurSize = new System.Drawing.Size(31, 31); // You can adjust the blur size as needed
                        CvInvoke.GaussianBlur((ImageColor == true ? bgrImage:grayImage), (ImageColor == true ? bgrImage : grayImage), blurSize, BlurValue, BlurValue);
                    }

                    if (TreshValue > 0 && TreshActive)
                    {
                        CvInvoke.Threshold((ImageColor == true ? bgrImage : grayImage), (ImageColor == true ? bgrImage : grayImage), TreshValue, MaxTreshValue, 0);
                    }
                    if (ErodeIterations > 1 && ErodeDilateActive)
                    {
                        var erodeElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new System.Drawing.Size(3, 3), new System.Drawing.Point(-1, -1));
                        CvInvoke.Erode((ImageColor == true ? bgrImage : grayImage), (ImageColor == true ? bgrImage : grayImage), erodeElement, new System.Drawing.Point(-1, -1), ErodeIterations, BorderType.Default, new MCvScalar(255, 255, 255));
                    }

                    if (DilateIterations > 1 && ErodeDilateActive)
                    {
                        var dilateElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new System.Drawing.Size(3, 3), new System.Drawing.Point(-1, -1));
                        CvInvoke.Dilate((ImageColor == true ? bgrImage : grayImage), (ImageColor == true ? bgrImage : grayImage), dilateElement, new System.Drawing.Point(-1, -1), DilateIterations, BorderType.Default, new MCvScalar(255, 255, 255));
                    }

                    if ((EdgeTresh1 > 0 || EdgeTresh2 > 0) && !ImageColor && EdgeActive)
                    {
                        Image<Gray, byte> Temp = grayImage;

                        CvInvoke.Canny(Temp, grayImage, EdgeTresh1, EdgeTresh2);

                    }

                    //Bitmap blurredBitmap = image.ToBitmap();

                    if (ImageColor)
                    {
                        Bitmap blurredBitmap = bgrImage.ToBitmap();
                        BgrImage = blurredBitmap.ToImage<Bgr, byte>();
                    }
                    else
                    {
                        Bitmap blurredBitmap = grayImage.ToBitmap();
                        GrayImage = blurredBitmap.ToImage<Gray, byte>();
                    }

                }
            }
        }


    }
}
