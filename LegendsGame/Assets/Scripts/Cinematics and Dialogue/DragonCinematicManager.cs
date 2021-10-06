using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonCinematicManager : MonoBehaviour
{
    bool isPlayingDialogue = true;
    DialogueManager dialogueManager;

    public void StartDragonDialogue()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        dialogueManager.setcurrentScript(Script.getOpeningDragonFightScript());
        dialogueManager.InitialiseDialogueManager();
    }

    private void Update()
    {
        if(isPlayingDialogue)
        {
            dialogueManager.UpdateLogic();
        }
    }
}
