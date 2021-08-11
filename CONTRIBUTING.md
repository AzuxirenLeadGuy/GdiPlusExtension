# Contributing to GdiPlusExtensions

First of all, thank you for your interest in contributing to this repository. 

It is requested to adhere to the programming style as given below.

## Programming Style

- **There should not be any newly defined public Type for representing an image**: all extension/helper methods should work on the `Bitmap`  class defined in `System.Drawing.Common`. Any new Type whose purpose is to describe an image (meant to be used as an alternative to `Bitmap`) is not allowed. Such Types can be used internally (and thus, are allowed to be `internal`)
- **The rulesets dictated by the .editorconfig file must be followed.**: Be sure to use extensions in your IDE to enable the rulesets defined in the given `.editorconfig` file.
- **All added features should be explained**: This can be done by including a proper documentation within the code, adding a markdown file within the folder/namespace for explaination or simply including an appropriate link. 
- **All available features must be showcased as a code in the "Examples" project**: The Example project takes a single image path from the CLI, and outputs the image in its appropriate folder with every extension/helper method of the code. The program is distributed in regions according to how the main project is organized in namespaces. This style should be kept consistent.
- **All added features must be mentioned in the README.md at root of the repository**
- **Do not include images within the repository unless necessary**: All images to showcase/demonstrate the features of code should be displayed in the Wiki, and not in the repository itself. This git repository is solely for code.

## What can I contribute

- Star this repository.
- This library doesn't have many features, so feel free to enhance this library.
- Check for any open issue to work on.
- If any bugs/issues exist, or there are some features you want to have added to this library, feel free to open an issue, 
- Improvements on the Wiki.
- Any grammatical/spelling mistakes. ¯\\_(ツ)_/¯

# Opening an issue:

- Kindly do not open an issue to ask a question. Use the Discussion board for that. 
- Please be clear and polite while describing the bug.
- In case the bug is related to code not drawing/modifying an image as expected, please include a sample image in the messages as well.

---
