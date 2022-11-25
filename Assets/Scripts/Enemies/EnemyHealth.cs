using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyHealth : LightInteractable
{
    public static Action<EnemyHealth> OnEnemyDeath;

    [SerializeField] private int currentHealth = 1;
    
    #region light interactable
    public override void InteractStart(ColorType __color)
    {
        if(color == __color)
            InvokeRepeating(nameof(ApplyDamage), 0, 1);
    }
    
    public override void InteractEnd(ColorType __color)
    {
        if(color == __color)
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
        OnEnemyDeath?.Invoke(this);
        gameObject.SetActive(false);
    }
}
