using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInventory : MonoBehaviour
{
    [SerializeField] private List<ObjectPickup> pickups = new();

    public void AddPickup(IEnumerable<ObjectPickup> toAdd)
    {
        pickups.AddRange(toAdd);
    }

    public void AddPickup(ObjectPickup toAdd)
    {
        if (IsInInventory(toAdd.type))
            RemovePickupFromType(toAdd.type);

        pickups.Add(toAdd);

        toAdd.EnableEffect();
    }

    public void RemovePickup(ObjectPickup toRemove)
    {
        pickups.Remove(toRemove);
        Destroy(toRemove);
    }

    public void RemovePickupFromType(ObjectPickupType objType)
    {
        ObjectPickup objToRemove = new();

        foreach(ObjectPickup obj in pickups)
        {
            if (obj.type == objType)
                objToRemove = obj;
        }

        RemovePickup(objToRemove);
    }

    private bool IsInInventory(ObjectPickupType objType)
    {
        foreach(ObjectPickup obj in pickups)
        {
            if (obj.type == objType)
                return true;
        }

        return false;
    }
}
