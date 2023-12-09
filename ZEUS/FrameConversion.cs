using System;
using System.Drawing;

namespace ZEUS
{
    public class FrameConversion
    {
        //Function to resize the image
        /// <summary>
        /// This method resizes a frame and saves the result to a desired destination
        /// </summary>
        /// <param name="imagePath">Full path of the input image</param>
        /// <param name="outputPath">Full path of the output image</param>
        /// <param name="outputWidth">Full path of the output image</param>
        /// <param name="outputHeight">Full path of the output image</param>
        /// <returns>None</returns>
        public void ResizeFrame(string imagePath, string outputPath, int outputWidth, int outputHeight)
        {
            try
            {
                // Load the image
                Bitmap inputImage = new Bitmap(imagePath);

                // Create a new Bitmap object for the resized image
                Bitmap resizedImage = new Bitmap(outputWidth, outputHeight);

                // Resample the image using high-quality interpolation
                using (Graphics g = Graphics.FromImage(resizedImage))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(inputImage, 0, 0, outputWidth, outputHeight);
                }

                // Save the resized image to the specified output path
                resizedImage.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);
                Console.WriteLine("Resized image saved successfully to " + outputPath);

                // Dispose the image objects
                resizedImage.Dispose();
                inputImage.Dispose();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        //Function to resize the image
        /// <summary>
        /// This method resizes a frame and returns the resized image to the user
        /// </summary>
        /// <param name="imagePath">Full path of the input image</param>
        /// <param name="outputWidth">Full path of the output image</param>
        /// <param name="outputHeight">Full path of the output image</param>
        /// <returns>Bitmap Resized Image</returns>
        public Bitmap ResizeFrame(string imagePath, int outputWidth, int outputHeight)
        {
            // Load the image
            Bitmap inputImage = new Bitmap(imagePath);

            // Create a new Bitmap object for the resized image
            Bitmap resizedImage = new Bitmap(outputWidth, outputHeight);

            // Resample the image using high-quality interpolation
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(inputImage, 0, 0, outputWidth, outputHeight);
            }

            return resizedImage;
        }
    }

    // Function to convert RGB to YUV for a single pixel
    // public void RGBtoYUV(int R, int G, int B, out int Y, out int U, out int V)
    // {
    //     Y = (int)(0.299 * R + 0.587 * G + 0.114 * B);
    //     U = (int)(-0.147 * R - 0.289 * G + 0.436 * B);
    //     V = (int)(0.615 * R - 0.515 * G - 0.100 * B);
    // }

    // Function to convert an entire frame from RGB to YUV
    // public void ConvertRGBtoYUV(int[][] RGBFrame, int[][] YUVFrame, int width, int height)
    // {
    //     // Iterate over the image excluding the last 2 columns and rows to ensure valid access
    //     for (int i = 0; i < height - 2; ++i)
    //     {
    //         for (int j = 0; j < width - 2; ++j)
    //         {
    //             int R = RGBFrame[i][j];
    //             int G = RGBFrame[i][j + 1];
    //             int B = RGBFrame[i][j + 2];

    //             int Y, U, V;
    //             RGBtoYUV(R, G, B, out Y, out U, out V);

    //             // Set YUV values at [i][j], [i][j+1], [i][j+2] positions respectively
    //             YUVFrame[i][j] = Y;
    //             YUVFrame[i][j + 1] = U;
    //             YUVFrame[i][j + 2] = V;
    //         }
    //     }
    // }

    // Function to resize a frame to the desired resolution
    // public void ResizeFrame(int[][] inputFrame, int[][] outputFrame, int inputWidth, int inputHeight, int outputWidth, int outputHeight)
    // {
    //     double widthRatio = (double)inputWidth / outputWidth;
    //     double heightRatio = (double)inputHeight / outputHeight;

    //     for (int i = 0; i < outputHeight; ++i)
    //     {
    //         for (int j = 0; j < outputWidth; ++j)
    //         {
    //             int x = (int)(j * widthRatio);
    //             int y = (int)(i * heightRatio);
    //             outputFrame[i][j] = inputFrame[y][x];
    //         }
    //     }
    // }


}
