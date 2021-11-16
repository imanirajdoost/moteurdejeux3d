using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject pooledObject;
    public int amount;

    private List<GameObject> poolList;

    private void Start()
    {
        poolList = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            GameObject target = Instantiate(pooledObject);
            target.SetActive(false);
            poolList.Add(target);
        }
    }

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
