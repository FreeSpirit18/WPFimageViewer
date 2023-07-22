namespace ImageViewer.MVVM.Model
{
    internal class Image
    {
        public string name { get; set; }
        public string path { get; set; }
        public Image() { }
        public Image(string name, string path)
        {
            this.name = name;
            this.path = path;
        }

    }
}
