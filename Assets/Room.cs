using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    Dictionary<int, Room> _sideRooms = new Dictionary<int, Room>();
    public Dictionary<Room, int> _roomsPaths = new Dictionary<Room, int>();
    public List<GameObject> _doorBlocks = new List<GameObject>();
    public List<Door> _unlockableDoors = new List<Door>();
    public Door door;
    private void Awake()
    {
        _sideRooms.Add(0, null);
        _sideRooms.Add(1, null);
        _sideRooms.Add(2, null);
        _sideRooms.Add(3, null);
    }

    public bool IsFinalRoom => door is null;

    public void Unlock()
    {
        door.Unlock();
    }
}
