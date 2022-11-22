using UnityEngine;

public class LightInteractable : MonoBehaviour
{
    public ColorType color { get; set; } = ColorType.Green;
    public int room { get; set; } = 0;
    public virtual void Interact(ColorType color) {}
}
