using System;
using System.Drawing;
using System.Drawing.Imaging;
using ZEUS;

class Program1
{
    static void Main()
    {
        // string directoryPath = $"D:\\Research\\Output\\Last1080p";
        // string outputPath = $"E:/PJTS/MINIPROJECT/POSTPROCESSING/SampleOutput/video.mp4";
        // string ffmpegpath = $"C:/ffmpeg/ffmpeg.exe";
        // int batchSize = 100;

        // DateTime startTime = DateTime.Now;
        // VideoSaving.CreateVideo(directoryPath, outputPath, 25, batchSize);
        // DateTime endTime = DateTime.Now;
        // Console.WriteLine("Video creation process finished.");
        // TimeSpan elapsedTime = endTime - startTime;
        // string formattedTime = string.Format("{0:D2}hrs:{1:D2}mins:{2:D2}secs:{3:D3}ms",
        //     elapsedTime.Hours,
        //     elapsedTime.Minutes,
        //     elapsedTime.Seconds,
        //     elapsedTime.Milliseconds);
        // Console.WriteLine($"Elapsed Time for creating video: {formattedTime}");
        string inputPath = $"E:\\PJTS\\MINIPROJECT\\POSTPROCESSING\\SampleInput\\frame_00190.jpg";
        string outputPath = $"E:\\PJTS\\MINIPROJECT\\POSTPROCESSING\\SampleOutput\\histogramequalizedFrame.jpg";
        string outputPath1 = $"E:\\PJTS\\MINIPROJECT\\POSTPROCESSING\\SampleOutput\\whitebalanceFrame.jpg";

        //For Color conversion
        //histogramequalization
        Bitmap frame = new Bitmap(inputPath);
        Bitmap colorCorrected = ColorCorrection.HistogramEqualization(frame);
        colorCorrected.Save(outputPath, ImageFormat.Png);

        //whitebalance
        Bitmap colorCorrected1 = ColorCorrection.WhiteBalance(frame);
        colorCorrected1.Save(outputPath1, ImageFormat.Png);

        //For Evaluation


        //For FrameConversion

        //For Frame Denoising

        //For FrameSharpening

        //FrameSmoothing

        //VideoSaving

    }
}


