using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    public GameObject[] bossroomPrefabs;
    //GameObject[] roomInstances;
    Dictionary<Coords, GameObject> roomInstances = new Dictionary<Coords, GameObject>(); 
    int[] roomTypes = {2,3,4};//normal rooms are > 1
    int[] bossRooms = {0};//boss room is only 0 for now.  TODO Can change if we have time to add more bosses.  if only 1 in the end, then just leave

    public GameObject levelPrefab;
    Transform transitionPosition;
    public Transform[] cameraTransitionPosition;
    GameObject camera;
    bool hasFinishedTransition = false;
    bool isTransitioning = false;
    string transitionDirection = "";
    float transitionSpeed;


    private void Start()
    {
        camera = Camera.main.gameObject;
    }

    private void FixedUpdate()
    {
        //when all items are collected, spawn dragon room in next room
        //"Perhaps if I go through another door, something special will happen"

        if(isTransitioning)
        {
            TransitionCamera(transitionDirection);
        }
    }

    public void StartCameraTransition(string dir)
    {
        isTransitioning = true;
        transitionDirection = dir;
    }

    float totalDistance = -1, currentDistanceNormalised = -1;

    private void TransitionCamera(string dir)
    {
        Vector3 move = camera.transform.position;
        float rate = 0.05f;

        switch(dir)
        {
            case "North":
                transitionPosition = cameraTransitionPosition[0];
                totalDistance = Mathf.Abs(transitionPosition.position.y);
                currentDistanceNormalised = camera.transform.position.y / totalDistance;
                break;
            case "East":
                transitionPosition = cameraTransitionPosition[1];
                totalDistance = Mathf.Abs(transitionPosition.position.x);
                currentDistanceNormalised = camera.transform.position.x / totalDistance;
                break;
            case "South":
                transitionPosition = cameraTransitionPosition[2];
                totalDistance = Mathf.Abs(transitionPosition.position.y);
                currentDistanceNormalised = camera.transform.position.y / totalDistance;
                break;
            case "West":
                transitionPosition = cameraTransitionPosition[3];
                totalDistance = Mathf.Abs(transitionPosition.position.x);
                currentDistanceNormalised = camera.transform.position.x / totalDistance;
                break;
        }

        currentDistanceNormalised += rate;
        float progress = easeInOutSine(currentDistanceNormalised);

        if(dir == "North" || dir == "South")
        {   //Don't think it will work giong down.
            //Might have to separate this if statement into a north and south so I can * -1
            move.y = totalDistance * progress * Time.fixedDeltaTime;
        }
        else
        {
            move.x = totalDistance * progress * Time.fixedDeltaTime;
        }
        camera.transform.position = move;
    }

    //x must be between 0 and 1
    private float easeInOutSine(float x)
    {
        return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
    }

public void InstantiateAllRooms(Coords coord, int roomType)
    {
        roomInstances.Add(coord, Instantiate(roomPrefabs[roomType-1]));
        roomInstances[coord].SetActive(false);
        roomInstances[coord].name = coord.ToString();
    }

    public void PopulateRoom(Coords roomToSetActive, Coords oldRoomCoords)
    {
        roomInstances[roomToSetActive].SetActive(true);
        try
        {
            roomInstances[oldRoomCoords].SetActive(false);
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
        return roomTypes[Random.Range(0, roomTypes.Length)];
    }

    public int GetRandomBossRoomType()
    {
        //Return a boss room
        return bossRooms[0];
    }
}
