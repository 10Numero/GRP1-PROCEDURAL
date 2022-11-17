using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Layout")]
public class Layout_Data : ScriptableObject
{
    public float _nbOfRooms;
    public int _easyRoomRate;
    public int _mediumRoomRate;
    public int _hardRoomRate;
}
