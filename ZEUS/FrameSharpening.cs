using System;
using OpenCvSharp;

namespace ZEUS
{
    public class FrameSharpening
    {
        public void unSharpMasking()
        {
            // Load the image
            Mat originalImage = Cv2.ImRead("input.jpg", ImreadModes.Color);

            // Convert to grayscale
            Mat grayImage = new Mat();
            Cv2.CvtColor(originalImage, grayImage, ColorConversionCodes.BGR2GRAY);

            // Apply Gaussian blur to create a blurred version
            Cv2.GaussianBlur(grayImage, grayImage, new Size(0, 0), 2); // Adjust the kernel size as needed

            // Calculate the unsharp mask
            Mat mask = new Mat();
            Cv2.Subtract(grayImage, originalImage.CvtColor(ColorConversionCodes.BGR2GRAY), mask);

            // Add the mask to the original image
            Mat sharpenedImage = new Mat();
            Cv2.AddWeighted(originalImage, 1.5, mask, 0.5, 0, sharpenedImage); // Adjust the parameters as needed

            // Save or display the sharpened image
            Cv2.ImWrite("sharpened.jpg", sharpenedImage);
            // Or display the image, for instance:
            // Cv2.ImShow("Sharpened Image", sharpenedImage);
            // Cv2.WaitKey(0);
        }
    }
}
