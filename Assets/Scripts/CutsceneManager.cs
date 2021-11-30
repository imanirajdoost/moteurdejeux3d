using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The first cutscene before starting the code
/// By Iman IRAJ DOOST
/// </summary>
public class CutsceneManager : MonoBehaviour
{
    #region Public Vars
    [Header("Cutscene Objects")]
    public Camera[] cameras;                    //Cameras used in cutscene
    public MoveCondor condorManager;            //Condor Manager to change condor's speed
    public ChickenAnimationManager chick;       //Object to change little chicken's animation
    public ChickenAnimationManager papaChick;   //Object to change papa chicken's animation
    public GameObject sliderObject;             //Slider UI object
    public GameObject tutObject;                //Tutorial Game object
    public GameObject mainPlayer;               //Main Player (Chicken)
    public GameObject startText;                //Starting text on UI
    public float condorNewSpeed = 3.5f;         //Condor's speed after the cutscene
    #endregion

    #region Private Vars
    private SoundManager soundManager;          //Sound Manager object
    private bool isStarted = false;             //Check if game is started
    #endregion

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        if (!isStarted)                             //Check if game is started, if not wait for player to press space
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isStarted = true;
                GameManager.instance.Started = true;
                StartCutscene();
            }
    }

    #region Methods

    
    public void StartCutscene()
    {
        if (soundManager != null)
        {
            soundManager.StopMenuMusic();
            soundManager.PlayMainMusic();
        }
        startText.SetActive(false);
        condorManager.gameObject.SetActive(true);
        StartCoroutine(SwitchCameraAfter(cameras[1], 2));
        StartCoroutine(GrabChickAfter(6f));
        StartCoroutine(SwitchCameraAfter(cameras[0], 12f));
        StartCoroutine(SurprisePapaChickenAfter(12f));
        StartCoroutine(StartGameAfter(15f));
    }

    private IEnumerator StartGameAfter(float t)
    {
        yield return new WaitForSeconds(t);
        mainPlayer.SetActive(true);
        papaChick.gameObject.SetActive(false);
        SwitchCamera(cameras[2]);
        condorManager.ChangeSpeed(condorNewSpeed);
        sliderObject.SetActive(true);
        StartCoroutine(ShowTutorialFor(10f));
    }

    private IEnumerator ShowTutorialFor(float t)
    {
        tutObject.SetActive(true);
        yield return new WaitForSeconds(t);
        tutObject.SetActive(false);
    }

    private IEnumerator SurprisePapaChickenAfter(float t)
    {
        yield return new WaitForSeconds(t);
        papaChick.ChangeToSurprise();
    }

    private IEnumerator GrabChickAfter(float t)
    {
        yield return new WaitForSeconds(t);
        condorManager.PickupChick(chick);
    }

    private IEnumerator SwitchCameraAfter(Camera c, float t)
    {
        yield return new WaitForSeconds(t);
        SwitchCamera(c);
    }

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

    #endregion
}
