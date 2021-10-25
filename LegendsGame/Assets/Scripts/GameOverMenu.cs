using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    //Loads player back into game (skipping intro scene)
    public void PlayGame() {

        //SceneManager.LoadScene("OpeningScene");
        SceneManager.LoadScene("Game");

    }

    //Loads main menu
    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
    }

    //Exits the game
    public void QuitGame() {

        Debug.Log("Get Outta My Game!");
        Application.Quit();
    }

}
