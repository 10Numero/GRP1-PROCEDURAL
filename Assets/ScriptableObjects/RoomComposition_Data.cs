using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room Composition")]
public class RoomComposition_Data : ScriptableObject
{
    [Header("Enemies config")]
    public int _nbOfEnemies;
    public int _redEnemyRate;
    public int _blueEnemyRate;
    public int _greenEnemyRate;

    [Header("Items config")]
    public int _nbOfItems;
    public int _commonItemRate;
    public int _rareItemRate;
    public int _legendaryItemRate;

    [Header("Receptors config")]
    public int _nbOfReceptors;
    public int _blueReceptor;
    public int _redReceptor;
    public int _greenReceptor;


}
