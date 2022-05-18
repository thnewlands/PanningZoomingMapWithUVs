# Panning & Zooming Map With UVs

This repo is a companion to this Unity Issue where I describe the problem in more detail: https://answers.unity.com/questions/1902756/how-would-you-go-about-panning-and-zooming-using-t.html

Currently the Rect approach works! It currently only clamps the scale. If you have a solution for clamping the position using this technique please send a pull request.

The shader approach still doesn't entirely. It's a good study in scaling from a single position in a shader though. The issue with the shader approach is that the mouse position isn't consistent when zoomed in.

You can replicate the approaches in the thread by opening either the RectApproach scene or ShaderApproach scene in their respective folders.

I made this repository to make it easier for people to help out with this problem. Feel free to use any code you find in here for other projects but don't hold me liable! Please add pull requests with comments that make this clearer for other people or add features to the approach.
