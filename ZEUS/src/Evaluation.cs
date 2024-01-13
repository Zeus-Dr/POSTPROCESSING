using System;
using System.Drawing;
using AForge.Imaging;
using AForge.Imaging.Filters;
using System.Diagnostics;

namespace ZEUS
{
    /// <summary>
    /// Provides methods for evaluating image and video quality metrics.
    /// </summary>
    public class Evaluation
    {
        /// <summary>
        /// Calculates the Peak Signal-to-Noise Ratio (PSNR) between two images.
        /// </summary>
        /// <param name="image1">The first image for comparison.</param>
        /// <param name="image2">The second image for comparison.</param>
        /// <returns>The PSNR value between the two images.</returns>
        public static double PSNR(Bitmap image1, Bitmap image2)
        {
            if (image1.Size != image2.Size)
                throw new ArgumentException("Images must be of the same size.");

            double mse = CalculateMeanSquaredError(image1, image2);

            if (mse == 0)
                return double.PositiveInfinity;

            const int maxValue = 255; // Maximum pixel value for an 8-bit image

            double psnr = 20 * Math.Log10(maxValue / Math.Sqrt(mse));
            return psnr;
        }

        /// <summary>
        /// Calculates the Mean Squared Error (MSE) between two images.
        /// </summary>
        /// <param name="image1">The first image for comparison.</param>
        /// <param name="image2">The second image for comparison.</param>
        /// <returns>The mean squared error between the two images.</returns>
        private static double CalculateMeanSquaredError(Bitmap image1, Bitmap image2)
        {
            double sumSquaredDiff = 0;

            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color originalPixel = image1.GetPixel(x, y);
                    Color distortedPixel = image2.GetPixel(x, y);

                    double diffR = originalPixel.R - distortedPixel.R;
                    double diffG = originalPixel.G - distortedPixel.G;
                    double diffB = originalPixel.B - distortedPixel.B;

                    sumSquaredDiff += (diffR * diffR + diffG * diffG + diffB * diffB) / 3.0;
                }
            }

            return sumSquaredDiff / (image1.Width * image1.Height);
        }

        /// <summary>
        /// Calculates the Structural Similarity Index (SSIM) between two images.
        /// </summary>
        /// <param name="image1">The first image for comparison.</param>
        /// <param name="image2">The second image for comparison.</param>
        /// <returns>The SSIM value indicating similarity between the images.</returns>
        public static double SSIM(Bitmap image1, Bitmap image2)
        {
            // Check if images have the same dimensions
            if (image1.Width != image2.Width || image1.Height != image2.Height)
            {
                throw new ArgumentException("Images must have the same dimensions.");
            }

            // Create the grayscale filter
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);

            // Apply the filter to the image
            Bitmap grayscaleImage1 = filter.Apply(image1);
            Bitmap grayscaleImage2 = filter.Apply(image2);

            // Calculate SSIM
            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0);
            TemplateMatch[] matchings = tm.ProcessImage(grayscaleImage1, grayscaleImage2);

            // Get SSIM value
            double ssim = matchings[0].Similarity;

            return ssim;
        }

        /// <summary>
        /// Calculates the Video Multimethod Assessment Fusion (VMAF) score between two videos using FFmpeg.
        /// </summary>
        /// <param name="ffmpegPath">The path to the FFmpeg executable.</param>
        /// <param name="videoPath1">The path to the first Video.</param>
        /// <param name="videoPath2">The path to the second Video.</param>
        /// <returns>The VMAF score between the two videos.</returns>
        public static double VMAFScore(string ffmpegPath, string videoPath1, string videoPath2)
        {
            string command = $"-i \"{videoPath1}\" -i \"{videoPath2}\" -lavfi libvmaf -f null -";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = ffmpegPath,
                Arguments = command,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            double vmafScore = 0.0;

            using (Process process = Process.Start(psi))
            {
                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data) && e.Data.Contains("VMAF score:"))
                    {
                        string vmafStr = e.Data.Substring(e.Data.IndexOf("VMAF score:") + 12).Trim();
                        if (double.TryParse(vmafStr, out double vmaf))
                        {
                            vmafScore = vmaf;
                        }
                    }
                };

                process.BeginErrorReadLine();
                process.WaitForExit();
            }

            return vmafScore;
        }
    }
}