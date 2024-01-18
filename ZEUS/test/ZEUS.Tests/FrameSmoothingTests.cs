using System.Drawing;
using Xunit;

namespace ZEUS.Tests
{
    public class FrameSmoothingTests
    {
        private const string TestImagePath = $"E:\\PJTS\\MINIPROJECT\\POSTPROCESSING\\SampleInput\\frame_00190.jpg"; // Update with your actual path

        [Fact]
        public void ApplyTemporalSmoothing_ShouldSmoothFrames()
        {
            // Arrange
            float alphaValue = 0.2f; // Update with your desired alpha value

            // Act
            var frameSmoothing = new FrameSmoothing(alphaValue);
            Bitmap currentFrame = new Bitmap(TestImagePath); // Replace with actual frame or path
            Bitmap result = frameSmoothing.ApplyTemporalSmoothing(currentFrame);

            // Assert
            Assert.NotNull(result); // Check if the smoothed frame is not null
        }
    }
}
