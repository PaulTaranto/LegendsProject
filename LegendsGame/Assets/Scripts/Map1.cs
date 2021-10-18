using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1 : MonoBehaviour
{
    public int numberOfRooms = 1;
//    private int roomCount = 0;
    //public Room[] rooms;
    private List<Coords> allRoomCoords = new List<Coords>();
    private Dictionary<Coords, Room> rooms = new Dictionary<Coords, Room>();
    public GameObject roomPrefab;
    public Coords currPlayerCoords, oldPlayerCoords;
    public Transform[] doorPositions;
    public GameObject door;
    public GameObject playerPrefab;
    GameObject player;
    MiniMap minimap;

    public GameObject wallsPrefab;

    public GameObject[] doorSprites;
    public GameObject[] wallSprites;
    public GameObject[] cornerSprites;

    [Range(0.0f, 1.0f)]
    public float xyzScaleSpriteTile;

    MapManager mapManager;

    GameObject wallsInstance;
    GameObject oldWallInstance;
    public GameObject floorPrefab;
    GameObject floorInstance;
    GameObject oldFloorInstance;

    public GameObject environmentParent;
    GameObject[] oldDoors;

    public List<Coords> getAllRoomCoords()
    {
        return allRoomCoords;
    }

    public Coords GetPlayerCoords()
    {
        return currPlayerCoords;
    }

    public void SetPlayerCoordsAndMoveRoom(Coords c)
    {
        oldPlayerCoords = currPlayerCoords;
        currPlayerCoords = c;

        // TODO (Aiden) instead of teleporting to center of room, one solution could be to wait until the player has moved off of the door
        // in the room they wish to enter before activating the ability to enter doors agian.
        // prevents player from immediately going through a room again

        int x = currPlayerCoords.X - oldPlayerCoords.X;
        int y = currPlayerCoords.Y - oldPlayerCoords.Y;
        Vector2 teleportPos = new Vector2(0,0);
        if (y == 1)
        {
            //Should be near the south door relative to the transitino, hence the +12s and +
            teleportPos = new Vector2(doorPositions[2].position.x, doorPositions[2].position.y + 12);
        }
        else if (x == 1)
        {
            //should be near west door
            teleportPos = new Vector2(doorPositions[3].position.x + 20, doorPositions[3].position.y);
        }
        else if (y == -1)
        {
            //should be near north
            teleportPos = new Vector2(doorPositions[0].position.x, doorPositions[0].position.y - 12);
        }
        else if (x == -1)
        {
            //should be near east door
            teleportPos = new Vector2(doorPositions[1].position.x - 20, doorPositions[1].position.y);
        }

        player.transform.position = teleportPos;

        PopulateCurrentRoom(rooms[c]);
    }

    void Start()
    {
        mapManager = GetComponent<MapManager>();
        minimap = GameObject.FindGameObjectWithTag("Minimap").GetComponent<MiniMap>();
        //rooms = new Room[numberOfRooms];
        try
        {
            // TODO (Aiden) I THINK A RANDOM CHANCE TECHNICALLY EXISTS TO CREATE A ROOM WITH NO DOORS ACTIVE
            // would be highly unlikely if it does occur but like its possible
            GenerateDungeon();
        }
        catch (System.ArgumentException)
        {
            // potential fix is just to set all doors to true, do the logic, then continue as normal.
            // no more fucky wucky
            Debug.Log("oops");
        }
        
        AssignRooms();
        PopulateCurrentRoom(rooms[allRoomCoords[Random.Range(0,allRoomCoords.Count)]]);
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        //for(int i = 0; i < allRoomCoords.Count; i++)
        //{
        //    Debug.Log(rooms[allRoomCoords[i]].ToString());
        //}
        player = Instantiate(playerPrefab);
        player.name = "Player";
    }

    public void DeleteOldEnvironment()
    {
        Destroy(oldFloorInstance);
        Destroy(oldWallInstance);
        mapManager.DisableRoom(oldPlayerCoords);

        Destroy(oldDoors[0].transform.parent.gameObject);

        foreach (GameObject g in oldDoors)
        {
            Destroy(g);
        }
    }

    private void AssignRooms()
    {
        for (int i = 0; i < allRoomCoords.Count; i++)
        {
            //First assign all rooms except the boss room.
            //Then afterwards, make the boss room a room at the end of the dungeon in a room which is NOT the players initial spawn room
            rooms[allRoomCoords[i]].roomType = mapManager.GetRandomRoomType();
            //TODO after last item is collected.  Spawn boss rom next
            mapManager.InstantiateAllRooms(allRoomCoords[i], rooms[allRoomCoords[i]].roomType);
            
        }

        for (int i = 0; i < allRoomCoords.Count; i++)
        {
            mapManager.DisableRoom(allRoomCoords[i]);
        }

        // Creates initial room
        GameObject room = Instantiate(roomPrefab, new Vector3(0, 0f, 0), Quaternion.identity);
    }
    bool firstTime = true;

    private void PopulateCurrentRoom(Room currentRoom)
    {
        mapManager.PopulateRoom(currentRoom.coordinates, oldPlayerCoords);

        GameObject environmentInstance = Instantiate(environmentParent);
        environmentInstance.transform.SetParent(GameObject.Find(currentRoom.coordinates.ToString()).transform);

        if(floorInstance != null)
        {
            oldFloorInstance = floorInstance;
        }
        floorInstance = Instantiate(floorPrefab);
        floorInstance.transform.SetParent(GameObject.Find(currentRoom.coordinates.ToString()).transform);

        currPlayerCoords = currentRoom.coordinates;
        //        Debug.Log("Current room: " + currentRoom.coordinates);
        if (wallsInstance != null)
        {
            oldWallInstance = wallsInstance;
        }
        wallsInstance = Instantiate(wallsPrefab);
        wallsInstance.transform.SetParent(GameObject.Find(currentRoom.coordinates.ToString()).transform);

        Vector3 doorPos = new Vector3(0,0,0);
        int x = 0;
        int y = 0;
        if (firstTime)
        {
            firstTime = false;
            minimap.SpawnMinimap(currPlayerCoords, oldPlayerCoords);
        }
        else
        {
            x = currPlayerCoords.X - oldPlayerCoords.X;
            y = currPlayerCoords.Y - oldPlayerCoords.Y;
            player.GetComponent<PlayerMovement>().SetPlayerControl(false);
            if(y == 1)
            {
                floorInstance.transform.position = new Vector2(floorInstance.transform.position.x, floorInstance.transform.position.y + 10);
                wallsInstance.transform.position = new Vector2(wallsInstance.transform.position.x, wallsInstance.transform.position.y + 10);
                mapManager.StartCameraTransition("North");
            }
            else if (x == 1)
            {
                floorInstance.transform.position = new Vector2(floorInstance.transform.position.x + 18, floorInstance.transform.position.y);
                wallsInstance.transform.position = new Vector2(wallsInstance.transform.position.x + 18, wallsInstance.transform.position.y);
                mapManager.StartCameraTransition("East");
            }
            else if (y == -1)
            {
                floorInstance.transform.position = new Vector2(floorInstance.transform.position.x, floorInstance.transform.position.y - 10);
                wallsInstance.transform.position = new Vector2(wallsInstance.transform.position.x, wallsInstance.transform.position.y - 10);
                mapManager.StartCameraTransition("South");
            }
            else if (x == -1)
            {
                floorInstance.transform.position = new Vector2(floorInstance.transform.position.x - 18, floorInstance.transform.position.y);
                wallsInstance.transform.position = new Vector2(wallsInstance.transform.position.x - 18, wallsInstance.transform.position.y);
                mapManager.StartCameraTransition("West");
            }
        }

        minimap.UpdateMiniMap(currPlayerCoords);

        //        Debug.Log("d");


        //TODO Don't do this until after transition
        //remove old door gameobjects


        //TODO do we really need to destory the walls?
        //remove old walls
        //environmentObjects = GameObject.FindGameObjectsWithTag("Wall");
        //foreach (GameObject g in environmentObjects)
        //{
        //    Destroy(g);
        //}

        // spawn in doors and assign them ability to switch between correct rooms

        oldDoors = GameObject.FindGameObjectsWithTag("Door");

        for (int i = 0; i < 4; i++)
        {
            // If the room has a door in the desired position
            if (currentRoom.doors[i])
            {
                if (y == 1)
                {
                    doorPos = new Vector3(doorPositions[i].position.x, doorPositions[i].position.y + 10);
                }
                else if (x == 1)
                {
                    doorPos = new Vector3(doorPositions[i].position.x + 18, doorPositions[i].position.y);
                }
                else if (y == -1)
                {
                    doorPos = new Vector3(doorPositions[i].position.x, doorPositions[i].position.y - 10);
                }
                else if (x == -1)
                {
                    doorPos = new Vector3(doorPositions[i].position.x - 18, doorPositions[i].position.y);
                }
                else if (x == 0 && y == 0)
                {
                    doorPos = new Vector3(doorPositions[i].position.x, doorPositions[i].position.y);
                }

                GameObject doorInstance = Instantiate(doorSprites[i], doorPos, Quaternion.identity);
                doorInstance.transform.SetParent(environmentInstance.transform);
                //environmentParent.SetParent(GameObject.Find(currPlayerCoords.ToString()).transform);
                Door doortemp = doorInstance.GetComponent<Door>();
                switch(i)
                {
                    //north
                    case 0:
                        doortemp.SetFromTo(currentRoom, rooms[new Coords(currentRoom.coordinates.X, currentRoom.coordinates.Y + 1)]);
                        break;
                    //east
                    case 1:
                        doortemp.SetFromTo(currentRoom, rooms[new Coords(currentRoom.coordinates.X + 1, currentRoom.coordinates.Y)]);
                        break;
                    //south
                    case 2:
                        doortemp.SetFromTo(currentRoom, rooms[new Coords(currentRoom.coordinates.X, currentRoom.coordinates.Y - 1)]);
                        break;
                    //west
                    case 3:
                        doortemp.SetFromTo(currentRoom, rooms[new Coords(currentRoom.coordinates.X - 1, currentRoom.coordinates.Y)]);
                        break;
                }
            }
        }

        //Vector3 position = new Vector3();
        //float distance;
        // Spawn in walls
        //TODO drawing in walls probably only needs to be done once
        //for (int i = 0; i < 4; i++)
        //{
        //    switch(i)
        //    {
        //        //north
        //        case 0:
        //            position = new Vector3(-8.27715969f, 4.3151598f, 0);
        //            //Fuck it we're hardcoding
        //            distance = 1.37389f;
        //            //float distanceX = 0.6309826f * 2.56f;
        //            for (int j = 0; j < 13; j++)
        //            {
        //                //position.x += distance;
        //                wallInstance = Instantiate(wallSprites[i], new Vector3(position.x + distance * j, position.y, position.z), Quaternion.identity);
        //                wallInstance.transform.SetParent(environmentParent);
        //            }
        //            break;
        //        case 1://east
        //            position = new Vector3(8.2495594f, 4.28755856f, 0);
        //            distance = 1.37389f;
        //            //float distanceX = 0.6309826f * 2.56f;
        //            for (int j = 0; j < 7; j++)
        //            {
        //                //position.x += distance;
        //                wallInstance = Instantiate(wallSprites[i], new Vector3(position.x, position.y - distance * j, position.z), Quaternion.identity);
        //                wallInstance.transform.SetParent(environmentParent);
        //            }
        //            //put east walls
        //            break;
        //        case 2://south
        //            position = new Vector3(-8.27715969f, -4.3151598f, 0);// - 3.6282148f, 0);
        //            //Fuck it we're hardcoding
        //            distance = 1.37389f;
        //            //float distanceX = 0.6309826f * 2.56f;
        //            for (int j = 0; j < 13; j++)
        //            {
        //                //position.x += distance;
        //                wallInstance = Instantiate(wallSprites[i], new Vector3(position.x + distance * j, position.y, position.z), Quaternion.identity);
        //                wallInstance.transform.SetParent(environmentParent);
        //            }
        //            break;
        //        case 3:
        //            //put west walls
        //            position = new Vector3(-8.2495594f, 4.28755856f, 0);
        //            distance = 1.37389f;
        //            //float distanceX = 0.6309826f * 2.56f;
        //            for (int j = 0; j < 7; j++)
        //            {
        //                //position.x += distance;
        //                wallInstance = Instantiate(wallSprites[i], new Vector3(position.x, position.y - distance * j, position.z), Quaternion.identity);
        //                wallInstance.transform.SetParent(environmentParent);
        //            }
        //            break;

        //    }
        //    //environmentParent.SetParent(GameObject.Find(currPlayerCoords.ToString()).transform);
        //    //Vector2 pos = new Vector2();
        //    //GameObject wallinstance = Instantiate(wallSprites[i], pos, Quaternion.identity);
        //}

        //Spawn in Corners
        //for (int i = 0; i < 4; i++)
        //{
        //    switch (i)
        //    {
        //        case 0:
        //            //top left
        //            position = new Vector3(-8.27715969f, 4.3151598f, 0);
        //            //Fuck it we're hardcoding
        //            break;
        //        case 1:
        //            //top right
        //            position = new Vector3(8.19779968f, 4.3151598f, 0);
        //            break;
        //        case 2:
        //            //bottom right
        //            position = new Vector3(8.19779968f, -4.3151598f, 0);
        //            break;
        //        case 3:
        //            //bottom left
        //            position = new Vector3(-8.2495594f, -4.3151598f, 0);
        //            break;
        //    }
        //    GameObject corner = Instantiate(cornerSprites[i], position, Quaternion.identity);
        //    corner.transform.SetParent(environmentParent);
        //    //Vector2 pos = new Vector2();
        //    //GameObject wallinstance = Instantiate(wallSprites[i], pos, Quaternion.identity);
        //}
    }

    //TODO Next time, generate the rooms such that they connect to each other... THEN add doors.
    // the below function does the opposite: Generates a room based on an available door
    // this eventually causes slowdown at the end of the function when any unused doors have to be culled 
    public void GenerateDungeon()
    {
        //GameObject room = Instantiate(roomPrefab, new Vector3(0,0,0), Quaternion.identity);
        
        //all true as first room has all doors open
        bool[] roomDoors = { true, true, true, true};
        Room[] connectedRooms = new Room[4];
        int x = (int)Mathf.Ceil(numberOfRooms / 2) + 1, y = x;
        Coords coords = new Coords(x, y);
        //currPlayerCoords = new Coords(x, y);

        //rooms to create
        for (int i = 0; i < numberOfRooms; i++)
        {
            //if not creating the initial room
            if(i!=0)
            {
                //TODO this way of setting the roomdoors could become problematic
                //randomise door bool
                roomDoors = randomiseDoors();

                //for loop to try all possible rooms
                //pick random room
                int roomIndex = Random.Range(0, allRoomCoords.Count);
                
                for (int j = 0; j < allRoomCoords.Count; j++)
                {
                    Room roomToCheck = rooms[allRoomCoords[roomIndex]];

                    //pick random door
                    int doorIndex = Random.Range(0, roomToCheck.doors.Length);

                    //for loop 4 times to try all possible doors
                    for (int k = 0; k < 4; k++)
                    {
                        //find door that's true
                        /*bool validDoor = false;
                        while (validDoor != true)
                        {
                            validDoor = roomToCheck.doors[doorIndex];
                        }*/

                        //Room[] test = new Room[4];
                        
                        Coords temp = roomToCheck.coordinates;
                        switch(doorIndex)
                        {
                            //north
                            case 0:
                                temp.Y++;
                                break;
                            //east
                            case 1:
                                temp.X++;
                                break;
                            //south
                            case 2:
                                temp.Y--;
                                break;
                            //west
                            case 3:
                                temp.X--;
                                break;
                        }
                        //check if the conencting room is empty
                        if (!allRoomCoords.Contains(temp))//roomToCheck.connectedRooms[doorIndex] == test[3])
                        {//empty
                         // if yes room connecting, generate room there and set x and y
                            switch (doorIndex)
                            {
                                //north
                                case 0:
                                    y++;
                                    break;
                                //east
                                case 1:
                                    x++;
                                    break;
                                //south
                                case 2:
                                    y--;
                                    break;
                                //west
                                case 3:
                                    x--;
                                    break;
                            }
                            roomDoors[doorIndex] = true;
                        }
                        //not empty
                        else
                        {
                            //cycle through the doors
                            // if no, pick new door from same room
                            doorIndex = doorIndex == 3 ? 0 : doorIndex += 1;
                            continue;
                        }
                    }
                    // if all doors from room are taken, pick new room
                    roomIndex = roomIndex == allRoomCoords.Count-1 ? 0 : roomIndex += 1;
                }


                // if by some fucken miracle all rooms are unavailable, set a random door which is closed to open and generate a room there

                //iterate through all available rooms
                for(int l = 0; l < allRoomCoords.Count; l++)
                {
                    //var to increment between north, east, south, west
                    int incrementer = Random.Range(0,4);
                    int count = 0;
                    bool overrider = false;
                    //while the overrider is false AND while the indexed door is true AND the count is less than 4
                    
                    while (overrider == false && rooms[allRoomCoords[l]].doors[incrementer] == true && count < 4 )
                    {
                        //logic to check if the room we tryna create a room in is invalid
                        Coords temp = rooms[allRoomCoords[l]].coordinates;
                        switch (incrementer)
                        {
                            //north
                            case 0:
                                temp.Y++;
                                roomDoors[2] = true;//south
                                break;
                            //east
                            case 1:
                                temp.X++;
                                roomDoors[3] = true;//west
                                break;
                            //south
                            case 2:
                                temp.Y--;
                                roomDoors[0] = true;//north
                                break;
                            //west
                            case 3:
                                temp.X--;
                                roomDoors[1] = true;//east
                                break;
                        }
                        // if allRoomCoords doesn't contain the position we are trying to create a new room in
                        if (!allRoomCoords.Contains(temp))
                        {
                            overrider = true;
                            //Debug.Log("we getting here g");
                            //Debug.Log("tempx" + temp.X);
                            coords.X = temp.X;
                            //Debug.Log("tempy" + temp.Y);
                            coords.Y = temp.Y;
                            break;
                        }
                        count++;
                        incrementer = incrementer == 3 ? 0 : incrementer += 1;
                    }
                    //the loop will only break when a valid room is found with a door equal to false
                    //hence set the door to true and this is the door we'll use to generate the new room
                    rooms[allRoomCoords[l]].doors[incrementer] = true;
                }
                // put roomDoors logic here (edit the already randomised bool to make sure the doors don't conflict with already created rooms)
                //put connectedRooms logic here
                //coords = new Coords(x, y);
                //north
                if (allRoomCoords.Contains(new Coords(coords.X, coords.Y + 1)) && roomDoors[0] == true)
                {
                    connectedRooms[0] = rooms[new Coords(coords.X, coords.Y + 1)];
                }
                //east
                if (allRoomCoords.Contains(new Coords(coords.X + 1, coords.Y)) && roomDoors[1] == true)
                {
                    connectedRooms[1] = rooms[new Coords(coords.X + 1, coords.Y)];
                }
                //south
                if (allRoomCoords.Contains(new Coords(coords.X, coords.Y - 1)) && roomDoors[2] == true)
                {
                    connectedRooms[2] = rooms[new Coords(coords.X, coords.Y - 1)];
                }
                //west
                if (allRoomCoords.Contains(new Coords(coords.X - 1, coords.Y)) && roomDoors[3] == true)
                {
                    connectedRooms[3] = rooms[new Coords(coords.X - 1, coords.Y)];
                }
            }
            
            allRoomCoords.Add(coords);
            Room roomToCreate = new Room(coords, roomDoors, connectedRooms);
            //Debug.Log("Adding: " + roomToCreate.ToString());
            //Debug.Log(coords.X + ", " + coords.Y);
            //Debug.Log(i);
            rooms.Add(coords, roomToCreate);
        }
        //Purge any unused doors

        //potential slow down with checking all rooms to 
        for (int roomToPurge = 0; roomToPurge < allRoomCoords.Count; roomToPurge++)
        {
            for (int doorCount = 0; doorCount < 4; doorCount++)
            {
                //if there is a door there
                if (rooms[allRoomCoords[roomToPurge]].doors[doorCount])
                {
                    switch (doorCount)
                    {
                        case 0:
                            //if the door does not connect to a coordinate found in the allRoomCoords list.
                            if (!allRoomCoords.Contains(new Coords(rooms[allRoomCoords[roomToPurge]].coordinates.X, rooms[allRoomCoords[roomToPurge]].coordinates.Y + 1)))
                            {
                                //set the door to false;
                                rooms[allRoomCoords[roomToPurge]].doors[doorCount] = false;
                            }
                            break;
                        case 1:
                            if (!allRoomCoords.Contains(new Coords(rooms[allRoomCoords[roomToPurge]].coordinates.X + 1, rooms[allRoomCoords[roomToPurge]].coordinates.Y)))
                            {
                                rooms[allRoomCoords[roomToPurge]].doors[doorCount] = false;
                            }
                            break;
                        case 2:
                            if (!allRoomCoords.Contains(new Coords(rooms[allRoomCoords[roomToPurge]].coordinates.X, rooms[allRoomCoords[roomToPurge]].coordinates.Y - 1)))
                            {
                                rooms[allRoomCoords[roomToPurge]].doors[doorCount] = false;
                            }
                            break;
                        case 3:
                            if (!allRoomCoords.Contains(new Coords(rooms[allRoomCoords[roomToPurge]].coordinates.X - 1, rooms[allRoomCoords[roomToPurge]].coordinates.Y)))
                            {
                                rooms[allRoomCoords[roomToPurge]].doors[doorCount] = false;
                            }
                            break;
                    }
                }
            }
        }
    }

    private int chooseDoor()
    {
        int returnVal = -1;
       
        return returnVal;
    }

    private int chooseDoor(int excluding)
    {
        int returnVal = -1;
        while(returnVal != excluding)
        {

        }
        return returnVal;
    }

    private bool[] randomiseDoors()
    {
        bool[] doors = new bool[4];
        doors[0] = Random.Range(0, 2) == 1;
        doors[1] = Random.Range(0, 2) == 1;
        doors[2] = Random.Range(0, 2) == 1;
        doors[3] = Random.Range(0, 2) == 1;
        return doors;
    }
}
