/* 
 * TransformEx 1.1
 * 
 * This script was made by Jason Peterson (DarkAkuma) of http://darkakuma.z-net.us/ 
*/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CanEditMultipleObjects, CustomEditor(typeof(Transform))]
public class TransformEx : Editor
{
    // 
    private static GUIContent positionLabelText = new GUIContent("Position", "The local position of this Game Object relative to the parent, in Unity Units.");
    private static GUIContent rotationLabelText = new GUIContent("Rotation", "The local rotation of this Game Object relative to the parent.");
    private static GUIContent scaleLabelText = new GUIContent("Scale", "The local scaling of this Game Object relative to the parent.");

    private static GUIContent worldPositionLabelText = new GUIContent("Position Measurements (World):", "The world position of this object, in real units of measurement.");

    private static GUIContent positionMillimetersLabelText = new GUIContent("Millimeters", "The world position of this Game Object relative to the parent, in Millimeters.");
    private static GUIContent positionCentimetersLabelText = new GUIContent("Centimeters", "The world position of this Game Object relative to the parent, in Centimeters.");
    private static GUIContent positionInchesLabelText = new GUIContent("Inches", "The world position of this Game Object relative to the parent, in Inches.");
    private static GUIContent positionFeetLabelText = new GUIContent("Feet", "The world position of this Game Object relative to the parent, in Feet.");
    private static GUIContent positionMetersLabelText = new GUIContent("Meters", "The world position of this Game Object relative to the parent, in Meters. Also effectivly Unity units.");
    private static GUIContent positionYardsLabelText = new GUIContent("Yards", "The world position of this Game Object relative to the parent, in Yards.");
    private static GUIContent positionKilometersLabelText = new GUIContent("Kilometers", "The world position of this Game Object relative to the parent, in Kilometers.");
    private static GUIContent positionMilesLabelText = new GUIContent("Miles", "The world position of this Game Object relative to the parent, in Miles.");

    private static GUIContent meshSizeLabelText = new GUIContent("Mesh Size", "The size of the mesh(es) under this Game Object, as calculated at their maximum bounds. Given in Unity units. \r\n\r\nClick the arrow to the left to see real world units of measurement and boundry lines in the Scene view.");
    private static GUIContent equalizeButtonText = new GUIContent("=", "Maintain the relative proportions of the mesh and equalize the other 2 axis' to the same scale as the last adjusted axis.");

    private static GUIContent sizeMeasurementsLabelText = new GUIContent("Size Measurements:", "The size of this Game Object's mesh(es) in real units of measurement.");
    private static GUIContent metricLabelText = new GUIContent("Metric", "");
    private static GUIContent imperialLabelText = new GUIContent("Imperial", "");

    private static GUIContent millimetersLabelText = new GUIContent("Millimeters", "The size of the Game Object in Millimeters, based on world scaling.");
    private static GUIContent centimetersLabelText = new GUIContent("Centimeters", "The size of the Game Object in Centimeters, based on world scaling.");
    private static GUIContent inchesLabelText = new GUIContent("Inches", "The size of the Game Object in Inches, based on world scaling.");
    private static GUIContent feetLabelText = new GUIContent("Feet", "The size of the Game Object in Feet, based on world scaling.");
    private static GUIContent metersLabelText = new GUIContent("Meters", "The size of the Game Object in Meters, based on world scaling.");
    private static GUIContent yardsLabelText = new GUIContent("Yards", "The size of the Game Object in Yards, based on world scaling.");
    private static GUIContent kilometersLabelText = new GUIContent("Kilometers", "The size of the Game Object in Kilometers, based on world scaling.");
    private static GUIContent milesLabelText = new GUIContent("Miles", "The size of the Game Object in Miles, based on world scaling.");

    private SerializedProperty positionProperty;
    private SerializedProperty rotationProperty;
    private SerializedProperty scaleProperty;

    private Transform transform;
    private List<Transform> transforms = new List<Transform>();

    static private bool isPositionListVisible = false;
    static private bool isMeshSizeListVisible = false;

    private Vector3 lastChange = Vector3.zero;
    
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
        // Need to our sync serializedObject first.
        serializedObject.Update();

        // Position        
        PositionPropertyField(positionProperty, positionLabelText);

        // Rotation
        RotationPropertyField(rotationProperty, rotationLabelText);

        // Scale
        ScalePropertyField(scaleProperty, scaleLabelText);

        // Real Measurement Mesh Sizes
        MeshSizePropertyFields();

        // Apply our changes.
        serializedObject.ApplyModifiedProperties();
    }

    private void PositionPropertyField(SerializedProperty property, GUIContent labelText)
    {
        // We're combining the position property and foldout property onto the same line.

        // We draw the position property first so it isnt included inside of the foldout.        
        EditorGUILayout.PropertyField(property, labelText);

        // Draw our little arrow for a drop down list of the position represented in other units of measurement.        
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

            EditorGUILayout.LabelField(worldPositionLabelText, EditorStyles.boldLabel);

            EditorGUI.indentLevel++;

            EditorGUI.BeginChangeCheck();

            // Show our custom real unit positions.
            Vector3 currentInches = transform.TransformEx().positionInInches;
            Vector3 currentFeet = transform.TransformEx().positionInFeet;
            Vector3 currentYards = transform.TransformEx().positionInYards;
            Vector3 currentMiles = transform.TransformEx().positionInMiles;

            Vector3 currentMillimeters = transform.TransformEx().positionInMillimeters;
            Vector3 currentCentimeters = transform.TransformEx().positionInCentimeters;
            Vector3 currentMeters = transform.TransformEx().positionInMeters;
            Vector3 currentKilometers = transform.TransformEx().positionInKilometers;

            if (IsImperialEnabled)
            {
                if (IsMetricEnabled)
                {
                    EditorGUILayout.LabelField(imperialLabelText, EditorStyles.boldLabel);

                    EditorGUI.indentLevel++;
                }

                if (IsInchesEnabled) currentInches = EditorGUILayout.Vector3Field(positionInchesLabelText, currentInches);
                if (IsFeetEnabled) currentFeet = EditorGUILayout.Vector3Field(positionFeetLabelText, currentFeet);
                if (IsYardsEnabled) currentYards = EditorGUILayout.Vector3Field(positionYardsLabelText, currentYards);
                if (IsMilesEnabled) currentMiles = EditorGUILayout.Vector3Field(positionMilesLabelText, currentMiles);

                if (IsMetricEnabled)
                    EditorGUI.indentLevel--;
            }

            if (IsMetricEnabled)
            {
                if (IsImperialEnabled)
                {
                    EditorGUILayout.LabelField(metricLabelText, EditorStyles.boldLabel);

                    EditorGUI.indentLevel++;
                }

                if (IsMillimetersEnabled) currentMillimeters = EditorGUILayout.Vector3Field(positionMillimetersLabelText, currentMillimeters);
                if (IsCentimetersEnabled) currentCentimeters = EditorGUILayout.Vector3Field(positionCentimetersLabelText, currentCentimeters);
                if (IsMetersEnabled) currentMeters = EditorGUILayout.Vector3Field(positionMetersLabelText, currentMeters);
                if (IsKilometersEnabled) currentKilometers = EditorGUILayout.Vector3Field(positionKilometersLabelText, currentKilometers);

                if (IsImperialEnabled)
                    EditorGUI.indentLevel--;
            }

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

            EditorGUI.indentLevel--;

            GUILayout.EndVertical();

            EditorGUI.showMixedValue = false;
        }
    }

    private void RotationPropertyField(SerializedProperty property, GUIContent labelText)
    {
        // If multiple objects are selected, we have to compare their values and not show any value at all if they are different.
        foreach (Transform t in transforms)
        {
            if (t.rotation != transform.rotation)
            {
                EditorGUI.showMixedValue = true;
                break;
            }
        }

        EditorGUI.BeginChangeCheck();

        // We don't use a EditorGUI.PropertyField so we can get back the new rotation if the user changes it.
        Vector3 currentRotation = EditorGUILayout.Vector3Field(labelText, transform.localRotation.eulerAngles);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObjects(targets, "New Rotation");

            // Set the rotations of all selected transforms to currentRotation.
            foreach (Transform t in transforms)
                t.localEulerAngles = currentRotation;

            property.serializedObject.SetIsDifferentCacheDirty();
        }

        EditorGUI.showMixedValue = false;
    }

    private void ScalePropertyField(SerializedProperty property, GUIContent labelText)
    {
        EditorGUI.BeginChangeCheck();

        // Draw our scale property.
        EditorGUILayout.PropertyField(property, labelText);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObjects(targets, "New Scale");

            // Update it ourselves, just incase... but probably not needed.
            transform.localScale = scaleProperty.vector3Value;
        }
    }

    private void MeshSizePropertyFields()
    {
        // We check the object and its children for MeshRenderers, and only display mesh size properties if any are found.
        // We also only show mesh size properties if a single object is selected.        
        if (HasMeshRenders())
        {
            EditorGUILayout.Separator();

            EditorGUI.BeginChangeCheck();

            // We're combining the Mesh Size property and foldout property onto the same line.

            // We draw the Mesh Size property first so it isnt included inside of the foldout.            
            Vector3 currentUnityUnits = EditorGUILayout.Vector3Field(meshSizeLabelText, transform.TransformEx().sizeInMeters);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObjects(targets, "New Size");

                // Apply any changes to the transforms of all selected objects.
                foreach (Transform t in transforms)
                {
                    // Changes our transforms scale.
                    if (currentUnityUnits != t.TransformEx().sizeInMeters) t.TransformEx().sizeInMeters = currentUnityUnits;
                }
            }

            // Draw our little arrow for a drop down list of the mesh size represented in the various units of measurement.
            bool isMeshSizeListVisible_ = EditorGUI.Foldout(GUILayoutUtility.GetLastRect(), isMeshSizeListVisible, new GUIContent(""));

            // Only draw our mesh size fields if the user clicks the little arrow.
            if (isMeshSizeListVisible_)
            {
                // If multiple objects are selected, we have to compare their values and not show any value at all if they are different.
                foreach (Transform t in transforms)
                {
                    // We only compare 1 value since they all represent the same physical size. Meters.... because why not? Its basically a Unity unit, so standard.
                    if (t.TransformEx().sizeInMeters != transform.TransformEx().sizeInMeters)
                    {
                        EditorGUI.showMixedValue = true;
                        break;
                    }
                }
                
                GUILayout.BeginVertical(EditorStyles.helpBox);

                GUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(sizeMeasurementsLabelText, EditorStyles.boldLabel);

                if (GUILayout.Button(equalizeButtonText, GUILayout.Width(25)))
                {
                    // See what axis was last changed and adjust the localScale of the other 2 to the same scale.
                    if (lastChange.x != 0) transform.localScale = new Vector3(lastChange.x, lastChange.x, lastChange.x);
                    else if (lastChange.y != 0) transform.localScale = new Vector3(lastChange.y, lastChange.y, lastChange.y);
                    else if (lastChange.z != 0) transform.localScale = new Vector3(lastChange.z, lastChange.z, lastChange.z);
                }

                GUILayout.EndHorizontal();

                EditorGUI.indentLevel++;
                
                EditorGUI.BeginChangeCheck();

                // Draw our Real measurement properties.
                Vector3 currentInches = transform.TransformEx().sizeInInches;
                Vector3 currentFeet = transform.TransformEx().sizeInFeet;
                Vector3 currentYards = transform.TransformEx().sizeInYards;
                Vector3 currentMiles = transform.TransformEx().sizeInMiles;

                Vector3 currentMillimeters = transform.TransformEx().sizeInMillimeters;
                Vector3 currentCentimeters = transform.TransformEx().sizeInCentimeters;
                Vector3 currentMeters = transform.TransformEx().sizeInMeters;
                Vector3 currentKilometers = transform.TransformEx().sizeInKilometers;
                
                if (IsImperialEnabled)
                {
                    if (IsMetricEnabled)
                    {
                        EditorGUILayout.LabelField(imperialLabelText, EditorStyles.boldLabel);

                        EditorGUI.indentLevel++;
                    }

                    if (IsInchesEnabled) currentInches = EditorGUILayout.Vector3Field(inchesLabelText, currentInches);
                    if (IsFeetEnabled) currentFeet = EditorGUILayout.Vector3Field(feetLabelText, currentFeet);                    
                    if (IsYardsEnabled) currentYards = EditorGUILayout.Vector3Field(yardsLabelText, currentYards);
                    if (IsMilesEnabled) currentMiles = EditorGUILayout.Vector3Field(milesLabelText, currentMiles);

                    if (IsMetricEnabled)
                        EditorGUI.indentLevel--;
                }                

                if (IsMetricEnabled)
                {
                    if (IsImperialEnabled)
                    {
                        EditorGUILayout.LabelField(metricLabelText, EditorStyles.boldLabel);

                        EditorGUI.indentLevel++;
                    }

                    if (IsMillimetersEnabled) currentMillimeters = EditorGUILayout.Vector3Field(millimetersLabelText, currentMillimeters);
                    if (IsCentimetersEnabled) currentCentimeters = EditorGUILayout.Vector3Field(centimetersLabelText, currentCentimeters);
                    if (IsMetersEnabled) currentMeters = EditorGUILayout.Vector3Field(metersLabelText, currentMeters);
                    if (IsKilometersEnabled) currentKilometers = EditorGUILayout.Vector3Field(kilometersLabelText, currentKilometers);

                    if (IsImperialEnabled)
                        EditorGUI.indentLevel--;
                }

                if (EditorGUI.EndChangeCheck())
                {   
                    Undo.RecordObjects(targets, "New Size");

                    //Vector3 lastScale = scaleProperty.vector3Value;
                    Vector3 lastScale = transform.localScale;

                    // Apply any changes to the transforms of all selected objects.
                    foreach (Transform t in transforms)
                    {
                        // Changes our transforms scale.
                        if (currentMillimeters != t.TransformEx().sizeInMillimeters) t.TransformEx().sizeInMillimeters = currentMillimeters;
                        else if (currentCentimeters != t.TransformEx().sizeInCentimeters) t.TransformEx().sizeInCentimeters = currentCentimeters;
                        else if (currentInches != t.TransformEx().sizeInInches) t.TransformEx().sizeInInches = currentInches;
                        else if (currentFeet != t.TransformEx().sizeInFeet) t.TransformEx().sizeInFeet = currentFeet;
                        else if (currentMeters != t.TransformEx().sizeInMeters) t.TransformEx().sizeInMeters = currentMeters;
                        else if (currentYards != t.TransformEx().sizeInYards) t.TransformEx().sizeInYards = currentYards;
                        else if (currentKilometers != t.TransformEx().sizeInKilometers) t.TransformEx().sizeInKilometers = currentKilometers;
                        else if (currentMiles != t.TransformEx().sizeInMiles) t.TransformEx().sizeInMiles = currentMiles;
                        else if (currentUnityUnits != t.TransformEx().sizeInMeters) t.TransformEx().sizeInMeters = currentUnityUnits;

                    }

                    // We see what axis was changed by whatever has the greatest difference.
                    float xDifference = Mathf.Abs(transform.localScale.x - lastScale.x);
                    float yDifference = Mathf.Abs(transform.localScale.y - lastScale.y);
                    float zDifference = Mathf.Abs(transform.localScale.z - lastScale.z);

                    lastChange = Vector3.zero;

                    // Remember what axis was last changed.
                    if (xDifference > yDifference && xDifference > zDifference) lastChange.x = transform.localScale.x;
                    else if (yDifference > xDifference && yDifference > zDifference) lastChange.y = transform.localScale.y;
                    else if (zDifference > xDifference && zDifference > yDifference) lastChange.z = transform.localScale.z;

                    scaleProperty.serializedObject.SetIsDifferentCacheDirty();
                }
                
                EditorGUI.indentLevel--;

                GUILayout.EndVertical();

                EditorGUI.showMixedValue = false;
            }

            // Force the gizmos to be redrawn if the user changed the option.
            if (isMeshSizeListVisible != isMeshSizeListVisible_)
            {
                isMeshSizeListVisible = isMeshSizeListVisible_;
                EditorUtility.SetDirty(target);
            }
        }
    }

    void OnSceneGUI()
    {
        if (Application.isEditor && !Application.isPlaying && isMeshSizeListVisible)
        {
            // We need meshInfo for knowing where the positions of the edges of the mesh(es) are.
            MeshInfo meshInfo = transform.GetMeshInfo();

            Handles.matrix = transform.localToWorldMatrix;

            // Draw some grids lines to better visually display the edges of the gameObjects mesh.
            Handles.color = Color.red;
            Handles.DrawLine(new Vector3(meshInfo.NegativeEdgeX(), meshInfo.PositiveEdgeY(), meshInfo.CenterPosition().z), new Vector3(meshInfo.PositiveEdgeX(), meshInfo.PositiveEdgeY(), meshInfo.CenterPosition().z));
            Handles.DrawLine(new Vector3(meshInfo.NegativeEdgeX(), meshInfo.NegativeEdgeY(), meshInfo.CenterPosition().z), new Vector3(meshInfo.PositiveEdgeX(), meshInfo.NegativeEdgeY(), meshInfo.CenterPosition().z));
            Handles.DrawLine(new Vector3(meshInfo.NegativeEdgeX(), meshInfo.CenterPosition().y, meshInfo.NegativeEdgeZ()), new Vector3(meshInfo.PositiveEdgeX(), meshInfo.CenterPosition().y, meshInfo.NegativeEdgeZ()));
            Handles.DrawLine(new Vector3(meshInfo.NegativeEdgeX(), meshInfo.CenterPosition().y, meshInfo.PositiveEdgeZ()), new Vector3(meshInfo.PositiveEdgeX(), meshInfo.CenterPosition().y, meshInfo.PositiveEdgeZ()));

            Handles.DrawLine(new Vector3(meshInfo.NegativeEdgeX(), meshInfo.PositiveEdgeY(), meshInfo.NegativeEdgeZ()), new Vector3(meshInfo.PositiveEdgeX(), meshInfo.PositiveEdgeY(), meshInfo.NegativeEdgeZ()));
            Handles.DrawLine(new Vector3(meshInfo.NegativeEdgeX(), meshInfo.PositiveEdgeY(), meshInfo.PositiveEdgeZ()), new Vector3(meshInfo.PositiveEdgeX(), meshInfo.PositiveEdgeY(), meshInfo.PositiveEdgeZ()));
            Handles.DrawLine(new Vector3(meshInfo.NegativeEdgeX(), meshInfo.NegativeEdgeY(), meshInfo.NegativeEdgeZ()), new Vector3(meshInfo.PositiveEdgeX(), meshInfo.NegativeEdgeY(), meshInfo.NegativeEdgeZ()));
            Handles.DrawLine(new Vector3(meshInfo.NegativeEdgeX(), meshInfo.NegativeEdgeY(), meshInfo.PositiveEdgeZ()), new Vector3(meshInfo.PositiveEdgeX(), meshInfo.NegativeEdgeY(), meshInfo.PositiveEdgeZ()));

            Handles.color = Color.green;
            Handles.DrawLine(new Vector3(meshInfo.NegativeEdgeX(), meshInfo.NegativeEdgeY(), meshInfo.CenterPosition().z), new Vector3(meshInfo.NegativeEdgeX(), meshInfo.PositiveEdgeY(), meshInfo.CenterPosition().z));
            Handles.DrawLine(new Vector3(meshInfo.PositiveEdgeX(), meshInfo.NegativeEdgeY(), meshInfo.CenterPosition().z), new Vector3(meshInfo.PositiveEdgeX(), meshInfo.PositiveEdgeY(), meshInfo.CenterPosition().z));
            Handles.DrawLine(new Vector3(meshInfo.CenterPosition().x, meshInfo.NegativeEdgeY(), meshInfo.NegativeEdgeZ()), new Vector3(meshInfo.CenterPosition().x, meshInfo.PositiveEdgeY(), meshInfo.NegativeEdgeZ()));
            Handles.DrawLine(new Vector3(meshInfo.CenterPosition().x, meshInfo.NegativeEdgeY(), meshInfo.PositiveEdgeZ()), new Vector3(meshInfo.CenterPosition().x, meshInfo.PositiveEdgeY(), meshInfo.PositiveEdgeZ()));

            Handles.DrawLine(new Vector3(meshInfo.NegativeEdgeX(), meshInfo.NegativeEdgeY(), meshInfo.NegativeEdgeZ()), new Vector3(meshInfo.NegativeEdgeX(), meshInfo.PositiveEdgeY(), meshInfo.NegativeEdgeZ()));
            Handles.DrawLine(new Vector3(meshInfo.NegativeEdgeX(), meshInfo.NegativeEdgeY(), meshInfo.PositiveEdgeZ()), new Vector3(meshInfo.NegativeEdgeX(), meshInfo.PositiveEdgeY(), meshInfo.PositiveEdgeZ()));
            Handles.DrawLine(new Vector3(meshInfo.PositiveEdgeX(), meshInfo.NegativeEdgeY(), meshInfo.NegativeEdgeZ()), new Vector3(meshInfo.PositiveEdgeX(), meshInfo.PositiveEdgeY(), meshInfo.NegativeEdgeZ()));
            Handles.DrawLine(new Vector3(meshInfo.PositiveEdgeX(), meshInfo.NegativeEdgeY(), meshInfo.PositiveEdgeZ()), new Vector3(meshInfo.PositiveEdgeX(), meshInfo.PositiveEdgeY(), meshInfo.PositiveEdgeZ()));

            Handles.color = Color.blue;
            Handles.DrawLine(new Vector3(meshInfo.CenterPosition().x, meshInfo.NegativeEdgeY(), meshInfo.NegativeEdgeZ()), new Vector3(meshInfo.CenterPosition().x, meshInfo.NegativeEdgeY(), meshInfo.PositiveEdgeZ()));
            Handles.DrawLine(new Vector3(meshInfo.CenterPosition().x, meshInfo.PositiveEdgeY(), meshInfo.NegativeEdgeZ()), new Vector3(meshInfo.CenterPosition().x, meshInfo.PositiveEdgeY(), meshInfo.PositiveEdgeZ()));
            Handles.DrawLine(new Vector3(meshInfo.NegativeEdgeX(), meshInfo.CenterPosition().y, meshInfo.NegativeEdgeZ()), new Vector3(meshInfo.NegativeEdgeX(), meshInfo.CenterPosition().y, meshInfo.PositiveEdgeZ()));
            Handles.DrawLine(new Vector3(meshInfo.PositiveEdgeX(), meshInfo.CenterPosition().y, meshInfo.NegativeEdgeZ()), new Vector3(meshInfo.PositiveEdgeX(), meshInfo.CenterPosition().y, meshInfo.PositiveEdgeZ()));

            Handles.DrawLine(new Vector3(meshInfo.NegativeEdgeX(), meshInfo.NegativeEdgeY(), meshInfo.NegativeEdgeZ()), new Vector3(meshInfo.NegativeEdgeX(), meshInfo.NegativeEdgeY(), meshInfo.PositiveEdgeZ()));
            Handles.DrawLine(new Vector3(meshInfo.NegativeEdgeX(), meshInfo.PositiveEdgeY(), meshInfo.NegativeEdgeZ()), new Vector3(meshInfo.NegativeEdgeX(), meshInfo.PositiveEdgeY(), meshInfo.PositiveEdgeZ()));
            Handles.DrawLine(new Vector3(meshInfo.PositiveEdgeX(), meshInfo.NegativeEdgeY(), meshInfo.NegativeEdgeZ()), new Vector3(meshInfo.PositiveEdgeX(), meshInfo.NegativeEdgeY(), meshInfo.PositiveEdgeZ()));
            Handles.DrawLine(new Vector3(meshInfo.PositiveEdgeX(), meshInfo.PositiveEdgeY(), meshInfo.NegativeEdgeZ()), new Vector3(meshInfo.PositiveEdgeX(), meshInfo.PositiveEdgeY(), meshInfo.PositiveEdgeZ()));
        }
    }

    /// <summary>
    /// Check this transform and its children for the existance of any mesh renderers.
    /// </summary>
    /// <returns></returns>
    private bool HasMeshRenders()
    {
        Transform[] objTranforms = transform.gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform nextTransform in objTranforms)
        {
            if (nextTransform.GetComponent<MeshFilter>())
                return true;
            else if (nextTransform.GetComponent<SkinnedMeshRenderer>())
                return true;
        }

        return false;
    }
       
    private const string menuName = "Edit/TransfromEx";
    private const string imperialMenuName = "Edit/TransfromEx/Imperial";
    private const string metricMenuName = "Edit/TransfromEx/Metric";

    private const string showImperial = "Show Imperial Units";
    private const string showMetric = "Show Metric Units";

    private const string showInches = "Show Inches";
    private const string showFeet = "Show Feet";
    private const string showYards = "Show Yards";
    private const string showMiles = "Show Miles";

    private const string showMillimeters = "Show Millimeters";
    private const string showCentimeters = "Show Centimeters";
    private const string showMeters = "Show Meters";
    private const string showKilometers = "Show Kilometers";

    public static bool IsImperialEnabled
    {
        get { return EditorPrefs.GetBool(showImperial, true); }
        set { EditorPrefs.SetBool(showImperial, value); }
    }

    public static bool IsMetricEnabled
    {
        get { return EditorPrefs.GetBool(showMetric, true); }
        set { EditorPrefs.SetBool(showMetric, value); }
    }

    public static bool IsMillimetersEnabled
    {
        get { return EditorPrefs.GetBool(showMillimeters, true); }
        set { EditorPrefs.SetBool(showMillimeters, value); }
    }

    public static bool IsCentimetersEnabled
    {
        get { return EditorPrefs.GetBool(showCentimeters, true); }
        set { EditorPrefs.SetBool(showCentimeters, value); }
    }

    public static bool IsInchesEnabled
    {
        get { return EditorPrefs.GetBool(showInches, true); }
        set { EditorPrefs.SetBool(showInches, value); }
    }

    public static bool IsFeetEnabled
    {
        get { return EditorPrefs.GetBool(showFeet, true); }
        set { EditorPrefs.SetBool(showFeet, value); }
    }

    public static bool IsMetersEnabled
    {
        get { return EditorPrefs.GetBool(showMeters, true); }
        set { EditorPrefs.SetBool(showMeters, value); }
    }

    public static bool IsYardsEnabled
    {
        get { return EditorPrefs.GetBool(showYards, true); }
        set { EditorPrefs.SetBool(showYards, value); }
    }

    public static bool IsKilometersEnabled
    {
        get { return EditorPrefs.GetBool(showKilometers, true); }
        set { EditorPrefs.SetBool(showKilometers, value); }
    }

    public static bool IsMilesEnabled
    {
        get { return EditorPrefs.GetBool(showMiles, true); }
        set { EditorPrefs.SetBool(showMiles, value); }
    }


    [MenuItem(imperialMenuName + "/" + showInches)]
    private static void ToggleInches()
    {
        IsInchesEnabled = !IsInchesEnabled;
    }

    [MenuItem(imperialMenuName + "/" + showFeet)]
    private static void ToggleFeet()
    {
        IsFeetEnabled = !IsFeetEnabled;
    }

    [MenuItem(imperialMenuName + "/" + showYards)]
    private static void ToggleYards()
    {
        IsYardsEnabled = !IsYardsEnabled;
    }

    [MenuItem(imperialMenuName + "/" + showMiles)]
    private static void ToggleMiles()
    {
        IsMilesEnabled = !IsMilesEnabled;
    }

    [MenuItem(metricMenuName + "/" + showMillimeters)]
    private static void ToggleMillimeters()
    {
        IsMillimetersEnabled = !IsMillimetersEnabled;
    }

    [MenuItem(metricMenuName + "/" + showCentimeters)]
    private static void ToggleCentimeters()
    {
        IsCentimetersEnabled = !IsCentimetersEnabled;
    }

    [MenuItem(metricMenuName + "/" + showMeters)]
    private static void ToggleMeters()
    {
        IsMetersEnabled = !IsMetersEnabled;
    }

    [MenuItem(metricMenuName + "/" + showKilometers)]
    private static void ToggleKilometers()
    {
        IsKilometersEnabled = !IsKilometersEnabled;
    }

    [MenuItem(menuName + "/" + showImperial)]
    private static void ToggleImperial()
    {
        IsImperialEnabled = !IsImperialEnabled;
    }

    [MenuItem(menuName + "/" + showMetric)]
    private static void ToggleMetric()
    {
        IsMetricEnabled = !IsMetricEnabled;
    }


    [MenuItem(metricMenuName + "/" + showMillimeters, true)]
    private static bool ToggleMillimetersValidate()
    {
        Menu.SetChecked(metricMenuName + "/" + showMillimeters, IsMillimetersEnabled);
        return true;
    }

    [MenuItem(metricMenuName + "/" + showCentimeters, true)]
    private static bool ToggleCentimetersValidate()
    {
        Menu.SetChecked(metricMenuName + "/" + showCentimeters, IsCentimetersEnabled);
        return true;
    }

    [MenuItem(imperialMenuName + "/" + showInches, true)]
    private static bool ToggleInchesValidate()
    {
        Menu.SetChecked(imperialMenuName + "/" + showInches, IsInchesEnabled);
        return true;
    }

    [MenuItem(imperialMenuName + "/" + showFeet, true)]
    private static bool ToggleFeetValidate()
    {
        Menu.SetChecked(imperialMenuName + "/" + showFeet, IsFeetEnabled);
        return true;
    }

    [MenuItem(metricMenuName + "/" + showMeters, true)]
    private static bool ToggleMetersValidate()
    {
        Menu.SetChecked(metricMenuName + "/" + showMeters, IsMetersEnabled);
        return true;
    }

    [MenuItem(imperialMenuName + "/" + showYards, true)]
    private static bool ToggleYardsValidate()
    {
        Menu.SetChecked(imperialMenuName + "/" + showYards, IsYardsEnabled);
        return true;
    }

    [MenuItem(metricMenuName + "/" + showKilometers, true)]
    private static bool ToggleKilometersValidate()
    {
        Menu.SetChecked(metricMenuName + "/" + showKilometers, IsKilometersEnabled);
        return true;
    }

    [MenuItem(imperialMenuName + "/" + showMiles, true)]
    private static bool ToggleMilesValidate()
    {
        Menu.SetChecked(imperialMenuName + "/" + showMiles, IsMilesEnabled);
        return true;
    }

    [MenuItem(menuName + "/" + showImperial, true)]
    private static bool ToggleImperialValidate()
    {
        Menu.SetChecked(menuName + "/" + showImperial, IsImperialEnabled);
        return true;
    }

    [MenuItem(menuName + "/" + showMetric, true)]
    private static bool ToggleMetricValidate()
    {
        Menu.SetChecked(menuName + "/" + showMetric, IsMetricEnabled);
        return true;
    }
}