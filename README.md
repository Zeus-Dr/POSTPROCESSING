# POST PROCESSING LIBRARY

![License](https://img.shields.io/badge/license-MIT-blue.svg)

## Overview

The Video Processing Library is a versatile post-processing library designed for enhancing and manipulating video frames. Whether you're working on video upscaling, denoising, color correction, or frame evaluation, this library provides a set of modules to streamline your post-processing workflows.

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Example Usage](#example-usage)
- [Documentation](#documentation)
- [Support](#support)

## Features

- **Frame Conversion:**
  - Convert frames between different color space models.
  - Resize or scale frames to desired resolutions.

- **Frame Denoising:**
  - Reduce noise in video frames using various denoising algorithms.

- **Frame Sharpening:**
  - Enhance the clarity and details in video frames.

- **Color Correction:**
  - Adjust color balance, saturation, or hue of video frames.

- **Frame Smoothing:**
  - Smooth out variations in video frames for improved visual quality

- **Frame Evaluation:**
  - Obtain evaluation metrics for comparing the quality of video frames.

- **Video Saving:**
  - Save processed video frames or entire videos in various formats.

## Installation
### dotnet (C#)

#### 1. Visit GitHub Releases

- Go to the releases page of the [GitHub repository.](https://github.com/Zeus-Dr/POSTPROCESSING.git)

#### 2. Download .nupkg file

- Locate the release version containing the NuGet package you want to install.
- Download the associated .nupkg file.

#### 3. Place .nupkg file in Project Directory

- Move the downloaded .nupkg file to your project directory.

#### 4. Open Terminal in Visual Studio Code

- Open Terminal in the project directory

#### 5. Install Package using .NET CLI

- Use the following command to install the NuGet package from the local .nupkg file:
  ```shell
  dotnet add package ZEUS --source .

## Example Usage
```csharp
using ZEUS;

class Program
{
    static void Main()
    {
        ColorCorrection colorcorrection = new ColorCorrection();
        colorcorrection.HistogramEqualization();
    }
}
```



## Documentation

For detailed documentation and examples, visit the documentation file.

## Support
If you encounter any issues or have questions, feel free to open an issue.
[Create a new issue](https://github.com/Zeus-Dr/POSTPROCESSING.git)



