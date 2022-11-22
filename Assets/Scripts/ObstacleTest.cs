using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleTest : LightInteractable
{
    public override void Interact(ColorType color)
    {
        Debug.Log("Obstacle");
        this.color = color;
    }
}
