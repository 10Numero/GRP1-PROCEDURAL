using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LightDetection : MonoBehaviour
{
    [SerializeField] LayerMask objectLayer;
    [SerializeField] LayerMask platformLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (((1 << other.gameObject.layer) & objectLayer) != 0)
        {
            // object enter
            Debug.Log("Object");
        }
        else if (((1 << other.gameObject.layer) & platformLayer) != 0)
        {
            // platform
        }
    }
}
