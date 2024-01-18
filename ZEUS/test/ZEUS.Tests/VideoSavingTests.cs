using System.IO;
using Xunit;

namespace ZEUS.Tests
{
    public class VideoSavingTests
    {
        private const string TestInputFolderPath = $"D:\\Research\\Output\\Last1080p"; // Update with your actual path
        private const string TestOutputFilePath = $"E:/PJTS/MINIPROJECT/POSTPROCESSING/SampleOutput/video.mp4"; // Update with your actual path

        [Fact]
        public void CreateVideo_ShouldCreateVideoFile()
        {
            // Arrange
            int frameRate = 30;
            int batchSize = 10;

            // Act
            VideoSaving.CreateVideo(TestInputFolderPath, TestOutputFilePath, frameRate, batchSize);

            // Assert
            Assert.True(File.Exists(TestOutputFilePath)); // Check if the output video file exists
        }
    }
}
