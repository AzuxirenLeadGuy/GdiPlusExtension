# Dithering

Dithering is used to create an illusion of color depth of images with a limited color palette. Colors not available in the palette are approximated by a diffusion of colored pixels from the limited color palette.

There are 4 main dithering categories.

- **Thresholding** (also average dithering): each pixel value is compared against a fixed threshold. This may be the simplest dithering algorithm there is, but it results in immense loss of detail and contouring.
- **Random dithering** was the first attempt (at least as early as 1951) to remedy the drawbacks of thresholding. Each pixel value is compared against a random threshold, resulting in a staticky image. Although this method doesn't generate patterned artifacts, the noise tends to swamp the detail of the image.
- **Patterning dithers** using a fixed pattern. For each of the input values a fixed pattern is placed in the output image. The biggest disadvantage of this technique is that the output image is larger (by a factor of the fixed pattern size) than the input pattern.
- **Ordered dithering** dithers using a "dither matrix". For every pixel in the image the value of the pattern at the corresponding location is used as a threshold. Neighboring pixels do not affect each other, making this form of dithering suitable for use in animations. Different patterns can generate completely different dithering effects. Though simple to implement, this dithering algorithm is not easily changed to work with free-form, arbitrary palettes.

Source: [Wikipedia](https://en.wikipedia.org/wiki/Dither)

