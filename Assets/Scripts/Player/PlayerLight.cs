using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class PlayerLight : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] GameObject lightGameObject;
    [SerializeField] SpriteShapeRenderer lightRenderer;

    [Header("Parameters")]
    [SerializeField] Color red;
    [SerializeField] Color green;
    [SerializeField] Color blue;

    private ColorType lightType = ColorType.Red;

    private int currentEnumVal;
    private List<int> enumVals;

    private bool isActive;

    private void Awake()
    {
        isActive = false;
        SetLight(0);
        lightGameObject.SetActive(false);

        enumVals = new List<int>();
        string[] biomeTypesStr = Enum.GetNames(typeof(ColorType));
        for (int i = 0; i < biomeTypesStr.Length; i++)
        {
            ColorType type = (ColorType)Enum.Parse(typeof(ColorType), biomeTypesStr[i]);
            enumVals.Add((int)type);
        }
    }

    void UpdateLight()
    {
        if (!isActive)
        {
            return;
        }

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

    public void OnActivateLight(InputAction.CallbackContext input)
    {
        if (input.started)
        {
            Activate();
        }
        else if (input.canceled)
        {
            Desactivate();
        }
    }

    void Activate()
    {
        isActive = true;
        lightGameObject.SetActive(true);
    }

    void Desactivate()
    {
        isActive = false;
        lightGameObject.SetActive(false);
    }

    void SetLight(ColorType type)
    {
        if (!isActive)
        {
            return;
        }

        lightType = type;
        UpdateLight();
    }

    void SetLight(int i)
    {
        if (!isActive)
        {
            return;
        }

        currentEnumVal = i % enumVals.Count;
        CastEnumVal();
        UpdateLight();
    }

    void CycleLight()
    {
        if (!isActive)
        {
            return;
        }

        currentEnumVal = (currentEnumVal + 1) % enumVals.Count;
        CastEnumVal();
        UpdateLight();
    }

    void CastEnumVal()
    {
        lightType = (ColorType)enumVals[currentEnumVal];
    }
}
