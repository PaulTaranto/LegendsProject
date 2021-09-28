using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalCinematicManager : MonoBehaviour
{
    LevelChanger levelChanger;
    public TMP_Text scriptText;
    public GameObject player;
    //Just in case
    GameObject playerInstance;
    bool isFinishedWritingText = false;
    float timeInbetweenChar;
    float maxTimeInBetweenChar = 0.25f;

    public GameObject playerHeadSprite;
    public GameObject masterHeadSprite;

    private void Start()
    {
        levelChanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
        //TODO make spawn in better position if it doesn't work
        playerInstance = Instantiate(player);
        //Hardcoded 0 value to ensure no bullshit
        scriptText.text = "";
        currentScript = Script.getfinalScript()[0];
        RenderScript();
        timeInbetweenChar = maxTimeInBetweenChar;
    }

    int currentScriptCount = 0;
    private void Update()
    {

        if (!isFinishedWritingText)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                timeInbetweenChar = 0;
                //set the time to wait to 0 to write the script immediately to screen
            }
            RenderScript();
        }

        //If the curent line has finished writing and the player hits space or left mouse button
        if (isFinishedWritingText && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            isFinishedWritingText = false;
            currentScriptCount++;
            count = 0;
            scriptText.text = "";
            talking = "";
            timeInbetweenChar = maxTimeInBetweenChar;
            currentScript = Script.getfinalScript()[currentScriptCount];
        }

        if (timeInbetweenChar > 0)
        {
            timeInbetweenChar -= Time.deltaTime;
        }
    }

    int count = 0;
    string currentScript;
    string talking = "";
    void RenderScript()
    {
        if (timeInbetweenChar <= 0)
        {
            if (count < currentScript.Length)
            {
                if(talking=="")
                {
                    //Logic to figure out who is talking
                    switch (currentScript.Substring(0, 4).ToUpper())
                    {
                        case "PLAY":
                            talking = "PLAY";
                            playerHeadSprite.SetActive(true);
                            masterHeadSprite.SetActive(false);
                            currentScript = currentScript.Substring(6);
                            break;
                        case "MAST":
                            talking = "MAST";
                            playerHeadSprite.SetActive(false);
                            masterHeadSprite.SetActive(true);
                            currentScript = currentScript.Substring(6);
                            break;
                    }
                }

                if (currentScript[count]=='{')
                {
                    //currentScript[count + i - 1] != '}'
                    for (int i = 0; i < currentScript.Length; i++)
                    {
                        //Debug.Log(currentScript[count + i-1]);
                        if(currentScript[count+i]=='}')
                        {
                            EffectHandler(currentScript.Substring(count+1,i-1));
                            //Get the part after the effect
                            currentScript = currentScript.Substring(i+1);
                            break;
                        }
                    }
                }
                scriptText.text += currentScript[count];
                count++;
                if (count == currentScript.Length)
                {
                    isFinishedWritingText = true;
                }
            }
        }
    }

    void EffectHandler(string effect)
    {
        if(effect.Substring(0,3).Equals("EFX"))
        {
            Debug.Log("Playing effect: " + effect.Substring(4));
            switch(effect.Substring(4))
            {
                case "FADE_TO_BLACK":
                    levelChanger.FadeToLevel("Credits");
                    break;
            }
        }
    }
}
