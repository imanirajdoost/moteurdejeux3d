using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public Camera[] cameras;
    public MoveCondor condorManager;
    public ChickenAnimationManager chick;
    public ChickenAnimationManager papaChick;

    public GameObject mainPlayer;

    private void Start()
    {

    }

    public void StartCutscene()
    {
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
        SwitchCamera(cameras[2]);
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
}
