using UnityEngine;

public abstract class LightInteractable : MonoBehaviour
{
    public ColorType color { get; set; } = ColorType.Green;
    public int room { get; set; } = 0;
    public abstract void InteractStart(ColorType color);
    public abstract void InteractEnd(ColorType color);
}
