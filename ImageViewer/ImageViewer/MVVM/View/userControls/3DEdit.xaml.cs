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
            };
            OpenTkControl.Start(settings);
        }
        


        //private void OpenTkControl_OnRender(TimeSpan delta)
        //{
        //    int vertexBufferHandle;
        //    int shaderProgramHandle;
        //    int vertexArrayHandle;

        //    GL.ClearColor(new Color4(0.3f, 0.4f, 0.5f, 1f));

        //    float[] vertices =
        //    {
        //         0f,    0.5f,
        //         0.5f, -0.5f,
        //        -0.5f, -0.5f
        //    };

        //    vertexBufferHandle = GL.GenBuffer();
        //    GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferHandle);
        //    GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        //    GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        //    vertexArrayHandle = GL.GenVertexArray();
        //    GL.BindVertexArray(vertexArrayHandle);

        //    GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferHandle);
        //    GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
        //    GL.EnableVertexAttribArray(0);

        //    GL.BindVertexArray(0);

        //    string vertexShader =
        //       @"#version 460 core
        //                            layout (location = 0) in vec2 aPosition;
                                    
        //                            void main()
        //                            {
        //                                gl_Position = vec4(aPosition, 0, 1.0);
        //                            }";

        //    string pixelShader =
        //        @"#version 460 core
        //                                out vec4 FragColor;
                                        
        //                                void main()
        //                                {
        //                                    FragColor = vec4(0.8, 0.2, 0.5, 1);
        //                                }";

        //    int vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
        //    GL.ShaderSource(vertexShaderHandle, vertexShader);
        //    GL.CompileShader(vertexShaderHandle);

        //    int pixelShaderHandle = GL.CreateShader(ShaderType.FragmentShader);
        //    GL.ShaderSource(pixelShaderHandle, pixelShader);
        //    GL.CompileShader(pixelShaderHandle);

        //    shaderProgramHandle = GL.CreateProgram();
        //    GL.AttachShader(shaderProgramHandle, vertexShaderHandle);
        //    GL.AttachShader(shaderProgramHandle, pixelShaderHandle);

        //    GL.LinkProgram(shaderProgramHandle);

        //    GL.DetachShader(shaderProgramHandle, vertexShaderHandle);
        //    GL.DetachShader(shaderProgramHandle, pixelShaderHandle);

        //    GL.DeleteShader(vertexShaderHandle);
        //    GL.DeleteShader(pixelShaderHandle);


        //    //----------------------------------------------------
        //    GL.Clear(ClearBufferMask.ColorBufferBit);

        //    GL.UseProgram(shaderProgramHandle);

        //    GL.BindVertexArray(vertexArrayHandle);

        //    GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

        //}

        //private void OpenTkControl_OnRender(TimeSpan delta)
        //{
        //    float alpha = 1.0f;
        //    var hue = 0.15f ;
        //    var c = Color4.FromHsv(new Vector4(alpha * hue, alpha * 0.75f, alpha * 0.75f, alpha));
        //    GL.ClearColor(c);
        //    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        //    GL.LoadIdentity();
        //    GL.Begin(PrimitiveType.Triangles);

        //    GL.Color4(Color4.Red);
        //    GL.Vertex2(0.0f, 0.5f);

        //    GL.Color4(Color4.Green);
        //    GL.Vertex2(0.58f, -0.5f);

        //    GL.Color4(Color4.Blue);
        //    GL.Vertex2(-0.58f, -0.5f);

        //    GL.End();
        //    GL.Finish();

        //}

         private void OpenTkControl_OnRender(TimeSpan delta)
         {
             GL.ClearColor(Color4.Blue);
             GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


             int vertexShader = GL.CreateShader(ShaderType.VertexShader);
             GL.ShaderSource(vertexShader,
                 @"#version 330 core
                 layout(location = 0) in vec3 aPosition;
                 void main()
                 {
                     gl_Position = vec4(aPosition, 1.0);
                 }"
             );

             int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
             GL.ShaderSource(fragmentShader,
                 @"#version 330 core
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


             GL.UseProgram(Handle);
             GL.BindVertexArray(VertexArrayObject);
             GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
         }

    }
}
