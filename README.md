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
- Dithering
	- Thresholding
	- Random Dithering
	- Patterning Dither or Error-Diffusion Dither
	- Ordered Dithering

You can view more details on the individual features in the respective README files of each folder in the source code.

You can check out the wiki as well

## Example code

The following example shows how to apply a filter using the extension methods `Filter()`, provided in this library

```cs
using Bitmap image = ...//Open a bitmap image

image.Filter(ConvolutionFilter.GaussianFilter); //Filter has been applied!
```

All features in the library including examples are covered in the Wiki as well. You can also check out the `Example` project to see the code for all the functions being applied to a single image.

## Adding this library to your code

You can add the [Nuget package](https://www.nuget.org/packages/Azuxiren.GdiPlusExtensions/) for this repository. You can also clone this repository add the `GdiPlusExtensions` project as a reference in your project.


## Contributing

Check out the [Contributing.md](./CONTRIBUTING.md) file detailed information.

## Have a question? or want to have a general discussion?

Head to the Discussions Tab. You can keep it informal. I probably might respond before the end of the world.

## License

This repository is licensed under the [MIT License](./LICENSE)