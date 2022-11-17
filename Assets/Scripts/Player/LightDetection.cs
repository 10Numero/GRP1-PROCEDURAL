using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetection : MonoBehaviour
{
    [SerializeField] LayerMask objectLayer;
    [SerializeField] LayerMask platformLayer;

    private void OnTriggerEnter2D(Collider other)
    {
        if (((1 << other.gameObject.layer) & objectLayer) != 0)
        {
            // object enter
        }
        else if (((1 << other.gameObject.layer) & platformLayer) != 0)
        {
            // platform
        }
    }
}
