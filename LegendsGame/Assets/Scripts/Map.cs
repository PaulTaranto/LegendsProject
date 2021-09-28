using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int numberOfRooms = 5;
    private int roomCount = 0;
    //public Room[] rooms;
    private List<Coords> allRoomCoords;
    private Dictionary<Coords, Room> rooms = new Dictionary<Coords, Room>();
    public GameObject roomPrefab;
    public Coords currPlayerCoords;

    void Start()
    {
        //rooms = new Room[numberOfRooms];
        currPlayerCoords = new Coords(3, 3);
        GenerateDungeon();
    }

    public void GenerateDungeon()
    {
        //GameObject room = Instantiate(roomPrefab, new Vector3(0,0,0), Quaternion.identity);

        //all true as first room has all doors open
        bool[] roomDoors = { true, true, true, true};
        Room[] connectedRooms = new Room[4];
        int x = 3, y = 3;

        for (int i = 0; i < numberOfRooms; i++)
        {
            //if not creating the initial room
            if(i!=0)
            {
                roomDoors = randomiseDoors();
                //pick random room
                Room roomToCheck = rooms[allRoomCoords[Random.Range(0,allRoomCoords.Count)]];
                //find door that's true
                bool goodDoor = false;
                int doorIndex = -1; //initilise to -1 
                while (goodDoor != true)
                {
                    doorIndex = Random.Range(0, roomToCheck.doors.Length);
                    goodDoor = roomToCheck.doors[doorIndex];
                }

                Room[] test = new Room[4];
                //check if the conencting room is empty
                if(roomToCheck.connectedRooms[doorIndex] == test[4])
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
                }
                //not empty
                else
                {
                    for(int counter = 0; i < 4; i++)
                    {

                    }
                    // if no, pick new door from same room
                    // if all doors from room are taken, pick new room
                    // if by some fucken miracle all rooms are unavailable, set a random door which is closed to open and generate a room there
                }

                // put roomDoors logic here (edit the already randomised bool to make sure the doors don't conflict with already created rooms)
                //put connectedRooms logic here
            }

            //if problem, copy constructor
            Coords coords = new Coords(x, y);

            allRoomCoords.Add(coords);
            rooms.Add(coords, new Room(coords, roomDoors, connectedRooms));
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
