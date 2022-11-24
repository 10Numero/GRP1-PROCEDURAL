using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    Dictionary<int, Room> _sideRooms = new Dictionary<int, Room>();
    public Dictionary<Room, int> _roomsPaths = new Dictionary<Room, int>();

    private void Awake()
    {
        _sideRooms.Add(0, null);
        _sideRooms.Add(1, null);
        _sideRooms.Add(2, null);
        _sideRooms.Add(3, null);
    }
}
