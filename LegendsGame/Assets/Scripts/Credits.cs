using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Credits : MonoBehaviour
{
    float rate = 175;
    bool canMove = true;

    void Update()
    {
        if(canMove)
        {
            transform.position += new Vector3(0, 1, 0) * rate * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rate = 1000;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                rate = 175;
            }
        }

        if(GetComponent<RectTransform>().anchoredPosition.y > -20)
        {
            if(gameObject.tag == "Undefined")
            {
                canMove = false;
            }
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
