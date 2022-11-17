using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateGraph : MonoBehaviour
{
    [SerializeField] private GameObject _nodePrefab;
    [SerializeField] private int _numberOfNodes;
    [SerializeField] private List<Node> _nodesList;


    private void Start()
    {
        GenerateGraph();
        DisplayGraph();
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
        while (nodesList.Count < _numberOfNodes)
        {
            bool nodeAdded = false;
            int nodePos = 0;
            int sideNode = 0;
            while (!nodeAdded)
            {
                nodePos = Random.Range(0, nodesList.Count - 1);
                sideNode = Random.Range(0, 3);
                nodeAdded = nodesList[nodePos].setNode(sideNode);
            }
            nodesList.Add(nodesList[nodePos].getNode(sideNode));
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

}