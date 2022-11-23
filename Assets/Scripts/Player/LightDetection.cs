using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LightDetection : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerLight playerLight;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var intractable = other.gameObject.GetComponent<LightInteractable>();
        if (intractable && intractable.room == playerController.room)
        {
            intractable.InteractStart(playerLight.Light);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var intractable = other.gameObject.GetComponent<LightInteractable>();
        if (intractable && intractable.room == playerController.room)
        {
            intractable.InteractEnd(playerLight.Light);
        }
    }
}
