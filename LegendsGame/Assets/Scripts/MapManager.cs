using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    Map1 map;
    public GameObject[] roomPrefabs;
    public GameObject[] bossroomPrefabs;
    //GameObject[] roomInstances;
    Dictionary<Coords, GameObject> roomInstances = new Dictionary<Coords, GameObject>(); 
    //int[] roomTypes = {2,3};//normal rooms are > 1
    int[] bossRooms = {0};//boss room is only 0 for now.  TODO Can change if we have time to add more bosses.  if only 1 in the end, then just leave

    public GameObject levelPrefab;
    private Transform transitionPosition;
    public Transform[] cameraTransitionPosition;
    GameObject mainCamera;
//    bool hasFinishedTransition = false;
    public bool isTransitioning = false;
    string transitionDirection = "";
 //   float transitionSpeed;

    Coords currentRoom;
    GameObject player;

    private void Start()
    {
        mainCamera = Camera.main.gameObject;
        map = GetComponent<Map1>();
    }

    void FixedUpdate()
    {
        if(player==null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        //when all items are collected, spawn dragon room in next room
        //"Perhaps if I go through another door, something special will happen"
        //ideas for dragon transition fall through hole in floor
        //      fade to black, footsteps, fade to screen, dragon boss
        if(isTransitioning)
        {
            TransitionCamera(transitionDirection);

            float threshold = 0.15f;
            if (Mathf.Abs(transitionPosition.position.x - mainCamera.transform.position.x) < threshold && Mathf.Abs(transitionPosition.position.y - mainCamera.transform.position.y) < threshold)
            {
                mainCamera.transform.position = transitionPosition.position;
                isTransitioning = false;
                player.transform.position = new Vector2(player.transform.position.x - transitionPosition.position.x, player.transform.position.y - transitionPosition.position.y);
                player.GetComponent<PlayerMovement>().SetPlayerControl(true);
                mainCamera.transform.position = new Vector3(0, 0, mainCamera.transform.position.z);
                roomInstances[currentRoom].transform.position = new Vector3(0, 0, roomInstances[currentRoom].transform.position.z + 1);//DONT REMOVE THIS +1
                map.DeleteOldEnvironment();
            }
        }
    }

    public void StartCameraTransition(string dir)
    {
        isTransitioning = true;
        transitionDirection = dir;
    }

//    float totalDistance = -1, currentDistanceNormalised = -1;

    void SetTransPos(Transform transform)
    {
        transitionPosition = transform;
    }

    private void TransitionCamera(string dir)
    {
        Vector3 move = mainCamera.transform.position;
        switch(dir)
        {
            case "North":
                SetTransPos(cameraTransitionPosition[0]);
                break;
            case "East":
                SetTransPos(cameraTransitionPosition[1]);
                break;
            case "South":
                SetTransPos(cameraTransitionPosition[2]);
                break;
            case "West":
                SetTransPos(cameraTransitionPosition[3]);
                break;
        }
        float rate;
        if (Mathf.Abs(transitionPosition.position.x - move.x) < 2 || Mathf.Abs(transitionPosition.position.y - move.y) < 2)
        {
            rate = 5.5f;
        }
        else
        {
            rate = 1.65f;
        }

        move.x = Mathf.Lerp(move.x, transitionPosition.position.x, rate * Time.deltaTime);
        move.y = Mathf.Lerp(move.y, transitionPosition.position.y, rate * Time.deltaTime);

        mainCamera.transform.position = move;
    }
    //    switch(dir)
    //    {
    //        case "North":
    //            transitionPosition = cameraTransitionPosition[0];
    //            totalDistance = Mathf.Abs(transitionPosition.position.y);
    //            currentDistanceNormalised = camera.transform.position.y / totalDistance;
    //            break;
    //        case "East":
    //            transitionPosition = cameraTransitionPosition[1];
    //            totalDistance = Mathf.Abs(transitionPosition.position.x);
    //            currentDistanceNormalised = camera.transform.position.x / totalDistance;
    //            break;
    //        case "South":
    //            transitionPosition = cameraTransitionPosition[2];
    //            totalDistance = Mathf.Abs(transitionPosition.position.y);
    //            currentDistanceNormalised = camera.transform.position.y / totalDistance;
    //            break;
    //        case "West":
    //            transitionPosition = cameraTransitionPosition[3];
    //            totalDistance = Mathf.Abs(transitionPosition.position.x);
    //            currentDistanceNormalised = camera.transform.position.x / totalDistance;
    //            break;
    //    }

    //    currentDistanceNormalised += rate;
    //    float progress = easeInOutSine(currentDistanceNormalised);

    //    if(dir == "North" || dir == "South")
    //    {   //Don't think it will work giong down.
    //        //Might have to separate this if statement into a north and south so I can * -1
    //        move.y = totalDistance * progress * Time.fixedDeltaTime;
    //    }
    //    else
    //    {
    //        move.x = totalDistance * progress * Time.fixedDeltaTime;
    //    }
    //    camera.transform.position = move;
    //}

    //x must be between 0 and 1
    //private float easeInOutSine(float x)
    //{
    //    return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
    //}

    public void DestoryRoom(Coords coords)
    {
        Destroy(roomInstances[coords]);
        roomInstances.Remove(coords);
    }

    public void InstantiateRoom(Coords coord, int roomType)
    {
        roomInstances.Add(coord, Instantiate(roomPrefabs[roomType]));
        roomInstances[coord].SetActive(true);
        roomInstances[coord].name = coord.ToString();
    }

    public void DisableRoom(Coords coord)
    {
        roomInstances[coord].SetActive(false);
    }

    public void PopulateRoom(Coords roomToSetActive, Coords oldRoomCoords)
    {
        //move room instances to correct position for the transition
        //only disable after the transition
        int x = roomToSetActive.X - oldRoomCoords.X;
        int y = roomToSetActive.Y - oldRoomCoords.Y;
        if (y == 1)
        {
            roomInstances[roomToSetActive].transform.position = new Vector3(cameraTransitionPosition[0].position.x, cameraTransitionPosition[0].position.y, roomInstances[roomToSetActive].transform.position.z);
        }
        else if (x == 1)
        {
            roomInstances[roomToSetActive].transform.position = new Vector3(cameraTransitionPosition[1].position.x, cameraTransitionPosition[1].position.y, roomInstances[roomToSetActive].transform.position.z);
        }
        else if (y == -1)
        {
            roomInstances[roomToSetActive].transform.position = new Vector3(cameraTransitionPosition[2].position.x, cameraTransitionPosition[2].position.y, roomInstances[roomToSetActive].transform.position.z);
        }
        else if (x == -1)
        {
            roomInstances[roomToSetActive].transform.position = new Vector3(cameraTransitionPosition[3].position.x, cameraTransitionPosition[3].position.y, roomInstances[roomToSetActive].transform.position.z);
        }

        currentRoom = roomToSetActive;
        roomInstances[roomToSetActive].SetActive(true);
        try
        {
            //This try catch is probably no longer needed.   Leaving just in case.
            //roomInstances[oldRoomCoords].SetActive(false);
        }
        catch (KeyNotFoundException)
        {
            Debug.Log("Technically a KeyNotFoundException was just thrown in MapManager but its fine if it only throws it once at the start. " +
                "If this is thrown more than once, look into it.");
        }
    }

    public int GetRandomRoomType()
    {
        //return type of rooms which are NOT Boss rooms
        // roomType index of 0 is the boss room
        // roomType index of 1 is the starting room
        // roomType indexes 2-6 are the special rooms containing collectables
        return Random.Range(4, roomPrefabs.Length);
    }

    public int GetRandomCollectableRoomType(List<int> excluding)
    {
        //return type of rooms which are NOT Boss rooms
        // roomType index of 0 is the boss room
        // roomType index of 1 is the starting room
        // roomType indexes 2-6 are the special rooms containing collectables

        int roomID = Random.Range(2, 4);
        if(excluding.Contains(roomID))
        {
            roomID = GetRandomCollectableRoomType(excluding);
        }

        return roomID;
    }



    public int GetRandomBossRoomType()
    {
        //Return a boss room
        return 0;
    }
}
