using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Sirenix.OdinInspector;
using UnityEngine;

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

    private List<Transform> visibleTargets = new List<Transform>();

    public System.Action<Transform> OnFindTargetUpdate;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!IsInLayerMask(other))
            return;
        
        var dirToTarget = ((Vector2)other.transform.position - (Vector2)transform.position).normalized;

        // Debug.Log("Right < " + transform.right + " > ");
        
        var angle = (float) (GetAngle(-transform.right, dirToTarget));

        // Debug.Log("Angle < " + angle + " >");
        
        if (!(angle < viewAngle + transform.rotation.eulerAngles.z / 2))
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
        var position = transform.position;
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(position, viewRadius);
        var dirLeft = -AngleToDir((viewAngle / 2) - 90 - (float)GetAngle(-transform.right, (Vector2)transform.rotation.eulerAngles)).normalized;
        var dirRight = AngleToDir(-(viewAngle / 2) + 90 - (float)GetAngle(-transform.right, (Vector2)transform.rotation.eulerAngles)).normalized;
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(position, (Vector2) position + (dirRight * viewRadius));
        Gizmos.DrawLine(position, (Vector2)position + (dirLeft * viewRadius));
    }

    private static Vector2 AngleToDir(float __angle)
    {
        return new Vector2(Mathf.Sin(Mathf.Deg2Rad * __angle), Mathf.Cos(Mathf.Deg2Rad * __angle));
    }

    private bool IsInLayerMask(Component comp)
    {
        return targetMask == (targetMask | (1 << comp.gameObject.layer));
    }

    private static double GetAngle(Vector2 me, Vector2 target) 
    {
        return Math.Atan2(target.y - me.y, target.x - me.x) * (180/Math.PI);
    }
}
