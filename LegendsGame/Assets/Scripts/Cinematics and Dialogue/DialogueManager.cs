using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
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
    public GameObject dragonheadSprite;
    public GameObject dragonheadSpriteMystery;
    AudioSource audio;

    public bool isFinishedDialogue = false;

    public void InitialiseDialogueManager()
    {
        audio = GetComponent<AudioSource>();
        levelChanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
        //TODO make spawn in better position if it doesn't look good
        playerInstance = Instantiate(player);
        //Hardcoded 0 value to ensure no bullshit
        scriptText.text = "";
        currentLine = currentScript[0];
        RenderScript();
        timeInbetweenChar = maxTimeInBetweenChar;
    }

    int currentScriptCount = 0;
    //Call this from another scripts update function when ready
    public void UpdateLogic()
    {
        if(currentScriptCount == currentScript.Length)
        {
            isFinishedDialogue = true;
        }

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
            currentLine = currentScript[currentScriptCount];
        }

        if (timeInbetweenChar > 0)
        {
            timeInbetweenChar -= Time.deltaTime;
        }
    }

    public void setcurrentScript(string[] script)
    {
        currentScript = script;
    }

    int count = 0;
    string currentLine;
    string[] currentScript;
    string talking = "";

    void RenderScript()
    {
        if (timeInbetweenChar <= 0)
        {
            if (count < currentLine.Length)
            {
                if(talking=="")
                {
                    //Logic to figure out who is talking
                    switch (currentLine.Substring(0, 4).ToUpper())
                    {
                        case "PLAY":
                            talking = "PLAY";
                            playerHeadSprite.SetActive(true);
                            masterHeadSprite.SetActive(false);
                            dragonheadSprite.SetActive(false);
                            dragonheadSpriteMystery.SetActive(false);
                            currentLine = currentLine.Substring(6);
                            break;
                        case "MAST":
                            talking = "MAST";
                            playerHeadSprite.SetActive(false);
                            masterHeadSprite.SetActive(true);
                            dragonheadSprite.SetActive(false);
                            dragonheadSpriteMystery.SetActive(false);
                            currentLine = currentLine.Substring(6);
                            break;
                        case "DRAG":
                            talking = "DRAG";
                            playerHeadSprite.SetActive(false);
                            masterHeadSprite.SetActive(false);
                            dragonheadSprite.SetActive(true);
                            dragonheadSpriteMystery.SetActive(false);
                            currentLine = currentLine.Substring(6);
                            break;
                        case "????":
                            talking = "????";
                            playerHeadSprite.SetActive(false);
                            masterHeadSprite.SetActive(false);
                            dragonheadSprite.SetActive(false);
                            dragonheadSpriteMystery.SetActive(true);
                            currentLine = currentLine.Substring(6);
                            break;
                    }
                }

                if (currentLine[count]=='{')
                {
                    //currentScript[count + i - 1] != '}'
                    for (int i = 0; i < currentLine.Length; i++)
                    {
                        //Debug.Log(currentScript[count + i-1]);
                        if(currentLine[count+i]=='}')
                        {
                            EffectHandler(currentLine.Substring(count+1,i-1));
                            //Get the part after the effect
                            currentLine = currentLine.Substring(i+1);
                            break;
                        }
                    }
                }
                scriptText.text += currentLine[count];
                //only play the effect every 5th character
                if(currentLine[count]=='.' || count % 5 == 0)
                {
                    audio.Play();
                }
                count++;
                if (count == currentLine.Length)
                {
                    isFinishedWritingText = true;
                }
            }
        }
    }

    void EffectHandler(string effect)
    {
        //mebbe make this if a swithc too
        if (effect.Substring(0, 3).Equals("EFX"))
        {
            Debug.Log("Playing effect: " + effect.Substring(4));
            switch (effect.Substring(4))
            {
                // Hardcoded to only work in final cinematic, if a fadetoblack in the script is needed elsewhere,
                // mebbe have like "FADE_TO_BLACK_CREDITS"
                // idk, its fine for now
                case "FADE_TO_BLACK_CREDITS":
                    levelChanger.FadeToLevel("Credits");
                    break;
                case "CAMERA_SHAKE":
                    break;
                case "PAUSE":
                    timeInbetweenChar = 0.9f;
                    break;
            }
        }
        else if (effect.Substring(0, 3).Equals("LVL"))
        {
            //TODO if time, really only need to get the fade to level here if we choose to switch level
            //also switching level ALWAYS has a fade effect which shouldn't be an issue for our project but nyeh not happy with the lack of freedom
            Debug.Log("Changing level/scene to: " + effect.Substring(4));
            switch (effect.Substring(4))
            {
                case "CREDITS":
                    levelChanger.FadeToLevel("Credits");
                    break;
                case "GAME":
                    levelChanger.FadeToLevel("Game");
                    break;
            }
        }
        else if (effect.Substring(0, 3).Equals("SFX"))
        {
            switch (effect.Substring(4))
            {

            }
        }
    }
}
