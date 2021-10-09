using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    public GameObject[] bossroomPrefabs;
    //GameObject[] roomInstances;
    Dictionary<Coords, GameObject> roomInstances = new Dictionary<Coords, GameObject>(); 
    int[] roomTypes = {1,2,3};//normal rooms are > 1
    int[] bossRooms = {0};//boss room is only 0 for now.  TODO Can change if we have time to add more bosses.  if only 1 in the end, then just leave
    
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
        }catch(KeyNotFoundException)
        {
            Debug.Log("Technically a KeyNotFoundException was just thrown in MapManager but its fine if it only throws it once at the start. " +
                "If this is thrown more than once, look into it.");
        }
        
    }

    public int GetRandomRoomType()
    {
        //return type of rooms which are NOT Boss rooms
        //TODO uncomment the rest in below line after debugging
        return roomTypes[0];//Random.Range(0, roomTypes.Length)];
    }

    public int GetRandomBossRoomType()
    {
        //Return a boss room
        return bossRooms[0];// Random.Range(0, bossRooms.Length)];
    }
}
