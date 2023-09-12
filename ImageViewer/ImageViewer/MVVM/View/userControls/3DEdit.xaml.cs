using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ImageViewer.MVVM.ViewModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenTK.Graphics.ES20;
using OpenTK.Wpf;
using OpenTK.Mathematics;

namespace ImageViewer.MVVM.View.userControls
{
    /// <summary>
    /// Interaction logic for _3DEdit.xaml
    /// </summary>
    public partial class _3DEdit : UserControl
    {
        public _3DEdit()
        {
            InitializeComponent();
            //_3DEditVM vm = new _3DEditVM();
            //DataContext = vm;
            var settings = new GLWpfControlSettings
            {
                MajorVersion = 4,
                MinorVersion = 6
            };
            OpenTkControl.Start(settings);
        }
        private void OpenTkControl_OnRender(TimeSpan delta)
        {
            GL.ClearColor(Color4.Blue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
    }
}
