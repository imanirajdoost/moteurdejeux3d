using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages item generation
/// </summary>
public class ItemGenerator : MonoBehaviour
{
    [Header("Items")]
    public ObjectPooler[] objectPoolersEasy;    //Object poolers for easy levels
    public ObjectPooler[] objectPoolersMedium;  //Object poolers for medium levels
    public ObjectPooler[] objectPoolersHard;    //Object poolers for hard levels

    private ObjectPooler[] selectedPooler;      //Object pooler selected based on level difficulty

    [Header("Boundary")]
    public float minX;          //Boundary of the spawned objects
    public float maxX;
    public float minY;
    public float maxY;

    [Header("Options")]
    public float itemSpace;         //Space between generated objects
    public float numberOfItems;     //Max objects to spawn before spawning again
    public GameObject papaChicken; 

    public Generator generatorManager;

    private int difficultyIndex = 0;    //difficulty index based on the level index

    private void Awake()
    {
        FindGeneratorManager();
    }

    private void UpdateDifficulty()
    {
        //Update pooler based on the selected map (change difficulty)
        selectedPooler = new ObjectPooler[objectPoolersEasy.Length];
        switch (generatorManager.GetSelectedMapIndex())
        {
            case 0:
            case 1:
                for (int i = 0; i < selectedPooler.Length; i++)
                    selectedPooler[i] = objectPoolersEasy[i];
                break;
            case 2:
                for (int i = 0; i < selectedPooler.Length; i++)
                    selectedPooler[i] = objectPoolersMedium[i];
                break;
            case 3:
            case 4:
                for (int i = 0; i < selectedPooler.Length; i++)
                    selectedPooler[i] = objectPoolersHard[i];
                break;
            default:
                for (int i = 0; i < selectedPooler.Length; i++)
                    selectedPooler[i] = objectPoolersEasy[i];
                break;
        }
    }

    private void FindGeneratorManager()
    {
        generatorManager = FindObjectOfType<Generator>(true);
    }

    private void OnEnable()
    {
        if (generatorManager == null)
            FindGeneratorManager();
        //Start when the generator starts
        generatorManager.OnGeneratorStart += GeneratorManager_OnGeneratorStart;
    }

    private void OnDisable()
    {
        generatorManager.OnGeneratorStart -= GeneratorManager_OnGeneratorStart;
    }

    private void GeneratorManager_OnGeneratorStart()
    {
        UpdateDifficulty();
        GenerateStuff();
    }

    private void GenerateStuff()
    {
        float space = 0;
        for (int i = 0; i < numberOfItems; i++)
        {
            //Get one of the poolers randomly
            //A speed up ring or an obstacle, etc.
            int randomIndex = Random.Range(0, selectedPooler.Length);
            //Debug.Log("Random index: " + randomIndex);
            if (selectedPooler.Length > 0 && randomIndex < selectedPooler.Length)
            {


                GameObject objectToSpawn = selectedPooler[randomIndex].GetPooledObject();

                //Change position randomly
                objectToSpawn.transform.position = new Vector3(generatorManager.transform.position.x - space,
                    Random.Range(minY, maxY),
                    Random.Range(minX, maxX));

                objectToSpawn.SetActive(true);

                space += itemSpace;
            }
        }
    }

}
