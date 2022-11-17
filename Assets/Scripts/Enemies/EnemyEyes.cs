using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Sirenix.OdinInspector;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class EnemyEyes : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, OnValueChanged("UpdateColliderRadius")] private float viewRadius = 50;
    [Range(0, 360), SerializeField] private float viewAngle = 90;

    [Header("Layers")]
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    [Header("Collider")] [SerializeField] 
    private CircleCollider2D col2d;

    // transform.right not working properly, same for transform.TransformDirection
    private Vector2 LocalDir()
    {
        var angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        var sin = Mathf.Sin( angle );
        var cos = Mathf.Cos( angle );
 
        Vector2 forward = new Vector3(
            Vector2.right.x * cos - Vector2.right.y * sin,
            Vector2.right.x * sin + Vector2.right.y * cos,
            0f );

        return forward;
    }

    public System.Action<Transform> OnFindTargetUpdate;
    
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IsInLayerMask(other))
            return;
        
        var dirToTarget = ((Vector2)other.transform.position - (Vector2)transform.position).normalized;
        
        var angle = (GetAngle(LocalDir(), dirToTarget));
        
        if (!(Mathf.Abs(angle) < viewAngle / 2))
        {
            OnFindTargetUpdate?.Invoke(null);
            return;
        }

        // if hit obstacle, return
        if (Physics2D.Raycast(transform.position, dirToTarget, 50, obstacleMask))
        {
            OnFindTargetUpdate?.Invoke(null);
            return;
        }
        
        Debug.Log("I see < " + other.name + " >");
        OnFindTargetUpdate?.Invoke(other.transform);
    }

    private void UpdateColliderRadius()
    {
        col2d.radius = viewRadius;
    }

    private void OnDrawGizmos()
    {
        var tr = transform;
        var position = tr.position;
        var rotation = tr.rotation;
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(position, viewRadius);
        var dirLeft = -AngleToDir((viewAngle / 2) - 90 - rotation.eulerAngles.z);
        var dirRight = AngleToDir(-(viewAngle / 2) + 90 - rotation.eulerAngles.z);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(position, (Vector2) position + (dirRight * viewRadius));
        Gizmos.DrawLine(position, (Vector2)position + (dirLeft * viewRadius));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(position, LocalDir() * viewRadius + (Vector2)position);
    }

    private static Vector2 AngleToDir(float __angle)
    {
        return new Vector2(Mathf.Sin(Mathf.Deg2Rad * __angle), Mathf.Cos(Mathf.Deg2Rad * __angle));
    }

    private bool IsInLayerMask(Component comp)
    {
        return targetMask == (targetMask | (1 << comp.gameObject.layer));
    }

    private static float GetAngle(Vector2 me, Vector2 target) 
    {
        return Vector2.SignedAngle(me, target);
    }
}
