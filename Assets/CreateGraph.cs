using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGraph : MonoBehaviour
{
    [SerializeField] private int _numberOfNodes;
    private List<Node> _nodesList = new List<Node>();
    [HideInInspector] public List<Vector2>  _relativePositionsFromIndex = new List<Vector2>();
    [HideInInspector] public Node _secretNode;
    [HideInInspector] public Dictionary<Vector2, Node> positions = new Dictionary<Vector2, Node>();


    public Node _finalNode;
    public int _distanceFromStartToFinish = 0;

    int c = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _nodesList[c].room.Unlock();
            c++;
        }
    }

    public void GenerateGraph()
    {
        
        
        _relativePositionsFromIndex.Clear();

        _relativePositionsFromIndex.Add(new Vector2(0, 10));
        _relativePositionsFromIndex.Add(new Vector2(20, 0));
        _relativePositionsFromIndex.Add(new Vector2(0, -10));
        _relativePositionsFromIndex.Add(new Vector2(-20, 0));

        
        Node baseNode = new Node();
        baseNode._nodePos = new Vector2(0, 0);
        _nodesList.Add(baseNode);
        positions.Add(baseNode._nodePos, baseNode);

        while (_nodesList.Count < _numberOfNodes)
        {
            Node prev = _nodesList[_nodesList.Count - 1];

            int sideNode = Random.Range(0, 4);

            Vector2 newPos = prev._nodePos + _relativePositionsFromIndex[sideNode];

            while (positions.ContainsKey(newPos))
            {
                sideNode = (sideNode + 1) % 4;
                if (prev._sideNodes[sideNode] is not null)
                {
                    sideNode = (sideNode + 1) % 4;
                }
                newPos = prev._nodePos + _relativePositionsFromIndex[sideNode];
            }

            bool deadLock = true;

            for(int i = 0; i<4; i++)
            {
                if(!positions.ContainsKey(newPos + _relativePositionsFromIndex[i]))
                {
                    deadLock = false;
                    break;
                }
            }

            if(deadLock)
            {
                continue;
            }

            Node newNode = prev.setNode(sideNode);
            newNode._nodePos = newPos;
            positions.Add(newPos, newNode);
            _nodesList.Add(newNode);
        }

        int adjacentRooms = 3;
        while(_secretNode is null)
        {
            bool spawned = false;
            for(int i = 0; i<_nodesList.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Vector2 sidePos = _nodesList[i]._nodePos + _relativePositionsFromIndex[j];
                    if (!positions.ContainsKey(sidePos))
                    {
                        int adjacents = 0;
                        List<Node> adjacentNodes = new List<Node>();
                        for(int k = 0; k<4; k++)
                        {
                            Vector2 pos = sidePos + _relativePositionsFromIndex[k];
                            if (positions.ContainsKey(pos))
                            {
                                adjacents++;

                                adjacentNodes.Add(positions[pos]);
                            }
                        }
                        if (adjacents == adjacentRooms)
                        {
                            _secretNode = new Node();
                            _secretNode._nodePos = sidePos;
                            positions.Add(_secretNode._nodePos, _secretNode);
                            Node adjacentNode = adjacentNodes[Random.Range(0, adjacentNodes.Count)];
                            int dir = _relativePositionsFromIndex.IndexOf(adjacentNode._nodePos - _secretNode._nodePos);
                            _secretNode.setNode(dir, adjacentNode);
                            adjacentNode.setNode((dir + 2) % 4, _secretNode); 

                            spawned = true;
                            break;
                        }
                    }
                }
                if(spawned)
                {
                    break;
                }
            }
            adjacentRooms--;
        }
    }

    public List<Node> getnodes()
    {
        return _nodesList;
    }
}