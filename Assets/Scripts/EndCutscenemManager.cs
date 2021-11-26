using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager script for the ending cutscene
/// By: Iman IRAJ DOOST
/// </summary>
public class EndCutscenemManager : MonoBehaviour
{
    #region Public Vars
    [Header("Options")]
    public Camera[] cameras;                //List of all cameras used in cutscene (including main at index 0)
    public GameObject fightEffectParent;    //The parent to instantiate fight effect objects inside
    public int fightEffectCounts = 50;      //Number of effects to spawn
    [Header("Objects")]
    public ObjectPooler fightEffectPooler;  //Pooler object for the effects
    public StressReceiver cameraShaker1;    //Camera shaker script on the first camera
    public StressReceiver cameraShaker2;    //Camera shaker script on the second camera
    public GameObject condorObject;         //Condor that falls on the ground
    public Animator papaChickAnimation;     //papa chicken animator
    public GameObject babyChick;            //baby chicken object
    public GameObject winPanel;             //Congrats winning panel
    #endregion

    #region Private Vars
    private SoundManager soundManager;      //Sound manager object
    private bool shouldShakeCam1 = false;     //Indicates wheather camera 1 should shake or not
    private bool shouldShakeCam2 = false;     //Indicates wheather camera 2 should shake or not
    #endregion

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();        //Cache the sound manager object
    }

    private void Update()
    {
        if (shouldShakeCam1)
            cameraShaker1.InduceStress(0.008f);
        if(shouldShakeCam2)
            cameraShaker2.InduceStress(1f);
    }

    public void StartEndCutscene()
    {
        if (soundManager != null)
            soundManager.StopMainMusic();               //Stop the main music
        shouldShakeCam1 = true;                         //Start shaking the camera
        SwitchCamera(cameras[1]);                       //Disable main camera and zoom into the face of papa chicken!
        if(soundManager != null)
            soundManager.PlayAirplaneSound();           //Play the airplane sound
        StartCoroutine(ShowFightAfter(5f));             //Show the fight scene
        StartCoroutine(DropCondorAfter(10.5f));            //Drop the condor from the air
        StartCoroutine(ShowPapachickAndChildAfter(14f)); //Show papa chicken and its child on the screen for victory
        StartCoroutine(ShowWinPanelAfter(17f));         //Show the winning panel
    }

    #region IEnumerator Methods

    private IEnumerator DropCondorAfter(float t)
    {
        yield return new WaitForSeconds(t);
        condorObject.SetActive(true);
        papaChickAnimation.gameObject.SetActive(true);
        papaChickAnimation.SetBool("Fly",true);
        babyChick.SetActive(true);
    }

    private IEnumerator ShowPapachickAndChildAfter(float t)
    {
        yield return new WaitForSeconds(t);
        SwitchCamera(cameras[3]);               //Switch to final camera
        if (soundManager != null)
            soundManager.PlayVictorySound();    //Play victory sound
    }

    private IEnumerator ShowWinPanelAfter(float t)
    {
        yield return new WaitForSeconds(t);
        winPanel.SetActive(true);
    }

    private IEnumerator ShowFightAfter(float t)
    {
        yield return new WaitForSeconds(t);
        shouldShakeCam1 = false;
        shouldShakeCam2 = true;              //Start shaking the second camera
        papaChickAnimation.gameObject.SetActive(false);
        SwitchCamera(cameras[2]);            //Show the fight camera
        SpawnRandomEffects();
        if (soundManager != null)
            soundManager.PlayPunchSound();  //play the punching sound
    }
    #endregion

    /// <summary>
    /// Enables the camera passed to the method and disables all other cameras
    /// </summary>
    /// <param name="c">Camera to enable</param>
    private void SwitchCamera(Camera c)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == c)
                cameras[i].gameObject.SetActive(true);
            else
                cameras[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Start spawning random effects
    /// </summary>
    private void SpawnRandomEffects()
    {
        StartCoroutine(SpawnObjectsWithDelay());
    }

    /// <summary>
    /// Start spawning random effects with a delay
    /// </summary>
    private IEnumerator SpawnObjectsWithDelay()
    {
        for (int i = 0; i < fightEffectCounts; i++)
        {
            GameObject g = fightEffectPooler.GetPooledObject();
            g.transform.SetParent(fightEffectParent.transform);
            g.transform.localPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f),15f);
            g.SetActive(true);
            yield return new WaitForSeconds(0.05f);
        }

        shouldShakeCam2 = false;
    }
}
