using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    //Starts game by loading game scene
    public void PlayGame() {

        SceneManager.LoadScene(1);

    }

    //Exits the game
    public void QuitGame() {

        Debug.Log("Get Outta My Game!");
        Application.Quit();
    }

}
