/* 
 * MeshInfo 1.0
 * 
 * This script was made by Jason Peterson (DarkAkuma) of http://darkakuma.z-net.us/
*/

using UnityEngine;
using System.Collections;

/// <summary>
/// This calculates and stores mesh data for a given transform and its children. Mesh data includes edge positions, cubic size in Unity units, and the center position.
/// </summary>
public class MeshInfo
{
    // We store the outter most edges of a mesh as bounds in local positions related to the given transform.    
    private Bounds collectiveBounds = new Bounds();

    // We need a transform to work with.
    private Transform transform;

    public MeshInfo(Transform t)
    {
        transform = t;

        ScanMesh(transform);
    }

    /// <summary>
    /// Scan through this transform and its children for meshs.
    /// </summary>
    /// <returns>Info on the mesh like edge positions, size and center.</returns>
    private void ScanMesh(Transform transform)
    {
        // In this class we create and expand our own custom bounding box. That bounding box is based off the outter edges of all meshes on, or childed to "transform".

        Transform[] objTranforms = transform.gameObject.GetComponentsInChildren<Transform>();

        // We need to iterate through this object and its children for meshes.
        foreach (Transform nextTransform in objTranforms)
        {
            //Debug.Log(nextTransform.name);

            //if (nextTransform.GetComponent<MeshFilter>())
            if (nextTransform.GetComponent<MeshFilter>() && nextTransform.GetComponent<MeshRenderer>())
                AddMeshData(nextTransform.GetComponent<MeshFilter>());
            else if (nextTransform.GetComponent<SkinnedMeshRenderer>())
                AddMeshData(nextTransform.GetComponent<SkinnedMeshRenderer>());
        }
    }

    /// <summary>
    /// This calculates the edges of a single mesh, and updates the collective bounding box with the outter most known edges of meshs under the given transform.
    /// </summary>
    /// <param name="meshFilter"></param>
    private void AddMeshData(MeshFilter meshFilter)
    {
        // Each mesh naturally is in the local space of the (child) object its attached to, but we need it in local space of the given transform. To do that we convert it from "mesh object local -> world -> given transform local.
        // We need to convert the center position of each meshes bounds the same, "mesh object local -> world -> given transform local.                 
        Bounds nextMeshesBounds = LocalToLocalBounds(meshFilter);

        // Once we have that info, it's time to add it to our custom collective bounding box.
        ExpandCollectiveBoundingBox(nextMeshesBounds);
    }

    /// <summary>
    /// This calculates the edges of a single mesh, and updates the collective bounding box with the outter most known edges of meshs under the given transform.
    /// </summary>
    /// <param name="meshFilter"></param>
    private void AddMeshData(SkinnedMeshRenderer skinnedMeshRenderer)
    {
        // Each mesh naturally is in the local space of the (child) object its attached to, but we need it in local space of the given transform. To do that we convert it from "mesh object local -> world -> given transform local.
        // We need to convert the center position of each meshes bounds the same, "mesh object local -> world -> given transform local. 
        Bounds nextMeshesBounds = LocalToLocalBounds(skinnedMeshRenderer);

        // Once we have that info, it's time to add it to our custom collective bounding box.
        ExpandCollectiveBoundingBox(nextMeshesBounds);
    }

    /// <summary>
    /// Return the bounds of the mesh in the local space of the given transform.
    /// </summary>
    /// <param name="meshFilter"></param>
    /// <returns></returns>
    private Bounds LocalToLocalBounds(MeshFilter meshFilter)
    {
        Bounds b = new Bounds();

        Vector3 worldCenterPosition;
        Vector3 worldExtents;

        Vector3 toLocalCenterPosition;
        Vector3 toLocalExtents;

        // 
        if (meshFilter.transform == transform)
        {
            // We don't need to convert the meshes bounds to local space of the given transform since they are the same, so just grab it directly.            
            toLocalCenterPosition = meshFilter.sharedMesh.bounds.center;
            toLocalExtents = meshFilter.sharedMesh.bounds.extents;
        }
        else
        {
            // We get the mesh bounds as world space incase the meshes transform is rotated. If so the rotation can cause the the collective mesh bounds to be larger then otherwise.

            // Get the meshes bounds as World space.
            worldCenterPosition = meshFilter.GetComponent<Renderer>().bounds.center;
            worldExtents = meshFilter.GetComponent<Renderer>().bounds.extents;

            // Convert from world space to the local space of our given transform.
            toLocalCenterPosition = transform.InverseTransformPoint(worldCenterPosition);
            toLocalExtents = transform.InverseTransformVector(worldExtents);
        }

        b.extents = toLocalExtents;
        b.center = toLocalCenterPosition;

        return b;
    }

    /// <summary>
    /// Return the bounds of the mesh in the local space of the given transform.
    /// </summary>
    /// <param name="skinnedMeshRenderer"></param>
    /// <returns></returns>
    private Bounds LocalToLocalBounds(SkinnedMeshRenderer skinnedMeshRenderer)
    {
        Bounds b = new Bounds();

        Vector3 worldCenterPosition;
        Vector3 worldExtents;

        Vector3 toLocalCenterPosition;
        Vector3 toLocalExtents;

        // 
        if (skinnedMeshRenderer.transform == transform)
        {
            // We don't need to convert the meshes bounds to local space of the given transform since they are the same, so just grab it directly.            
            toLocalCenterPosition = skinnedMeshRenderer.sharedMesh.bounds.center;
            toLocalExtents = skinnedMeshRenderer.sharedMesh.bounds.extents;
        }
        else
        {
            // We get the mesh bounds as world space incase the meshes transform is rotated. If so the rotation can cause the the collective mesh bounds to be larger then otherwise.

            // Get the meshes bounds as World space.
            worldCenterPosition = skinnedMeshRenderer.GetComponent<Renderer>().bounds.center;
            worldExtents = skinnedMeshRenderer.GetComponent<Renderer>().bounds.extents;

            // Convert from world space to the local space of our given transform.
            toLocalCenterPosition = transform.InverseTransformPoint(worldCenterPosition);
            toLocalExtents = transform.InverseTransformVector(worldExtents);
        }

        b.extents = toLocalExtents;
        b.center = toLocalCenterPosition;

        return b;
    }

    /// <summary>
    /// Add the bounds of a mesh to the custom bounding box of this class.
    /// </summary>
    /// <param name="nextMeshesBounds"></param>
    private void ExpandCollectiveBoundingBox(Bounds nextMeshesBounds)
    {
        // We do this first by creating the bounds at the +,+,+ corner's position with no given size, then expanding it to the -,-,- corners position.
        // To get these positions we first need to clean the extents axises of any negative values since any negative value would cause us to be dealing with a different corner of the bounding box then expected. (example: +,-,+ != either the +,+,+ or -,-,- corners.)
        // Then we add/subtract those cleaned extent axis values from the center position to get our final +,+,+ / -,-,- corner positions.
        if (collectiveBounds.extents == Vector3.zero && collectiveBounds.center == Vector3.zero)
        {
            // This is the first mesh found, so create our new bounding box.
            collectiveBounds = new Bounds(new Vector3(nextMeshesBounds.center.x + Mathf.Abs(nextMeshesBounds.extents.x), nextMeshesBounds.center.y + Mathf.Abs(nextMeshesBounds.extents.y), nextMeshesBounds.center.z + Mathf.Abs(nextMeshesBounds.extents.z)), Vector3.zero);
            collectiveBounds.Encapsulate(new Vector3(nextMeshesBounds.center.x - Mathf.Abs(nextMeshesBounds.extents.x), nextMeshesBounds.center.y - Mathf.Abs(nextMeshesBounds.extents.y), nextMeshesBounds.center.z - Mathf.Abs(nextMeshesBounds.extents.z)));
        }
        else
        {
            // This is the 2nd or later mesh found under the given transform, so we Encapsulate its corner positions to expand our bounding box if any part exceeds the previous edges our bounding box encompassed.
            collectiveBounds.Encapsulate(new Vector3(nextMeshesBounds.center.x + Mathf.Abs(nextMeshesBounds.extents.x), nextMeshesBounds.center.y + Mathf.Abs(nextMeshesBounds.extents.y), nextMeshesBounds.center.z + Mathf.Abs(nextMeshesBounds.extents.z)));
            collectiveBounds.Encapsulate(new Vector3(nextMeshesBounds.center.x - Mathf.Abs(nextMeshesBounds.extents.x), nextMeshesBounds.center.y - Mathf.Abs(nextMeshesBounds.extents.y), nextMeshesBounds.center.z - Mathf.Abs(nextMeshesBounds.extents.z)));
        }
    }

    /// <summary>
    /// Mesh size in unity units.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetMeshSize()
    {
        return new Vector3(PositiveEdgeX() - NegativeEdgeX(), PositiveEdgeY() - NegativeEdgeY(), PositiveEdgeZ() - NegativeEdgeZ());
    }

    /// <summary>
    /// Mesh size in unity units. (Converts axis direction)
    /// </summary>
    /// <returns></returns>

    public Vector3 GetMeshSizeEx()
    {
        return transform.InverseTransformDirection(transform.TransformPoint(new Vector3(PositiveEdgeX(), PositiveEdgeY(), PositiveEdgeZ()))) - transform.InverseTransformDirection(transform.TransformPoint(new Vector3(NegativeEdgeX(), NegativeEdgeY(), NegativeEdgeZ())));
    }

    /// <summary>
    /// Get the transform this mesh data belongs to.
    /// </summary>
    /// <returns></returns>
    public Transform GetTransform()
    {
        return transform;
    }

    /// <summary>
    /// Get the meshs edge position on the +x axis for this transform.
    /// </summary>
    /// <returns></returns>
    public float PositiveEdgeX()
    {
        return collectiveBounds.center.x + collectiveBounds.extents.x;
    }

    /// <summary>
    /// Get the meshs edge position on the -x axis for this transform.
    /// </summary>
    /// <returns></returns>
    public float NegativeEdgeX()
    {
        return collectiveBounds.center.x - collectiveBounds.extents.x;
    }

    /// <summary>
    /// Get the meshs edge position on the +y axis for this transform.
    /// </summary>
    /// <returns></returns>
    public float PositiveEdgeY()
    {
        return collectiveBounds.center.y + collectiveBounds.extents.y;
    }

    /// <summary>
    /// Get the meshs edge position on the -y axis for this transform.
    /// </summary>
    /// <returns></returns>
    public float NegativeEdgeY()
    {
        return collectiveBounds.center.y - collectiveBounds.extents.y;
    }

    /// <summary>
    /// Get the meshs edge position on the +z axis for this transform.
    /// </summary>
    /// <returns></returns>
    public float PositiveEdgeZ()
    {
        return collectiveBounds.center.z + collectiveBounds.extents.z;
    }

    /// <summary>
    /// Get the meshs edge position on the -z axis for this transform.
    /// </summary>
    /// <returns></returns>
    public float NegativeEdgeZ()
    {
        return collectiveBounds.center.z - collectiveBounds.extents.z;
    }

    /// <summary>
    /// Get the meshs center position for this transform.
    /// </summary>
    /// <returns></returns>
    public Vector3 CenterPosition()
    {
        return collectiveBounds.center;
    }

    /// <summary>
    /// Rescales the transform of this object.
    /// </summary>
    /// <param name="unit">meter/feet/inches scale value in Unity units (meters).</param>
    /// <param name="desiredSize">The size in meters/feet/inches that we want to scale the object too.</param>
    public Vector3 GetScale(float unit, Vector3 desiredSize)
    {
        Vector3 newScale = Vector3.one;

        Vector3 desiredSizeinUnityUnits = desiredSize * unit;

        // Only change a axis value if a new value is given.
        if (desiredSize.x != 0)
            newScale.x = desiredSizeinUnityUnits.x / GetMeshSize().x;
        if (desiredSize.y != 0)
            newScale.y = desiredSizeinUnityUnits.y / GetMeshSize().y;
        if (desiredSize.z != 0)
            newScale.z = desiredSizeinUnityUnits.z / GetMeshSize().z;

        // Scan through the parents of this object to factor in their scale.
        Transform parent = transform.parent;
        while (parent)
        {
            newScale.x /= transform.parent.localScale.x;
            newScale.y /= transform.parent.localScale.y;
            newScale.z /= transform.parent.localScale.z;

            parent = parent.parent;
        }

        return newScale;
    }
}