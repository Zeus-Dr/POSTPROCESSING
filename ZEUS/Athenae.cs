using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging.ComplexFilters;
using System.Drawing;
using System.Drawing.Imaging;

namespace ZEUS
{
    public class Athenae
    {
        // Function to apply Gaussian Filtering to an image
        public Bitmap ApplyGaussianFilter(Bitmap image, int kernelSize)
        {
            // Create the filter
            GaussianBlur filter = new GaussianBlur(kernelSize, 3);

            // Apply the filter to the image
            Bitmap result = filter.Apply(image);

            return result;
        }

        public void ApplyMedianFilter(Bitmap image, int windowSize, string outputPath)
        {
            // Apply Median filter with a window size of 3x3
            Median filter = new Median(); // By default, the window size is 3x3
            Bitmap filteredImage = filter.Apply(image);

            // Save the filtered image (replace with your save code)
            filteredImage.Save(outputPath, ImageFormat.Png);

            // Display success message
            Console.WriteLine("Median filtering applied and saved as output.jpg");
        }

        public void ApplyWaveletFilter(Bitmap image, int windowSize, string outputPath)
        {
            // Convert the image to grayscale
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap grayscaleImage = filter.Apply(image);

            // Create Haar wavelet
            GetXHaarWavelet wavelet = new GetXHaarWavelet();

            // Create a wavelet transform using the Haar wavelet
            WaveletTransform transform = new WaveletTransform(wavelet);

            // Apply forward wavelet transform
            double[,] coefficients = wavelet.Apply(grayscaleImage);

            // Manipulate coefficients (for example, thresholding, filtering)

            // Apply inverse wavelet transform
            Bitmap reconstructedImage = wavelet.Inverse(coefficients);

            // Save the reconstructed image (replace with your save code)
            reconstructedImage.Save("output.jpg");

            // Display success message
            Console.WriteLine("Wavelet transform applied and saved as output.jpg");
        }
    }
}
