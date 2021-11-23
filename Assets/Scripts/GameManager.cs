using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject papaChicken;
    public GameObject condorObject;

    public float maxDistance = 20f;
    public float winDist = 1f;

    public Slider sliderDistance;

    private void Update()
    {
        float dist = CalculateDistance();
        UpdateSlider(dist);
        if (dist < winDist)
            Debug.Log("YOU WONNNNNNNNNNN!");
        if (dist > maxDistance)
            Debug.Log("YOU LOST!!!!!!");
    }

    private float CalculateDistance()
    {
        return 1- Normalize(Vector2.Distance(papaChicken.transform.position, condorObject.transform.position));
    }

    private void UpdateSlider(float val)
    {
        sliderDistance.value = val;
    }

    /// <summary>
    ///  Normalize Distance between [0,1]
    /// result = xi - min(x) / max(x) - min(x)
    /// </summary>
    /// <param name="val">value to be normalized</param>
    /// <returns>normalized value between 0 and 1</returns>
    private float Normalize(float val)
    {
        return val / maxDistance;
    }
}
