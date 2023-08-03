using System.ComponentModel;

namespace ImageViewer.MVVM.Model
{
    internal class FolderImage: INotifyPropertyChanged
    {
        public string ?Name { get; set; }
        public string ?Path { get; set; }
        public FolderImage() { }


        public FolderImage(string name, string path)
        {
            this.Name = name;
            this.Path = path;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
