using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    
    public int health;
    public int numberOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [SerializeField] private GameObject gameOverScreen;
    private bool isGameOver = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(health > numberOfHearts) {
            health = numberOfHearts;
        }

        //Test if lose heart when damaged
        if (Input.GetKeyDown(KeyCode.Space)) {
            TakeDamage(1);
        }

        for (int i = 0; i < hearts.Length; i++) {

            if(i < health) {
                hearts[i].sprite = fullHeart;
            }
            else {
                hearts[i].sprite = emptyHeart;
            }

            if(i < numberOfHearts) {
                hearts[i].enabled = true;
            }
            else {
                hearts[i].enabled = false;
            }
        }

        //Check if health is 0
        if (health == 0) {
            isGameOver = true;
        }

        if (isGameOver) {
            gameOverScreen.SetActive(true);
        }
    }

    //Take damage
    void TakeDamage(int damage) {
        health -= damage;
        if (health < 1) {
            Debug.Log("Game Over");
        }

    }



    
}
