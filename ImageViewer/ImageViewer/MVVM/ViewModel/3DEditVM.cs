using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace ImageViewer.MVVM.ViewModel
{
    internal class _3DEditVM : ViewModelBase
    {
        public _3DEditVM() {
            
        }

        private GameWindow viewWindow; 

        public GameWindow ViewWindow
        {
            get { return viewWindow; }
            set { 
                viewWindow = value;
                OnPropertyChange("ViewWindow");
            }
        }


        public void SetUpViewPort()
        {
            using (this.viewWindow = new GameWindow(GameWindowSettings.Default, new NativeWindowSettings() { Size = (400, 350), Title = "ViewPort" }))
            {

            }
        }


    }
}
