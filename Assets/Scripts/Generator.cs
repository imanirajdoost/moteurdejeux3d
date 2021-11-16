using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public ObjectPooler environementPooler;
    public GameObject destroyer;
    public GameObject papaChicken;

    public float generatorOffset = 100f;
    public float envOffset = 127f;

    public GameObject generatorMarker;
    public GameObject lastObject;

    public Queue<GameObject> objectsToDestroy;

    private void Start()
    {
        generatorMarker.transform.position = papaChicken.transform.position;
        objectsToDestroy = new Queue<GameObject>();
        objectsToDestroy.Enqueue(lastObject);
    }

    private void Update()
    {
        if (papaChicken.transform.position.x < generatorMarker.transform.position.x)
        {
            generatorMarker.transform.position = new Vector3(generatorMarker.transform.position.x - generatorOffset,
                generatorMarker.transform.position.y,
                generatorMarker.transform.position.z);

            GameObject env = environementPooler.GetPooledObject();
            env.transform.position = new Vector3(lastObject.transform.position.x - envOffset,
                lastObject.transform.position.y, lastObject.transform.position.z);
            objectsToDestroy.Enqueue(env);
            lastObject = env;
            env.SetActive(true);
        }


        if(destroyer.transform.position.x < objectsToDestroy.Peek().transform.position.x)
        {
            GameObject g = objectsToDestroy.Dequeue();
            g.SetActive(false);
        }
    }
}
