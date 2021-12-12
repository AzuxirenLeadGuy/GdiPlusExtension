# GdiPlusExtensions

This library intends to provide awesome helper functions, all supporting the `Bitmap` class from `System.Drawing.Common`, provided in highly clean and sufficiently documented code.

## Functionalities in the repository

The repository contains the following:

- Filters
	- Convolution based Filters
		- Box Blur filter
		- Gaussian filter
	- Median Filter
	- Bilateral Filter
	- ColorTint Filter
- Dithering
	- Thresholding
	- Random Dithering
	- Patterning Dither or Error-Diffusion Dither
	- Ordered Dithering
- Color Scaling

You can view more details on the individual features in the respective README files of each folder in the source code.

You can check out the [wiki](https://github.com/AzuxirenLeadGuy/GdiPlusExtension/wiki) as well

## Example code

The following example shows how to apply a filter using the extension method `Filter()`, provided in this library

Given an image 

![](https://raw.githubusercontent.com/wiki/AzuxirenLeadGuy/GdiPlusExtension/Images/x.jpg)

we can apply filter as shown

```cs
using Bitmap image = ...//Open a bitmap image

image.Filter(ConvolutionFilter.GaussianFilter); //Filter has been applied!
```

to get

![](https://raw.githubusercontent.com/wiki/AzuxirenLeadGuy/GdiPlusExtension/Images/FilteredImages/x-Gaussian_Blur_Filter.png)

All features in the library including examples are covered in the [Wiki](https://github.com/AzuxirenLeadGuy/GdiPlusExtension/wiki) as well. You can also check out the `Example` project to see the code for all the functions being applied to a single image.

## Adding this library to your code

You can add the [Nuget package](https://www.nuget.org/packages/Azuxiren.GdiPlusExtensions/) for this repository. You can also clone this repository and add the `GdiPlusExtensions` project as a reference in your project.


## Contributing

Check out the [Contributing.md](./CONTRIBUTING.md) detailed information on contribtion to this repository.

## Have a question? or want to have a general discussion?

Head to the Discussions Tab. You can keep it informal. I probably might respond.

## Notice any bugs/Issues in the code? Or do you have any suggestions to improve?

You can create an issue for it.

## License

This repository is licensed under the [MIT License](./LICENSE)

## Release notes

- v 0.1
	- Initial release
