using System.Drawing;
using System.Drawing.Imaging;
using ZEUS;

namespace Greece
{
    class Program1
    {
        static void Main(string[] args)
        {
            //Trying out White Balance of the Color Correction Module
            string inputPath = "path_to_input_image.jpg";
            string outputPath = "path_to_output_image.jpg";
            Bitmap inputImage = new Bitmap(inputPath);

            Bitmap colorCorrectedImage = ColorCorrection.WhiteBalance(inputImage);
            //Use the color-corrected image as neccessary
        }
    }
}