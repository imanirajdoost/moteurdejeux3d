using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages different states of the game
/// This is a singletone class
/// Written by: Iman IRAJ DOOST and Ahmad JREDA
/// </summary>
public class GameManager : MonoBehaviour
{
    public bool isDebug;                    //Used for some scripts to unlock supervisor mode and to return all unlocked things

    public static GameManager instance;     //Singletone object

    [Header("Options")]
    public bool isDead = false;             //Wheather or not the player is dead
    public bool isWon = false;              //Wheather or not the player has won
    public bool IsPaused = false;           //Wheather or not the game is paused 
    public bool Started = false;            //Wheather or not the lvl is started
    public bool CanRestart = false;

    public GameObject deathScreen;          //Screen to show when player fails

    public GameObject papaChicken;          //Papa chicken object
    public GameObject condorObject;         //Condor object

    [Header("Win and Lose Options")]
    public float maxDistance = 20f;         //Max Distance to be from the condor (Player will lose if they are further away)
    public float winDist = 1f;              //Minimum distance from condor to win the game

    [Header("UI")]
    public Slider sliderDistance;           //Slider that shows player's distance to condor
    public Text coinTextUI;                 //Text UI that shows the number of coins that player has collected


    public int nbcoin = 0;                  //Number of collected coins
    private EndCutscenemManager endSceneManager;
    private CutsceneManager startCutsceneManager;
    private Generator generator;

    private static readonly string SAVED_COIN = "SavedCoin";

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);

        if (endSceneManager == null)
            endSceneManager = FindObjectOfType<EndCutscenemManager>();

        if (startCutsceneManager == null)
            startCutsceneManager = FindObjectOfType<CutsceneManager>();

        if (generator == null)
            generator = FindObjectOfType<Generator>(true);
        nbcoin = PlayerPrefs.GetInt(SAVED_COIN, 0);
        UpdateUI();
    }

    /// <summary>
    /// Adds a coin to the inventory and updates UI
    /// </summary>
    public void AddCoin()
    {
        nbcoin++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        coinTextUI.text = nbcoin.ToString();
    }

    void saveCoin()
    {
        PlayerPrefs.SetInt(SAVED_COIN, nbcoin);
    }

    public int GetCurrentCoinCount()
    {
        return GameManager.instance.nbcoin;
    }

    /// <summary>
    /// Show the ending cutscene when player wins
    /// </summary>
    private void Win()
    {
        if (isDead)
        {
            Debug.Log("You can't win when you are dead!");
            return;
        }
            
        Debug.Log("YOU WON!");
        if (!isWon)
        {
            saveCoin();
            isWon = true;
            StartCoroutine(waitAnimationForRestart(15));
            //papaChicken.GetComponent<Animator>().SetBool("CondorColide", true);
            ScoreManager.instance.SaveHighScore(generator.GetSelectedMapIndex());
            endSceneManager.StartEndCutscene();
            papaChicken.SetActive(false);
            condorObject.SetActive(false);
            //Started = false;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    private IEnumerator waitAnimationForRestart(float t)
    {
        yield return new WaitForSeconds(t);
        CanRestart = true;
    }

    /// <summary>
    /// Show game over when player loses
    /// </summary>
    private void Lose()
    {
        Debug.Log("YOU lost!!");
        GameOver();
    }

    private void Update()
    {
        if (!Started && !isDead)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                generator.NextMap();
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                generator.PreviousMap();
            if (Input.GetKeyDown(KeyCode.Space))        //Start game
            {
                if (generator.IsCurrentLevelUnlocked())
                {
                    Started = true;
                    ScoreManager.instance.UpdateUI();
                    startCutsceneManager.StartCutscene();
                }
            }
                return;
        }
        if (CanRestart)
        {
            if (Input.GetKeyDown(KeyCode.Space))        //Start game
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }

        //Calculate distance from player to condor each frame to see if player has won/lost
        //Get the normal of the distance [0,1] and update the slider accordingly
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
                Started = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }


    }

    /// <summary>
    /// What happens when player's game is over
    /// </summary>
    public void GameOver()
    {
        if (isDebug)
            return;
        ScoreManager.instance.SaveHighScore(generator.GetSelectedMapIndex());
        deathScreen.SetActive(true);
        Time.timeScale = 0;
        Started = false;
        isDead = true;
    }

    /// <summary>
    /// Normalizes the distance between player and condor between [0,1] to update the slider accordingly
    /// </summary>
    /// <returns>Normalized distance between 0 and 1</returns>
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
