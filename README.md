# About
This is a small repository for sharing some helpful Unity scripts that people might like to make use of and adapt. I cannot offer support for any of these, but you're welcome to make pull requests to add your own contributions! I welcome improvements and feedback. :) 

## BuildVRMBlendShapeClips.cs
This script is meant to help automate the tedious process of adding custom and ARKit blendshape values to VRM blendshape clips. I built this for my husband and I and thought other people might be able to make use of it, so I'm sharing it here. I recommend you have at least a passing familiarity with Unity, C#, and the VRM export process before using this, or have a friend that can help walk you through it. This does NOT create a fully usable VRM - it's only meant to save some time building the blendshape clips as part of the VRM setup/build process.

### Inputs / Actions
This script takes the following inputs:
1. A source VRC avatar (specifically a post-build VRCFury Editor Test Copy - this takes care of all the build steps to give you a complete avatar for export to VRM)
2. An exported VRM version of the source VRC avatar
3. The BlendShapeProxy file of the VRM
4. A chosen blendshape preset

When run, the script will:
1. Loop through all skinned mesh renderers and their blendshapes on the chosen VRM
2. Locate matching skinned mesh renderers and their blendshapes on the chosen VRC avatar
3. Copy all matching blendshape values to the `Neutral` BlendShapeClip of the VRM
4. Add predefined viseme values to the default VRM viseme BlendShapeClips
5. Create ARKit BlendShapeClips and copy predefined ARKit blendshape values to them

### Requirements
1. A Unity project created with VRChat Creator Companion.
2. UniVRM (installed in the Unity project through your preferred method).
 
### Usage
1. Add the BuildVRMBlendshapeClips.cs file somewhere in your VRChat Unity project.
2. Check the console to ensure it compliled correctly and did not produce errors.
3. Navigate to Tools > Azimuth > Build VRM Blend Shape Clips and follow the instructions in the dialog.

### Gotcha's 
- If your avatar is not present in the blendshape preset list, a new map for viseme and ARKit blendshape names/values can be added in the script itself.
    - If you do this, please submit a pull request so we can add more to the base script here for others to use!
- Many avatar creators are now including the Blend Shape Optimizer VRCFury component by default. This must be disabled/removed before building your editor test copy for the script input, otherwise your viseme/ARKit blendshapes may be purged, and none of the ARKit blendshapes will be populated since they won't exist on the skinned mesh renderers of the VRM.
