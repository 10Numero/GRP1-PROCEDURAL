using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeManager : MonoBehaviour
{
    [SerializeField] Spike spikePrefab;
    [SerializeField] float minProbability = 0.25f;

    public void Spawn(CreateGraph graph)
    {
        for (int i = 1; i < graph.getnodes().Count; ++i)
        {
            if (Random.Range(0f, 1f) < Mathf.Max((1 - ((float)i / graph.getnodes().Count)), minProbability))
            {
                Spike spike = Instantiate(spikePrefab, graph.getnodes()[i]._nodePos + new Vector2(Random.Range(-9, 9), Random.Range(-4, 4)), Quaternion.identity);
                spike.room = i;
            }
        }
    }
}
