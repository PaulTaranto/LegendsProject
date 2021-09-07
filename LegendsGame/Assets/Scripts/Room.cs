

using System.Collections.Generic;

public class Room
{
    public Room(Coords coords, bool[] doorArray, Room[] connectedRoomsArray)
    {
        doors = doorArray;
        coordinates = coords;
        connectedRooms = connectedRoomsArray;
    }

    public bool[] doors { get; }
    public Coords coordinates = new Coords(-1, -1);
    public Room[] connectedRooms { get; }
    public int roomType;

    public override string ToString()
    {
        string s = $"";
        s += $"Room at {coordinates.X}, {coordinates.Y}.";
        s += $"North: ({doors[0]}), ";//, ({connectedRooms[0].coordinates.X==-1})";
                                   //},{connectedRooms[0].coordinates.Y})),";
        s += $"East: ({doors[1]}), ";//, ({connectedRooms[1].coordinates.X},{connectedRooms[1].coordinates.Y})), ";
        s += $"South: ({doors[2]}), ";//, ({connectedRooms[2].coordinates.X},{connectedRooms[2].coordinates.Y})), ";
        s += $"West: ({doors[3]})";//, ({connectedRooms[3].coordinates.X},{connectedRooms[3].coordinates.Y})))";
        return s;
    }
    public void SetConnectedRoom(int index, Room room) => connectedRooms[index] = room;

    public static bool Empty()
    {

        return false;
    }

    public static bool operator ==(Room lhs, Room rhs)
    {
        if(lhs.doors == rhs.doors)
        {
            if(lhs.coordinates == rhs.coordinates)
            {
                if(lhs.connectedRooms == rhs.connectedRooms)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool operator !=(Room lhs, Room rhs)
    {
        if (lhs.doors == rhs.doors)
        {
            if (lhs.coordinates == rhs.coordinates)
            {
                if (lhs.connectedRooms == rhs.connectedRooms)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
/*
public class Room
{
    //private bool[] doors;

    //the dictionary saying which door connects to which room
    private Dictionary<bool, Room> doors = new Dictionary<bool, Room>();
    private int[] position = new int[2];

    public void SetDoors(bool[] doorArray)
    {
        //doors = doorArray;
        int count = 0;
        foreach(bool b in doors.Keys)
        {
            b = doorArray[count];
            count++;
        }
    }

    public void SetPosition(int[] pos)
    {
        position = pos;
    }

    public void GenerateRoom()
    {

    }
}
*/