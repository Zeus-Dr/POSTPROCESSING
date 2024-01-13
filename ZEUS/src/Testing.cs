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


// public static void CreateVideo(List<Bitmap> frames, string outputPath, int frameRate)
// {
//     if (frames.Count == 0)
//     {
//         Console.WriteLine("No frames to create video from.");
//         return;
//     }

//     // Get width and height of the frames
//     int width = frames[0].Width;
//     int height = frames[0].Height;

//     // Create a new video file
//     VideoFileWriter writer = new VideoFileWriter();
//     writer.Open(outputPath, width, height, frameRate, VideoCodec.MPEG4);

//     // Add frames to the video
//     foreach (Bitmap frame in frames)
//     {
//         writer.WriteVideoFrame(frame);
//     }

//     // Close the video file
//     writer.Close();

//     Console.WriteLine("Video created successfully at: " + outputPath);
// }


// public static void CreateVideo(string ffmpegPath, string inputFolderPath, string outputFilePath, int frameRate)
// {
//     Process ffmpegProcess = new Process();

//     // Set the FFmpeg executable path
//     ffmpegProcess.StartInfo.FileName = ffmpegPath;

//     // Example command to create a video from images using FFmpeg
//     // ffmpegProcess.StartInfo.Arguments = $"-framerate {frameRate} -start_number 190 -i {inputFolderPath}\\frame_%05d.jpg -c:v libx264 -r 30 -pix_fmt yuv420p {outputFilePath}";
//     ffmpegProcess.StartInfo.Arguments = $"-framerate {frameRate} -i {inputFolderPath}\\frame_%05d.jpg -c:v libx264 -r 30 -pix_fmt yuv420p {outputFilePath}";


//     ffmpegProcess.StartInfo.UseShellExecute = false;
//     ffmpegProcess.StartInfo.RedirectStandardOutput = true;

//     // Start the process
//     ffmpegProcess.Start();

//     // Wait for FFmpeg to complete
//     ffmpegProcess.WaitForExit();

//     Console.WriteLine("Video created successfully.");
// }

// public static void CreateVideoParallel(string ffmpegPath, string inputFolderPath, string outputFilePath, int frameRate, int chunks)
// {
//     int totalFrames = CountFrames(inputFolderPath); // Function to count total frames

//     int framesPerChunk = totalFrames / chunks;

//     string tempOutputFolder = "temp_outputs"; // Temporary folder for partial videos

//     // Create temp output folder if it doesn't exist
//     if (!System.IO.Directory.Exists(tempOutputFolder))
//     {
//         System.IO.Directory.CreateDirectory(tempOutputFolder);
//     }

//     // Launch FFmpeg for each chunk
//     Parallel.For(0, chunks, i =>
//     {
//         int startFrame = i * framesPerChunk + 1;
//         int endFrame = (i + 1) * framesPerChunk;

//         string tempOutputFile = $"{tempOutputFolder}\\output_{i}.mp4";

//         Process ffmpegProcess = new Process();
//         ffmpegProcess.StartInfo.FileName = ffmpegPath;

//         ffmpegProcess.StartInfo.Arguments = $"-framerate {frameRate} -start_number {startFrame} -i {inputFolderPath}\\frame_%05d.jpg -c:v libx264 -r 30 -pix_fmt yuv420p -frames:v {framesPerChunk} {tempOutputFile}";

//         ffmpegProcess.StartInfo.UseShellExecute = false;
//         ffmpegProcess.StartInfo.RedirectStandardOutput = true;

//         // Start the process
//         ffmpegProcess.Start();
//         ffmpegProcess.WaitForExit();
//     });

//     // Merge partial videos into final output
//     MergeVideos(tempOutputFolder, outputFilePath, chunks, ffmpegPath);
// }

// // Function to count frames in the input folder
// private static int CountFrames(string inputFolderPath)
// {
//     string[] files = System.IO.Directory.GetFiles(inputFolderPath, "*.jpg");
//     return files.Length;
// }

// // Function to merge videos using FFmpeg
// private static void MergeVideos(string inputFolderPath, string outputFilePath, int chunks, string ffmpegPath)
// {
//     Process ffmpegProcess = new Process();
//     ffmpegProcess.StartInfo.FileName = ffmpegPath; // Use FFmpeg again for merging

//     string fileList = "";

//     for (int i = 0; i < chunks; i++)
//     {
//         fileList += $"-i {inputFolderPath}\\output_{i}.mp4 ";
//     }

//     ffmpegProcess.StartInfo.Arguments = $"{fileList}-filter_complex \"";
//     for (int i = 0; i < chunks; i++)
//     {
//         ffmpegProcess.StartInfo.Arguments += $"[{i}:v:0]";
//     }
//     ffmpegProcess.StartInfo.Arguments += $"concat=n={chunks}:v=1:a=0[outv]\" -map \"[outv]\" -c:v libx264 -pix_fmt yuv420p {outputFilePath}";

//     ffmpegProcess.StartInfo.UseShellExecute = false;
//     ffmpegProcess.StartInfo.RedirectStandardOutput = true;

//     // Start the process
//     ffmpegProcess.Start();
//     ffmpegProcess.WaitForExit();

//     // Clean up temp files
//     for (int i = 0; i < chunks; i++)
//     {
//         System.IO.File.Delete($"{inputFolderPath}\\output_{i}.mp4");
//     }

//     Console.WriteLine("Video created successfully.");
// }



// using Accord.Math.Wavelets;

// using System.Drawing;

// namespace ZEUS
// {
//     class WaveletDenoising
//     {
//         public Bitmap DenoiseImage(Bitmap originalImage, double thresholdValue)
//         {
//             // Convert the original image to a double array
//             double[,] imageArray = ImageToDoubleArray(originalImage);

//             // Apply wavelet transform
//             Haar haar = new Haar(); // You can choose a different wavelet
//             haar.Compute(imageArray, 3); // Adjust the number of decomposition levels

//             // Apply thresholding to the wavelet coefficients
//             ApplyThreshold(haar, thresholdValue);

//             // Reconstruct the denoised image
//             double[,] denoisedImageArray = haar.Reconstruct();

//             // Convert the denoised double array to Bitmap
//             Bitmap denoisedImage = DoubleArrayToImage(denoisedImageArray);

//             return denoisedImage;
//         }

//         // Helper method to convert Bitmap to double array
//         private double[,] ImageToDoubleArray(Bitmap image)
//         {
//             int width = image.Width;
//             int height = image.Height;
//             double[,] imageArray = new double[width, height];

//             for (int y = 0; y < height; y++)
//             {
//                 for (int x = 0; x < width; x++)
//                 {
//                     imageArray[x, y] = image.GetPixel(x, y).GetBrightness();
//                 }
//             }

//             return imageArray;
//         }

//         // Helper method to convert double array to Bitmap
//         private Bitmap DoubleArrayToImage(double[,] imageArray)
//         {
//             int width = imageArray.GetLength(0);
//             int height = imageArray.GetLength(1);
//             Bitmap denoisedImage = new Bitmap(width, height);

//             for (int y = 0; y < height; y++)
//             {
//                 for (int x = 0; x < width; x++)
//                 {
//                     double pixelValue = imageArray[x, y];
//                     int intValue = (int)(255 * pixelValue);
//                     Color newColor = Color.FromArgb(intValue, intValue, intValue);
//                     denoisedImage.SetPixel(x, y, newColor);
//                 }
//             }

//             return denoisedImage;
//         }

//         // Helper method to apply threshold to wavelet coefficients
//         private void ApplyThreshold(Haar wavelet, double threshold)
//         {
//             for (int level = 1; level <= wavelet.Levels; level++)
//             {
//                 for (int direction = 0; direction < 3; direction++)
//                 {
//                     double[,] coeffs = wavelet.GetCoefficients(level, direction);

//                     for (int y = 0; y < coeffs.GetLength(1); y++)
//                     {
//                         for (int x = 0; x < coeffs.GetLength(0); x++)
//                         {
//                             if (Math.Abs(coeffs[x, y]) < threshold)
//                             {
//                                 coeffs[x, y] = 0; // Apply hard thresholding
//                                                   // Or coeffs[x, y] *= 0.5; for soft thresholding, adjust the scaling factor as needed
//                             }
//                         }
//                     }
//                     wavelet.SetCoefficients(level, direction, coeffs);
//                 }
//             }
//         }
//     }

// }

// static void Main()
// {
//     string directoryPath = $"E:/PJTS/MINIPROJECT/POSTPROCESSING/SampleInput";
//     // string directoryPath = $"D:/Research/Output/Last1080p";
//     // Assuming you have a sequence of Bitmap frames stored in an array or list
//     DateTime startTime = DateTime.Now;
//     IEnumerable<Bitmap> frames = LoadFramesLazy(directoryPath); // Lazy loading frames
//     DateTime endTime = DateTime.Now;

//     TimeSpan elapsedTime = endTime - startTime;
//     Console.WriteLine($"Elapsed Time for Loading Frames: {elapsedTime.TotalMilliseconds} milliseconds");

//     // Create an instance of TemporalAveragingSmoothing
//     FrameSmoothing smoother = new FrameSmoothing(0.5f); // Adjust alpha value as needed
//     DateTime startTime1 = DateTime.Now;
//     // Apply smoothing to each frame

//     int frameNumber = 0;
//     foreach (Bitmap frame in frames)
//     {
//         DateTime startTime2 = DateTime.Now;
//         Bitmap smoothedFrame = smoother.ApplyTemporalSmoothing(frame);
//         DateTime endTime2 = DateTime.Now;
//         TimeSpan elapsedTime2 = endTime2 - startTime2;

//         Console.WriteLine($"Elapsed Time: {elapsedTime2.TotalMilliseconds} milliseconds");
//         // Display or use the smoothed frame as needed
//         SaveFrame(smoothedFrame, frameNumber);

//         frameNumber++;
//     }
//     DateTime endTime1 = DateTime.Now;
//     TimeSpan elapsedTime1 = endTime1 - startTime1;
//     string formattedTime = string.Format("{0:D2}hrs:{1:D2}mins:{2:D2}secs:{3:D3}ms",
//         elapsedTime1.Hours,
//         elapsedTime1.Minutes,
//         elapsedTime1.Seconds,
//         elapsedTime1.Milliseconds);

//     Console.WriteLine($"Total Elapsed Time: {formattedTime}");
// }

// static IEnumerable<Bitmap> LoadFramesLazy(string directoryPath)
// {
//     if (Directory.Exists(directoryPath))
//     {
//         string[] imageFiles = Directory.GetFiles(directoryPath);

//         foreach (string filePath in imageFiles)
//         {
//             Bitmap bitmap = null;
//             try
//             {
//                 bitmap = new Bitmap(filePath);
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"Error loading image '{Path.GetFileName(filePath)}' as Bitmap: {ex.Message}");
//             }

//             if (bitmap != null)
//             {
//                 yield return bitmap; // Yield the bitmap if it was successfully loaded
//             }
//         }
//     }
//     else
//     {
//         Console.WriteLine($"Directory {directoryPath} does not exist.");
//     }
// }



// static void SaveFrame(Bitmap frame, int i)
// {
//     string outputPath = $"E:/PJTS/MINIPROJECT/POSTPROCESSING/SampleOutput/SmoothedFrame_{i}.jpg";

//     // Save the filtered image to a new file
//     frame.Save(outputPath, ImageFormat.Png);
//     // Assuming 'smoothedFrame' is the Bitmap you want to save
//     // 'filePath' is the path where you want to save the frame
//     // 'qualityLevel' is the JPEG compression quality level (0-100)
//     // EncoderParameters encoderParams = new EncoderParameters(1);
//     // encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, qualityLevel);

//     // ImageCodecInfo jpgEncoder = GetEncoderInfo("image/jpeg");

//     // smoothedFrame.Save(filePath, jpgEncoder, encoderParams);

//     System.Console.WriteLine($"Frame {i} saved");
// }