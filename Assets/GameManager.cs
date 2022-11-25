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
    [SerializeField] SpawnEnemies _enemiesSpawner;

    public void Start()
    {

        if (!_graphManager.GenerateGraph())
        {
            SceneManager.LoadScene(0);
        }

        _layoutManager.CreateLayoutFromGraph(_graphManager);

        if (!_receptorsManager.CreateReceptors(_graphManager))
        {
            SceneManager.LoadScene(0);
        }

        _chestManager.CreateChests(_graphManager);

        _spikeManager.Spawn(_graphManager);

        _enemiesSpawner.Spawn(_graphManager);

    }
}
