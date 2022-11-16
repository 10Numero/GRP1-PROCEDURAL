using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Platform : MonoBehaviour
{
    [Header("Behaviour")]
    [SerializeField] private bool moveXOrY = false;
    [SerializeField] private bool moveXAndY = false;
    [SerializeField] private bool rotateZ = false;

    [Header("Parameters")]
    [SerializeField] private ColorType type;
    [SerializeField] private Vector2 moveSpeedRange = new Vector2(2, 8);
    [SerializeField] private Vector2 rotateSpeedRange = new Vector2(75, 100);
    [SerializeField] private LayerMask blockingLayers;

    private Rigidbody2D rb;

    private Vector2 speed;
    private Vector2 speedSign;

    private float rotateSpeed;
    private float rotateSign;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();


        rb.constraints = RigidbodyConstraints2D.None;
        if (moveXOrY)
        {
            bool x = Random.Range(0f, 1f) > 0.5f ? true : false;
            if (x)
            {
                rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
            }
            else
            {
                rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
            }
        }
        else if (!moveXAndY)
        {
            rb.constraints |= RigidbodyConstraints2D.FreezePositionX;
            rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
        }

        if (!rotateZ)
        {
            rb.constraints |= RigidbodyConstraints2D.FreezeRotation;
        }

        transform.rotation = Quaternion.identity;

        speed = new Vector2(Random.Range(moveSpeedRange.x, moveSpeedRange.y), Random.Range(moveSpeedRange.x, moveSpeedRange.y));
        speedSign = new Vector2(Random.Range(0f, 1f) > 0.5f ? 1 : -1, Random.Range(0f, 1f) > 0.5f ? 1 : -1);

        rotateSpeed = Random.Range(rotateSpeedRange.x, rotateSpeedRange.y);
        rotateSign = Random.Range(0f, 1f) > 0.5f ? 1 : -1;
    }

    #region Editor
#if UNITY_EDITOR

    bool oldXorY = false;
    bool oldXandY = false;

    private void OnValidate()
    {
        if (oldXorY != moveXOrY)
        {
            oldXorY = moveXOrY;

            if (moveXAndY && moveXOrY)
            {
                moveXAndY = false;
                oldXandY = false;
            }
        }

        if (oldXandY != moveXAndY)
        {
            oldXandY = moveXAndY;

            if (moveXAndY && moveXOrY)
            {
                moveXOrY = false;
                oldXorY = false;
            }
        }

        if (Application.isPlaying)
        { 
            Awake();
        }
    }
#endif
    #endregion

    private void Update()
    {
        rb.velocity = speedSign * speed;
        rb.angularVelocity = rotateSign * rotateSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & blockingLayers) != 0)
        {
            speedSign *= -1;
        }
    }
}
