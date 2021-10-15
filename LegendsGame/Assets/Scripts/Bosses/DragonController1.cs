using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController1 : MonoBehaviour
{
    public GameObject dragonTarget;

    public GameObject dragonDialogueManagerPrefab;
    public GameObject dialogueManagerPrefab;
    GameObject dialogueManagerInstance;
    GameObject dragonDialogueManagerInstance;

    bool hasGeneratedDragons = false;
    //bool isFinishedTalking = false;

    public GameObject headPrefab, neckPrefab;
    Dragon[] dragons;

    //10 to 17 are like the only values actually work
    //NOTE: this is an 
    [Range(10,17)]
    public int numberOfNeckSegments;

    //Don't put max above 3.  Untested, will likely produce shit yet interesting results
    [Range(1,3)]
    public int numberOfDragons;

    public int aliveDragonCount;

    public GameObject finalItem;
    GameObject finalItemInstance;
    public Transform finalItemPosition;
    PlayerMovement player;
    float timer;

    //public Transform[,] spawns;

    bool endingCinematic = false;
    void Start()
    {
        dialogueManagerInstance = Instantiate(dialogueManagerPrefab);
        //The below line of code, as bad as it is.  Sets the transform of the dialogue canvas instantiated above to the empty gameobject representing the currentroom
        //Its only run once so it's fine.
        dialogueManagerInstance.transform.SetParent(GameObject.Find(GameObject.Find("generators").GetComponent<Map1>().GetPlayerCoords().ToString()).transform);
        dragonDialogueManagerInstance = Instantiate(dragonDialogueManagerPrefab);
        dragonDialogueManagerInstance.GetComponent<DragonCinematicManager>().StartDragonDialogue();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        finalItemPosition = GameObject.Find("FinalItemPosition").transform;
        aliveDragonCount = numberOfDragons;
    }

    public void HandleDragonDeath()
    {
        aliveDragonCount--;

        if (aliveDragonCount <= 0)
        {
            //TODO finish this...
            //but what needs doing??? Why don't i explain more to myself??
            //I think it's to implement a death animation
            endingCinematic = true;
            timer = 2;
            player.SetPlayerControl(false);
            finalItemInstance = Instantiate(finalItem, new Vector3(finalItemPosition.position.x, finalItemPosition.position.y + 5), finalItem.transform.rotation);
        }
    }

    private void Update()
    {
        //Debug.Log("dragons:" + hasGeneratedDragons);
        //Debug.Log("dioagloe" + dialogueManagerInstance.GetComponentInChildren<DialogueManager>().isFinishedDialogue);

        //the getcomponent should optimally be cached in a variable as its taxing on the system but its fine for now until it works... optimising later
        if (!hasGeneratedDragons && dialogueManagerInstance.GetComponentInChildren<DialogueManager>().isFinishedDialogue)
        {
            Destroy(dialogueManagerInstance);
            Destroy(dragonDialogueManagerInstance);
            GenerateDragons();
            hasGeneratedDragons = true;
        }

        if(endingCinematic)
        {
            PlayEndingCinematic();
        }
    }

    private void PlayEndingCinematic()
    {
        //It has been tested that the code is able to reach this point upon dragon death
        //aka everything is linked correctly
        float moveSpeed = 1.5f;
        finalItemInstance.transform.position = new Vector3(finalItemInstance.transform.position.x, Mathf.Lerp(finalItemInstance.transform.position.y, finalItemPosition.position.y, moveSpeed * Time.deltaTime));
        float threshold = 0.3f;
        if (finalItemInstance.transform.position.y - finalItemPosition.transform.position.y < threshold)
        {
            timer -= Time.deltaTime;
            if(timer <=0)
            {
                endingCinematic = false;
                player.SetPlayerControl(true);
                //After collection, fade to black
                //then fade in to final cinematic with comedic ending
            }
        }
    }

    //Generate the dragons from the parameters set in the inspector (numberOfNeckSegments and numberOfDragons variables)
    // Spawning from prefab would have worked too, but what's life without a little overcomplication???
    void GenerateDragons()
    {
        //Reusable array setup IK in dragons
        IKManager iKManager;
        IKManager ikForHead;

        //Array to contain the root of each dragon
        dragons = new Dragon[numberOfDragons];

        //Reusable array to spawn in dragons
        GameObject[] temporaryDragonSegments = new GameObject[numberOfNeckSegments]; // used to have a +1 for head segment but it works without so we leaving it

        Vector3 spawnPos;
        float yOffset = 0;

        for (int i = 0; i < numberOfDragons; i++)
        {
            switch(numberOfDragons)
            {
                //No need to run a case for numberOfDragons == 0 as it would just set the yOffset to 0... which already occurs during the yOffset declaration
                case 2:
                    yOffset = 2.5f;
                    break;
                case 3:
                    yOffset = 3;
                    break;
            }
            //should basically guarantee the dragon heads get offset correctly
            yOffset = i % 2 == 0 ? yOffset *= -1 : yOffset;

            //If 3 dragon heads spawn, spawn the last one at y = 0
            yOffset = i == 2 ? 0 : yOffset;

            //determine initial spawn pos for head
            // I don't like that we hard code a few values here (e.g 8 being a value which causes nice positioning determined through trial and error)
            spawnPos = new Vector3(8 - numberOfNeckSegments * neckPrefab.GetComponentInChildren<BoxCollider2D>().size.x * neckPrefab.GetComponentInChildren<BoxCollider2D>().transform.lossyScale.x 
                                    , 0 + yOffset, 0);

            //spawn dragon head and set to first index in array
            temporaryDragonSegments[0] = Instantiate(headPrefab, spawnPos, headPrefab.transform.localRotation);

            //Setup IK
            iKManager = temporaryDragonSegments[0].GetComponents<IKManager>()[0];
            iKManager.joints = new IKJoint[numberOfNeckSegments-3];// - 3 to ensure secondary IK works correctly
            //temporaryDragonSegments[0].GetComponent<IKJoint>().SetStartOffset();

            //Setup secondary IK
            ikForHead = temporaryDragonSegments[0].GetComponents<IKManager>()[1];
            ikForHead.joints = new IKJoint[3];
            ikForHead.joints[ikForHead.joints.Length - 1] = temporaryDragonSegments[0].GetComponent<IKJoint>();
            ikForHead.joints[ikForHead.joints.Length - 1].rotationAxis = new Vector3(0, 0, 1);
            //ikForHead.joints[ikForHead.joints.Length - 1].minAngle = -1.5f;
            //ikForHead.joints[ikForHead.joints.Length - 1].maxAngle = 2f;

            for (int j = 1; j < numberOfNeckSegments; j++)
            {
                //Do math to edit the x position's spawn pos
                spawnPos.x = temporaryDragonSegments[0].transform.position.x + j * neckPrefab.GetComponentInChildren<BoxCollider2D>().size.x * neckPrefab.GetComponentInChildren<BoxCollider2D>().transform.lossyScale.x;

                //Instantiate and store the neck segment
                temporaryDragonSegments[j] = Instantiate(neckPrefab, spawnPos, neckPrefab.transform.localRotation);

                //Set the previous neck segments parent to the new object
                temporaryDragonSegments[j - 1].transform.SetParent(temporaryDragonSegments[j].transform);
                int angle = 30;
                if (j < 3)
                {
                    angle = 30;
                    //Edit the IKJoint of the neck segment
                    ikForHead.joints[ikForHead.joints.Length - j - 1] = temporaryDragonSegments[j].GetComponent<IKJoint>();
                    ikForHead.joints[ikForHead.joints.Length - j - 1].rotationAxis = new Vector3(0, 0, 1);
                
                    ikForHead.joints[ikForHead.joints.Length - j - 1].minAngle = -angle;
                    ikForHead.joints[ikForHead.joints.Length - j - 1].maxAngle = angle;

                    //iKManager.Joints[iKManager.Joints.Length - j - 1]._rotationAxis = 'z';
                    //iKManager.Joints[iKManager.Joints.Length - j - 1].SetStartOffset();

                    temporaryDragonSegments[j].name += j;
                    continue;
                }

                //Edit the IKJoint of the neck segment
                iKManager.joints[iKManager.joints.Length - j - 1 + 3] = temporaryDragonSegments[j].GetComponent<IKJoint>();
                iKManager.joints[iKManager.joints.Length - j - 1 + 3].rotationAxis = new Vector3(0,0,1);

                //if we are spawning in the last 3 segments, give them more range of motion
                if (j == numberOfNeckSegments - 1 || j == numberOfNeckSegments - 2 || j == numberOfNeckSegments - 3)
                {
                    //Give the last neck segment almost complete range of motion
                    angle = j==numberOfNeckSegments - 1 ? 180 : 60;
                }

                iKManager.joints[iKManager.joints.Length - j - 1 + 3].minAngle = -angle;
                iKManager.joints[iKManager.joints.Length - j - 1 + 3].maxAngle = angle;

                //iKManager.Joints[iKManager.Joints.Length - j - 1]._rotationAxis = 'z';
                //iKManager.Joints[iKManager.Joints.Length - j - 1].SetStartOffset();

                temporaryDragonSegments[j].name += j;
            }

            //The for loop below is one of the most necessary lines of code I've ever written.  I'm sure there's a better way but 12:30am me doesn't feel like working it out
            //basically ensures the startOffset variable which is essential for the IK to work is set correctly
            for(int j = 0; j < numberOfNeckSegments; j++)
            {
                temporaryDragonSegments[j].GetComponent<IKJoint>().SetStartOffset();
            }
            //Set dragon head IK to current IK setup
            temporaryDragonSegments[0].GetComponents<IKManager>()[0].joints = iKManager.joints;

            //Set the target transform of the IK system to the head of the dragon
            temporaryDragonSegments[0].GetComponents<IKManager>()[0].targetTransform = Instantiate(dragonTarget);
            temporaryDragonSegments[0].GetComponents<IKManager>()[0].StartDragon();

            //Store the new dragon in the dragons array
            dragons[i] = temporaryDragonSegments[0].GetComponent<Dragon>();
            dragons[i].dragonTarget = temporaryDragonSegments[0].GetComponents<IKManager>()[0].targetTransform;
            dragons[i].baseOfNeck = temporaryDragonSegments[numberOfNeckSegments-1].transform;

            //setup and assign secondary IK system
            temporaryDragonSegments[0].GetComponents<IKManager>()[1].joints = ikForHead.joints;
            temporaryDragonSegments[0].GetComponents<IKManager>()[1].targetTransform = GameObject.FindGameObjectWithTag("Player");
            temporaryDragonSegments[0].GetComponents<IKManager>()[1].StartDragon();
            temporaryDragonSegments[0].GetComponents<IKManager>()[1].angles[2] = 10;
        }
    }
}