using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    Map1 map;
    List<Coords> roomCoords;
    //Coords currPlayerCoords;
    public Transform minimapCenter;
    public GameObject minimapNode;
    int center;

    GameObject[] nodes;

    float xmultiplier = 1.075f;
    float ymultiplier = 1.15f;

    //liams a movie buff
    public void SpawnMinimap(Coords currentPlayerCoordinate, Coords oldPlayerCoords)
    {
        map = GameObject.FindGameObjectWithTag("MapGenerator").GetComponent<Map1>();
        //currPlayerCoords = map.GetPlayerCoords();
        center = (int)Mathf.Ceil(map.numberOfRooms / 2) + 1;
        nodes = new GameObject[map.numberOfRooms];
        roomCoords = map.getAllRoomCoords();

        //Keep the active cell in the center of the minimap at all times
        //Use the minimap background as a mask to cut out rest of minimap
        //currPlayerCoords = currentPlayerCoordinate;
        for (int i = 0; i < roomCoords.Count; i++)
        {
            //(x position - center x position) + minimapcenter.x
            ////(y position - center y position) + minimapcenter.y

            nodes[i] = Instantiate(minimapNode, new Vector3(minimapCenter.position.x + minimapNode.transform.localScale.x * xmultiplier * (roomCoords[i].X - center), minimapCenter.position.y + minimapNode.transform.localScale.y * ymultiplier * (roomCoords[i].Y - center), 0), Quaternion.identity);
        }

        UpdateMiniMap(currentPlayerCoordinate, oldPlayerCoords);
    }

    public void UpdateMiniMap(Coords currentPlayerCoords, Coords oldPlayerCoords)
    {
        //shift the map to have the current room in the center

        //get distance from currentplayer coord to int center variable
        //TODO issue is that it keeps comparing to center, i think there's an issue with 
        float distanceX = (minimapNode.transform.localScale.x * xmultiplier) * (oldPlayerCoords.X - currentPlayerCoords.X/*TODO perhaps do desired room???*/);
        float distanceY = (minimapNode.transform.localScale.y * ymultiplier) * (oldPlayerCoords.Y - currentPlayerCoords.Y);

        //distanceX += minimapCenter.position.x;
        //distanceY += minimapCenter.position.y;

        //Debug.Log(distanceX);

        //shift all nodes by this amount to trend the current room to center
        for (int i = 0; i < roomCoords.Count; i++)
        {
            Vector3 move = nodes[i].transform.position;
            move.x -= distanceX;
            move.y -= distanceY;
            nodes[i].transform.position = move;
        }

            //this is wrong for reasons i don't feel like explaining
            /*int distanceX, distanceY;
            distanceX = currPlayerCoords.X - roomToMoveTo.X;
            distanceY = currPlayerCoords.Y - roomToMoveTo.Y;
            for(int i = 0; i < roomCoords.Count; i++)
            {
                Vector3 move = nodes[i].transform.position;
                move -= new Vector3(distanceX, distanceY, 0);
                nodes[i].transform.position = move;
            }*/
            //make the colours for the room not active white
            //make the colour of the current room yellow
        }
    }


