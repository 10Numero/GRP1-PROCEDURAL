using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField, ReadOnly] private int damage;

    public void Init(int __damage)
    {
        damage = __damage;
    }
}
