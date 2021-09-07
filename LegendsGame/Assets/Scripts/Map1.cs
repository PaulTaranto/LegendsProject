using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1 : MonoBehaviour
{
    public int numberOfRooms = 1;
    private int roomCount = 0;
    //public Room[] rooms;
    private List<Coords> allRoomCoords = new List<Coords>();
    private Dictionary<Coords, Room> rooms = new Dictionary<Coords, Room>();
    public GameObject roomPrefab;
    public Coords currPlayerCoords;
    public Transform[] doorPositions;
    public GameObject door;
    public GameObject playerPrefab;
    GameObject player;

    MapManager mapManager;

    public Coords GetPlayerCoords()
    {
        return currPlayerCoords;
    }

    public void SetPlayerCoordsAndMoveRoom(Coords c)
    {
        currPlayerCoords = c;
        player.transform.position = new Vector3(0, 0, 0);
        PopulateCurrentRoom(rooms[c]);
    }

    void Start()
    {
        mapManager = GetComponent<MapManager>();
        //rooms = new Room[numberOfRooms];
        try
        {
            GenerateDungeon();
        }
        catch (System.ArgumentException)
        {
            //potential fix is just to set all doors to true, do the logic, then continue as normal.
            // no more fucky wucky
            Debug.Log("oops");
        }
        
        AssignRooms();
        PopulateCurrentRoom(rooms[allRoomCoords[Random.Range(0,allRoomCoords.Count)]]);
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        for(int i = 0; i < allRoomCoords.Count; i++)
        {
            Debug.Log(rooms[allRoomCoords[i]].ToString());
        }

        player = Instantiate(playerPrefab);
    }

    private void AssignRooms()
    {
        for (int i = 0; i < allRoomCoords.Count; i++)
        {
            rooms[allRoomCoords[i]].roomType = mapManager.SetRandomRoomType();
        }

        //Creates initial room
        GameObject room = Instantiate(roomPrefab);
    }

    //assign types of rooms from mapmanager to each room
    //can be random
    private void PopulateCurrentRoom(Room currentRoom)
    {
        //remove old doors
        GameObject[] doorObjects = GameObject.FindGameObjectsWithTag("Door");
        foreach(GameObject g in doorObjects)
        {
            Destroy(g);
        }

        currPlayerCoords = currentRoom.coordinates;
        Debug.Log("Current room: " + currentRoom.coordinates);
        //spawn in doors and assign them ability to switch between correct rooms
        for(int i = 0; i < 4; i++)
        {
            //If the room has a door in the desired position
            if(currentRoom.doors[i])
            {
                GameObject doorInstance = Instantiate(door, doorPositions[i].position, Quaternion.identity);
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
                //TODO populate door script to connect rooms together   
            }
        }
    }

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
                //logic to add doors, connect rooms, etc
                roomDoors = randomiseDoors();

                //TODO this way of setting the roomdoors could become problematic
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
