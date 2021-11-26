using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Disables the object after a certain amount of time
/// By Iman IRAJ DOOST
/// </summary>
public class DisableAfter : MonoBehaviour
{
    #region Private Vars
    [SerializeField] private float timeToDisable = 2f;      //Time to pass before disabling the object
    #endregion

    #region Methods

    private void OnEnable()
    {
        StartCoroutine(DisableMeAfter(timeToDisable));
    }

    /// <summary>
    /// Disable the object after t seconds
    /// </summary>
    /// <param name="t">Seconds to disable the object</param>
    /// <returns></returns>
    private IEnumerator DisableMeAfter(float t)
    {
        yield return new WaitForSeconds(t);
        gameObject.SetActive(false);
    }
    #endregion
}
