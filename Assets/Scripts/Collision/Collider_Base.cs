using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collider_Base : MonoBehaviour
{
    public virtual void OnTriggerEnter2D(Collider2D col) { }
    public virtual void OnTriggerStay2D(Collider2D col) { }
    public virtual void OnTriggerExit2D(Collider2D col) { }
}
