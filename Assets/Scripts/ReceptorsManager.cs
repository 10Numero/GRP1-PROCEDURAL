using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptorsManager : MonoBehaviour
{

    [SerializeField] Receptor receptorPrefab;

    [HideInInspector] Receptor receptor1;
    [HideInInspector] Receptor receptor2;
    [HideInInspector] Receptor receptor3;

    [HideInInspector] public Node node1;
    [HideInInspector] public Node node2;
    [HideInInspector] public Node node3;

    CreateGraph graph;

    private int m_active = 0;
    public int active 
    {
        get => m_active;
        set
        {
            m_active = value;
            ActivatedReceptor();
        }
    }

    private static ReceptorsManager _instance;
    public static ReceptorsManager Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }

    void ActivatedReceptor()
    {
        if (m_active < 3)
        {
            return;
        }

        foreach (var door in graph._secretNode.room.secretDoors)
        {
            door.GetComponent<Collider2D>().enabled = false;
        }
        foreach (var mask in graph._secretNode.room.secretMasks)
        {
            mask.SetActive(true);
        }
    }

    public bool CreateReceptors(CreateGraph graph)
    {
        this.graph = graph;
        List<Node> potentialNodes = new List<Node>();

        Vector2 up = graph._secretNode._nodePos + graph._relativePositionsFromIndex[0];
        if (graph.positions.ContainsKey(up))
        {
            potentialNodes.Add(graph.positions[up]);
        }

        Vector2 right = graph._secretNode._nodePos + graph._relativePositionsFromIndex[1];
        if (graph.positions.ContainsKey(right))
        {
            potentialNodes.Add(graph.positions[right]);
        }

        
        Vector2 down = graph._secretNode._nodePos + graph._relativePositionsFromIndex[2];
        if (graph.positions.ContainsKey(down))
        {
            potentialNodes.Add(graph.positions[down]);
        }

        Vector2 left = graph._secretNode._nodePos + graph._relativePositionsFromIndex[3];
        if (graph.positions.ContainsKey(left))
        {
            potentialNodes.Add(graph.positions[left]);
        }

        Vector2 upRight = graph._secretNode._nodePos + graph._relativePositionsFromIndex[0] + graph._relativePositionsFromIndex[1];
        if (graph.positions.ContainsKey(upRight))
        {
            potentialNodes.Add(graph.positions[upRight]);
        }
        
        Vector2 downRight = graph._secretNode._nodePos + graph._relativePositionsFromIndex[2] + graph._relativePositionsFromIndex[1];
        if (graph.positions.ContainsKey(downRight))
        {
            potentialNodes.Add(graph.positions[downRight]);
        }
        
        Vector2 downLeft = graph._secretNode._nodePos + graph._relativePositionsFromIndex[2] + graph._relativePositionsFromIndex[3];
        if (graph.positions.ContainsKey(downLeft))
        {
            potentialNodes.Add(graph.positions[downLeft]);
        }
        
        Vector2 upLeft = graph._secretNode._nodePos + graph._relativePositionsFromIndex[0] + graph._relativePositionsFromIndex[3];
        if (graph.positions.ContainsKey(upLeft))
        {
            potentialNodes.Add(graph.positions[upLeft]);
        }

        if (potentialNodes.Count < 3)
        {
            return false;
        }

        node1 = potentialNodes[Random.Range(0, potentialNodes.Count)];
        potentialNodes.Remove(node1);
        
        node2 = potentialNodes[Random.Range(0, potentialNodes.Count)];
        potentialNodes.Remove(node2);
        
        node3 = potentialNodes[Random.Range(0, potentialNodes.Count)];
        potentialNodes.Remove(node3);

        receptor1 = Instantiate(receptorPrefab, node1._nodePos + new Vector2(Random.Range(-9, 9), Random.Range(-4, 4)), Quaternion.identity);
        receptor1.room = graph.getnodes().IndexOf(node1);

        receptor2 = Instantiate(receptorPrefab, node2._nodePos + new Vector2(Random.Range(-9, 9), Random.Range(-4, 4)), Quaternion.identity);
        receptor2.room = graph.getnodes().IndexOf(node2);

        receptor3 = Instantiate(receptorPrefab, node3._nodePos + new Vector2(Random.Range(-9, 9), Random.Range(-4, 4)), Quaternion.identity);
        receptor3.room = graph.getnodes().IndexOf(node3);

        return true;
    }
}
