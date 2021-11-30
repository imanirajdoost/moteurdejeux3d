using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class pause : MonoBehaviour
{
    public string mainMenuScene;
    public GameObject pauseMenu;
    public bool isPaused;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //si quand le joueur appuit il est deja en pause alors le jeu reprend 
            if (isPaused)
            {
                isPaused = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                GameManager.instance.IsPaused = true;

            }
            else
            {
                //mise en pause du jeu 
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
            
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
