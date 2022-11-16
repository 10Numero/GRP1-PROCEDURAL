using System;
using UnityEngine;

[RequireComponent(typeof(EnemyEyes))]
public class EnemyController : MonoBehaviour
{
    private Rigidbody2DMovement movement;
    [SerializeField] private EnemyEyes eyes;

    private void Awake()
    {
        movement = GetComponent<Rigidbody2DMovement>();
        eyes.OnFindTargetUpdate += OnFindTargetUpdate;
    }

    private void OnFindTargetUpdate(Transform obj)
    {
        // see player
        if (obj != null)
        {
            movement.SetDirection(transform.right);
            
            // look at
            var dir = obj.position - transform.position;
            var angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
            movement.SetDirection(Vector2.zero);
    }

    private void OnValidate()
    {
        eyes = GetComponent<EnemyEyes>();
    }

    private void Start()
    {
        enabled = false;
    }

    private void OnDisable()
    {
        movement.SetDirection(Vector2.zero);
    }

    private void OnDestroy()
    {
        eyes.OnFindTargetUpdate -= OnFindTargetUpdate;
    }
}
