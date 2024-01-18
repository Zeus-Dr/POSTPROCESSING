using System.Drawing;
using System.IO;
using Xunit;

namespace ZEUS.Tests
{
    public class FrameConversionTests
    {
        // private const string TestImagePath = "path/to/your/test-image.jpg"; // Update with your actual path
        private const string TestImagePath = $"E:\\PJTS\\MINIPROJECT\\POSTPROCESSING\\SampleInput\\frame_00190.jpg"; // Update with your actual path

        [Fact]
        public void ResizeFrame_Save_ShouldSaveResizedImage()
        {
            // Arrange
            string outputPath = $"E:\\PJTS\\MINIPROJECT\\POSTPROCESSING\\SampleOutput\\resizedFrame.jpg"; // Update with your actual path
            int outputWidth = 100; // Update with your desired width
            int outputHeight = 100; // Update with your desired height

            // Act
            var frameConversion = new FrameConversion();
            frameConversion.ResizeFrame(TestImagePath, outputPath, outputWidth, outputHeight);

            // Assert
            Assert.True(File.Exists(outputPath)); // Check if the file was created
        }

        [Fact]
        public void ResizeFrame_Return_ShouldReturnResizedImage()
        {
            // Arrange
            int outputWidth = 100; // Update with your desired width
            int outputHeight = 100; // Update with your desired height

            // Act
            var frameConversion = new FrameConversion();
            Bitmap resizedImage = frameConversion.ResizeFrame(TestImagePath, outputWidth, outputHeight);

            // Assert
            Assert.NotNull(resizedImage); // Check if the resized image is not null
            Assert.Equal(outputWidth, resizedImage.Width); // Check if the width matches
            Assert.Equal(outputHeight, resizedImage.Height); // Check if the height matches
        }
    }
}
