using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningCinematicManager : MonoBehaviour
{
    DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        dialogueManager.setcurrentScript(Script.getOpeningScript());
        dialogueManager.InitialiseDialogueManager();
    }

    private void Update()
    {
        dialogueManager.UpdateLogic();
        //Have logic in here to control if the player can move etc
    }
}
