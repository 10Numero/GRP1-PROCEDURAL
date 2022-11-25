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
    [SerializeField] bool switchToNewColorWhenAdded = true;
    [SerializeField] Color red;
    [SerializeField] Color green;
    [SerializeField] Color blue;

    [Header("Debug")]
    [SerializeField] bool enableMask = false;

    public ColorType Light => accessable[current];
    
    private List<int> enumVals;

    private int m_current = 0;
    private int current
    {
        get => m_current;
        set
        {
            m_current = value;

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

    private List<ColorType> accessable;

    private bool isActive;

    private static PlayerLight _instance;
    public static PlayerLight Instance => _instance;
    private void Awake()
    {
        _instance = this;

#if UNITY_EDITOR
        if (enableMask)
#endif
        {
            lightRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
        
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
        if (!input.performed || !isActive)
            return;

        current = (current + 1) % accessable.Count;
    }

    public void OnActivateLight(InputAction.CallbackContext input)
    {
        if (input.started)
        {
            isActive = true;
            lightGameObject.SetActive(true);
        }
        else if (input.canceled)
        {
            isActive = false;
            lightGameObject.SetActive(false);
        }
    }

    
    public void AddNextColor()
    {
        if (accessable.Count == enumVals.Count)
        {
            return;
        }

        accessable.Add((ColorType)enumVals[accessable.Count]);

        if (switchToNewColorWhenAdded)
        {
            current = accessable.Count - 1;
        }
    }
}
