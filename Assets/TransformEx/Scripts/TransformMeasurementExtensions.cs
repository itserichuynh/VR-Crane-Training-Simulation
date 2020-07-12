/* 
 * TransformMeasurementExtensions 1.0
 * 
 * This script was made by Jason Peterson (DarkAkuma) of http://darkakuma.z-net.us/
*/

using UnityEngine;
using System.Collections;

/// <summary>
/// Extend Transforms to include options for converting it from Unity units to real measurments.
/// </summary>
public static class TransformMeasurementExtensions
{
    static MeshInfo meshInfo;
    static Transform transform;

    /// <summary>
    /// Provides access to methods for managing the position and size of the mesh(es) of this transform in real world units of measurment.
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static TransformExt TransformEx(this Transform t)
    {
        return new TransformExt(t, GetMeshInfo(t));
    }
    
    /// <summary>
    /// Gets the meshInfo for the transform.
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static MeshInfo GetMeshInfo(this Transform t)
    {
        if (meshInfo == null || t != transform)
        {        
            meshInfo = new MeshInfo(t);            
            transform = t;
        }
             
         return meshInfo;
    }
}