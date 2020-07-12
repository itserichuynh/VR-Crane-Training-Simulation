/* 
 * TransformEx 1.1
 * 
 * This script was made by Jason Peterson (DarkAkuma) of http://darkakuma.z-net.us/ 
*/

using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;

[CanEditMultipleObjects, CustomEditor(typeof(RectTransform))]
public class RectTransformEx : Editor
{
    // 
    private static GUIContent positionLabelText = new GUIContent("Ex Positions", "The local position of this Game Object relative to the parent, in Unity Units.");
    
    private static GUIContent worldPositionLabelText = new GUIContent("World Positions", "The world position of this object, in real units of measurment.");

    private static GUIContent positionMillimetersLabelText = new GUIContent("Millimeters", "The world position of this Game Object relative to the parent, in Millimeters.");
    private static GUIContent positionCentimetersLabelText = new GUIContent("Centimeters", "The world position of this Game Object relative to the parent, in Centimeters.");
    private static GUIContent positionInchesLabelText = new GUIContent("Inches", "The world position of this Game Object relative to the parent, in Inches.");
    private static GUIContent positionFeetLabelText = new GUIContent("Feet", "The world position of this Game Object relative to the parent, in Feet.");
    private static GUIContent positionMetersLabelText = new GUIContent("Meters", "The world position of this Game Object relative to the parent, in Meters. Also effectivly Unity units.");
    private static GUIContent positionYardsLabelText = new GUIContent("Yards", "The world position of this Game Object relative to the parent, in Yards.");
    private static GUIContent positionKilometersLabelText = new GUIContent("Kilometers", "The world position of this Game Object relative to the parent, in Kilometers.");
    private static GUIContent positionMilesLabelText = new GUIContent("Miles", "The world position of this Game Object relative to the parent, in Miles.");

    private SerializedProperty positionProperty;
    private SerializedProperty rotationProperty;
    private SerializedProperty scaleProperty;

    private Transform transform;
    private List<Transform> transforms = new List<Transform>();
    private Editor m_editor;

    static private bool isPositionListVisible = false;
    static private bool isMeshSizeListVisible = false;

    public void OnEnable()
    {
        // It's just nifty having access to the transform in a familiar variable.
        transform = (Transform)target;

        // Again, it's nice having access to our list of selected transforms, in a list.
        foreach (UnityEngine.Object obj in (UnityEngine.Object[])targets)
            transforms.Add((Transform)obj);

        // Get our normal properties ready.
        positionProperty = serializedObject.FindProperty("m_LocalPosition");
        rotationProperty = serializedObject.FindProperty("m_LocalRotation");
        scaleProperty = serializedObject.FindProperty("m_LocalScale");
    }

    public override void OnInspectorGUI()
    {
        if (m_editor == null)
        {
            Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {   
                System.Type type = assembly.GetType("UnityEditor.RectTransformEditor");

                if (type != null)
                {
                    m_editor = Editor.CreateEditor(target, type);
                    break;
                }
            }
        }

        if (m_editor != null)
        {
            m_editor.OnInspectorGUI();

            // Need to our sync serializedObject first.
            serializedObject.Update();

            // Position        
            PositionPropertyField(positionProperty, positionLabelText);

            // Apply our changes.
            serializedObject.ApplyModifiedProperties();
        }
    }

    private void PositionPropertyField(SerializedProperty property, GUIContent labelText)
    {
        // We're combining the position property and foldout property onto the same line.

        // We draw the position property first so it isnt included inside of the foldout.        
        //EditorGUILayout.PropertyField(property, labelText);
        EditorGUILayout.LabelField(labelText);

        // Draw our little arrow for a drop down list of the position represented in other units of measurment.        
        isPositionListVisible = EditorGUI.Foldout(GUILayoutUtility.GetLastRect(), isPositionListVisible, new GUIContent(""));

        // Only draw our extra position fields if the user clicks the little arrow.
        if (isPositionListVisible)
        {
            // If multiple objects are selected, we have to compare their values and not show any value at all if they are different.
            foreach (Transform t in transforms)
            {
                if (t.position != transform.position)
                {
                    EditorGUI.showMixedValue = true;
                    break;
                }
            }

            GUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUI.indentLevel++;

            EditorGUILayout.LabelField(worldPositionLabelText, EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();

            // Show our custom real unit positions.
            Vector3 currentMillimeters = transform.TransformEx().positionInMillimeters;
            Vector3 currentCentimeters = transform.TransformEx().positionInCentimeters;
            Vector3 currentInches = transform.TransformEx().positionInInches;
            Vector3 currentFeet = transform.TransformEx().positionInFeet;
            Vector3 currentMeters = transform.TransformEx().positionInMeters;
            Vector3 currentYards = transform.TransformEx().positionInYards;
            Vector3 currentKilometers = transform.TransformEx().positionInKilometers;
            Vector3 currentMiles = transform.TransformEx().positionInMiles;

            if (TransformEx.IsMillimetersEnabled) currentMillimeters = EditorGUILayout.Vector3Field(positionMillimetersLabelText, currentMillimeters);
            if (TransformEx.IsCentimetersEnabled) currentCentimeters = EditorGUILayout.Vector3Field(positionCentimetersLabelText, currentCentimeters);
            if (TransformEx.IsInchesEnabled) currentInches = EditorGUILayout.Vector3Field(positionInchesLabelText, currentInches);
            if (TransformEx.IsFeetEnabled) currentFeet = EditorGUILayout.Vector3Field(positionFeetLabelText, currentFeet);
            if (TransformEx.IsMetersEnabled) currentMeters = EditorGUILayout.Vector3Field(positionMetersLabelText, currentMeters);
            if (TransformEx.IsYardsEnabled) currentYards = EditorGUILayout.Vector3Field(positionYardsLabelText, currentYards);
            if (TransformEx.IsKilometersEnabled) currentKilometers = EditorGUILayout.Vector3Field(positionKilometersLabelText, currentKilometers);
            if (TransformEx.IsMilesEnabled) currentMiles = EditorGUILayout.Vector3Field(positionMilesLabelText, currentMiles);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObjects(targets, "New Position");

                // Apply any changes to the transforms of all selected objects.
                foreach (Transform t in transforms)
                {
                    // Change our transforms positions.
                    if (currentMillimeters != t.TransformEx().positionInMillimeters) t.TransformEx().positionInMillimeters = currentMillimeters;
                    else if (currentCentimeters != t.TransformEx().positionInCentimeters) t.TransformEx().positionInCentimeters = currentCentimeters;
                    else if (currentInches != t.TransformEx().positionInInches) t.TransformEx().positionInInches = currentInches;
                    else if (currentFeet != t.TransformEx().positionInFeet) t.TransformEx().positionInFeet = currentFeet;
                    else if (currentMeters != t.TransformEx().positionInMeters) t.TransformEx().positionInMeters = currentMeters;
                    else if (currentYards != t.TransformEx().positionInYards) t.TransformEx().positionInYards = currentYards;
                    else if (currentKilometers != t.TransformEx().positionInKilometers) t.TransformEx().positionInKilometers = currentKilometers;
                    else if (currentMiles != t.TransformEx().positionInMiles) t.TransformEx().positionInMiles = currentMiles;
                }
            }

            EditorGUILayout.Separator();

            EditorGUI.indentLevel--;

            GUILayout.EndVertical();

            EditorGUI.showMixedValue = false;
        }
    }    
}