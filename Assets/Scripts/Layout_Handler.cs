using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layout_Handler : MonoBehaviour
{
    [SerializeField] List<Room> _topOpenedRooms;
    [SerializeField] List<Room> _rightOpenedRooms;
    [SerializeField] List<Room> _bottomOpenedRooms;
    [SerializeField] List<Room> _leftOpenedRooms;
}
