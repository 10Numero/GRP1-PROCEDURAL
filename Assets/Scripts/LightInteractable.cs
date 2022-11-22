using UnityEngine;

public class LightInteractable : MonoBehaviour
{
    public int room { get; set; } = 0;
    public virtual void Interact(ColorType color) {}
}
