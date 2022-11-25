using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Spike : LightInteractable
{
    [Header("Dependencies")]
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Color Parameters")]
    [SerializeField] Color _default = Color.grey;
    [SerializeField] Color _green = Color.green;
    [SerializeField] Color _blue = Color.blue;
    [SerializeField] Color _red = Color.red;

    private void Awake()
    {
        spriteRenderer.color = _default;
    }

    public override void InteractStart(ColorType color)
    {
        this.color = color;

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
    }
    public override void InteractEnd(ColorType color) { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth && enemyHealth.color == color)
        {
            // Apply Damage to Enemy
        }
    }

}
