using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class PlayerLight : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] SpriteShapeRenderer lightRenderer;

    [Header("Parameters")]
    [SerializeField] Color red;
    [SerializeField] Color green;
    [SerializeField] Color blue;

    private ColorType lightType = ColorType.Red;

    private int currentEnumVal;
    private List<int> enumVals;

    private void Awake()
    {
        enumVals = new List<int>();
        string[] biomeTypesStr = Enum.GetNames(typeof(ColorType));
        for (int i = 0; i < biomeTypesStr.Length; i++)
        {
            ColorType type = (ColorType)Enum.Parse(typeof(ColorType), biomeTypesStr[i]);
            enumVals.Add((int)type);
        }

        SetLight(0);
    }

    void UpdateLight()
    {
        switch (lightType)
        {
            case ColorType.Red:
                lightRenderer.color = red;
                break;
            case ColorType.Green:
                lightRenderer.color = green;
                break;
            case ColorType.Blue:
                lightRenderer.color = blue;
                break;
        }
    }

    public void OnChangeLight(InputAction.CallbackContext input)
    {
        if (!input.performed)
            return;

        CycleLight();
    }

    void SetLight(ColorType type)
    {
        lightType = type;
        UpdateLight();
    }

    void SetLight(int i)
    {
        currentEnumVal = i % enumVals.Count;
        CastEnumVal();
        UpdateLight();
    }

    void CycleLight()
    {
        currentEnumVal = (currentEnumVal + 1) % enumVals.Count;
        CastEnumVal();
        UpdateLight();
    }

    void CastEnumVal()
    {
        lightType = (ColorType)enumVals[currentEnumVal];
    }
}
