using System.Drawing;
using AForge.Imaging.Filters;

namespace ZEUS
{
    /// <summary>
    /// Provides methods to apply Frame Sharpening on a Bitmap image.
    /// </summary>
    public class FrameSharpening
    {
        /// <summary>
        /// Applies unsharp masking to the provided image.
        /// </summary>
        /// <param name="originalImage">The original Bitmap image.</param>
        /// <param name="radius">The radius for Gaussian blur.</param>
        /// <returns>Returns a Bitmap with the unsharp masking applied.</returns>
        public static Bitmap ApplyUnsharpMasking(Bitmap originalImage, float radius)
        {
            // Apply Gaussian blur
            AForge.Imaging.Filters.GaussianBlur filter = new AForge.Imaging.Filters.GaussianBlur(radius);
            Bitmap blurredImage = filter.Apply(originalImage);

            // Convert Bitmaps to AForge.Imaging.UnmanagedImage
            AForge.Imaging.UnmanagedImage unmanagedOriginal = AForge.Imaging.UnmanagedImage.FromManagedImage(originalImage);
            AForge.Imaging.UnmanagedImage unmanagedBlurred = AForge.Imaging.UnmanagedImage.FromManagedImage(blurredImage);

            // Calculate unsharp masking
            AForge.Imaging.Filters.Subtract subtractFilter = new AForge.Imaging.Filters.Subtract(unmanagedBlurred);
            subtractFilter.ApplyInPlace(unmanagedOriginal);

            AForge.Imaging.Filters.Add addFilter = new AForge.Imaging.Filters.Add(originalImage);
            addFilter.ApplyInPlace(unmanagedOriginal);

            return unmanagedOriginal.ToManagedImage();
        }

        /// <summary>
        /// Applies a sharpening effect to the input image using a high-pass filter and strength adjustment.
        /// </summary>
        /// <param name="inputImage">The input Bitmap image to be sharpened.</param>
        /// <param name="strength">The strength of the sharpening effect. Value between 0 and 1.</param>
        /// <returns>A Bitmap image with the sharpening effect applied.</returns>
        public static Bitmap ApplyHighPassFiltering(Bitmap inputImage, double strength)
        {
            // Convert Bitmap to AForge's Grayscale
            Bitmap grayscaleImage = Grayscale.CommonAlgorithms.BT709.Apply(inputImage);

            // Apply high-pass filtering using AForge.NET
            HomogenityEdgeDetector filter = new HomogenityEdgeDetector();
            Bitmap filteredImage = filter.Apply(grayscaleImage);

            // Restore color by overlaying filtered grayscale on the original image
            Bitmap colorRestoredImage = new Bitmap(inputImage.Width, inputImage.Height);
            Color colorPixel, filteredPixel, restoredPixel;

            for (int x = 0; x < inputImage.Width; x++)
            {
                for (int y = 0; y < inputImage.Height; y++)
                {
                    colorPixel = inputImage.GetPixel(x, y);
                    filteredPixel = filteredImage.GetPixel(x, y);

                    int red = (int)(strength * colorPixel.R + (1 - strength) * filteredPixel.R);
                    int green = (int)(strength * colorPixel.G + (1 - strength) * filteredPixel.G);
                    int blue = (int)(strength * colorPixel.B + (1 - strength) * filteredPixel.B);

                    restoredPixel = Color.FromArgb(red, green, blue);
                    colorRestoredImage.SetPixel(x, y, restoredPixel);
                }
            }

            return colorRestoredImage;
        }
    }
}

