using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using ImageViewer.MVVM.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using WinForms = System.Windows.Forms;
using OpenCVLibrary;

namespace ImageViewer.MVVM.ViewModel
{
    internal class MainWindowVM : ViewModelBase
    {
        
        public MainWindowVM() {
            DisplayImage = new FolderImage("", "");
            Images = new ObservableCollection<FolderImage>();
            thresholdTypes = new ObservableCollection<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("Binary", 0),
                new KeyValuePair<string, int>("BinaryInv", 1),
                new KeyValuePair<string, int>("Trunc", 2),
                new KeyValuePair<string, int>("ToZero", 3),
                new KeyValuePair<string, int>("ToZeroInv", 4)
            };
            SelectedThreshold = new KeyValuePair<string, int>("Binary", 0);

            ImageActive = false;
            Grayscale = false;
            Mode = "ligth";
            BlurActive = true;
            TreshActive = true;
            ErodeDilateActive = true;
            EdgeActive = true;


            SelectCommand = new RelayCommand(execute => Select_Click(), canExecute => { return true; });
            ClearCommand = new RelayCommand(execute => Clear_Click(), canExecute => { return true; });
            SaveCommand = new RelayCommand(execute => SaveImage(), canExecute => { return true; });
            NigthCommand = new RelayCommand(execute => ChangeToDark(), canExecute => { return true; });
            DayCommand = new RelayCommand(execute => ChangeToLigth(), canExecute => { return true; });
        }
        public RelayCommand SelectCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand NigthCommand { get; private set; }
        public RelayCommand DayCommand { get; private set; }


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

        private ObservableCollection<KeyValuePair<string, int>> thresholdTypes;
        public ObservableCollection<KeyValuePair<string, int>> ThresholdTypes
        {
            get { return thresholdTypes; }
            set
            {
                thresholdTypes = value;
                OnPropertyChange("ThresholdType");
            }
        }
        private KeyValuePair<string, int> selectedThreshold;
        public KeyValuePair<string, int> SelectedThreshold
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
                if (displayImage != null && displayImage.Path != "")
                {
                    ImageActive = true;
                    BgrImage = new Image<Bgr, byte>(value.Path);
                    GrayImage = new Image<Gray, byte>(value.Path);
                }else
                    ImageActive = false;

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

        private bool imageActive;
        public bool ImageActive
        {
            get { return imageActive; }
            set
            {
                imageActive = value;
                ApplyFilter();
                OnPropertyChange("ImageActive");
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
                ApplyFilter();
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
        //----------------------------------------------------
        private string mode;
        public string Mode
        {
            get { return mode; }
            set { 
                mode = value;
                OnPropertyChange("Mode");
            }
        }
        //-----------------------------------------------------
        private void ChangeToDark()
        {
            Mode = "nigth";
        }
        private void ChangeToLigth()
        {
            Mode = "ligth";
        }
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
            ImageActive = false;

        }

        private void Clear_Click()
        {
            ImageActive = false;
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
            if (DisplayImage != null && DisplayImage.Path != "")
            {

                using (Mat image = new Mat(DisplayImage.Path))
                {

                    if (BlurValue > 1 && BlurActive)
                    {
                        System.Drawing.Size blurSize = new System.Drawing.Size(BlurValue, BlurValue);

                        //CvInvoke.GaussianBlur(image, image, blurSize, 0);
                        Filter.WrapGaussianBlur(image, image, blurSize);
                    }

                    if (TreshValue > 0 && TreshActive)
                    {
                        Filter.WrapThreshold(image, image, TreshValue, MaxTreshValue, SelectedThreshold.Value);
                        //CvInvoke.Threshold(image, image, TreshValue, MaxTreshValue, SelectedThreshold.Value);
                    }
                    if (ErodeDilateActive)
                    {

                        Filter.WrapErode(image, image, ErodeIterations);
                        //var erodeElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new System.Drawing.Size(3, 3), new System.Drawing.Point(-1, -1));
                        //CvInvoke.Erode(image, image, erodeElement, new System.Drawing.Point(-1, -1), ErodeIterations, BorderType.Default, new MCvScalar(255, 255, 255));
                    }

                    if (ErodeDilateActive)
                    {
                        Filter.WrapDilate(image, image, DilateIterations);
                        //var dilateElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new System.Drawing.Size(3, 3), new System.Drawing.Point(-1, -1));
                        //CvInvoke.Dilate(image, image, dilateElement, new System.Drawing.Point(-1, -1), DilateIterations, BorderType.Default, new MCvScalar(255, 255, 255));
                    }

                    if ((EdgeTresh1 > 0 || EdgeTresh2 > 0) && Grayscale && EdgeActive)
                    {
                        Mat temp = image.Clone();
                        Filter.WrapCanny(temp, image, EdgeTresh1, EdgeTresh2);
                        //CvInvoke.Canny(Temp, image, EdgeTresh1, EdgeTresh2);

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
