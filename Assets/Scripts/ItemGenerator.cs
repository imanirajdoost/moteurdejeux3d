using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [Header("Items")]
    public ObjectPooler speedUpPooler;

    [Header("Boundary")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    [Header("Options")]
    public float space;
}
