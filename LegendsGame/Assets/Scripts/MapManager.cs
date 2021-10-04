using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    //GameObject[] roomInstances;
    Dictionary<Coords, GameObject> roomInstances = new Dictionary<Coords, GameObject>(); 
    int[] roomTypes = {1,2,3};//normal rooms are > 1
    int[] bossRooms = {0};//boss room is only 0 for now.  TODO Can change if we have time to add more bosses.  if only 1 in the end, then just leave
    
    public void InstantiateAllRooms(Coords coord, int roomType)
    {
        Debug.Log("wank");
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
        }catch(KeyNotFoundException e)
        {
            Debug.Log(e);
        }
        
    }

    public int GetRandomRoomType()
    {
        //return type of rooms which are NOT Boss rooms
        return roomTypes[Random.Range(0, roomTypes.Length)];
    }

    public int GetRandomBossRoomType()
    {
        //Return a boss room
        return bossRooms[0];// Random.Range(0, bossRooms.Length)];
    }
}
