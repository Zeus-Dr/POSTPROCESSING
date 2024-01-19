using System.Diagnostics;

namespace ZEUS
{
    public class VideoSaving
    {
        /// <summary>
        /// Creates a video by combining frames from a specified input folder and saves it to the specified output file path.
        /// </summary>
        /// <param name="inputFolderPath">The path to the folder containing input frames.</param>
        /// <param name="outputFilePath">The path to the output video file.</param>
        /// <param name="frameRate">The frame rate of the output video.</param>
        /// <param name="batchSize">The number of frames processed in each batch.</param>
        public static void CreateVideo(string inputFolderPath, string outputFilePath, int frameRate, int batchSize, string ffmpegPath)
        {
            string[] imagePaths = Directory.GetFiles(inputFolderPath, "frame_*.jpg", SearchOption.TopDirectoryOnly);
            List<List<string>> batches = PartitionIntoBatches(imagePaths, batchSize);

            List<string> intermediateVideos = new List<string>();

            // Process each batch in parallel and save frames to separate intermediate video files
            Parallel.ForEach(batches, (batch, state, index) =>
            {
                string intermediateOutput = Path.Combine(Path.GetDirectoryName(outputFilePath), $"intermediate_{index:00000}.mp4");
                ProcessBatch(batch, frameRate, intermediateOutput, ffmpegPath);
                lock (intermediateVideos)
                {
                    intermediateVideos.Add(intermediateOutput);
                }
            });

            // Combine all intermediate videos into a single final video file
            CombineVideos(intermediateVideos, outputFilePath, ffmpegPath);

            // Cleanup: Delete intermediate video files
            intermediateVideos.ForEach(File.Delete);

            Console.WriteLine("Video creation process finished.");
        }

        /// <summary>
        /// Processes a batch of image files and generates an intermediate video file.
        /// </summary>
        /// <param name="batch">The list of image file paths in the batch.</param>
        /// <param name="frameRate">The frame rate of the output video.</param>
        /// <param name="intermediateOutput">The path to the intermediate video file.</param>
        private static void ProcessBatch(List<string> batch, int frameRate, string intermediateOutput, string ffmpegPath)
        {
            string inputFiles = string.Join(" ", batch.Select(file => $"-i {file}"));

            // Modify the ffmpeg arguments for encoding
            string ffmpegArgs = $"-framerate {frameRate} {inputFiles} -c:v libx264 -crf 25 -preset medium -y {intermediateOutput}";

            RunFFmpegCommand(ffmpegArgs, ffmpegPath);
        }

        /// <summary>
        /// Combines multiple intermediate videos into a single final video file.
        /// </summary>
        /// <param name="intermediateVideos">The list of intermediate video file paths.</param>
        /// <param name="outputFilePath">The path to the final output video file.</param>
        private static void CombineVideos(List<string> intermediateVideos, string outputFilePath, string ffmpegPath)
        {
            string concatFilePath = Path.Combine(Path.GetDirectoryName(outputFilePath), "intermediate.txt");

            // Create a text file listing all intermediate videos for concatenation
            using (StreamWriter sw = File.CreateText(concatFilePath))
            {
                foreach (string videoFile in intermediateVideos)
                {
                    sw.WriteLine($"file '{videoFile}'");
                }
            }

            // Transcode and explicitly set the output frame rate
            string ffmpegArgs = $"-f concat -safe 0 -i {concatFilePath} -c:v libx264 -vf fps=30 -r 30 {outputFilePath} -y";
            RunFFmpegCommand(ffmpegArgs, ffmpegPath);

            // Cleanup: Delete the intermediate text file
            File.Delete(concatFilePath);
        }

        /// <summary>
        /// Runs an FFmpeg command with the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments to be passed to the FFmpeg command.</param>
        private static void RunFFmpegCommand(string arguments, string ffmpegPath)
        {
            Process ffmpegProcess = new Process();
            ffmpegProcess.StartInfo.FileName = ffmpegPath;
            ffmpegProcess.StartInfo.Arguments = arguments;

            // Use CPU instead of GPU for encoding
            ffmpegProcess.StartInfo.EnvironmentVariables["FFREPORT"] = "level=32"; // Set FFREPORT environment variable for CPU encoding
            ffmpegProcess.StartInfo.EnvironmentVariables["FF_CODEC_THREADS"] = "0"; // Set threads to 0 for auto-threading (CPU)

            ffmpegProcess.Start();
            ffmpegProcess.WaitForExit();
        }

        /// <summary>
        /// Partitions a source array into batches of a specified size.
        /// </summary>
        /// <param name="source">The array to be partitioned.</param>
        /// <param name="batchSize">The size of each batch.</param>
        /// <returns>A list of batches, where each batch is a list of elements.</returns>
        private static List<List<string>> PartitionIntoBatches(string[] source, int batchSize)
        {
            var batches = new List<List<string>>();

            for (int i = 0; i < source.Length; i += batchSize)
            {
                var batch = source.Skip(i).Take(batchSize).ToList();
                batches.Add(batch);
            }

            return batches;
        }



    }

}
