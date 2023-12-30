using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ZEUS
{
    public class FrameSmoothing
    {
        private Bitmap previousFrame;
        private float alpha;

        public FrameSmoothing(float alphaValue)
        {
            alpha = alphaValue;
            previousFrame = null;
        }

        public Bitmap ApplyTemporalSmoothing(Bitmap currentFrame)
        {
            if (previousFrame == null)
            {
                previousFrame = new Bitmap(currentFrame);
                return new Bitmap(currentFrame); // Return a copy of the input frame
            }

            int width = currentFrame.Width;
            int height = currentFrame.Height;

            BitmapData currentData = currentFrame.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData previousData = previousFrame.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            Bitmap smoothedFrame = new Bitmap(width, height);
            BitmapData smoothedData = smoothedFrame.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* currentPtr = (byte*)currentData.Scan0;
                byte* previousPtr = (byte*)previousData.Scan0;
                byte* smoothedPtr = (byte*)smoothedData.Scan0;

                int bytesPerPixel = 4; // Assuming 32bpp (4 bytes per pixel)
                int byteCount = width * height * bytesPerPixel;

                // Parallel processing of image data
                Parallel.For(0, byteCount, i =>
                {
                    int currentPixelValue = currentPtr[i];
                    int previousPixelValue = previousPtr[i];

                    // Calculate weighted average
                    smoothedPtr[i] = (byte)(alpha * currentPixelValue + (1 - alpha) * previousPixelValue);
                });
            }

            currentFrame.UnlockBits(currentData);
            previousFrame.UnlockBits(previousData);
            smoothedFrame.UnlockBits(smoothedData);

            // Update previous frame for the next iteration
            previousFrame = new Bitmap(currentFrame);

            return smoothedFrame;
        }
    }
}

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