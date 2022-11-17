using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Platform : MonoBehaviour
{
    [Header("Behaviour")]
    [SerializeField] private ColorType type;
    [SerializeField] private bool moveXOrY = false;
    [SerializeField] private bool moveXAndY = false;

    [Header("Parameters")]
    [SerializeField] private Vector2 moveSpeedRange = new Vector2(2, 8);
    [SerializeField] private float rotateSpeed = 75;
    [SerializeField] private LayerMask blockingLayers;

    private Rigidbody2D rb;

    private Vector2 speed;
    private Vector2 speedSign;

    private float targetRotation;
    private float rotateSign;

    private bool active = false;

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

        transform.rotation = Quaternion.identity;

        speed = new Vector2(Random.Range(moveSpeedRange.x, moveSpeedRange.y), Random.Range(moveSpeedRange.x, moveSpeedRange.y));
        speedSign = new Vector2(Random.Range(0f, 1f) > 0.5f ? 1 : -1, Random.Range(0f, 1f) > 0.5f ? 1 : -1);

        targetRotation = 0;
        rotateSign = -1;
    }

    public void Activate()
    {
        active = true;

        if (type == ColorType.Green)
        {
            rotateSign *= -1;
            targetRotation = targetRotation == 90 ? 0 : 90;
        }
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
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Activate();
        }

        if (active)
        {
            rb.velocity = speedSign * speed;

            if (type == ColorType.Green)
            {
                rb.angularVelocity = rotateSign * rotateSpeed;

                transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(transform.rotation.eulerAngles.z > 180 ? transform.rotation.eulerAngles.z - 360 : transform.rotation.eulerAngles.z, 0, 90));
                if (transform.rotation.eulerAngles.z == targetRotation)
                {
                    active = false;
                    rb.angularVelocity = 0;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & blockingLayers) != 0)
        {
            speedSign *= -1;
        }
    }
}
