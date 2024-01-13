using System.Drawing;
using Xunit;

namespace ZEUS.Tests
{
    public class FrameSharpeningTests
    {
        private const string TestImagePath = $"E:\\PJTS\\MINIPROJECT\\POSTPROCESSING\\SampleInput\\frame_00190.jpg"; // Update with your actual path

        [Fact]
        public void ApplyUnsharpMasking_ShouldApplyUnsharpMasking()
        {
            // Arrange
            float radius = 2.0f; // Update with your desired radius

            // Act
            Bitmap result = FrameSharpening.ApplyUnsharpMasking(new Bitmap(TestImagePath), radius);

            // Assert
            Assert.NotNull(result); // Check if the sharpened image is not null
        }

        [Fact]
        public void ApplyHighPassFiltering_ShouldApplyHighPassFiltering()
        {
            // Arrange
            double strength = 0.5; // Update with your desired strength

            // Act
            Bitmap result = FrameSharpening.ApplyHighPassFiltering(new Bitmap(TestImagePath), strength);

            // Assert
            Assert.NotNull(result); // Check if the sharpened image is not null
        }
    }
}
