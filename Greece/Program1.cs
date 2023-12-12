using System.Drawing;
using System.Drawing.Imaging;
using ZEUS;

namespace Greece
{
    class Program1
    {
        static void Main(string[] args)
        {
            string inputPath = $"E:/PJTS/MINIPROJECT/POSTPROCESSING/SampleInput/frame_00523.jpg";
            Bitmap inputImage = new Bitmap(inputPath);

            // Apply high pass filtering and sharpening
            double sharpenStrength = 0; // Adjust this value for different sharpening effects

            DateTime start = DateTime.Now;
            Bitmap sharpenedImage = FrameSharpening.ApplyHighPassFiltering(inputImage, sharpenStrength);
            DateTime end = DateTime.Now;

            Console.WriteLine($"Time taken: {(end - start).TotalMilliseconds} ms");
            // Save the sharpened image
            string timestamp = DateTime.Now.ToString("yyyMMddHHmmss");
            string outputPath = $"E:/PJTS/MINIPROJECT/POSTPROCESSING/SampleOutput/6HSharpened_{timestamp}.jpg";
            sharpenedImage.Save(outputPath, ImageFormat.Png);
        }
    }
}