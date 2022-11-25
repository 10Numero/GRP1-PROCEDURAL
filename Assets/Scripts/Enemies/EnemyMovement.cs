using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyEyes))]
public class EnemyMovement : MonoBehaviour
{
    public enum EnemyMode
    {
        StationaryRond,
        LineMoveTriangle,
        FreeMoveCarre
    }
    
    [Header("Mode")] 
    [SerializeField] private EnemyMode _enemyMode;
    
    [Header("Components")]
    [SerializeField] private EnemyEyes eyes;
    [SerializeField] private Rigidbody2DMovement movement;
    [SerializeField] private EnemyHealth health;
    
    [HideInInspector] public List<Vector3> _lineMovePoints = new List<Vector3>();
    
    private int _lineMovePointIndex = 1;
    public bool _spottedPlayer;

    
    #region monobehaviour
    private void Awake()
    {
        movement = GetComponent<Rigidbody2DMovement>();
        eyes.OnFindTargetUpdate += OnFindTargetUpdate;
    }
    
    private void Update()
    {
        if (_enemyMode == EnemyMode.LineMoveTriangle)
        {
            if (_spottedPlayer)
                return;
        
            movement.SetDirection(transform.right);

            LookAt(_lineMovePoints[_lineMovePointIndex]);
        
            var dst = Vector2.Distance(transform.position, _lineMovePoints[_lineMovePointIndex]);

            if (dst <= 0.1f)
            {
                _lineMovePointIndex++;

                if (_lineMovePointIndex >= 2)
                    _lineMovePointIndex = 0;
            }
        }
        else if (_enemyMode == EnemyMode.FreeMoveCarre)
        {
            if (health.room != PlayerController.Instance.room)
            {
                movement.SetDirection(Vector2.zero);
                return;
            }

            LookAt(PlayerController.Instance.transform.position);
            movement.SetDirection(transform.right);
        }
    }

    private void OnDisable()
    {
        movement.SetDirection(Vector2.zero);
    }

    private void OnDestroy()
    {
        eyes.OnFindTargetUpdate -= OnFindTargetUpdate;
    }
    
    private void OnValidate()
    {
        eyes = GetComponent<EnemyEyes>();
    }
    #endregion

    private void OnFindTargetUpdate(Transform obj)
    {
        if (_enemyMode != EnemyMode.FreeMoveCarre || health.room != PlayerController.Instance.room)
        {
            movement.SetDirection(Vector2.zero);
            return;
        }

        // see player
        if (obj != null)
        {
            _spottedPlayer = true;

            movement.SetDirection(transform.right);

            // look at
            LookAt(obj.position);
        }
        else
        {
            _spottedPlayer = false;
        }
    }

    
    
    private void LookAt(Vector3 obj)
    {
        // look at
        var dir = obj - transform.position;
        var angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void SetEnemyMode(EnemyMode __enemyMode) => _enemyMode = __enemyMode;
}