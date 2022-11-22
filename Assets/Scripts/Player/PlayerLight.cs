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
    
    private List<int> enumVals;

    private int m_current = 0;
    private int current
    {
        get => m_current;
        set
        {
            m_current = value;
            UpdateColor();
        }
    }
    private List<ColorType> accessable;

    private bool isActive;

    private void Awake()
    {
        isActive = false;
        lightGameObject.SetActive(false);

        enumVals = new List<int>();
        string[] biomeTypesStr = Enum.GetNames(typeof(ColorType));
        for (int i = 0; i < biomeTypesStr.Length; i++)
        {
            ColorType type = (ColorType)Enum.Parse(typeof(ColorType), biomeTypesStr[i]);
            enumVals.Add((int)type);
        }


        accessable = new List<ColorType>();
        accessable.Add((ColorType)enumVals[0]);
        current = 0;
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

    public ColorType Light => accessable[current];

    public void AddColor()
    {
        if (accessable.Count == enumVals.Count)
        {
            return;
        }

        accessable.Add((ColorType)enumVals[accessable.Count]);
        current = accessable.Count - 1;
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

    void CycleLight()
    {
        if (!isActive)
        {
            return;
        }

        current = (current + 1) % accessable.Count;
    }

    void UpdateColor()
    {
        switch (accessable[current])
        {
            default:
            case ColorType.Green:
                lightRenderer.color = green;
                break;
            case ColorType.Blue:
                lightRenderer.color = blue;
                break;
            case ColorType.Red:
                lightRenderer.color = red;
                break;
        }
    }
}
