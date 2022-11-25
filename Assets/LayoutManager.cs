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
        Room secretRoom = Instantiate(_baseRoom, graph._secretNode._nodePos, Quaternion.identity, _grid.transform);
        secretRoom.gameObject.name = "Secret Room";
        graph._secretNode.room = secretRoom;
        for (int i = 0; i < graph.getnodes().Count; i++)
        {
            Node node = graph.getnodes()[i];
            Room room = Instantiate(_baseRoom, node._nodePos, Quaternion.identity, _grid.transform);
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
                    if(graph.getnodes().IndexOf(node._sideNodes[j]) > i)
                    {
                        node.room.door = node.room._unlockableDoors[j];
                    }

                    node.room._doorBlocks[j].SetActive(false);
                    node.room._unlockableDoors[j].gameObject.SetActive(true);
                    node.room._unlockableDoors[j].adjacent = node._sideNodes[j].room._unlockableDoors[(j + 2) % 4];
                }
            }
        }
        for (int j = 0; j < 4; j++)
        {
            if (graph._secretNode._sideNodes[j] is not null)
            {
                graph._secretNode.room._doorBlocks[j].SetActive(false);
                graph._secretNode.room._unlockableDoors[j].gameObject.SetActive(true);
                graph._secretNode.room._unlockableDoors[j].adjacent = graph._secretNode._sideNodes[j].room._unlockableDoors[(j + 2) % 4];
            }
        }
    }




    //public void SpawnSecretRoom(CreateGraph graph)
    //{
    //    bool isPlaced = false;
    //    while(!isPlaced)
    //    {
    //        Node sideNode = graph.getnodes()[Random.Range(1, graph.getnodes().Count)];
    //        for(int i = 0; i<4; i++)
    //        {
    //            if(sideNode.getSideNodes()[i] is null)
    //            {
    //                Debug.Log(sideNode.getSideNodes()[i]);
    //                for(int j = 0; j<4; j++)
    //                {
    //                    Vector2 otherSideRoom = sideNode._nodePos + _relativePositionsFromIndex[i] + _relativePositionsFromIndex[j];
    //                    if (_roomsCreated.ContainsKey(otherSideRoom) && otherSideRoom != sideNode._nodePos)
    //                    {
    //                        Debug.Log(sideNode._nodePos);
    //                        Debug.Log(sideNode._nodePos + _relativePositionsFromIndex[i]);
    //                        Debug.Log(otherSideRoom);
    //                        Room secretRoom = Instantiate(_baseRoom, sideNode._nodePos + _relativePositionsFromIndex[i], Quaternion.identity, _grid.transform);
    //                        _roomsCreated.Add(secretRoom.transform.position, secretRoom);
    //                        isPlaced = true;
    //                        secretRoom.name = "Secret Room";
    //                        break;
    //                    }
    //                }
    //                break;
    //            }
    //        }
    //    }
    //}

}
