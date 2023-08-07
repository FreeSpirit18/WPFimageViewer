#include "opencv2/opencv.hpp"
#include <iostream>

#define EXPORT_METHOD extern "C" __declspec(dllexport)

EXPORT_METHOD
int TestFunck()
{
    return 1;

}

EXPORT_METHOD
void WrapGaussianBlur(cv::Mat* inputImage, cv::Mat* outputImage, cv::Size blurSize)
{
    cv::GaussianBlur(*inputImage, *outputImage, blurSize, 0);
}

EXPORT_METHOD
void WrapThreshold(cv::Mat* inputImage, cv::Mat* outputImage, double threshValue, double maxThreshValue, int thresholdType)
{
    cv::threshold(*inputImage, *outputImage, threshValue, maxThreshValue, thresholdType);
}

EXPORT_METHOD
void WrapErode(cv::Mat* inputImage, cv::Mat* outputImage, double erodeIterations)
{
    cv::Mat element = cv::getStructuringElement(cv::MORPH_RECT, cv::Size(3, 3), cv::Point(-1, -1));
    cv::erode(*inputImage, *outputImage, element, cv::Point(-1, -1), erodeIterations, cv::BORDER_DEFAULT, cv::Scalar(255, 255, 255));

}

EXPORT_METHOD
void WrapDilate(cv::Mat* inputImage, cv::Mat* outputImage, double dilateIterations)
{

    cv::Mat element = cv::getStructuringElement(cv::MORPH_RECT, cv::Size(3, 3), cv::Point(-1, -1));
    cv::dilate(*inputImage, *outputImage, element, cv::Point(-1, -1), dilateIterations, cv::BORDER_DEFAULT, cv::Scalar(255, 255, 255));

}

EXPORT_METHOD
void WrapCanny(cv::Mat* inputImage, cv::Mat* outputImage, double edgeTresh1, double edgeTresh2)
{

    cv::Canny(*inputImage, *outputImage, edgeTresh1, edgeTresh2);

}
