// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include "opencv2/opencv.hpp"


#define EXPORT_METHOD extern "C" __declspec(dllexport)

EXPORT_METHOD
int TestFunck()
{
    return 1;

}

EXPORT_METHOD
void WrapGaussianBlur(const cv::Mat& inputImage, cv::Size blurSize)
{
    //cv::Mat blurredImage;
    cv::GaussianBlur(inputImage, inputImage, blurSize, 0);
    
}

EXPORT_METHOD
cv::Mat WrapThreshold(const cv::Mat& inputImage, double threshValue, double maxThreshValue, int thresholdType)
{
    cv::Mat treshImage;
    cv::threshold(inputImage, treshImage, threshValue, maxThreshValue, thresholdType);
    return treshImage;
}

EXPORT_METHOD
cv::Mat WrapErode(const cv::Mat& inputImage, double erodeIterations)
{
    cv::Mat erodedImage;
    cv::Mat element = cv::getStructuringElement(cv::MORPH_RECT, cv::Size(3, 3), cv::Point(-1, -1));
    cv::erode(inputImage, erodedImage, element, cv::Point(-1, -1), erodeIterations, cv::BORDER_DEFAULT, cv::Scalar(255, 255, 255));

    return erodedImage;
}

EXPORT_METHOD
cv::Mat WrapDilate(const cv::Mat& inputImage, double dilateIterations)
{
    cv::Mat dilatedImage;
    cv::Mat element = cv::getStructuringElement(cv::MORPH_RECT, cv::Size(3, 3), cv::Point(-1, -1));
    cv::dilate(inputImage, dilatedImage, element, cv::Point(-1, -1), dilateIterations, cv::BORDER_DEFAULT, cv::Scalar(255, 255, 255));

    return dilatedImage;
}

EXPORT_METHOD
cv::Mat WrapCanny(const cv::Mat& inputImage, double edgeTresh1, double edgeTresh2)
{
    cv::Mat cannyImage;
    cv::Canny(inputImage, cannyImage, edgeTresh1, edgeTresh2);

    return cannyImage;
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

