using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages different states of the game
/// This is a singletone class
/// Written by: Iman IRAJ DOOST
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;     //Singletone object

    public bool isDead = false;             //Wheather or not the player is dead
    public bool isWon = false;              //Wheather or not the player has won

    public GameObject deathScreen;

    public GameObject papaChicken;
    public GameObject condorObject;

    public float maxDistance = 20f;
    public float winDist = 1f;

    public Slider sliderDistance;

    private EndCutscenemManager endSceneManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);

        if (endSceneManager == null)
            endSceneManager = FindObjectOfType<EndCutscenemManager>();
    }

    private void Win()
    {
        Debug.Log("YOU WONNNNNNNNNNN!");
        if (!isWon)
        {
            isWon = true;
            endSceneManager.StartEndCutscene();
            papaChicken.SetActive(false);
            condorObject.SetActive(false);
        }

    }

    private void Lose()
    {
        Debug.Log("YOU lost!!");
    }

    private void Update()
    {
        float NormalDist = CalculateDistance();
        UpdateSlider(NormalDist);
        float dist = Vector2.Distance(papaChicken.transform.position,condorObject.transform.position);
        if (dist < winDist)
            Win();
        if (dist > maxDistance)
            Lose();

        if (isDead)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void GameOver()
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0;
        isDead = true;
    }

    private float CalculateDistance()
    {
        return 1- Normalize(Vector2.Distance(papaChicken.transform.position, condorObject.transform.position));
    }

    private void UpdateSlider(float val)
    {
        sliderDistance.value = val;
    }

    /// <summary>
    ///  Normalize Distance between [0,1]
    /// result = xi - min(x) / max(x) - min(x)
    /// </summary>
    /// <param name="val">value to be normalized</param>
    /// <returns>normalized value between 0 and 1</returns>
    private float Normalize(float val)
    {
        return val / maxDistance;
    }
}
