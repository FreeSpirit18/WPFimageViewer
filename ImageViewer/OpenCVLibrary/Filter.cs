using System.Runtime.InteropServices;
using System;
using Emgu.CV;

namespace OpenCVLibrary
{
    public class Filter
    {
        [DllImport("WrapperCV.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int TestFunck();

        [DllImport("WrapperCV.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WrapGaussianBlur(Mat inputImage, System.Drawing.Size blurSize);
    }
}