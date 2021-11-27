using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Disables object after some time
/// By Iman IRAJ DOOST
/// </summary>
public class Disabler : MonoBehaviour
{
    private GameObject papaChicken;
    public float offset = 5;

    private void OnEnable()
    {
        if (papaChicken == null)
            papaChicken = FindObjectOfType<Charateremodel>(true).gameObject;
    }

    private void Update()
    {
        if (papaChicken.transform.position.x + offset < transform.position.x)
            gameObject.SetActive(false);
    }
}
