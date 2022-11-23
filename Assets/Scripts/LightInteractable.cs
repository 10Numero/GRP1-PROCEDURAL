using UnityEngine;

public abstract class LightInteractable : MonoBehaviour
{
    public ColorType color { get; set; } = ColorType.Green;
    public int room { get; set; } = 0;
    public abstract void Interact(ColorType color);
}
