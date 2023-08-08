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
        public static extern void WrapGaussianBlur(IntPtr inputImage, IntPtr outputImage, System.Drawing.Size blurSize);

        [DllImport("WrapperCV.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WrapThreshold(IntPtr inputImage, IntPtr outputImage, double threshValue, double maxThreshValue, int thresholdType);

        [DllImport("WrapperCV.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WrapErode(IntPtr inputImage, IntPtr outputImage, double erodeIterations);

        [DllImport("WrapperCV.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WrapDilate(IntPtr inputImage, IntPtr outputImage, double dilateIterations);

        [DllImport("WrapperCV.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void WrapCanny(IntPtr inputImage, IntPtr outputImage, double edgeTresh1, double edgeTresh2);
    }
}