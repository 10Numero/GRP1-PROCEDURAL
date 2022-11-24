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

    private void Start()
    {
        _relativePositionsFromIndex.Add(new Vector2(0, 10));
        _relativePositionsFromIndex.Add(new Vector2(20, 0));
        _relativePositionsFromIndex.Add(new Vector2(0, -10));
        _relativePositionsFromIndex.Add(new Vector2(-20, 0));
    }
    public Node GenerateGraph()
    {
        List<Node> nodesList = new List<Node>();
        GameObject baseObject = Instantiate(_nodePrefab);
        Node baseNode = baseObject.GetComponent<Node>();
        nodesList.Add(baseNode);
        _nodesCreated.Add(new Vector2(0, 0), baseNode);

        while (nodesList.Count < _numberOfNodes)
        {
            bool nodeAdded = false;
            int nodePos = 0;
            int sideNode = 0;
            while (!nodeAdded)
            {
                nodePos = Random.Range(0, nodesList.Count - 1);
                sideNode = Random.Range(0, 3);
                if(!_nodesCreated.ContainsKey(nodesList[nodePos]._nodePos +  _relativePositionsFromIndex[sideNode])) nodeAdded = nodesList[nodePos].setNode(sideNode);
            }
            nodesList.Add(nodesList[nodePos].getNode(sideNode));
            Vector2 newNodePos = new Vector2(nodesList[nodePos]._nodePos.x + _relativePositionsFromIndex[sideNode].x, nodesList[nodePos]._nodePos.y + _relativePositionsFromIndex[sideNode].y);
            _nodesCreated.Add(newNodePos, nodesList[nodePos].getNode(sideNode));
            nodesList[nodePos].getNode(sideNode)._nodePos = newNodePos;
        }

        _nodesList = nodesList;
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
}