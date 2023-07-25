using System.ComponentModel;

namespace ImageViewer.MVVM.Model
{
    internal class Image: INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public Image() { }


        public Image(string name, string path)
        {
            this.Name = name;
            this.Path = path;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
