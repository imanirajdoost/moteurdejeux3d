using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A list of cached game objects to avoid using Create/Destroy objects
/// and instead, using Enable/Disable to avoid memory and processing overhead
/// </summary>
public class ObjectPooler : MonoBehaviour
{
    public GameObject pooledObject;         //Object to be pooled
    public int amount;                      //Number of spawned objects of the pooled object at the start of the scene

    private List<GameObject> poolList;      //List of the pooled objects

    private void Start()
    {
        //Instantiate the list and spawn the game objects
        poolList = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            GameObject target = Instantiate(pooledObject);
            target.SetActive(false);
            poolList.Add(target);
        }
    }

    /// <summary>
    /// Returns a pooled object from the list
    /// If no object is available, it will create one
    /// </summary>
    /// <returns>An object from the list</returns>
    public GameObject GetPooledObject()
    {
        if (poolList == null)
            poolList = new List<GameObject>();
        for (int i = 0; i < poolList.Count; i++)
        {
            if (poolList[i] != null && !poolList[i].activeInHierarchy)
                return poolList[i];
        }

        GameObject extra = Instantiate(pooledObject);
        extra.SetActive(false);
        poolList.Add(extra);
        return extra;
    }
}
