using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCinematicManager : MonoBehaviour
{
    DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        dialogueManager.setcurrentScript(Script.getfinalScript());
        dialogueManager.InitialiseDialogueManager(false);
    }

    private void Update()
    {
        dialogueManager.UpdateLogic();
        //Have logic in here to control if the player can move etc
    }
}
