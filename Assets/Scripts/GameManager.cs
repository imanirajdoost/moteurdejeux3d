using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isDead = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

    public GameObject deathScreen;

    public GameObject papaChicken;
    public GameObject condorObject;

    public float maxDistance = 20f;
    public float winDist = 1f;

    public Slider sliderDistance;

    private void Update()
    {
        float dist = CalculateDistance();
        UpdateSlider(dist);
        if (dist < winDist)
            Debug.Log("YOU WONNNNNNNNNNN!");
        if (dist > maxDistance)
            Debug.Log("YOU LOST!!!!!!");

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
