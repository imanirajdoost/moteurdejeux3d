using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private static readonly string PREF_LEVEL = "score_lvl";

    private int tempScore;

    public Text scoreText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        ResetScore();
    }

    public void ResetScore()
    {
        tempScore = 0;
    }

    public void AddScore(int scoreToAdd)
    {
        tempScore += scoreToAdd;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + tempScore;
    }

    public void SaveHighScore(int sceneIndex)
    {
        if (tempScore > PlayerPrefs.GetInt(PREF_LEVEL + sceneIndex, 0))
            PlayerPrefs.SetInt(PREF_LEVEL+sceneIndex,tempScore);
        ResetScore();
    }
}
