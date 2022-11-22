using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] CreateGraph _graphManager;
    [SerializeField] LayoutManager _layoutManager;


    public void Start()
    {
        _graphManager.GenerateGraph();
        _layoutManager.CreateLayoutFromGraph(_graphManager);
    }
}
