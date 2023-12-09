using AForge.Imaging.Filters;
using System.Drawing;

namespace ZEUS
{
    public class FrameDenoising
    {
        // Function to apply Gaussian Filtering to an image
        /// <summary>
        /// This method applies Gaussian Filter to a frame and returns a filtered image
        /// </summary>
        /// <param name="image">Image to be filtered</param>
        /// <param name="kernelSize">kernelSize along which the image will be blurred</param>
        /// <returns>Bitmap Filtered Image</returns>
        public Bitmap ApplyGaussianFilter(Bitmap image, int kernelSize)
        {
            // Create the filter
            GaussianBlur filter = new GaussianBlur(kernelSize, 3);

            // Apply the filter to the image
            Bitmap result = filter.Apply(image);

            return result;
        }

        /// <summary>
        /// This method applies Median Filter to a frame and returns a filtered image
        /// </summary>
        /// <param name="image">Image to be filtered</param>
        /// <param name="windowSize">windowSize along which the image will be filtered</param>
        /// <returns>Bitmap Filtered Image</returns>
        public Bitmap ApplyMedianFilter(Bitmap image, int windowSize)
        {
            // Apply Median filter with a window size
            Median filter = new Median(windowSize); // By default, the window size is 3x3
            Bitmap result = filter.Apply(image);

            return result;
        }
    }
}
