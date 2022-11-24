using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutManager : MonoBehaviour
{
    [SerializeField] private Room _baseRoom;
    [SerializeField] private GameObject _grid;
    [SerializeField] private Dictionary<Vector2, Room> _roomsCreated = new Dictionary<Vector2, Room>();
    [SerializeField] private List<Vector3> _relativePositionsFromIndex = new List<Vector3>();
    [SerializeField] private int[,] _roomsMatrix;

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
        _roomsMatrix = new int[graph.getnodes().Count, graph.getnodes().Count];
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
                if (!_roomsCreated.ContainsKey(newRoom.transform.position + _relativePositionsFromIndex[i])) CreateRoomFromNode(sideNodes[i], newRoom.transform.position + _relativePositionsFromIndex[i]);
            }
        }
    }

    public void SpawnSceretRoom(CreateGraph graph)
    {
        bool isPlaced = false;
        while(!isPlaced)
        {
            Node sideNode = graph.getnodes()[Random.Range(1, graph.getnodes().Count)];
            for(int i = 0; i<4; i++)
            {
                if(sideNode.getSideNodes()[i] is null)
                {
                    for(int j = 0; j<4; j++)
                    {
                    }
                }
            }
        }
    }

}
