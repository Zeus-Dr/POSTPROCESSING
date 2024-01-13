using System.Drawing;
using System.Drawing.Imaging;
using Xunit;

namespace ZEUS.Tests
{
    public class ColorCorrectionTests
    {

        string inputPath = $"E:\\PJTS\\MINIPROJECT\\POSTPROCESSING\\SampleInput\\frame_00190.jpg";
        string outputPath = $"E:\\PJTS\\MINIPROJECT\\POSTPROCESSING\\SampleOutput\\histogramequalizedFrame.jpg";
        string outputPath1 = $"E:\\PJTS\\MINIPROJECT\\POSTPROCESSING\\SampleOutput\\whitebalanceFrame.jpg";
        string outputPath2 = $"E:\\PJTS\\MINIPROJECT\\POSTPROCESSING\\SampleOutput\\whitebalanceFrame2.jpg";

        [Fact]
        public void HistogramEqualization_ShouldReturnColorCorrectedImage()
        {
            // Arrange
            Bitmap originalImage = new Bitmap(inputPath); // Provide the actual path to your test image
            Bitmap expectedImage = new Bitmap(outputPath); // Provide the expected result image

            // Act
            Bitmap resultImage = ColorCorrection.HistogramEqualization(originalImage);

            // Assert
            Assert.Equal(expectedImage.Size, resultImage.Size);

            for (int x = 0; x < expectedImage.Width; x++)
            {
                for (int y = 0; y < expectedImage.Height; y++)
                {
                    Color expectedPixel = expectedImage.GetPixel(x, y);
                    Color resultPixel = resultImage.GetPixel(x, y);

                    Assert.Equal(expectedPixel, resultPixel);
                }
            }
        }

        [Fact]
        public void WhiteBalance_ShouldReturnColorCorrectedImage()
        {
            // Arrange
            Bitmap originalImage = new Bitmap(inputPath); // Provide the actual path to your test image
            Bitmap expectedImage = new Bitmap(outputPath1); // Provide the expected result image

            // Act
            Bitmap resultImage = ColorCorrection.WhiteBalance(originalImage);

            resultImage.Save(outputPath2, ImageFormat.Png);
            resultImage = new Bitmap(outputPath2);

            // Assert
            Assert.Equal(expectedImage.Size, resultImage.Size);

            for (int x = 0; x < expectedImage.Width; x++)
            {
                for (int y = 0; y < expectedImage.Height; y++)
                {
                    Color expectedPixel = expectedImage.GetPixel(x, y);
                    Color resultPixel = resultImage.GetPixel(x, y);

                    Assert.Equal(expectedPixel, resultPixel);
                }
            }
        }
    }
}
