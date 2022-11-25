using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    [SerializeField] List<Color> colors = new List<Color>();
    public void Spawn(CreateGraph _graph)
    {
        colors.Add(Color.green);
        colors.Add(Color.blue);
        colors.Add(Color.red);
        for(int i = 1;i<_graph.getnodes().Count/3; i++)
        {
            int nbOfEnemies = Random.Range(1, 4);
            for(int j = 0; j<=nbOfEnemies; j++)
            {
                GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], new Vector2(_graph.getnodes()[i]._nodePos.x + Random.Range(-8, 8), _graph.getnodes()[i]._nodePos.y + Random.Range(-3, 3)), Quaternion.identity);
                enemy.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
        for(int i = _graph.getnodes().Count/3 + 1; i<(_graph.getnodes().Count/3) * 2; i++)
        {
            int nbOfEnemies = Random.Range(4, 7);
            for(int j = 0; j<=nbOfEnemies; j++)
            {
                GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], new Vector2(_graph.getnodes()[i]._nodePos.x + Random.Range(-8, 8), _graph.getnodes()[i]._nodePos.y + Random.Range(-3, 3)), Quaternion.identity);
                enemy.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, 2)];
            }
        }
        for(int i = (_graph.getnodes().Count / 3) * 2; i<_graph.getnodes().Count; i++)
        {
            int nbOfEnemies = Random.Range(7, 9);
            for(int j = 0; j<=nbOfEnemies; j++)
            {
                GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], new Vector2(_graph.getnodes()[i]._nodePos.x + Random.Range(-8, 8), _graph.getnodes()[i]._nodePos.y + Random.Range(-3, 3)), Quaternion.identity);
                enemy.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, 3)]; ;
            }
        }
    }
}
