using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Generates random objects using object poolers
/// By Iman IRAJ DOOST
/// </summary>
public class Generator : MonoBehaviour
{
    public ObjectPooler[] environementPooler;           //List of environmenets (maps)
    public ObjectPooler selectedEnvironement;           //Selected environement object
    public GameObject destroyer;                        //Destroyer pointer that destroys (disables) objects that are far away
    public GameObject papaChicken;                      //Papa chicken object
    public GameObject[] mapObjects;                     //Starting map environements that should change with environement change

    public float generatorOffset = 100f;                //Offset of the generator
    public float envOffset = 127f;                      //Offset of the environement object

    public GameObject generatorMarker;                  //Generator pointer that starts generating from its current position
    public GameObject lastObject;                       //Last generated object

    public Queue<GameObject> objectsToDestroy;          //Queue that holds all the objects generated to destroy in order

    public delegate void generatorDelegate();
    public event generatorDelegate OnGeneratorStart;    //Event that fires when generator starts

    private int selectedEnvIndex = 0;                   //Currently selected environement index
    private SoundManager soundManager;
    private LevelsManager levelManager;                 //Manages locked/unlocked levels

    public GameObject lockedLevelPanel;                 //Shown when a level is locked
    public TextMeshProUGUI lockedLevelText;             //Text shown on locked level

    private void Awake()
    {
        GetSoundManager();
        GetLevelManager();
    }

    private void OnEnable()
    {
        generatorMarker.transform.position = papaChicken.transform.position;    //Start the generator marker from the position of the player
        objectsToDestroy = new Queue<GameObject>();
        objectsToDestroy.Enqueue(lastObject);
        selectedEnvironement = environementPooler[selectedEnvIndex];
    }

    private void GetLevelManager()
    {
        if (levelManager == null)
            levelManager = FindObjectOfType<LevelsManager>(true);
    }

    private void GetSoundManager()
    {
        if(soundManager == null)
            soundManager = FindObjectOfType<SoundManager>();
    }

    public int GetSelectedMapIndex()
    {
        return selectedEnvIndex;
    }

    /// <summary>
    /// Shows next map
    /// </summary>
    public void NextMap()
    {
        if (environementPooler != null && environementPooler.Length > 0)
        {
            selectedEnvIndex++;
            if (selectedEnvIndex >= environementPooler.Length)
                selectedEnvIndex = 0;
        }
        UpdateSelectedEnvironement();
        GetSoundManager();
        if (soundManager != null)
            soundManager.PlayCameraSound();
    }

    /// <summary>
    /// Shows previous map
    /// </summary>
    public void PreviousMap()
    {
        if (environementPooler != null && environementPooler.Length > 0)
        {
            selectedEnvIndex--;
            if (selectedEnvIndex < 0)
                selectedEnvIndex = environementPooler.Length - 1;
        }
        UpdateSelectedEnvironement();
        GetSoundManager();
        if (soundManager != null)
            soundManager.PlayCameraSound();
    }

    /// <summary>
    /// Updates the selected map on the UI
    /// </summary>
    private void UpdateSelectedEnvironement()
    {
        selectedEnvironement = environementPooler[selectedEnvIndex];

        if (mapObjects != null && mapObjects.Length > 0)
        {
            for (int i = 0; i < mapObjects.Length; i++)
            {
                mapObjects[i].SetActive(false);
                GameObject g = selectedEnvironement.GetPooledObject();
                g.transform.position = mapObjects[i].transform.position;
                mapObjects[i] = g;
                g.SetActive(true);
            }
        }
        GetLevelManager();
        if(levelManager != null)
        {
            if (IsCurrentLevelUnlocked())
            {
                lockedLevelPanel.SetActive(false);
            }
            else
            {
                lockedLevelText.text = "You need " + levelManager.GetCoinsNeededForLevel(selectedEnvIndex) + " Coins to Unlock this Level";
                lockedLevelPanel.SetActive(true);
            }
        }

        ScoreManager.instance.UpdateHighScore(selectedEnvIndex);
    }

    /// <summary>
    /// Checks if currently selected level is unlocked
    /// </summary>
    /// <returns>true if level is unlocked</returns>
    public bool IsCurrentLevelUnlocked()
    {
        GetLevelManager();
        if (levelManager.IsLevelUnlocked(selectedEnvIndex))
            return true;
        return false;
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

            GameObject env = selectedEnvironement.GetPooledObject();
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
