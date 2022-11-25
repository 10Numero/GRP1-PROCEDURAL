using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    [SerializeField] GameObject _chestAndKey;
    Node node1;
    Node node2;
    Node node3;

    public void CreateChests(CreateGraph _graphManager)
    {
        node1 = _graphManager.positions[new Vector2(0, 0)];
        node2 = _graphManager.getnodes()[(_graphManager.getnodes().Count - 1)/3];
        node3 = _graphManager.getnodes()[((_graphManager.getnodes().Count - 1)/3) * 2];

        GameObject chest1 = Instantiate(_chestAndKey, node1._nodePos, Quaternion.identity);
        GameObject chest2 = Instantiate(_chestAndKey, node2._nodePos, Quaternion.identity);
        GameObject chest3 = Instantiate(_chestAndKey, node3._nodePos, Quaternion.identity);

        chest1.transform.GetChild(0).transform.position = new Vector2(node1._nodePos.x + Random.Range(-8, 8), node1._nodePos.y + Random.Range(-3, 3));
        chest1.transform.GetChild(1).transform.position = new Vector2(node1._nodePos.x + 8, node1._nodePos.y - 3);

        chest2.transform.GetChild(0).transform.position = new Vector2(node2._nodePos.x + Random.Range(-8, 8), node2._nodePos.y + Random.Range(-3, 3));
        chest2.transform.GetChild(1).transform.position = new Vector2(node2._nodePos.x + 8, node2._nodePos.y - 3);

        chest3.transform.GetChild(0).transform.position = new Vector2(node3._nodePos.x + Random.Range(-8, 8), node3._nodePos.y + Random.Range(-3, 3));
        chest3.transform.GetChild(1).transform.position = new Vector2(node3._nodePos.x + 8, node3._nodePos.y - 3);
    }
}
