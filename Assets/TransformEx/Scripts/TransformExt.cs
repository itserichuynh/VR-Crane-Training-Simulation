/* 
 * TransformExt 1.0
 * 
 * This script was made by Jason Peterson (DarkAkuma) of http://darkakuma.z-net.us/
*/

using UnityEngine;
using System.Collections;

// Methods for our Transform.TransformEx().
public class TransformExt
{
    // We set this in the Constructor so we can use its info with our custom methods.
    private MeshInfo meshInfo;    
    private Transform transform;

    public TransformExt(Transform t, MeshInfo mi)
    {
        meshInfo = mi;
        transform = t;
    }

    /// <summary>    
    /// Get/Set the size of this mesh in Millimeters.
    /// </summary>        
    public Vector3 sizeInMillimeters
    {
        get { return (meshInfo.GetMeshSizeEx() / UnitType.Millimeters); }
        set { meshInfo.GetTransform().localScale = meshInfo.GetScale(UnitType.Millimeters, value); }
    }

    /// <summary>    
    /// Get/Set the size of this mesh in Centimeters.
    /// </summary>        
    public Vector3 sizeInCentimeters
    {
        get { return (meshInfo.GetMeshSizeEx() / UnitType.Centimeters); }
        set { meshInfo.GetTransform().localScale = meshInfo.GetScale(UnitType.Centimeters, value); }
    }

    /// <summary>    
    /// Get/Set the size of this mesh in Inches.
    /// </summary>        
    public Vector3 sizeInInches
    {
        get { return (meshInfo.GetMeshSizeEx() / UnitType.Inches); }
        set { meshInfo.GetTransform().localScale = meshInfo.GetScale(UnitType.Inches, value); }
    }

    /// <summary>    
    /// Get/Set the size of this mesh in Feet.
    /// </summary>        
    public Vector3 sizeInFeet
    {
        get { return (meshInfo.GetMeshSizeEx() / UnitType.Feet); }
        set { meshInfo.GetTransform().localScale = meshInfo.GetScale(UnitType.Feet, value); }
    }

    /// <summary>    
    /// Get/Set the size of this mesh in meters.
    /// </summary>        
    public Vector3 sizeInMeters
    {
        get { return (meshInfo.GetMeshSizeEx() / UnitType.Meters); }
        set { meshInfo.GetTransform().localScale = meshInfo.GetScale(UnitType.Meters, value); }
    }

    /// <summary>    
    /// Get/Set the size of this mesh in Yards.
    /// </summary>        
    public Vector3 sizeInYards
    {
        get { return (meshInfo.GetMeshSizeEx() / UnitType.Yards); }
        set { meshInfo.GetTransform().localScale = meshInfo.GetScale(UnitType.Yards, value); }
    }

    /// <summary>    
    /// Get/Set the size of this mesh in Kilometers.
    /// </summary>        
    public Vector3 sizeInKilometers
    {
        get { return (meshInfo.GetMeshSizeEx() / UnitType.Kilometers); }
        set { meshInfo.GetTransform().localScale = meshInfo.GetScale(UnitType.Kilometers, value); }
    }

    /// <summary>    
    /// Get/Set the size of this mesh in Miles.
    /// </summary>        
    public Vector3 sizeInMiles
    {
        get { return (meshInfo.GetMeshSizeEx() / UnitType.Miles); }
        set { meshInfo.GetTransform().localScale = meshInfo.GetScale(UnitType.Miles, value); }
    }

    /// <summary>
    /// Get/Set the position of this object in Millimeters.
    /// </summary>    
    public Vector3 positionInMillimeters
    {
        get { return (transform.position / UnitType.Millimeters); }
        set { transform.position = (value * UnitType.Millimeters); }
    }

    /// <summary>
    /// Get/Set the position of this object in Centimeters.
    /// </summary>    
    public Vector3 positionInCentimeters
    {
        get { return (transform.position / UnitType.Centimeters); }
        set { transform.position = (value * UnitType.Centimeters); }
    }

    /// <summary>
    /// Get/Set the position of this object in Inches.
    /// </summary>    
    public Vector3 positionInInches
    {
        get { return (transform.position / UnitType.Inches); }
        set { transform.position = (value * UnitType.Inches); }
    }

    /// <summary>
    /// Get/Set the position of this object in Feet.
    /// </summary>
    public Vector3 positionInFeet
    {
        get { return (transform.position / UnitType.Feet); }
        set { transform.position = (value * UnitType.Feet); }
    }

    /// <summary>
    /// Get/Set the position of this object in Meters.
    /// </summary>
    public Vector3 positionInMeters
    {
        get { return (transform.position / UnitType.Meters); }
        set { transform.position = (value * UnitType.Meters); }
    }

    /// <summary>
    /// Get/Set the position of this object in Yards.
    /// </summary>    
    public Vector3 positionInYards
    {
        get { return (transform.position / UnitType.Yards); }
        set { transform.position = (value * UnitType.Yards); }
    }

    /// <summary>
    /// Get/Set the position of this object in Kilometers.
    /// </summary>    
    public Vector3 positionInKilometers
    {
        get { return (transform.position / UnitType.Kilometers); }
        set { transform.position = (value * UnitType.Kilometers); }
    }

    /// <summary>
    /// Get/Set the position of this object in Miles.
    /// </summary>
    public Vector3 positionInMiles
    {
        get { return (transform.position / UnitType.Miles); }
        set { transform.position = (value * UnitType.Miles); }
    }
}
