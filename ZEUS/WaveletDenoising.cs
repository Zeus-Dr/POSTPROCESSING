// using Accord.Math.Wavelets;

// using System.Drawing;

// namespace ZEUS
// {
//     class WaveletDenoising
//     {
//         public Bitmap DenoiseImage(Bitmap originalImage, double thresholdValue)
//         {
//             // Convert the original image to a double array
//             double[,] imageArray = ImageToDoubleArray(originalImage);

//             // Apply wavelet transform
//             Haar haar = new Haar(); // You can choose a different wavelet
//             haar.Compute(imageArray, 3); // Adjust the number of decomposition levels

//             // Apply thresholding to the wavelet coefficients
//             ApplyThreshold(haar, thresholdValue);

//             // Reconstruct the denoised image
//             double[,] denoisedImageArray = haar.Reconstruct();

//             // Convert the denoised double array to Bitmap
//             Bitmap denoisedImage = DoubleArrayToImage(denoisedImageArray);

//             return denoisedImage;
//         }

//         // Helper method to convert Bitmap to double array
//         private double[,] ImageToDoubleArray(Bitmap image)
//         {
//             int width = image.Width;
//             int height = image.Height;
//             double[,] imageArray = new double[width, height];

//             for (int y = 0; y < height; y++)
//             {
//                 for (int x = 0; x < width; x++)
//                 {
//                     imageArray[x, y] = image.GetPixel(x, y).GetBrightness();
//                 }
//             }

//             return imageArray;
//         }

//         // Helper method to convert double array to Bitmap
//         private Bitmap DoubleArrayToImage(double[,] imageArray)
//         {
//             int width = imageArray.GetLength(0);
//             int height = imageArray.GetLength(1);
//             Bitmap denoisedImage = new Bitmap(width, height);

//             for (int y = 0; y < height; y++)
//             {
//                 for (int x = 0; x < width; x++)
//                 {
//                     double pixelValue = imageArray[x, y];
//                     int intValue = (int)(255 * pixelValue);
//                     Color newColor = Color.FromArgb(intValue, intValue, intValue);
//                     denoisedImage.SetPixel(x, y, newColor);
//                 }
//             }

//             return denoisedImage;
//         }

//         // Helper method to apply threshold to wavelet coefficients
//         private void ApplyThreshold(Haar wavelet, double threshold)
//         {
//             for (int level = 1; level <= wavelet.Levels; level++)
//             {
//                 for (int direction = 0; direction < 3; direction++)
//                 {
//                     double[,] coeffs = wavelet.GetCoefficients(level, direction);

//                     for (int y = 0; y < coeffs.GetLength(1); y++)
//                     {
//                         for (int x = 0; x < coeffs.GetLength(0); x++)
//                         {
//                             if (Math.Abs(coeffs[x, y]) < threshold)
//                             {
//                                 coeffs[x, y] = 0; // Apply hard thresholding
//                                                   // Or coeffs[x, y] *= 0.5; for soft thresholding, adjust the scaling factor as needed
//                             }
//                         }
//                     }
//                     wavelet.SetCoefficients(level, direction, coeffs);
//                 }
//             }
//         }
//     }

// }
