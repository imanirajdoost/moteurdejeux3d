using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRoad : MonoBehaviour
{
    public GameObject cube1;
    public GameObject cube2;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(cube1.transform.position, cube2.transform.position);
    }
}
