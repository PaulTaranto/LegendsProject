using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Map1 map;

    bool active = true;

    private void Start()
    {
        map = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<Map1>();
    }
    //public Dictionary<Coords, Room> fromTo = new Dictionary<Coords, Room>();
    public Room[] fromTo = new Room[2];
    public void SetFromTo(Room fromRoom, Room toRoom)
    {        
        fromTo[0] = fromRoom;
        fromTo[1] = toRoom;

//        Debug.Log(fromTo[0] + ", " + fromTo[1]);
        //fromTo.Add(fromRoom.coordinates, fromRoom);
        //fromTo.Add(toRoom.coordinates, toRoom);
    }

    public void DisableDoor()
    {
        active = false;
    }

    public void EnableDoor()
    {
        active = true;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            if(active)
            {
                map.SetPlayerCoordsAndMoveRoom(fromTo[1].coordinates);
            }
        }
        //if collision obejct tag is player
        // if the player is in the from room, move them to the TO room
        // and vice versa
        //do all this logic in the Map class
        // i.e map.moveRoom
        // have a fadeout or something idk
    }
}
