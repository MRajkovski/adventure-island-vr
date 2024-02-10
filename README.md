# Adventure Island VR

## A starter project for an adventure game in VR

### Instructions

```

  Can be played with most VR Headsets with controller support, though PCVR is advised until performance update.

  There is a graphics button in the main menu options which can disable the more impactful details,
  currently only disables fog and post processing.

  The game uses default XRI Inputs from OpenXR and XR Interaction Toolkit.

  Special input features included are:

  -Rope swings at designated areas - once a dot appears on a swingable surface,
  press the Right or Left trigger to attach the rope,
  then the right secondary button to pull yourself up the rope.

  -Player can jump using the right primary button, but must be grounded
  -- *Some areas of the terrain are buggy and might not allow smooth and instant jumps* --

  -A small hand menu appears when you face your left hand upwards. Within the menu,
   there is a demo spawn button which enables some interactables at the beach spawn.

  -Can climb designated surfaces like the wall on the beach after enabling the demo.
  No buttons necessary, simply place your hand on the climbing spots and push yourself up.

```

### Required packages

```

  Terrain and grass: https://assetstore.unity.com/packages/3d/environments/landscapes/terrain-sample-asset-pack-145808
  -- *In the package manager, download the terrain samples pack too* --

  Current water shader: https://assetstore.unity.com/packages/2d/textures-materials/water/simple-water-shader-urp-191449

  Current trees for terrain: https://assetstore.unity.com/packages/3d/vegetation/trees/conifers-botd-142076
  -- *Once you add this to the project, there is another .unitypackage for URP you need to add. It's within the imported folder* --

  Current bridge assets: https://assetstore.unity.com/packages/3d/props/exterior/modular-wooden-bridge-tiles-29501

  Current rocks assets: https://assetstore.unity.com/packages/3d/props/exterior/rock-and-boulders-2-6947

```

**Any and all materials imported should be converted to URP. You can do so by using the *Search by type* button inside of Unity's Project window,
  then selecting the materials from each imported folder and going Edit > Rendering > Materials > Convert Selected Built-In Materials to URP**
  
### Example videos and demo builds *Work In Progress*

Adventure island demo [video](https://youtu.be/z_0cTuw8fi4)

PCVR [demo]()
Android [demo]()
