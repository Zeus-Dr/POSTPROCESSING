using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ZEUS
{
    public class FrameSmoothing
    {
        private Bitmap previousFrame;
        private float alpha;

        public FrameSmoothing(float alphaValue)
        {
            alpha = alphaValue;
            previousFrame = null;
        }

        public Bitmap ApplyTemporalSmoothing(Bitmap currentFrame)
        {
            if (previousFrame == null)
            {
                previousFrame = new Bitmap(currentFrame);
                return new Bitmap(currentFrame); // Return a copy of the input frame
            }

            int width = currentFrame.Width;
            int height = currentFrame.Height;

            BitmapData currentData = currentFrame.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData previousData = previousFrame.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            Bitmap smoothedFrame = new Bitmap(width, height);
            BitmapData smoothedData = smoothedFrame.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* currentPtr = (byte*)currentData.Scan0;
                byte* previousPtr = (byte*)previousData.Scan0;
                byte* smoothedPtr = (byte*)smoothedData.Scan0;

                int bytesPerPixel = 4; // Assuming 32bpp (4 bytes per pixel)
                int byteCount = width * height * bytesPerPixel;

                // Parallel processing of image data
                Parallel.For(0, byteCount, i =>
                {
                    int currentPixelValue = currentPtr[i];
                    int previousPixelValue = previousPtr[i];

                    // Calculate weighted average
                    smoothedPtr[i] = (byte)(alpha * currentPixelValue + (1 - alpha) * previousPixelValue);
                });
            }

            currentFrame.UnlockBits(currentData);
            previousFrame.UnlockBits(previousData);
            smoothedFrame.UnlockBits(smoothedData);

            // Update previous frame for the next iteration
            previousFrame = new Bitmap(currentFrame);

            return smoothedFrame;
        }
    }
}

