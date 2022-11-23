using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleTest : LightInteractable
{
    
    public override void InteractStart(ColorType color)
    {
        Debug.Log("Obstacle");
        this.color = color;
    }

    public override void InteractEnd(ColorType color)
    {

    }
}
