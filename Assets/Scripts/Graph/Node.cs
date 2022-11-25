using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Node
{
    public Dictionary<int, Node> _sideNodes = new Dictionary<int, Node>();
    public Vector2 _nodePos;
    public bool _mustBeEmpty = false;
    public int _distanceFromStartRoom = 0;
    public Room room;

    public Node ()
    {
        _sideNodes.Add(0, null);
        _sideNodes.Add(1, null);
        _sideNodes.Add(2, null);
        _sideNodes.Add(3, null);
    }

    public Node setNode(int index)
    {
        Node baseNode = new Node();
        _sideNodes[index] = baseNode;
        baseNode.setNode((index +2)%4, this);
        return baseNode;
    }

    public bool setNode(int index, Node node)
    {
        _sideNodes[index] = node;
        return true;
    }

}
