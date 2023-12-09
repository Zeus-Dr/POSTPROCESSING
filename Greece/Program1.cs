using System.Drawing;
using ZEUS;

namespace Greece
{
    class Program1
    {
        static void Main(string[] args)
        {
            //Trying out the Filtering methods of FrameDenoising Module
            FrameDenoising denoiser = new FrameDenoising();
            Bitmap inputImage = new Bitmap("input_image.jpg");
            int filterSize = 5; // Set the filter size

            Bitmap resultImage = denoiser.ApplyGaussianFilter(inputImage, filterSize);
            // Use or save the resultImage as needed

            // Use or save the resultImage as needed
        }
    }
}