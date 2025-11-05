#if UNITY_EDITOR
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UniGLTF;
using UnityEditor;
using UnityEngine;
using VRM;
using System.Linq;

[System.Serializable]
public class VTuberBlendShapeMapValue
{
    public string ShapeName;
    public float ShapeValue;

    public VTuberBlendShapeMapValue(string shapeName, float shapeValue)
    {
        ShapeName = shapeName;
        ShapeValue = shapeValue;
    }
}

[System.Serializable]
public class VTuberBlendShapeMap
{
    public Dictionary<BlendShapePreset, List<VTuberBlendShapeMapValue>> Visemes = new();
    public Dictionary<string, List<VTuberBlendShapeMapValue>> ARKit = new();
}

[System.Serializable]
public class VTuberBlendShapePreset
{
    public string Name;
    public VTuberBlendShapeMap Map = new();
}

public class AMBuildVRMBlendShapeClips : EditorWindow
{
    private readonly List<VTuberBlendShapePreset> Presets = new()
    {
        new VTuberBlendShapePreset()
        {
            Name = "Winterpaw Canine 1.5.2.1",
            Map = new VTuberBlendShapeMap() 
            {
                Visemes = new() 
                { 
                    { BlendShapePreset.Neutral, null },
                    { BlendShapePreset.A, new List<VTuberBlendShapeMapValue> { new ("vrc.v_aa", 100.0f) } },
                    { BlendShapePreset.I, new List<VTuberBlendShapeMapValue> { new ("vrc.v_ih", 100.0f) } },
                    { BlendShapePreset.U, new List<VTuberBlendShapeMapValue> { new ("vrc.v_ou", 100.0f) } },
                    { BlendShapePreset.E, new List<VTuberBlendShapeMapValue> { new ("vrc.v_e", 100.0f) } },
                    { BlendShapePreset.O, new List<VTuberBlendShapeMapValue> { new ("vrc.v_oh", 100.0f) } },
                    { BlendShapePreset.Blink, new List<VTuberBlendShapeMapValue> { new ("Blink", 100.0f) } },
                    { BlendShapePreset.Blink_L, new List<VTuberBlendShapeMapValue> { new ("Blink L", 100.0f) } },
                    { BlendShapePreset.Blink_R, new List<VTuberBlendShapeMapValue> { new ("Blink R", 100.0f) } },
                    { BlendShapePreset.Joy, new List<VTuberBlendShapeMapValue> { new ("Closed Smile Eyes", 100.0f), new ("Mouth Smile", 100.0f) } },
                    { BlendShapePreset.Angry, new List<VTuberBlendShapeMapValue> { new ("Angry Eyes", 100.0f), new ("Sad-Angry Mouth", 100.0f) } },
                    { BlendShapePreset.Sorrow, new List<VTuberBlendShapeMapValue> { new ("Sad Eyes", 100.0f), new ("Sad-Angry Mouth", 100.0f) } },
                    { BlendShapePreset.Fun, new List<VTuberBlendShapeMapValue> { new ("Smile Eyes", 100.0f), new ("Mouth Smile", 100.0f) } },
                    { BlendShapePreset.LookUp, new List<VTuberBlendShapeMapValue> { new ("VRM.LookUp", 100.0f) } },
                    { BlendShapePreset.LookDown, new List<VTuberBlendShapeMapValue> { new ("VRM.LookDown", 100.0f) } },
                    { BlendShapePreset.LookLeft, new List<VTuberBlendShapeMapValue> { new ("VRM.LookLeft", 100.0f) } },
                    { BlendShapePreset.LookRight, new List<VTuberBlendShapeMapValue> { new ("VRM.LookRight", 100.0f) } }
                },
                ARKit = new()
                {
                    { "browDownLeft", new List<VTuberBlendShapeMapValue> { new ("browDownLeft", 100.0f) } },
                    { "browDownRight", new List<VTuberBlendShapeMapValue> { new ("browDownRight", 100.0f) } },
                    { "browInnerUp", new List<VTuberBlendShapeMapValue> { new ("browInnerUp", 100.0f) } },
                    { "browOuterUpLeft", new List<VTuberBlendShapeMapValue> { new ("browOutterUpLeft", 100.0f) } },
                    { "browOuterUpRight", new List<VTuberBlendShapeMapValue> { new ("browOutterUpRight", 100.0f) } },
                    { "cheekPuff", new List<VTuberBlendShapeMapValue> { new ("cheekPuff", 100.0f) } },
                    { "cheekSquintLeft", new List<VTuberBlendShapeMapValue> { new ("cheekSquintLeft", 100.0f) } },
                    { "cheekSquintRight", new List<VTuberBlendShapeMapValue> { new ("cheekSquintRight", 100.0f) } },
                    { "eyeBlinkLeft", new List<VTuberBlendShapeMapValue> { new ("eyeBlinkLeft", 100.0f) } },
                    { "eyeBlinkRight", new List<VTuberBlendShapeMapValue> { new ("eyeBlinkRight", 100.0f) } },
                    { "eyeLookDownLeft", new List<VTuberBlendShapeMapValue> { new ("eyeLookDownLeft", 100.0f) } },
                    { "eyeLookDownRight", new List<VTuberBlendShapeMapValue> { new ("eyeLookDownRight", 100.0f) } },
                    { "eyeLookInLeft", new List<VTuberBlendShapeMapValue> { new ("eyeLookInLeft", 100.0f) } },
                    { "eyeLookInRight", new List<VTuberBlendShapeMapValue> { new ("eyeLookInRight", 100.0f) } },
                    { "eyeLookOutLeft", new List<VTuberBlendShapeMapValue> { new ("eyeLookOutLeft", 100.0f) } },
                    { "eyeLookOutRight", new List<VTuberBlendShapeMapValue> { new ("eyeLookOutRight", 100.0f) } },
                    { "eyeLookUpLeft", new List<VTuberBlendShapeMapValue> { new ("eyeLookUpLeft", 100.0f) } },
                    { "eyeLookUpRight", new List<VTuberBlendShapeMapValue> { new ("eyeLookUpRight", 100.0f) } },
                    { "eyeSquintLeft", new List<VTuberBlendShapeMapValue> { new ("eyeSquintLeft", 100.0f) } },
                    { "eyeSquintRight", new List<VTuberBlendShapeMapValue> { new ("eyeSquintRight", 100.0f) } },
                    { "eyeWideLeft", new List<VTuberBlendShapeMapValue> { new ("eyeWideLeft", 100.0f) } },
                    { "eyeWideRight", new List<VTuberBlendShapeMapValue> { new ("eyeWideRight", 100.0f) } },
                    { "jawForward", new List<VTuberBlendShapeMapValue> { new ("jawForward", 100.0f) } },
                    { "jawLeft", new List<VTuberBlendShapeMapValue> { new ("jawLeft", 100.0f) } },
                    { "jawOpen", new List<VTuberBlendShapeMapValue> { new ("jawOpen", 100.0f) } },
                    { "jawRight", new List<VTuberBlendShapeMapValue> { new ("jawRight", 100.0f) } },
                    { "mouthClose", new List<VTuberBlendShapeMapValue> { new ("mouthClose", 100.0f) } },
                    { "mouthFrownLeft", new List<VTuberBlendShapeMapValue> { new ("mouthFrownLeft", 100.0f) } },
                    { "mouthFrownRight", new List<VTuberBlendShapeMapValue> { new ("mouthFrownRight", 100.0f) } },
                    { "mouthFunnel", new List<VTuberBlendShapeMapValue> { new ("mouthFunnel", 100.0f) } },
                    { "mouthLeft", new List<VTuberBlendShapeMapValue> { new ("mouthLeft", 100.0f) } },
                    { "mouthLowerDownLeft", new List<VTuberBlendShapeMapValue> { new ("mouthLowerDownLeft", 100.0f) } },
                    { "mouthLowerDownRight", new List<VTuberBlendShapeMapValue> { new ("mouthLowerDownRight", 100.0f) } },
                    { "mouthPressLeft", new List<VTuberBlendShapeMapValue> { new ("mouthPressLeft", 100.0f) } },
                    { "mouthPressRight", new List<VTuberBlendShapeMapValue> { new ("mouthPressRight", 100.0f) } },
                    { "mouthPucker", new List<VTuberBlendShapeMapValue> { new ("mouthPucker", 100.0f) } },
                    { "mouthRight", new List<VTuberBlendShapeMapValue> { new ("mouthRight", 100.0f) } },
                    { "mouthRollLower", new List<VTuberBlendShapeMapValue> { new ("mouthRollLower", 100.0f) } },
                    { "mouthRollUpper", new List<VTuberBlendShapeMapValue> { new ("mouthRollUpper", 100.0f) } },
                    { "mouthShrugLower", new List<VTuberBlendShapeMapValue> { new ("mouthShrugLower", 100.0f) } },
                    { "mouthShrugUpper", new List<VTuberBlendShapeMapValue> { new ("mouthShrugUpper", 100.0f) } },
                    { "mouthSmileLeft", new List<VTuberBlendShapeMapValue> { new ("mouthSmileLeft", 100.0f) } },
                    { "mouthSmileRight", new List<VTuberBlendShapeMapValue> { new ("mouthSmileRight", 100.0f) } },
                    { "mouthUpperUpLeft", new List<VTuberBlendShapeMapValue> { new ("mouthUpperUpLeft", 100.0f) } },
                    { "mouthUpperUpRight", new List<VTuberBlendShapeMapValue> { new ("mouthUpperUpRight", 100.0f) } },
                    { "noseSneerLeft", new List<VTuberBlendShapeMapValue> { new ("noseSneerLeft", 100.0f) } },
                    { "noseSneerRight", new List<VTuberBlendShapeMapValue> { new ("noseSneerRight", 100.0f) } },
                    { "tongueOut", new List<VTuberBlendShapeMapValue> { new ("tongueOut", 100.0f) } }
                }
            }
        },
        new VTuberBlendShapePreset()
        {
            Name = "RustWhisker 1.2",
            Map = new VTuberBlendShapeMap
            {
                Visemes = new()
                {
                    { BlendShapePreset.Neutral, null },
                    { BlendShapePreset.A, new List<VTuberBlendShapeMapValue> { new ("AA", 100.0f) } },
                    { BlendShapePreset.I, new List<VTuberBlendShapeMapValue> { new ("IH", 100.0f) } },
                    { BlendShapePreset.U, new List<VTuberBlendShapeMapValue> { new ("OU", 100.0f) } },
                    { BlendShapePreset.E, new List<VTuberBlendShapeMapValue> { new ("EE", 100.0f) } },
                    { BlendShapePreset.O, new List<VTuberBlendShapeMapValue> { new ("OH", 100.0f) } },
                    { BlendShapePreset.Blink, new List<VTuberBlendShapeMapValue> { new ("EYELIDS Closed", 100.0f) } },
                    { BlendShapePreset.Blink_L, new List<VTuberBlendShapeMapValue> { new ("EYELIDS Closed_L", 100.0f) } },
                    { BlendShapePreset.Blink_R, new List<VTuberBlendShapeMapValue> { new ("EYELIDS Closed_R", 100.0f) } },
                    { BlendShapePreset.Joy, new List<VTuberBlendShapeMapValue> { new ("MOUTH Grin", 100.0f) } },
                    { BlendShapePreset.Angry, new List<VTuberBlendShapeMapValue> { new ("EYEBROWS Angry", 100.0f), new("MOUTH Show Teeth", 100.0f) } },
                    { BlendShapePreset.Sorrow, new List<VTuberBlendShapeMapValue> { new ("EYEBROWS Sad", 100.0f), new("MOUTH Frown", 100.0f) } },
                    { BlendShapePreset.Fun, new List<VTuberBlendShapeMapValue> { new ("MOUTH Smile", 100.0f) } }, // Not sure this is enough, may adjust later
                    { BlendShapePreset.LookUp, new List<VTuberBlendShapeMapValue> { new ("EyesUp", 100.0f) } },
                    { BlendShapePreset.LookDown, new List<VTuberBlendShapeMapValue> { new ("EyesDown", 100.0f) } },
                    { BlendShapePreset.LookLeft, new List<VTuberBlendShapeMapValue> { new ("EyesLeft", 100.0f) } },
                    { BlendShapePreset.LookRight, new List<VTuberBlendShapeMapValue> { new ("EyesRight", 100.0f) } }
                },
                ARKit = new()
                {
                    { "browDownLeft", new List<VTuberBlendShapeMapValue> { new ("browDownLeft", 100.0f) } },
                    { "browDownRight", new List<VTuberBlendShapeMapValue> { new ("browDownRight", 100.0f) } },
                    { "browInnerUp", new List<VTuberBlendShapeMapValue> { new ("browInnerUp", 100.0f) } },
                    { "browOuterUpLeft", new List<VTuberBlendShapeMapValue> { new ("browOutterUpLeft", 100.0f) } },
                    { "browOuterUpRight", new List<VTuberBlendShapeMapValue> { new ("browOutterUpRight", 100.0f) } },
                    { "cheekPuff", new List<VTuberBlendShapeMapValue> { new ("cheekPuff", 100.0f) } },
                    { "cheekSquintLeft", new List<VTuberBlendShapeMapValue> { new ("cheekSquintLeft", 100.0f) } },
                    { "cheekSquintRight", new List<VTuberBlendShapeMapValue> { new ("cheekSquintRight", 100.0f) } },
                    { "eyeBlinkLeft", new List<VTuberBlendShapeMapValue> { new ("eyeBlinkLeft", 100.0f) } },
                    { "eyeBlinkRight", new List<VTuberBlendShapeMapValue> { new ("eyeBlinkRight", 100.0f) } },
                    { "eyeLookDownLeft", new List<VTuberBlendShapeMapValue> { new ("eyeLookDownLeft", 100.0f) } },
                    { "eyeLookDownRight", new List<VTuberBlendShapeMapValue> { new ("eyeLookDownRight", 100.0f) } },
                    { "eyeLookInLeft", new List<VTuberBlendShapeMapValue> { new ("eyeLookInLeft", 100.0f) } },
                    { "eyeLookInRight", new List<VTuberBlendShapeMapValue> { new ("eyeLookInRight", 100.0f) } },
                    { "eyeLookOutLeft", new List<VTuberBlendShapeMapValue> { new ("eyeLookOutLeft", 100.0f) } },
                    { "eyeLookOutRight", new List<VTuberBlendShapeMapValue> { new ("eyeLookOutRight", 100.0f) } },
                    { "eyeLookUpLeft", new List<VTuberBlendShapeMapValue> { new ("eyeLookUpLeft", 100.0f) } },
                    { "eyeLookUpRight", new List<VTuberBlendShapeMapValue> { new ("eyeLookUpRight", 100.0f) } },
                    { "eyeSquintLeft", new List<VTuberBlendShapeMapValue> { new ("eyeSquintLeft", 100.0f) } },
                    { "eyeSquintRight", new List<VTuberBlendShapeMapValue> { new ("eyeSquintRight", 100.0f) } },
                    { "eyeWideLeft", new List<VTuberBlendShapeMapValue> { new ("eyeWideLeft", 100.0f) } },
                    { "eyeWideRight", new List<VTuberBlendShapeMapValue> { new ("eyeWideRight", 100.0f) } },
                    { "jawForward", new List<VTuberBlendShapeMapValue> { new ("jawForward", 100.0f) } },
                    { "jawLeft", new List<VTuberBlendShapeMapValue> { new ("jawLeft", 100.0f) } },
                    { "jawOpen", new List<VTuberBlendShapeMapValue> { new ("jawOpen", 100.0f) } },
                    { "jawRight", new List<VTuberBlendShapeMapValue> { new ("jawRight", 100.0f) } },
                    { "mouthClose", new List<VTuberBlendShapeMapValue> { new ("mouthClose", 100.0f) } },
                    { "mouthFrownLeft", new List<VTuberBlendShapeMapValue> { new ("mouthFrownLeft", 100.0f) } },
                    { "mouthFrownRight", new List<VTuberBlendShapeMapValue> { new ("mouthFrownRight", 100.0f) } },
                    { "mouthFunnel", new List<VTuberBlendShapeMapValue> { new ("mouthFunnel", 100.0f) } },
                    { "mouthLeft", new List<VTuberBlendShapeMapValue> { new ("mouthLeft", 100.0f) } },
                    { "mouthLowerDownLeft", new List<VTuberBlendShapeMapValue> { new ("mouthLowerDownLeft", 100.0f) } },
                    { "mouthLowerDownRight", new List<VTuberBlendShapeMapValue> { new ("mouthLowerDownRight", 100.0f) } },
                    { "mouthPressLeft", new List<VTuberBlendShapeMapValue> { new ("mouthPressLeft", 100.0f) } },
                    { "mouthPressRight", new List<VTuberBlendShapeMapValue> { new ("mouthPressRight", 100.0f) } },
                    { "mouthPucker", new List<VTuberBlendShapeMapValue> { new ("mouthPucker", 100.0f) } },
                    { "mouthRight", new List<VTuberBlendShapeMapValue> { new ("mouthRight", 100.0f) } },
                    { "mouthRollLower", new List<VTuberBlendShapeMapValue> { new ("mouthRollLower", 100.0f) } },
                    { "mouthRollUpper", new List<VTuberBlendShapeMapValue> { new ("mouthRollUpper", 100.0f) } },
                    { "mouthShrugLower", new List<VTuberBlendShapeMapValue> { new ("mouthShrugLower", 100.0f) } },
                    { "mouthShrugUpper", new List<VTuberBlendShapeMapValue> { new ("mouthShrugUpper", 100.0f) } },
                    { "mouthSmileLeft", new List<VTuberBlendShapeMapValue> { new ("mouthSmileLeft", 100.0f) } },
                    { "mouthSmileRight", new List<VTuberBlendShapeMapValue> { new ("mouthSmileRight", 100.0f) } },
                    { "mouthUpperUpLeft", new List<VTuberBlendShapeMapValue> { new ("mouthUpperUpLeft", 100.0f) } },
                    { "mouthUpperUpRight", new List<VTuberBlendShapeMapValue> { new ("mouthUpperUpRight", 100.0f) } },
                    { "noseSneerLeft", new List<VTuberBlendShapeMapValue> { new ("noseSneerLeft", 100.0f) } },
                    { "noseSneerRight", new List<VTuberBlendShapeMapValue> { new ("noseSneerRight", 100.0f) } },
                    { "tongueOut", new List<VTuberBlendShapeMapValue> { new ("tongueOut", 100.0f) } }
                }
            }
        }
    };

    // Working dictionaries to select presets.
    private VTuberBlendShapePreset activePreset;
    private string[] presetNames;
    private int selectedPresetIndex = 0;

    // Other working data
    GameObject sourceAvatarVRC = null;
    GameObject sourceAvatarVRM = null;
    BlendShapeAvatar targetBlendShapeAvatar = null;
    bool clearBlendShapeAvatarClips = false;
    bool applyVisemes = true;
    bool applyARKit = true;
    bool applyVRCBlendShapes = true;
    bool applyVRCBlendShapesNeutralOnly = true;
    bool logVisemes = true;
    bool logARKit = true;
    bool logVRCBlendShapes = true;

    void OnEnable()
    {
        activePreset = Presets[0];
        presetNames = Presets.Select(p => p.Name).ToArray();
    }

    [MenuItem("Tools/Azimuth/Build VRM Blend Shape Clips...")]
    public static void ShowWindow()
    {
        GetWindow<AMBuildVRMBlendShapeClips>("VRM Clip Builder");
    }

    void OnGUI()
    {
        GUIStyle labelStyle = GUI.skin.label;

        labelStyle.fontStyle = FontStyle.Bold;

        GUILayout.Label("NOTE: BACK UP YOUR PROJECT!! I make no promises about this functioning properly or as expected, so ALWAYS back up your project first! I am not responsible for your project blowing up, even if this script/tool is used exactly as directed/expected. The steps below may also not be 100% complete. If there are issues I will try updating them over time, but no promises.", labelStyle);
        
        GUILayout.Space(10);

        labelStyle.fontStyle = FontStyle.Normal;

        GUILayout.Label("The process for this is as follows:");
        GUILayout.Label("    1. Use VRCFury to build a test copy of your avatar and rename it in the scene (you'll need this later).");
        GUILayout.Label("    2. Duplicate the test copy in the scene, and add a 'Z' to the end of the name.");
        GUILayout.Label("    3. Select the 'Z' test copy in the scene, and click the 'Zero Blendshapes' button below.");
        GUILayout.Label("    4. Create a new empty folder named '_VRMExport' in your Unity project.");
        GUILayout.Label("    5. Export the 'Z' test copy as a VRM, giving it a unique name and saving it to the '_VRMExport' folder you created.");
        GUILayout.Label("    6. Drag the exported VRM in the '_VRMExport' folder back into the scene.");
        GUILayout.Label("    7. Select your blendshape preset below.");
        GUILayout.Label("    8. Drag your original VRCFury test copy into the 'Source VRC Avatar' slot below.");
        GUILayout.Label("    9. Drag the exported VRM you added to the scene into the 'Source VRM Avatar' slot below.");
        GUILayout.Label("   10. Navigate to '_VRMExport\\<name>.BlendShapes' folder and drag the 'BlendShape' file into the 'Blend Shape Avatar' slot below.");
        GUILayout.Label("   11. Configure all the checkboxes below.");
        GUILayout.Label("   12. Click 'Apply'.");

        GUILayout.Space(10);
        
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        if (GUILayout.Button("Zero Blendshapes of Selected Object"))
        {
            ZeroSelectedObjectBlendshapes();
        }

        GUILayout.Space(10);

        GUILayout.Label("Blendshape names are avatar-specific; choose yours. If yours is not in this list then someone needs to add another map to the script.");
        int newIndex = EditorGUILayout.Popup("Blendshape Preset", selectedPresetIndex, presetNames);
        if (newIndex != selectedPresetIndex)
        {
            selectedPresetIndex = newIndex;
            activePreset = Presets[selectedPresetIndex];
            Debug.Log($"Swapped active blendshape map to '{activePreset.Name}'");
        }

        GUILayout.Space(10);

        GUILayout.Label("Source VRC Avatar (post-VRCFury build, non-zeroed blendshapes)");
        sourceAvatarVRC = (GameObject) EditorGUILayout.ObjectField(sourceAvatarVRC, typeof(GameObject), true); // Source of blendshape data to apply to the VRM.

        GUILayout.Label("Source VRM Avatar (zeroed blendshapes)");
        sourceAvatarVRM = (GameObject) EditorGUILayout.ObjectField(sourceAvatarVRM, typeof(GameObject), true); // Source of blendshape names that can be applied to the VRM blendshape clips. 

        GUILayout.Label("Blend Shape Avatar (in the '<name>.BlendShapes' folder of the .VRM after being exported into the Unity project)");
        targetBlendShapeAvatar = (BlendShapeAvatar) EditorGUILayout.ObjectField(targetBlendShapeAvatar, typeof(BlendShapeAvatar), false); // Target BlendShapeAvatar to apply blendshape clip changes to.

        GUILayout.Space(10);

        clearBlendShapeAvatarClips = EditorGUILayout.Toggle("Clear Existing Clips", clearBlendShapeAvatarClips);

        EditorGUILayout.BeginHorizontal();
        {
            applyVisemes = EditorGUILayout.Toggle("Apply Visemes", applyVisemes);
            logVisemes = EditorGUILayout.Toggle("Log Visemes", logVisemes);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            applyARKit = EditorGUILayout.Toggle("Apply ARKit", applyARKit);
            logARKit = EditorGUILayout.Toggle("Log ARKit", logARKit);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            applyVRCBlendShapes = EditorGUILayout.Toggle("Apply VRC BlendShapes", applyVRCBlendShapes);
            logVRCBlendShapes = EditorGUILayout.Toggle("Log VRC Blendshapes", logVRCBlendShapes);
        }
        EditorGUILayout.EndHorizontal();

        applyVRCBlendShapesNeutralOnly = EditorGUILayout.Toggle("Neutral Only", applyVRCBlendShapesNeutralOnly);

        if (GUILayout.Button("Apply"))
        {
            BuildBlendshapeClips();
        }
    }

    private void ZeroSelectedObjectBlendshapes()
    {
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject == null)
        {
            Debug.LogWarning("No object selected. Please select an object in the hierarchy.");
            return;
        }

        SkinnedMeshRenderer[] skinnedMeshRenderers = selectedObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        if (skinnedMeshRenderers.Length == 0)
        {
            Debug.LogWarning("No Skinned Mesh Renderers found in the selected object.");
            return;
        }

        foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
        {
            int blendShapeCount = skinnedMeshRenderer.sharedMesh.blendShapeCount;
            for (int i = 0; i < blendShapeCount; i++)
            {
                skinnedMeshRenderer.SetBlendShapeWeight(i, 0f);
            }
        }

        Debug.Log("Blendshapes zeroed for all Skinned Mesh Renderers in the selected object.");
    }

    private void BuildBlendshapeClips()
    {
        if (sourceAvatarVRC != null && sourceAvatarVRM != null && targetBlendShapeAvatar != null)
        {
            // Get skinned mesh renderers in sourceAvatarVRC.
            SkinnedMeshRenderer[] sourceRenderersVRC = sourceAvatarVRC.GetComponentsInChildren<SkinnedMeshRenderer>();

            // Get skinned mesh renderers in sourceAvatarVRM.
            SkinnedMeshRenderer[] sourceRenderersVRM = sourceAvatarVRM.GetComponentsInChildren<SkinnedMeshRenderer>();

            ///// PRE-WORK /////

            // If enabled, clear existing BlendShapeClips from the BlendShapeAvatar object.
            if (clearBlendShapeAvatarClips)
            {
                foreach (BlendShapeClip existingClip in targetBlendShapeAvatar.Clips)
                {
                    existingClip.Values = new BlendShapeBinding[] { };
                }
            }

            ///// VISEMES /////

            if (applyVisemes)
            {
                foreach (KeyValuePair<BlendShapePreset, List<VTuberBlendShapeMapValue>> viseme in activePreset.Map.Visemes)
                {
                    BlendShapeClip targetClip = null;

                    // Try to locate an existing clip for the viseme.
                    foreach (BlendShapeClip existingClip in targetBlendShapeAvatar.Clips)
                    {
                        if (existingClip.BlendShapeName == viseme.Key.ToString())
                        {
                            targetClip = existingClip;

                            if (logVisemes)
                            {
                                Debug.Log($"Visemes: Blend Shape Clip '{viseme.Key}': Found");
                            }
                        }
                    }

                    // If existing clip is not found, create a new one.
                    if (targetClip == null)
                    {
                        var targetBlendShapeAvatarPath = AssetDatabase.GetAssetPath(
                            targetBlendShapeAvatar
                        );
                        var targetClipPath = Path.Combine(Path.GetDirectoryName(targetBlendShapeAvatarPath), $"{viseme.Key}.asset");
                        targetClip = CreateInstance<BlendShapeClip>();
                        targetClip.BlendShapeName = viseme.Key.ToString();
                        targetClip.Preset = viseme.Key;

                        AssetDatabase.CreateAsset(targetClip, targetClipPath);
                        AssetDatabase.ImportAsset(targetClipPath);

                        targetBlendShapeAvatar.Clips.Add(targetClip);

                        if (logVisemes)
                        {
                            Debug.LogWarning($"Visemes: Blend Shape Clip '{viseme.Key}': Created at '{targetClipPath}'");
                        }
                    }

                    if (viseme.Value != null)
                    {
                        // Loop through the skinned mesh renderers in the sourceRenderersVRM and build BlendShipBindings for blendshapes matching our predefined settings data.
                        List<BlendShapeBinding> newBindings = new();

                        foreach (SkinnedMeshRenderer rendererVRM in sourceRenderersVRM)
                        {
                            if (rendererVRM.sharedMesh != null)
                            {
                                for (int i = 0; i < rendererVRM.sharedMesh.blendShapeCount; i++)
                                {
                                    string blendShapeName = rendererVRM.sharedMesh.GetBlendShapeName(i);

                                    foreach (VTuberBlendShapeMapValue visemeSetting in viseme.Value)
                                    {
                                        if (visemeSetting.ShapeName == blendShapeName)
                                        {
                                            string blendShapePath = BuildBlendShapeClipBlendShapePath(rendererVRM.transform);

                                            if (logVisemes)
                                            {
                                                Debug.Log($"Visemes: Blend Shape Clip '{viseme.Key}': Skinned Mesh Renderer '{rendererVRM.gameObject.name}': Found '{visemeSetting.ShapeName}' at '{blendShapePath}/{blendShapeName}'");
                                            }

                                            BlendShapeBinding newBinding = new()
                                            {
                                                Index = i,
                                                RelativePath = blendShapePath,
                                                Weight = visemeSetting.ShapeValue
                                            };

                                            newBindings.Add(newBinding);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (logVisemes)
                                {
                                    Debug.LogWarning($"Visemes: Blend Shape Clip '{viseme.Key}': Skinned Mesh Renderer '{rendererVRM.gameObject.name}': Does not appear to contain a mesh and is being skipped");
                                }
                            }
                        }

                        // Apply the new blendshape bindings to the clip.
                        targetClip.Values = newBindings.ToArray();
                    }
                }
            }

            ///// ARKIT /////

            if (applyARKit)
            {
                foreach (KeyValuePair<string, List<VTuberBlendShapeMapValue>> arkit in activePreset.Map.ARKit)
                {
                    BlendShapeClip targetClip = null;

                    // Try to locate an existing clip for the viseme.
                    foreach (BlendShapeClip existingClip in targetBlendShapeAvatar.Clips)
                    {
                        if (existingClip.BlendShapeName == arkit.Key.ToString())
                        {
                            targetClip = existingClip;

                            if (logARKit)
                            {
                                Debug.Log($"ARKit: Blend Shape Clip '{arkit.Key}': Found");
                            }
                        }
                    }

                    // If existing clip is not found, create a new one.
                    if (targetClip == null)
                    {
                        var targetBlendShapeAvatarPath = AssetDatabase.GetAssetPath(
                            targetBlendShapeAvatar
                        );
                        var targetClipPath = Path.Combine(
                            Path.GetDirectoryName(targetBlendShapeAvatarPath),
                            $"{arkit.Key}.asset"
                        );
                        targetClip = ScriptableObject.CreateInstance<BlendShapeClip>();
                        targetClip.BlendShapeName = arkit.Key.ToString();
                        targetClip.Preset = BlendShapePreset.Unknown;

                        AssetDatabase.CreateAsset(targetClip, targetClipPath);
                        AssetDatabase.ImportAsset(targetClipPath);

                        targetBlendShapeAvatar.Clips.Add(targetClip);

                        if (logARKit)
                        {
                            Debug.LogWarning($"ARKit: Blend Shape Clip '{arkit.Key}': Created at '{targetClipPath}'");
                        }
                    }

                    if (arkit.Value != null)
                    {
                        // Loop through the skinned mesh renderers in the sourceRenderersVRM and build BlendShipBindings for blendshapes matching our predefined settings data.
                        List<BlendShapeBinding> newBindings = new();

                        foreach (SkinnedMeshRenderer rendererVRM in sourceRenderersVRM)
                        {
                            if (rendererVRM.sharedMesh != null)
                            {
                                for (int i = 0; i < rendererVRM.sharedMesh.blendShapeCount; i++)
                                {
                                    string blendShapeName = rendererVRM.sharedMesh.GetBlendShapeName(i);

                                    foreach (VTuberBlendShapeMapValue settingARKit in arkit.Value)
                                    {
                                        if (settingARKit.ShapeName == blendShapeName)
                                        {
                                            string blendShapePath = BuildBlendShapeClipBlendShapePath(rendererVRM.transform );

                                            if (logARKit)
                                            {
                                                Debug.Log($"ARKit: Blend Shape Clip '{arkit.Key}': Skinned Mesh Renderer '{rendererVRM.gameObject.name}': Found '{settingARKit.ShapeName}' at '{blendShapePath}/{blendShapeName}'");
                                            }

                                            BlendShapeBinding newBinding = new()
                                            {
                                                Index = i,
                                                RelativePath = blendShapePath,
                                                Weight = settingARKit.ShapeValue
                                            };

                                            newBindings.Add(newBinding);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (logARKit)
                                {
                                    Debug.LogWarning($"ARKit: Blend Shape Clip '{arkit.Key}': Skinned Mesh Renderer '{rendererVRM.gameObject.name}': Does not appear to contain a mesh and is being skipped");
                                }
                            }
                        }

                        // Apply the new blendshape bindings to the clip.
                        targetClip.Values = newBindings.ToArray();
                    }
                }
            }

            ///// VRC BLENDSHAPES /////

            if (applyVRCBlendShapes)
            {
                foreach (BlendShapeClip existingClip in targetBlendShapeAvatar.Clips)
                {
                    if (applyVRCBlendShapesNeutralOnly && existingClip.BlendShapeName != "Neutral")
                    {
                        continue;
                    }

                    // Loop through the skinned mesh renderers in the sourceRenderersVRC and build BlendShipBindings for blendshapes set on the model.
                    List<BlendShapeBinding> newBindingsVRC = new(existingClip.Values);

                    foreach (SkinnedMeshRenderer sourceRendererVRM in sourceRenderersVRM)
                    {
                        foreach (SkinnedMeshRenderer sourceRendererVRC in sourceRenderersVRC)
                        {
                            if (logVRCBlendShapes)
                            {
                                Debug.Log($"VRC Blendshapes: VRM '{sourceRendererVRM.transform.name}'; VRC '{sourceRendererVRC.transform.name}'; BlendShapeClip '{existingClip.BlendShapeName}'");
                            }

                            if (sourceRendererVRM.sharedMesh != null)
                            {
                                if (sourceRendererVRM.transform.name == sourceRendererVRC.transform.name)
                                {
                                    for (int i = 0; i < sourceRendererVRM.sharedMesh.blendShapeCount; i++)
                                    {
                                        string blendShapeName = sourceRendererVRM.sharedMesh.GetBlendShapeName(i);
                                        string blendShapePath = BuildBlendShapeClipBlendShapePath(sourceRendererVRM.transform);
                                        int blendShapeIndex = sourceRendererVRC.sharedMesh.GetBlendShapeIndex(blendShapeName);

                                        if (blendShapeIndex != -1)
                                        {
                                            float blendShapeWeight = sourceRendererVRC.GetBlendShapeWeight(i);

                                            if (blendShapeWeight > 0.0f)
                                            {
                                                if (logVRCBlendShapes)
                                                {
                                                    Debug.Log($"VRC Blendshapes: VRM '{sourceRendererVRM.transform.name}'; VRC '{sourceRendererVRC.transform.name}'; VRM blendshape '{blendShapePath}/{blendShapeName}': Found matching VRC blendshape with weight of '{blendShapeWeight}'; applying.");
                                                }

                                                BlendShapeBinding newBindingVRC = new()
                                                {
                                                    Index = i,
                                                    RelativePath = blendShapePath,
                                                    Weight = blendShapeWeight
                                                };

                                                newBindingsVRC.Add(newBindingVRC);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Apply the new blendshape bindings to the clip.
                    existingClip.Values = newBindingsVRC.ToArray();
                }
            }
        }
        else
        {
            EditorUtility.DisplayDialog("Error", "Please provide all source/target VRC, VRM, and BlendShapeAvatar objects.", "OK");
        }
    }


    string BuildBlendShapeClipBlendShapePath(Transform transform)
    {
        Transform currentTransform = transform;
        List<string> pathElements = new();

        while (currentTransform.parent != null)
        {
            pathElements.Add(currentTransform.name);
            currentTransform = currentTransform.parent;
        }

        pathElements.Reverse();

        return string.Join("/", pathElements);
    }
}
#endif
