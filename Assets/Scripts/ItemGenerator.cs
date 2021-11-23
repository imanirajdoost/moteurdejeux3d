using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [Header("Items")]
    public ObjectPooler[] objectPoolers;

    [Header("Boundary")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    [Header("Options")]
    public float itemSpace;
    public float numberOfItems;
    public GameObject papaChicken;

    public Generator generatorManager;

    private void Awake()
    {
        generatorManager = FindObjectOfType<Generator>();
    }

    private void OnEnable()
    {
        generatorManager.OnGeneratorStart += GeneratorManager_OnGeneratorStart;
    }

    private void OnDisable()
    {
        generatorManager.OnGeneratorStart -= GeneratorManager_OnGeneratorStart;
    }

    private void GeneratorManager_OnGeneratorStart()
    {
        GenerateStuff();
    }

    private void GenerateStuff()
    {
        float space = 0;
        for (int i = 0; i < numberOfItems; i++)
        {
            //Get one of the poolers randomly
            //A speed up ring or an obstacle, etc.
            int randomIndex = Random.Range(0, objectPoolers.Length);
            Debug.Log("Random index: " + randomIndex);
            GameObject objectToSpawn = objectPoolers[randomIndex].GetPooledObject();

            //Change position randomly
            objectToSpawn.transform.position = new Vector3(generatorManager.transform.position.x - space,
                Random.Range(minY, maxY),
                Random.Range(minX, maxX));

            objectToSpawn.SetActive(true);

            space += itemSpace;
        }
    }

}
