using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receptor : LightInteractable
{
    [Header("Dependencies")]
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Color Parameters")]
    [SerializeField] Color _default = Color.grey;
    [SerializeField] Color _green = Color.green;
    [SerializeField] Color _blue = Color.blue;
    [SerializeField] Color _red = Color.red;

    bool active = false;

    private void Awake()
    {
        spriteRenderer.color = _default;
    }

    public override void InteractStart(ColorType color)
    {
        this.color = color;

        if (!active)
        {
            ReceptorsManager.Instance.active = ReceptorsManager.Instance.active + 1;
        }

        switch (this.color)
        {
            case ColorType.Green:
                spriteRenderer.color = _green;
                break;
            case ColorType.Blue:
                spriteRenderer.color = _blue;
                break;
            case ColorType.Red:
                spriteRenderer.color = _red;
                break;
            default:
                spriteRenderer.color = _default;
                break;
        }

        active = true;
    }

    public override void InteractEnd(ColorType color) { }


}
