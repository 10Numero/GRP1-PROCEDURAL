using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Door adjacent;

    public void Unlock()
    {
        adjacent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
