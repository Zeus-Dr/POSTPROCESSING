using System.Drawing;
using System.Drawing.Imaging;
using ZEUS;

namespace Greece
{


    // class Program
    // {
    //     static void Main(string[] args)
    //     {
    //         //Trying out White Balance of the Color Correction Module
    //         string inputPath = "path_to_input_image.jpg";
    //         string outputPath = "path_to_output_image.jpg";
    //         Bitmap inputImage = new Bitmap(inputPath);

    //         Bitmap colorCorrectedImage = ColorCorrection.WhiteBalance(inputImage);
    //         //Use the color-corrected image as neccessary
    //     }

    // }
    // class Program
    // {
    //     static void Main(string[] args)
    //     {
    //         try
    //         {
    //             string timestamp = DateTime.Now.ToString("yyyMMddHHmmss");
    //             string imagePath = $"E:/PJTS/MINIPROJECT/IMPLEMENTATION2/SampleInput/frame_00523.jpg";
    //             string outputPath = $"E:/PJTS/MINIPROJECT/IMPLEMENTATION2/SampleOutput/GFilteredOutput_{timestamp}.jpg";

    //             // Load the image
    //             Bitmap inputImage = new Bitmap(imagePath);

    //             // Create an instance of Athena class
    //             ZEUS.Athena athena = new ZEUS.Athena();

    //             // Define the kernel size and sigma for the Gaussian filter
    //             int kernelSize = 3; // Change as needed
    //             double sigma = 1.4; // Change as needed

    //             // Apply Gaussian filter to the image
    //             Bitmap filteredImage = athena.ApplyGaussianFilter(inputImage, kernelSize, sigma);

    //             // Save the filtered image to a new file
    //             filteredImage.Save(outputPath, ImageFormat.Png);
    //             Console.WriteLine($"Filtered image saved to {outputPath}");

    //             // Display the filtered image (optional)
    //             // Uncomment this block if you want to display the filtered image
    //             // using (System.Diagnostics.Process process = new System.Diagnostics.Process())
    //             // {
    //             //     process.StartInfo.FileName = outputImagePath;
    //             //     process.Start();
    //             // }

    //             // Dispose image objects
    //             inputImage.Dispose();
    //             filteredImage.Dispose();

    //         }
    //         catch (Exception ex)
    //         {
    //             Console.WriteLine($"Error: {ex.Message}");
    //         }
    //     }
    // }

    // class Program
    // {
    //     static void Main(string[] args)
    //     {
    //         try
    //         {
    //             string timestamp = DateTime.Now.ToString("yyyMMddHHmmss");
    //             string imagePath = $"E:/PJTS/MINIPROJECT/IMPLEMENTATION2/SampleInput/frame_00523.jpg";
    //             string outputPath = $"E:/PJTS/MINIPROJECT/IMPLEMENTATION2/SampleOutput/FilteredOutput_{timestamp}.jpg";

    //             // Load the image
    //             Bitmap inputImage = new Bitmap(imagePath);

    //             // Create an instance of Athena class
    //             ZEUS.Athena athena = new ZEUS.Athena();

    //             // Define the window size for the median filter
    //             int windowSize = 3; // Change as needed

    //             // Apply median filter to the image
    //             Bitmap filteredImage = athena.ApplyMedianFilter(inputImage, windowSize);

    //             // Save the filtered image to a new file
    //             filteredImage.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);
    //             Console.WriteLine($"Filtered image saved to {outputPath}");

    //             // Display the filtered image (optional)
    //             // Uncomment this block if you want to display the filtered image
    //             // using (System.Diagnostics.Process process = new System.Diagnostics.Process())
    //             // {
    //             //     process.StartInfo.FileName = outputPath;
    //             //     process.Start();
    //             // }

    //             // Dispose image objects
    //             inputImage.Dispose();
    //             filteredImage.Dispose();

    //             // Wait for user input before closing the console
    //             Console.ReadLine();
    //         }
    //         catch (Exception ex)
    //         {
    //             Console.WriteLine($"Error: {ex.Message}");
    //         }
    //     }
    // }
    // class Pic
    // {
    //     static void Main(string[] args)
    //     {
    //         //Trying out the Resizing method of Coronos
    //         string timestamp = DateTime.Now.ToString("yyyMMddHHmmss");
    //         string imagePath = $"E:/PJTS/MINIPROJECT/IMPLEMENTATION2/SampleInput/frame_01939.jpg";
    //         string outputPath = $"E:/PJTS/MINIPROJECT/IMPLEMENTATION2/SampleOutput/Resized720pOutput_{timestamp}.jpg";
    //         ZEUS.Coronos coronos = new ZEUS.Coronos();
    //         coronos.ResizeFrame(imagePath, outputPath);

    //         //Trying out denoising method of Athena
    //     }
    // }

    // class Program
    // {
    //     static void Main(string[] args)
    //     {
    //         int result = ZEUS.Testing.Add(5, 3);
    //         Console.WriteLine("Addition result: " + result);

    //         string greeting = ZEUS.Testing.Greet("Zeus");
    //         Console.WriteLine(greeting);
    //         ZEUS.Testing.SayHello();

    //         //////////////////////////////////////
    //         /////////////////////////////////////

    //         // Example input RGB frame (3x3 for demonstration)
    //         int[][] RGBFrame =
    //         [
    //             [255, 0, 0],
    //             [0, 255, 0],
    //             [0, 0, 255]
    //         ];

    //         int width = 3; // Width of the frame
    //         int height = 3; // Height of the frame

    //         // Allocate memory for the output YUV frame
    //         int[][] YUVFrame = new int[height][];
    //         for (int i = 0; i < height; ++i)
    //         {
    //             YUVFrame[i] = new int[width];
    //         }

    //         // Create an instance of the Coronos class
    //         ZEUS.Coronos coronos = new ZEUS.Coronos();

    //         // Call the ConvertRGBtoYUV function
    //         coronos.ConvertRGBtoYUV(RGBFrame, YUVFrame, width, height);

    //         // Display the converted YUV frame (for demonstration)
    //         Console.WriteLine("YUV Frame:");
    //         for (int i = 0; i < height; ++i)
    //         {
    //             for (int j = 0; j < width; ++j)
    //             {
    //                 Console.Write(YUVFrame[i][j] + " ");
    //             }
    //             Console.WriteLine();
    //         }

    //         //////////////////////////////////////
    //         ///////// Testing ResizeFrame /////////

    //         // Example input frame (3x3 for demonstration)
    //         int[][] inputFrame =
    //         [
    //             [1, 2, 3],
    //             [4, 5, 6],
    //             [7, 8, 9]
    //         ];

    //         int inputWidth = 3; // Input frame width
    //         int inputHeight = 3; // Input frame height

    //         int outputWidth = 2; // Desired output frame width
    //         int outputHeight = 2; // Desired output frame height

    //         // Allocate memory for the output frame
    //         int[][] outputFrame = new int[outputHeight][];
    //         for (int i = 0; i < outputHeight; ++i)
    //         {
    //             outputFrame[i] = new int[outputWidth];
    //         }

    //         // Call the ResizeFrame method
    //         coronos.ResizeFrame(inputFrame, outputFrame, inputWidth, inputHeight, outputWidth, outputHeight);

    //         // Display the resized frame (for demonstration)
    //         Console.WriteLine("Resized Frame:");
    //         for (int i = 0; i < outputHeight; ++i)
    //         {
    //             for (int j = 0; j < outputWidth; ++j)
    //             {
    //                 Console.Write(outputFrame[i][j] + " ");
    //             }
    //             Console.WriteLine();
    //         }

    //         // Wait for user input before closing the console
    //         Console.ReadLine();
    //     }

    // }

}
