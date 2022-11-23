using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyHealth : LightInteractable
{
    [SerializeField, ReadOnly] private ColorType _colorType;
    [SerializeField, ReadOnly] private int currentHealth;
    
    #region light interactable
    public override void InteractStart(ColorType __color)
    {
        if(_colorType == __color)
            InvokeRepeating(nameof(ApplyDamage), 0, 1);
    }
    
    public override void InteractEnd(ColorType __color)
    {
        if(_colorType == __color)
            CancelInvoke(nameof(ApplyDamage));
    }
    #endregion

    private void ApplyDamage()
    {
        currentHealth--;
        
        if(currentHealth <= 0)
            Death();
    }
    
    private void Death()
    {
        // spawn particle on death ?
        gameObject.SetActive(false);
    }
}
