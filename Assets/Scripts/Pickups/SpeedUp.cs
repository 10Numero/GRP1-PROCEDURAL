using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : ObjectPickup
{
    private Collider2D myCollider2D;

    private void Awake()
    {
        myCollider2D = GetComponent<Collider2D>();
    }

    protected override void PickUp(PickupInventory colliderInventory)
    {
        base.PickUp(colliderInventory);
        myCollider2D.enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public override void EnableEffect()
    {
        base.EnableEffect();

    }
}
