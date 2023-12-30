using System;
using System.Drawing;
using System.Drawing.Imaging;
using ZEUS;
// class Program1
// {
//     static void Main()
//     {
//         // Load the original and distorted images as Bitmaps
//         Bitmap originalImage = new Bitmap($"E:/PJTS/MINIPROJECT/POSTPROCESSING/SampleInput/frame_00190.jpg");
//         // Bitmap distortedImage = new Bitmap($"E:/PJTS/MINIPROJECT/POSTPROCESSING/SampleInput/frame_00200.jpg");
//         Bitmap distortedImage = new Bitmap($"E:/PJTS/MINIPROJECT/POSTPROCESSING/SampleOutput/AFrame.jpg");

//         Evaluation psnrCalculator = new Evaluation();

//         try
//         {
//             DateTime startTime = DateTime.Now;
//             // Calculate PSNR between the images
//             // double psnrValue = psnrCalculator.CalculatePSNR(originalImage, distortedImage);
//             double ssimValue = Evaluation.CalculateSSIM(originalImage, distortedImage);
//             DateTime endTime = DateTime.Now;
//             TimeSpan elapsedTime1 = endTime - startTime;
//             string formattedTime = string.Format("{0:D2}hrs:{1:D2}mins:{2:D2}secs:{3:D3}ms",
//                 elapsedTime1.Hours,
//                 elapsedTime1.Minutes,
//                 elapsedTime1.Seconds,
//                 elapsedTime1.Milliseconds);

//             Console.WriteLine($"Total Elapsed Time: {formattedTime}");
//             Console.WriteLine($"SSIM value: {ssimValue}");
//         }
//         catch (ArgumentException ex)
//         {
//             // Console.WriteLine($"Error: {ex.Message}");
//             Console.WriteLine(ex);
//         }
//         finally
//         {
//             // Dispose Bitmaps to free up resources
//             originalImage.Dispose();
//             distortedImage.Dispose();
//         }
//     }
// }


class Program
{
    static void Main()
    {
        string VideoPath1 = "path_to_video_1.mp4";
        string VideoPath2 = "path_to_video_2.mp4";

        double ssim = Evaluation.SSIM(new Bitmap(VideoPath1), new Bitmap(VideoPath2));
        Console.WriteLine($"SSIM score: {ssim}");
    }
}
