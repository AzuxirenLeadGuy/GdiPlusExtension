# Filters

Filtering is a technique for modifying or enhancing an image. Filtering is a neighborhood operation, in which the value of any given pixel in the output image is determined by applying some algorithm to the values of the pixels in the neighborhood of the corresponding input pixel. It means that for each pixel location (x,y) in the source image (normally, rectangular), its neighborhood is considered and used to compute the response. In case of a linear filter, it is a weighted sum of pixel values. In case of morphological operations, it is the minimum or maximum values, and so on. The computed response is stored in the destination image at the same location (x,y). It means that the output image will be of the same size as the input image. 

The following filters are included in this repository

- **Convolution based**: Convolution is the process of adding each element of the image to its local neighbors, weighted by the kernel.
	- **Gaussian Filter**: Blurs an image using a Gaussian filter. The function convolves the source image with the specified Gaussian kernel. 
	- **Box Blur Filter**: It simply takes the average of all the pixels under the kernel area and replaces the central element. 
- **Bilateral Filter**: is highly effective in noise removal while keeping edges sharp. But the operation is slower compared to other filters. We already saw that a Gaussian filter takes the neighbourhood around the pixel and finds its Gaussian weighted average. This Gaussian filter is a function of space alone, that is, nearby pixels are considered while filtering. It doesn't consider whether pixels have almost the same intensity. It doesn't consider whether a pixel is an edge pixel or not. So it blurs the edges also, which we don't want to do. Bilateral filtering also takes a Gaussian filter in space, but one more Gaussian filter which is a function of pixel difference. The Gaussian function of space makes sure that only nearby pixels are considered for blurring, while the Gaussian function of intensity difference makes sure that only those pixels with similar intensities to the central pixel are considered for blurring. So it preserves the edges since pixels at edges will have large intensity variation.
- **Median Filter**: takes the median of all the pixels under the kernel area and the central element is replaced with this median value. This is highly effective against salt-and-pepper noise in an image. Interestingly, in the above filters, the central element is a newly calculated value which may be a pixel value in the image or a new value. But in median blurring, the central element is always replaced by some pixel value in the image. It reduces the noise effectively. Its kernel size should be a positive odd integer.
- **ColorTint Filter**: Applies a linear interpolation for every pixel and brings it closer to the color provided in the parameter, giving it a tint effect. 


Source: OpenCV docs
