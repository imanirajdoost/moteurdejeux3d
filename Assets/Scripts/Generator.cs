using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates random objects using object poolers
/// By Iman IRAJ DOOST
/// </summary>
public class Generator : MonoBehaviour
{
    public ObjectPooler environementPooler;             //Pooler of environmenet (map)
    public GameObject destroyer;                        //Destroyer pointer that destroys (disables) objects that are far away
    public GameObject papaChicken;                      //Papa chicken object

    public float generatorOffset = 100f;                //Offset of the generator
    public float envOffset = 127f;                      //Offset of the environement object

    public GameObject generatorMarker;                  //Generator pointer that starts generating from its current position
    public GameObject lastObject;                       //Last generated object

    public Queue<GameObject> objectsToDestroy;          //Queue that holds all the objects generated to destroy in order

    public delegate void generatorDelegate();
    public event generatorDelegate OnGeneratorStart;    //Event that fires when generator starts

    private void Start()
    {
        generatorMarker.transform.position = papaChicken.transform.position;    //Start the generator marker from the position of the player
        objectsToDestroy = new Queue<GameObject>();
        objectsToDestroy.Enqueue(lastObject);
    }

    private void Update()
    {
        //Generate objects when player reaches the generator marker
        if (papaChicken.transform.position.x < generatorMarker.transform.position.x)
        {
            if (OnGeneratorStart != null)
                OnGeneratorStart();
            //Move the generator marker
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

        //If other objects are too far away, disable them
        if(destroyer.transform.position.x < objectsToDestroy.Peek().transform.position.x)
        {
            GameObject g = objectsToDestroy.Dequeue();
            g.SetActive(false);
        }
    }
}
