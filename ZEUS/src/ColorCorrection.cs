using System;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging.Filters;

namespace ZEUS
{
    /// <summary>
    /// Provides methods to apply Color Correction on a Bitmap image.
    /// </summary>
    public class ColorCorrection
    {
        /// <summary>
        /// Performs histogram equalization color correction on the input image.
        /// </summary>
        /// <param name="originalImage">The original image to be color-corrected.</param>
        /// <returns>The color-corrected image after histogram equalization.</returns>
        public static Bitmap HistogramEqualization(Bitmap originalImage)
        {
            // Create an instance of the HistogramEqualization filter
            HistogramEqualization filter = new HistogramEqualization();

            // Apply the filter to the original image
            Bitmap equalizedImage = filter.Apply(originalImage);

            return equalizedImage;
        }

        /// <summary>
        /// Applies white balance correction to the provided image.
        /// </summary>
        /// <param name="originalImage">The original image to be corrected.</param>
        /// <returns>The image with white balance correction applied.</returns>
        public static Bitmap WhiteBalance(Bitmap originalImage)
        {
            BitmapData originalData = originalImage.LockBits(new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                                                            ImageLockMode.ReadOnly,
                                                            PixelFormat.Format24bppRgb);

            int bytesPerPixel = 3; // 24bpp image (3 bytes per pixel)
            int heightInPixels = originalData.Height;
            int widthInBytes = originalData.Width * bytesPerPixel;

            unsafe
            {
                byte* ptr = (byte*)originalData.Scan0;

                // Calculate average color values for R, G, and B channels
                int totalR = 0, totalG = 0, totalB = 0;
                int pixelCount = 0;

                for (int y = 0; y < heightInPixels; y++)
                {
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        int index = y * originalData.Stride + x;

                        byte blue = ptr[index];
                        byte green = ptr[index + 1];
                        byte red = ptr[index + 2];

                        totalR += red;
                        totalG += green;
                        totalB += blue;
                        pixelCount++;
                    }
                }

                int avgR = totalR / pixelCount;
                int avgG = totalG / pixelCount;
                int avgB = totalB / pixelCount;

                // Apply white balance correction to each pixel
                for (int y = 0; y < heightInPixels; y++)
                {
                    byte* currentLine = ptr + (y * originalData.Stride);

                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        int index = x;

                        byte blue = currentLine[index];
                        byte green = currentLine[index + 1];
                        byte red = currentLine[index + 2];

                        int newR = (int)((red / (float)avgR) * 255);
                        int newG = (int)((green / (float)avgG) * 255);
                        int newB = (int)((blue / (float)avgB) * 255);

                        newR = Math.Max(0, Math.Min(255, newR));
                        newG = Math.Max(0, Math.Min(255, newG));
                        newB = Math.Max(0, Math.Min(255, newB));

                        currentLine[index] = (byte)newB;
                        currentLine[index + 1] = (byte)newG;
                        currentLine[index + 2] = (byte)newR;
                    }
                }
            }

            originalImage.UnlockBits(originalData);

            return originalImage;
        }

    }



}
