//using OpenTK.Graphics.ES20;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Wpf;
using System;
using System.Windows.Controls;

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
        //private void OpenTkControl_OnRender(TimeSpan delta)
        //{
        //    //float[] vertices = {
        //    //    -0.5f, -0.5f, 0.0f, //Bottom-left vertex
        //    //    0.5f, -0.5f, 0.0f, //Bottom-right vertex
        //    //    0.0f,  0.5f, 0.0f  //Top vertex
        //    //};

        //    //GL.ClearColor(Color4.Blue);
        //    //GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        //    //int VertexBufferObject = GL.GenBuffer();
        //    //GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        //    //GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        //    float alpha = 1.0f;

        //    var hue =  0.3f ;
        //    var c = Color4.FromHsv(new Vector4(alpha * hue, alpha * 0.75f, alpha * 0.75f, alpha));
        //    OpenTK.Graphics.OpenGL.GL.ClearColor(c);
        //    OpenTK.Graphics.OpenGL.GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        //    OpenTK.Graphics.OpenGL.GL.LoadIdentity();

        //    OpenTK.Graphics.OpenGL.GL.Begin(OpenTK.Graphics.OpenGL.PrimitiveType.Triangles);

        //    OpenTK.Graphics.OpenGL.GL.Color4(Color4.Red);
        //    OpenTK.Graphics.OpenGL.GL.Vertex2(0.0f, 0.5f);

        //    OpenTK.Graphics.OpenGL.GL.Color4(Color4.Green);
        //    OpenTK.Graphics.OpenGL.GL.Vertex2(0.58f, -0.5f);

        //    OpenTK.Graphics.OpenGL.GL.Color4(Color4.Blue);
        //    OpenTK.Graphics.OpenGL.GL.Vertex2(-0.58f, -0.5f);

        //    OpenTK.Graphics.OpenGL.GL.End();
        //    OpenTK.Graphics.OpenGL.GL.Finish();


        //}

        private void OpenTkControl_OnRender(TimeSpan delta)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.PushMatrix();
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(1f, 0f, 0f);
            GL.Vertex3(0, 0, 0);

            GL.Color3(1f, 0f, 0f);
            GL.Vertex3(1, 0, 0);

            GL.Color3(1f, 0f, 0f);
            GL.Vertex3(1, 1, 0);

            GL.Color3(1f, 0f, 0f);
            GL.Vertex3(0, 1, 0);

            GL.End();
            GL.PopMatrix();

        }

    }
}
