using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{
    [SerializeField] private Room _baseRoom;
    [SerializeField] private GameObject _grid;
    public List<Room> _roomsCreated = new List<Room>();

    public void CreateLayoutFromGraph(CreateGraph graph)
    {
        Room.FinishedAll = false;

        Room secretRoom = Instantiate(_baseRoom, graph._secretNode._nodePos, Quaternion.identity, _grid.transform);
        secretRoom.roomNb = -1;
        secretRoom.gameObject.name = "Secret Room";
        graph._secretNode.room = secretRoom;
        for (int i = 0; i < graph.getnodes().Count; i++)
        {
            Node node = graph.getnodes()[i];
            Room room = Instantiate(_baseRoom, node._nodePos, Quaternion.identity, _grid.transform);
            room.roomNb = i;
            room.gameObject.name = "Base Room " + i;
            node.room = room;
            _roomsCreated.Add(room);
        }

        for(int i = 0; i< graph.getnodes().Count; i++) {
            Node node = graph.getnodes()[i];
            for (int j = 0; j < 4; j++)
            {
                if (node._sideNodes[j] is not null)
                {
                    int index = graph.getnodes().IndexOf(node._sideNodes[j]);
                    if (index > i)
                    {
                        node.room.door = node.room._unlockableDoors[j];
                    }

                    if (index != -1)
                    {
                        node.room._doorBlocks[j].SetActive(false);
                        node.room._unlockableDoors[j].gameObject.SetActive(true);
                        node.room._unlockableDoors[j].adjacent = node._sideNodes[j].room._unlockableDoors[(j + 2) % 4];
                        node.room._unlockableDoors[j].adjacentMask = node._sideNodes[j].room.doorMasks[(j + 2) % 4].gameObject;
                        node.room._unlockableDoors[j].mask = node.room.doorMasks[j].gameObject;
                    }
                    else
                    {
                        secretRoom.secretDoors.Add(node.room._doorBlocks[j]);
                        secretRoom.secretMasks.Add(node.room.doorMasks[j].gameObject);
                    }
                }
            }
        }

        for (int j = 0; j < 4; j++)
        {
            if (graph._secretNode._sideNodes[j] is not null)
            {
                secretRoom.secretDoors.Add(graph._secretNode.room._doorBlocks[j]);
                secretRoom.secretMasks.Add(graph._secretNode.room.doorMasks[j].gameObject);
            }
        }

        graph.getnodes()[0].room.Unlock();
    }
}
