using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    Dictionary<int, Room> _sideRooms = new Dictionary<int, Room>();
    public Dictionary<Room, int> _roomsPaths = new Dictionary<Room, int>();
    public List<GameObject> _doorBlocks = new List<GameObject>();
    public List<Door> _unlockableDoors = new List<Door>();
    public List<SpriteMask> doorMasks = new List<SpriteMask>();
    public Door door;

    public List<GameObject> secretDoors = new List<GameObject>();
    public List<GameObject> secretMasks = new List<GameObject>();

    [HideInInspector] public int roomNb = -1;

    [HideInInspector] public List<EnemyHealth> enemies = new List<EnemyHealth>();

    public static bool FinishedAll = false;

    public void OnEnterRoom()
    {
        PlayerController.Instance.room = roomNb;

        if (roomNb == -1 && FinishedAll)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void Awake()
    {
        EnemyHealth.OnEnemyDeath += OnEnemyDeath;

        _sideRooms.Add(0, null);
        _sideRooms.Add(1, null);
        _sideRooms.Add(2, null);
        _sideRooms.Add(3, null);
    }

    private void OnEnemyDeath(EnemyHealth obj)
    {
        if (enemies.Contains(obj))
        {
            enemies.Remove(obj);

            if (enemies.Count == 0)
            {
                Unlock();
            }
        }
    }

    public bool IsFinalRoom => door is null;

    public void Unlock()
    {
        if (roomNb != -1 && door is null)
        {
            FinishedAll = true;
            return;
        }
        else if (roomNb == -1)
        {
            return;
        }

        door.Unlock();
    }
}
