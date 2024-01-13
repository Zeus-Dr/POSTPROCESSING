using System.Drawing;
using Xunit;

namespace ZEUS.Tests
{
    public class EvaluationTests
    {
        string inputPath = $"E:\\PJTS\\MINIPROJECT\\POSTPROCESSING\\SampleInput\\frame_00190.jpg";
        // private const string ImagePath1 = "path/to/your/image1.jpg"; // Update with your actual path
        // private const string ImagePath2 = "path/to/your/image2.jpg"; // Update with your actual path
        // private const string FfmpegPath = "path/to/ffmpeg.exe"; // Update with your actual path
        private const string FfmpegPath = $"C:/ffmpeg/ffmpeg.exe";
        // private const string VideoPath1 = "path/to/your/video1.mp4"; // Update with your actual path
        private const string VideoPath1 = $"E:\\PJTS\\MINIPROJECT\\POSTPROCESSING\\SampleInput\\Burna_Boy_-_Last_Last_[Official_Music_Video](1080p).mp4"; // Update with your actual path
                                                                                                                                                          // private const string VideoPath2 = "path/to/your/video2.mp4"; // Update with your actual path
        private const string VideoPath2 = $"E:\\PJTS\\MINIPROJECT\\POSTPROCESSING\\SampleInput\\Burna_Boy_-_Last_Last_[Official_Music_Video](1080p).mp4";

        [Fact]
        public void PSNR_ShouldReturnCorrectValue()
        {
            // Arrange
            Bitmap image1 = new Bitmap(inputPath);
            Bitmap image2 = new Bitmap(inputPath);

            // Act
            double psnr = Evaluation.PSNR(image1, image2);

            // Assert
            Assert.NotNull(image1);
            Assert.NotNull(image2);
            Assert.True(psnr >= 0); // PSNR should be non-negative
        }

        [Fact]
        public void SSIM_ShouldReturnCorrectValue()
        {
            // Arrange
            Bitmap image1 = new Bitmap(inputPath);
            Bitmap image2 = new Bitmap(inputPath);

            // Act
            double ssim = Evaluation.SSIM(image1, image2);

            // Assert
            Assert.NotNull(image1);
            Assert.NotNull(image2);
            Assert.InRange(ssim, 0, 1); // SSIM should be in the range [0, 1]
        }


        [Theory]
        [InlineData(FfmpegPath, VideoPath1, VideoPath2)]
        public void VMAFScore_ShouldReturnCorrectScore(string ffmpegPath, string videoPath1, string videoPath2)
        {
            // Act
            double vmafScore = Evaluation.VMAFScore(ffmpegPath, videoPath1, videoPath2);

            // Assert
            Assert.True(vmafScore >= 0); // Ensure a non-negative score
        }
    }
}
