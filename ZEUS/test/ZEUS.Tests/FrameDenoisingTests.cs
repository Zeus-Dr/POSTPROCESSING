using System.Drawing;
using Xunit;

namespace ZEUS.Tests
{
    public class FrameDenoisingTests
    {
        private const string TestImagePath = $"E:\\PJTS\\MINIPROJECT\\POSTPROCESSING\\SampleInput\\frame_00190.jpg"; // Update with your actual path

        [Fact]
        public void ApplyGaussianFilter_ShouldFilterImageWithGaussian()
        {
            // Arrange
            int kernelSize = 3; // Update with your desired kernel size

            // Act
            var frameDenoising = new FrameDenoising();
            Bitmap result = frameDenoising.ApplyGaussianFilter(new Bitmap(TestImagePath), kernelSize);

            // Assert
            Assert.NotNull(result); // Check if the filtered image is not null
            // Add more assertions based on the expected behavior of Gaussian filtering
        }

        [Fact]
        public void ApplyMedianFilter_ShouldFilterImageWithMedian()
        {
            // Arrange
            int windowSize = 3; // Update with your desired window size

            // Act
            var frameDenoising = new FrameDenoising();
            Bitmap result = frameDenoising.ApplyMedianFilter(new Bitmap(TestImagePath), windowSize);

            // Assert
            Assert.NotNull(result); // Check if the filtered image is not null
        }
    }
}
