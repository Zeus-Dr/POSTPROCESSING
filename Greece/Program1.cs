using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using ZEUS;
using AForge.Imaging;
using AForge.Imaging.Filters;

class Program1
{
    static void Main()
    {
        string directoryPath = $"E:/PJTS/MINIPROJECT/POSTPROCESSING/SampleInput";

        // Assuming you have a sequence of Bitmap frames stored in an array or list
        DateTime startTime = DateTime.Now;
        List<Bitmap> frames = LoadFrames(directoryPath); // Function to load frames
        DateTime endTime = DateTime.Now;
        TimeSpan elapsedTime = endTime - startTime;

        Console.WriteLine($"Elapsed Time: {elapsedTime.TotalMilliseconds} milliseconds");

        for (int i = 0; i < frames.Count; i++)
        {
            // Create filters for smoothing
            IFilter filter = new Difference(frames[i - 1]);
            filter.ApplyInPlace(frames[i]);
        }

        // // Create an instance of TemporalAveragingSmoothing
        // TemporalAveragingSmoothing smoother = new TemporalAveragingSmoothing(0.5f); // Adjust alpha value as needed
        // DateTime startTime1 = DateTime.Now;
        // // Apply smoothing to each frame
        // foreach (Bitmap frame in frames)
        // {
        //     DateTime startTime2 = DateTime.Now;
        //     Bitmap smoothedFrame = smoother.ApplyTemporalSmoothing(frame);
        //     DateTime endTime2 = DateTime.Now;
        //     TimeSpan elapsedTime2 = endTime2 - startTime2;

        //     Console.WriteLine($"Elapsed Time: {elapsedTime2.TotalMilliseconds} milliseconds");
        //     // Display or use the smoothed frame as needed
        //     SaveFrame(smoothedFrame);
        // }
        // DateTime endTime1 = DateTime.Now;
        // TimeSpan elapsedTime1 = endTime1 - startTime1;

        // Console.WriteLine($"Elapsed Time: {elapsedTime1.TotalMilliseconds} milliseconds");
    }

    static List<Bitmap> LoadFrames(string directoryPath)
    {
        List<Bitmap> bitmaps = new List<Bitmap>();

        if (Directory.Exists(directoryPath))
        {
            string[] imageFiles = Directory.GetFiles(directoryPath);

            foreach (string filePath in imageFiles)
            {
                try
                {
                    Bitmap bitmap = new Bitmap(filePath);
                    bitmaps.Add(bitmap);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading image '{Path.GetFileName(filePath)}' as Bitmap: {ex.Message}");
                }
            }
        }
        else
        {
            Console.WriteLine($"Directory {directoryPath} does not exist.");
        }

        return bitmaps;
    }

    static void SaveFrame(Bitmap frame)
    {
        string timestamp = DateTime.Now.ToString("yyyMMddHHmmss");
        string outputPath = $"E:/PJTS/MINIPROJECT/POSTPROCESSING/SampleOutput/SmoothedFrame_{timestamp}.jpg";

        // Save the filtered image to a new file
        frame.Save(outputPath, ImageFormat.Png);
        Console.WriteLine($"Smoothed Frame saved to {outputPath}");
    }
}
