using UnityEngine;

public class Collider_Proxy : MonoBehaviour
{
    [SerializeField] private Collider_Base _colliderBase;
    
    private void OnTriggerEnter2D(Collider2D col) => _colliderBase.OnTriggerEnter2D(col);

    private void OnTriggerStay2D(Collider2D col) => _colliderBase.OnTriggerStay2D(col);

    private void OnTriggerExit2D(Collider2D col) => _colliderBase.OnTriggerExit2D(col);
}
