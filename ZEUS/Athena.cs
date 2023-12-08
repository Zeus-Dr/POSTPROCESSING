using System;
using System.Collections.Generic;
using System.Drawing;

namespace ZEUS
{
    public class Athena
    {
        // Function to apply Median Filtering to a colored image
        public Bitmap ApplyMedianFilter(Bitmap image, int windowSize)
        {
            int width = image.Width;
            int height = image.Height;
            int halfWindow = windowSize / 2;

            Bitmap resultImage = new Bitmap(width, height);

            for (int y = halfWindow; y < height - halfWindow; y++)
            {
                for (int x = halfWindow; x < width - halfWindow; x++)
                {
                    List<int> redValues = new List<int>();
                    List<int> greenValues = new List<int>();
                    List<int> blueValues = new List<int>();

                    // Collect pixel values in the window for each channel
                    for (int j = -halfWindow; j <= halfWindow; j++)
                    {
                        for (int i = -halfWindow; i <= halfWindow; i++)
                        {
                            Color pixel = image.GetPixel(x + i, y + j);
                            redValues.Add(pixel.R);
                            greenValues.Add(pixel.G);
                            blueValues.Add(pixel.B);
                        }
                    }

                    // Sort values to find median for each channel
                    redValues.Sort();
                    greenValues.Sort();
                    blueValues.Sort();

                    int medianRed = redValues[redValues.Count / 2];
                    int medianGreen = greenValues[greenValues.Count / 2];
                    int medianBlue = blueValues[blueValues.Count / 2];

                    // Set the pixel value to the median for each channel
                    Color medianColor = Color.FromArgb(medianRed, medianGreen, medianBlue);
                    resultImage.SetPixel(x, y, medianColor);
                }
            }

            return resultImage;
        }

        // Function to apply Gaussian Filtering to an image
        public Bitmap ApplyGaussianFilter(Bitmap image, int kernelSize, double sigma)
        {
            int width = image.Width;
            int height = image.Height;
            Bitmap resultImage = new Bitmap(width, height);

            // Create Gaussian kernel
            double[,] kernel = CreateGaussianKernel(kernelSize, sigma);

            // Apply convolution to each pixel of the image
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color newColor = Convolve(image, x, y, kernel, kernelSize);
                    resultImage.SetPixel(x, y, newColor);
                }
            }

            return resultImage;
        }

        // Function to create a Gaussian kernel
        private double[,] CreateGaussianKernel(int kernelSize, double sigma)
        {
            double[,] kernel = new double[kernelSize, kernelSize];
            double sumTotal = 0;
            int midpoint = kernelSize / 2;

            for (int i = -midpoint; i <= midpoint; i++)
            {
                for (int j = -midpoint; j <= midpoint; j++)
                {
                    double distance = (i * i + j * j) / (2 * sigma * sigma);
                    double value = Math.Exp(-distance) / (2 * Math.PI * sigma * sigma);
                    sumTotal += value;
                    kernel[i + midpoint, j + midpoint] = value;
                }
            }

            // Normalize the kernel
            for (int i = 0; i < kernelSize; i++)
            {
                for (int j = 0; j < kernelSize; j++)
                {
                    kernel[i, j] /= sumTotal;
                }
            }

            return kernel;
        }

        // Function to perform convolution on an image pixel
        private Color Convolve(Bitmap image, int x, int y, double[,] kernel, int kernelSize)
        {
            double red = 0, green = 0, blue = 0;
            int width = image.Width;
            int height = image.Height;
            int midpoint = kernelSize / 2;

            for (int i = -midpoint; i <= midpoint; i++)
            {
                for (int j = -midpoint; j <= midpoint; j++)
                {
                    int pixelX = Math.Min(Math.Max(x + j, 0), width - 1);
                    int pixelY = Math.Min(Math.Max(y + i, 0), height - 1);
                    Color pixel = image.GetPixel(pixelX, pixelY);

                    red += pixel.R * kernel[i + midpoint, j + midpoint];
                    green += pixel.G * kernel[i + midpoint, j + midpoint];
                    blue += pixel.B * kernel[i + midpoint, j + midpoint];
                }
            }

            return Color.FromArgb((int)red, (int)green, (int)blue);
        }
    }
}
