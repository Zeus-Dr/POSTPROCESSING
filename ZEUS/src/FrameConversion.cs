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
}
