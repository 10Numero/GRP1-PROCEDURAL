using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Node : MonoBehaviour
{
    [SerializeField] private GameObject _nodePrefab;
    [SerializeField] private int _nodeNb;
    [SerializeField] Dictionary<int, Node> _sideNodes = new Dictionary<int, Node>();
    [SerializeField] public Vector2 _nodePos;

    private void Awake()
    {
        _sideNodes.Add(0, null);
        _sideNodes.Add(1, null);
        _sideNodes.Add(2, null);
        _sideNodes.Add(3, null);
    }

    public bool setNode(int index)
    {
        if (_sideNodes[index] is not null) return false;
        GameObject baseObject = Instantiate(_nodePrefab);
        Node newNode = baseObject.GetComponent<Node>();
        _sideNodes[index] = newNode;
        switch(index)
        {
            case 0:
                newNode.setNode(2, this);
                break;
            case 1:
                newNode.setNode(3, this);
                break;
            case 2:
                newNode.setNode(0, this);
                break;
            case 3:
                newNode.setNode(1, this);
                break;
        }
        return true;
    }

    public bool setNode(int index, Node node)
    {
        if (_sideNodes[index] is not null) return false;
        _sideNodes[index] = node;
        return true;
    }

    public Node getNode(int index)
    {
        return _sideNodes[index];
    }


    public Dictionary<int, Node> getSideNodes()
    {
        return _sideNodes;
    }
    public void DisplayNode()
    {
        for(int i = 0; i< 3; i++)
        {
            if (_sideNodes[i] is not null) Debug.Log("La node " + gameObject.name + " a comme voisin sur la " + i + " la node " + _sideNodes[i].name);
        }
    }

}
