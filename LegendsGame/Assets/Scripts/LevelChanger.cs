using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    private int levelToLoadInt = -1;
    private string levelToLoad;

    public void FadeToLevel(int levelIndex)
    {
        animator.SetTrigger("FadeOut");
        levelToLoadInt = levelIndex;
    }

    public void FadeToLevel(string levelIndex)
    {
        animator.SetTrigger("FadeOut");
        levelToLoad = levelIndex;
    }

    public void OnFadeComplete()
    {
        if (levelToLoadInt == -1)
        {
            //Load from string
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            //Load from int
            SceneManager.LoadScene(levelToLoadInt);
        }
    }
}
