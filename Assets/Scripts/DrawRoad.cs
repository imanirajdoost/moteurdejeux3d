using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to draw a debug line so that we can see the
/// path that we can take
/// By Iman IRAJ DOOST
/// </summary>
public class DrawRoad : MonoBehaviour
{
    public GameObject cube1;        //Starting Point
    
    /// <summary>
    /// Draw a line from the starting point
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(cube1.transform.position, 10000 * Vector3.left);
    }
}
