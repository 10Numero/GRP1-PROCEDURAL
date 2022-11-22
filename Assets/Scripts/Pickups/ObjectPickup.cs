using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectPickupType
{
    SpeedUp,
    LightUp,
    LifeUp,
    MobSpeedDown,
    MobRangeDown,
    MobLifeDown,
    GreenSpeedDown,
    RedSpeedDown,
    BlueSpeedDown
}

public class ObjectPickup : MonoBehaviour
{
    [SerializeField] private ObjectPickupType objectPicupType;
    public ObjectPickupType type => objectPicupType;

    [HideInInspector] public int durability = 3;

    protected PickupInventory ownerInventory;

    protected virtual void PickUp(PickupInventory colliderInventory)
    {
        ownerInventory = colliderInventory;
        ownerInventory.AddPickup(this);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out PickupInventory colliderInventory))
        {
            PickUp(colliderInventory);
        }
    }

    public virtual void EnableEffect()
    {

    }

}
