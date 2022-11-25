using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public Color green;
    public Color blue;
    public Color red;
    public List<EnemyHealth> enemyPrefabs;
    [SerializeField] List<Color> colors = new List<Color>();
    public void Spawn(CreateGraph _graph)
    {
        colors.Add(green);
        colors.Add(blue);
        colors.Add(red);
        for(int i = 1;i<_graph.getnodes().Count/3; i++)
        {
            int nbOfEnemies = Random.Range(1, 4);
            for(int j = 0; j<=nbOfEnemies; j++)
            {
                EnemyHealth enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], new Vector2(_graph.getnodes()[i]._nodePos.x + Random.Range(-8, 8), _graph.getnodes()[i]._nodePos.y + Random.Range(-3, 3)), Quaternion.identity);
                _graph.getnodes()[i].room.enemies.Add(enemy);
                enemy.room = i;
                enemy.color = ColorType.Green;
                enemy.GetComponent<SpriteRenderer>().color = colors[0];
                enemy.GetComponent<EnemyMovement>()._lineMovePoints.Add(enemy.transform.position);
                enemy.GetComponent<EnemyMovement>()._lineMovePoints.Add(new Vector2(_graph.getnodes()[i]._nodePos.x + Random.Range(-8, 8), _graph.getnodes()[i]._nodePos.y + Random.Range(-3, 3)));
            }
        }
        for(int i = _graph.getnodes().Count/3; i<(_graph.getnodes().Count/3) * 2; i++)
        {
            int nbOfEnemies = Random.Range(4, 7);
            for(int j = 0; j<=nbOfEnemies; j++)
            {
                EnemyHealth enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], new Vector2(_graph.getnodes()[i]._nodePos.x + Random.Range(-8, 8), _graph.getnodes()[i]._nodePos.y + Random.Range(-3, 3)), Quaternion.identity);
                _graph.getnodes()[i].room.enemies.Add(enemy);
                enemy.room = i;
                int color = Random.Range(0, 2);
                enemy.color = (ColorType)color;
                enemy.GetComponent<SpriteRenderer>().color = colors[color];
                enemy.GetComponent<EnemyMovement>()._lineMovePoints.Add(enemy.transform.position);
                enemy.GetComponent<EnemyMovement>()._lineMovePoints.Add(new Vector2(_graph.getnodes()[i]._nodePos.x + Random.Range(-8, 8), _graph.getnodes()[i]._nodePos.y + Random.Range(-3, 3)));
            }
        }
        for(int i = (_graph.getnodes().Count / 3) * 2; i<_graph.getnodes().Count; i++)
        {
            int nbOfEnemies = Random.Range(7, 9);
            for(int j = 0; j<=nbOfEnemies; j++)
            {
                EnemyHealth enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], new Vector2(_graph.getnodes()[i]._nodePos.x + Random.Range(-8, 8), _graph.getnodes()[i]._nodePos.y + Random.Range(-3, 3)), Quaternion.identity);
                _graph.getnodes()[i].room.enemies.Add(enemy);
                enemy.room = i;
                int color = Random.Range(0, 3);
                enemy.color = (ColorType)color;
                enemy.GetComponent<SpriteRenderer>().color = colors[color];
                enemy.GetComponent<EnemyMovement>()._lineMovePoints.Add(enemy.transform.position);
                enemy.GetComponent<EnemyMovement>()._lineMovePoints.Add(new Vector2(_graph.getnodes()[i]._nodePos.x + Random.Range(-8, 8), _graph.getnodes()[i]._nodePos.y + Random.Range(-3, 3)));
            }
        }
    }
}
