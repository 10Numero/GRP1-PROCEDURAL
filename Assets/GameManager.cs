using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] CreateGraph _graphManager;
    [SerializeField] LayoutManager _layoutManager;
    [SerializeField] ReceptorsManager _receptorsManager;
    [SerializeField] ChestSpawner _chestManager;
    [SerializeField] SpikeManager _spikeManager;

    public void Start()
    {
        _graphManager.GenerateGraph();
        _layoutManager.CreateLayoutFromGraph(_graphManager);
        if (!_receptorsManager.CreateReceptors(_graphManager))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        _chestManager.CreateChests(_graphManager);
        _spikeManager.Spawn(_graphManager);
    }
}
