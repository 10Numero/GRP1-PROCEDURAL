using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Door adjacent;
    public GameObject adjacentMask;

    public GameObject mask;

    public void Unlock()
    {
        adjacent.gameObject.SetActive(false);
        adjacentMask.SetActive(true);
        mask.SetActive(true);
        gameObject.SetActive(false);
    }
}
