using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public static bool IsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        
        //Pause game is escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (IsPaused) {
                Resume();
            }
            else  {
                Pause();
            }
        }

    }

    
    //Resume game
    public void Resume() {
        //Debug.Log("Unpaused");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;

    }

    //Pause game
    void Pause() {
//        Debug.Log("Paused");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        IsPaused = false;
        SceneManager.LoadScene(0);

    }

    public void QuitGame() {
        Debug.Log("Get Outta My Game!");
        Application.Quit();
    }
}
