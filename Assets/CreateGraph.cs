using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGraph : MonoBehaviour
{
    [SerializeField] private GameObject _nodePrefab;
    [SerializeField] private int _numberOfNodes;
    [SerializeField] private List<Node> _nodesList;
    [SerializeField] private Dictionary<Vector2, Node> _nodesCreated = new Dictionary<Vector2, Node>();
    [SerializeField] private List<Vector2>  _relativePositionsFromIndex = new List<Vector2>();

    public Node _finalNode;
    public int _distanceFromStartToFinish = 0;

    private void Start()
    {
        _relativePositionsFromIndex.Add(new Vector2(0, 10));
        _relativePositionsFromIndex.Add(new Vector2(20, 0));
        _relativePositionsFromIndex.Add(new Vector2(0, -10));
        _relativePositionsFromIndex.Add(new Vector2(-20, 0));
    }

    private void Update()
    {
        if (Input.GetKeyDown("space")) DisplayGraph();
    }
    public Node GenerateGraph()
    {
        List<Node> nodesList = new List<Node>();
        GameObject baseObject = Instantiate(_nodePrefab);
        Node baseNode = baseObject.GetComponent<Node>();
        nodesList.Add(baseNode);
        baseNode._distanceFromStartRoom = 0;
        _nodesCreated.Add(new Vector2(0, 0), baseNode);

        while (nodesList.Count < _numberOfNodes + 2)
        {
            bool nodeAdded = false;
            int nodePos = 0;
            int sideNode = 0;
            while (!nodeAdded)
            {
                checkNeighbours(nodesList);
                nodePos = Random.Range(0, nodesList.Count - 1);
                sideNode = Random.Range(0, 3);
                if(!_nodesCreated.ContainsKey(nodesList[nodePos]._nodePos +  _relativePositionsFromIndex[sideNode])) nodeAdded = nodesList[nodePos].setNode(sideNode);
            }
            nodesList.Add(nodesList[nodePos].getNode(sideNode));
            Vector2 newNodePos = new Vector2(nodesList[nodePos]._nodePos.x + _relativePositionsFromIndex[sideNode].x, nodesList[nodePos]._nodePos.y + _relativePositionsFromIndex[sideNode].y);
            _nodesCreated.Add(newNodePos, nodesList[nodePos].getNode(sideNode));
            nodesList[nodePos].getNode(sideNode)._nodePos = newNodePos;
            nodesList[nodePos].getNode(sideNode).setDistance();
        }
        _nodesList = nodesList;
        setDistanceStartFinish();
        return baseNode;
    }

    public void DisplayGraph()
    {
        for (int i = 0; i < _nodesList.Count; i++)
        {
            _nodesList[i].DisplayNode();
        }
    }

    public List<Node> getnodes()
    {
        return _nodesList;
    }

    public void checkNeighbours(List<Node> nodes)
    {
        for (int i = 0; i< nodes.Count; i++)
        {
            for(int j = 0; j<4; j++)
            {
                if(nodes[i]._sideNodes[j] is null && _nodesCreated.ContainsKey(nodes[i]._nodePos + _relativePositionsFromIndex[j]))
                {
                    nodes[i].setNode(j, _nodesCreated[nodes[i]._nodePos + _relativePositionsFromIndex[j]]);
                    switch(j) {
                        case 0:
                            _nodesCreated[nodes[i]._nodePos + _relativePositionsFromIndex[j]].setNode(2, nodes[i]);
                            break;
                        case 1:
                            _nodesCreated[nodes[i]._nodePos + _relativePositionsFromIndex[j]].setNode(3, nodes[i]);
                            break;
                        case 2:
                            _nodesCreated[nodes[i]._nodePos + _relativePositionsFromIndex[j]].setNode(0, nodes[i]);
                            break;
                        case 3:
                            _nodesCreated[nodes[i]._nodePos + _relativePositionsFromIndex[j]].setNode(1, nodes[i]);
                            break;
                    }
                }
            }
        }
    }

    public void setDistanceStartFinish()
    {
        Node lastNode = _nodesList[0];
        int lastNodeDistance = 0;
        int lastIndex = 0;
        for(int i = 1; i<_nodesList.Count; i++)
        {
            if(_nodesList[i]._distanceFromStartRoom >= lastNodeDistance)
            {
                lastNodeDistance = _nodesList[i]._distanceFromStartRoom;
                lastNode = _nodesList[i];
                lastIndex = i;
            }
        }
        _nodesList.RemoveAt(lastIndex);
        _nodesList.Add(lastNode);
        _distanceFromStartToFinish = lastNodeDistance;
        _finalNode = lastNode;
        lastNode.name = "Final node";
    }
}