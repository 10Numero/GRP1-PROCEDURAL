using System.Collections.Generic;
using UnityEngine;

public class RoomEnemiesManager : MonoBehaviour
{
    private readonly List<EnemyMovement> enemiesInRoom = new();

    public void SetAllEnemiesInRoomActive(bool isActive)
    {
        foreach(var enemy in enemiesInRoom)
        {
            enemy.enabled = isActive;
        }
    }

    public void AddEnemyToRoom(EnemyMovement enemyToAdd)
    {
        enemiesInRoom.Add(enemyToAdd);
    }

    public void RemoveEnemyFromRoom(EnemyMovement enemyToRemove)
    {
        enemiesInRoom.Remove(enemyToRemove);
    }
}
