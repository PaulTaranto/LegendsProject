using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //TODO ask if prefabs better or to draw in code
    public void PopulateRoom(int roomType)
    {
        switch(roomType)
        {
            case 1:
                Room1();
                break;
        }
    }

    private void Room1()
    {

    }

    public int SetRandomRoomType()
    {
        return 0;
    }
}
