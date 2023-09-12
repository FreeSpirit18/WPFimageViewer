//using OpenTK.Graphics.ES20;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Wpf;
using System;
using System.Windows.Controls;
using System.Diagnostics;


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
                MinorVersion = 6,
                RenderContinuously = false,
            };
            OpenTkControl.Start(settings);
        }
        private void OpenTkControl_OnRender(TimeSpan delta)
        {
            GL.ClearColor(Color4.Blue);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Viewport(0, 0, 300, 300);

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader,
                @"#version 460 core
                layout(location = 0) in vec3 aPosition;
                void main()
                {
                    gl_Position = vec4(aPosition, 1.0);
                }"
            );

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader,
                @"#version 460 core
                out vec4 FragColor;
                void main()
                {
                    FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
                }"
            );

            GL.CompileShader(vertexShader);

            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int success1);
            if (success1 == 0)
            {
                string infoLog = GL.GetShaderInfoLog(vertexShader);
                Trace.WriteLine(infoLog);
            }

            GL.CompileShader(fragmentShader);

            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out int success2);
            if (success2 == 0)
            {
                string infoLog = GL.GetShaderInfoLog(fragmentShader);
                Trace.WriteLine(infoLog);
            }

            var Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);

            GL.LinkProgram(Handle);

            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success3);
            if (success3 == 0)
            {
                string infoLog = GL.GetProgramInfoLog(Handle);
                Console.WriteLine(infoLog);
            }

            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);



            float[] vertices = {
                -0.5f, -0.5f, 0.0f, //Bottom-left vertex
                0.5f, -0.5f, 0.0f, //Bottom-right vertex
                0.0f,  0.5f, 0.0f  //Top vertex
            };

            int VertexArrayObject = GL.GenVertexArray();

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            int VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.UseProgram(Handle);
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        }


    }
}
