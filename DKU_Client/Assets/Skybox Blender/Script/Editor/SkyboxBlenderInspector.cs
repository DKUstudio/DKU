using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SkyboxBlender))]
public class SkyboxBlenderInspector : Editor
{
    SerializedProperty skyboxMaterials,
    makeFirstMaterialSkybox,
    blendSpeed,
    timeToWait,
    loop,
    
    updateLighting,
    updateReflections,
    updateEveryFrames,

    keepRotating,
    rotateToAngle,
    rotationSpeed,
    stopRotationOnBlendFinish;


    void OnEnable()
    {
        skyboxMaterials = serializedObject.FindProperty("skyboxMaterials");
        makeFirstMaterialSkybox = serializedObject.FindProperty("makeFirstMaterialSkybox");

        blendSpeed = serializedObject.FindProperty("blendSpeed");
        timeToWait = serializedObject.FindProperty("timeToWait");
        loop = serializedObject.FindProperty("loop");

        updateLighting = serializedObject.FindProperty("updateLighting");
        updateReflections = serializedObject.FindProperty("updateReflections");
        updateEveryFrames = serializedObject.FindProperty("updateEveryFrames");

        keepRotating = serializedObject.FindProperty("keepRotating");
        rotateToAngle = serializedObject.FindProperty("rotateToAngle");
        rotationSpeed = serializedObject.FindProperty("rotationSpeed");
        stopRotationOnBlendFinish = serializedObject.FindProperty("stopRotationOnBlendFinish");
    }


    public override void OnInspectorGUI()
    {
        var button = GUILayout.Button("Click for more tools");
        if (button) Application.OpenURL("https://assetstore.unity.com/publishers/39163");
        EditorGUILayout.Space(5);

        SkyboxBlender script = (SkyboxBlender) target;
        int space = 15;

        EditorGUILayout.Space(space);

        EditorGUILayout.LabelField("Material Options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(skyboxMaterials);
        EditorGUILayout.PropertyField(makeFirstMaterialSkybox);

        
        EditorGUILayout.Space(space);


        EditorGUILayout.LabelField("Blend Options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(blendSpeed);
        EditorGUILayout.PropertyField(timeToWait);
        EditorGUILayout.PropertyField(loop);


        EditorGUILayout.Space(space);


        EditorGUILayout.LabelField("Lighting Options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(updateLighting);
        EditorGUILayout.PropertyField(updateReflections);
        if (script.updateLighting || script.updateReflections) {
            EditorGUILayout.PropertyField(updateEveryFrames);
        }


        EditorGUILayout.Space(space);


        EditorGUILayout.LabelField("Rotations Options", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(keepRotating);
        
        if (!script.keepRotating) {
            EditorGUILayout.PropertyField(rotateToAngle);
        }

        EditorGUILayout.PropertyField(rotationSpeed);
        EditorGUILayout.PropertyField(stopRotationOnBlendFinish);
        

        serializedObject.ApplyModifiedProperties();
    }
}
