using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyAttack : Collider_Base
{
    [SerializeField] private int damage;

    public void Init(int __damage)
    {
        damage = __damage;
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        var life = col.GetComponent<Life>();

        if (!life)
            return;

        ApplyDamage(life);
    }
    
    private void ApplyDamage(Life __life)
    {
        __life.TakeDamage((uint)damage);
    }
}
