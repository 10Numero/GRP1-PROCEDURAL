using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{
    [SerializeField] private Room _baseRoom;
    [SerializeField] private GameObject _grid;
    [SerializeField] private Dictionary<Vector2, Room> _roomsCreated = new Dictionary<Vector2, Room>();
    [SerializeField] private List<Vector2> _relativePositionsFromIndex = new List<Vector2>();

    private void Awake()
    {
        _relativePositionsFromIndex.Add(new Vector2(0, 10));
        _relativePositionsFromIndex.Add(new Vector2(20, 0));
        _relativePositionsFromIndex.Add(new Vector2(0, -10));
        _relativePositionsFromIndex.Add(new Vector2(-20, 0));
    }
    public void CreateLayoutFromGraph(CreateGraph graph)
    {
        List<Node> nodesList = graph.getnodes();
        CreateRoomFromNode(nodesList[0], new Vector2(0, 0));
    }

    public void CreateRoomFromNode(Node node, Vector2 position)
    {
        Room newRoom = Instantiate(_baseRoom, position, Quaternion.identity, _grid.transform);
        _roomsCreated.Add(newRoom.transform.position, newRoom);

        Dictionary<int, Node> sideNodes = node.getSideNodes();
        for( int i = 0; i<4; i++)
        {
            if(sideNodes[i] != null)
            {
                if (!_roomsCreated.ContainsKey((Vector2) newRoom.transform.position + _relativePositionsFromIndex[i])) CreateRoomFromNode(sideNodes[i], (Vector2) newRoom.transform.position + _relativePositionsFromIndex[i]);
            }
        }
    }

    public void SpawnSecretRoom(CreateGraph graph)
    {
        bool isPlaced = false;
        while(!isPlaced)
        {
            Node sideNode = graph.getnodes()[Random.Range(1, graph.getnodes().Count)];
            for(int i = 0; i<4; i++)
            {
                if(sideNode.getSideNodes()[i] is null)
                {
                    Debug.Log(sideNode.getSideNodes()[i]);
                    for(int j = 0; j<4; j++)
                    {
                        Vector2 otherSideRoom = sideNode._nodePos + _relativePositionsFromIndex[i] + _relativePositionsFromIndex[j];
                        if (_roomsCreated.ContainsKey(otherSideRoom) && otherSideRoom != sideNode._nodePos)
                        {
                            Debug.Log(sideNode._nodePos);
                            Debug.Log(sideNode._nodePos + _relativePositionsFromIndex[i]);
                            Debug.Log(otherSideRoom);
                            Room secretRoom = Instantiate(_baseRoom, sideNode._nodePos + _relativePositionsFromIndex[i], Quaternion.identity, _grid.transform);
                            _roomsCreated.Add(secretRoom.transform.position, secretRoom);
                            isPlaced = true;
                            secretRoom.name = "Secret Room";
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }

}
