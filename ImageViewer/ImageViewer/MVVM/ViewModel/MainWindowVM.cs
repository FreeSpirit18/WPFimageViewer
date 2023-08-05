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
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImageViewer.MVVM.ViewModel
{
    internal class MainWindowVM : ViewModelBase
    {
        
        public MainWindowVM() {
            DisplayImage = new FolderImage("", emty);
            Images = new ObservableCollection<FolderImage>();
            thresholdTypes = new ObservableCollection<KeyValuePair<string, ThresholdType>>()
            {
                new KeyValuePair<string, ThresholdType>("Binary", ThresholdType.Binary),
                new KeyValuePair<string, ThresholdType>("BinaryInv", ThresholdType.BinaryInv),
                new KeyValuePair<string, ThresholdType>("Trunc", ThresholdType.Trunc),
                new KeyValuePair<string, ThresholdType>("ToZero", ThresholdType.ToZero),
                new KeyValuePair<string, ThresholdType>("ToZeroInv", ThresholdType.ToZeroInv)
            };
            SelectedThreshold = new KeyValuePair<string, ThresholdType>("Binary", ThresholdType.Binary);

            Grayscale = false;

            BlurActive = true;
            TreshActive = true;
            ErodeDilateActive = true;
            EdgeActive = true;


            SelectCommand = new RelayCommand(execute => Select_Click(), canExecute => { return true; });
            ClearCommand = new RelayCommand(execute => Clear_Click(), canExecute => { return true; });
            SaveCommand = new RelayCommand(execute => SaveImage(), canExecute => { return true; });
        }
        public RelayCommand SelectCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        private readonly string emty = @"C:\\Users\\admin\\Documents\\GitHub\\WPFimageViewer\\ImageViewer\\ImageViewer\\images\\No_Image_Available.jpg";
        
        private const string dllPath = @"C:\\Users\\admin\\Documents\\GitHub\\WPFimageViewer\\ImageViewer\\x64\\Debug\OpencvWrapper.dll";

        [DllImport(dllPath)]
        private static extern void WrapGaussianBlur(Mat inputImage, System.Drawing.Size blurSize);
        
        private string Folder { get; set; }

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

        private int blurValue;
        public int BlurValue
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

        private ObservableCollection<KeyValuePair<string, ThresholdType>> thresholdTypes;
        public ObservableCollection<KeyValuePair<string, ThresholdType>> ThresholdTypes
        {
            get { return thresholdTypes; }
            set
            {
                thresholdTypes = value;
                OnPropertyChange("ThresholdType");
            }
        }
        private KeyValuePair<string, ThresholdType> selectedThreshold;
        public KeyValuePair<string, ThresholdType> SelectedThreshold
        {
            get { return selectedThreshold; }
            set
            {
                selectedThreshold = value;
                OnPropertyChange("SelectedThreshold");
                ApplyFilter();
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


        private bool grayscale;
        public bool Grayscale
        {
            get { return grayscale; }
            set
            {
                grayscale = value;
                OnPropertyChange("Grayscale");
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
        private void Select_Click()
        {
            WinForms.FolderBrowserDialog dialog = new WinForms.FolderBrowserDialog();
            
            WinForms.DialogResult result = dialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                Folder = dialog.SelectedPath;

                Select_Images();
            }
            else
            {
                //MessageBox.Show("Could not open image.", "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Select_Images()
        {
            string[] allFiles = Directory.GetFiles(Folder);


            string[] imageExtensions = { ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".tiff" };

            string[] ImagePaths = allFiles.Where(file => imageExtensions.Contains(Path.GetExtension(file).ToLower())).ToArray();

            Images.Clear();
            for (int i = 0; i < ImagePaths.Length; i++)
            {
                Images.Add(new FolderImage(Path.GetFileName(ImagePaths[i]), ImagePaths[i]));

            }
            DisplayImage = new FolderImage("", emty);

        }

        private void Clear_Click()
        {
            DisplayImage = new FolderImage("", emty);
            //Images.Clear();
        }

        public void SaveImage()
        {
            // Show the SaveFileDialog to get the file path and name 
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                if (File.Exists(filePath))
                {
                    try { 
                        File.Delete(filePath);
                    } catch {
                        MessageBoxResult result = MessageBox.Show("The file already is currentley opened", "File opened", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                if (Grayscale)
                {
                    var grayImageToSave = GrayImage.ToBitmap();

                    using (grayImageToSave)
                    {
                        grayImageToSave.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    
                }
                else
                {
                    var bgrImageToSave = BgrImage.ToBitmap();

                    using (bgrImageToSave)
                    {
                        bgrImageToSave.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
                Select_Images();
            }
        }


        private void ApplyFilter()
        {
            if (DisplayImage != null && DisplayImage.Path != emty)
            {

                using (Mat image = new Mat(DisplayImage.Path))
                {

                    if (BlurValue > 1 && BlurActive)
                    {
                        System.Drawing.Size blurSize = new System.Drawing.Size(BlurValue, BlurValue);

                        CvInvoke.GaussianBlur(image, image, blurSize, 0);
                        
                        //WrapGaussianBlur(image, blurSize);
                    }

                    if (TreshValue > 0 && TreshActive)
                    {
                        CvInvoke.Threshold(image, image, TreshValue, MaxTreshValue, SelectedThreshold.Value);
                    }
                    if (ErodeDilateActive)
                    {
                        var erodeElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new System.Drawing.Size(3, 3), new System.Drawing.Point(-1, -1));
                        CvInvoke.Erode(image, image, erodeElement, new System.Drawing.Point(-1, -1), ErodeIterations, BorderType.Default, new MCvScalar(255, 255, 255));
                    }

                    if (ErodeDilateActive)
                    {
                        var dilateElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new System.Drawing.Size(3, 3), new System.Drawing.Point(-1, -1));
                        CvInvoke.Dilate(image, image, dilateElement, new System.Drawing.Point(-1, -1), DilateIterations, BorderType.Default, new MCvScalar(255, 255, 255));
                    }

                    if ((EdgeTresh1 > 0 || EdgeTresh2 > 0) && Grayscale && EdgeActive)
                    {
                        Mat Temp = image.Clone();

                        CvInvoke.Canny(Temp, image, EdgeTresh1, EdgeTresh2);

                    }

                    if (Grayscale)
                    {
                        Bitmap blurredBitmap = image.ToBitmap();
                        GrayImage = blurredBitmap.ToImage<Gray, byte>();
                    }
                    else
                    {
                        Bitmap blurredBitmap = image.ToBitmap();
                        BgrImage = blurredBitmap.ToImage<Bgr, byte>();
                    }

                }
            }
        }


    }
}
