using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 lastDirection;
    private Life life;
    private Rigidbody2DMovement movement;


    public int room = 0;

    private Vector2 PointerPosition { get; set; }


    private static PlayerController _instance;
    public static PlayerController Instance => _instance;
    private void Awake()
    {
        _instance = this;

        life = GetComponent<Life>();
        movement = GetComponent<Rigidbody2DMovement>();
    }

    private void Start()
    {
        life.onDie = OnDie();
    }

    private void Update()
    {
        transform.up = (PointerPosition - (Vector2)transform.position).normalized;
    }

    public void OnPointer(InputAction.CallbackContext input)
    {
        if (!Camera.main)
        {
            return;
        }
        Vector3 mousePos = input.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        PointerPosition = Camera.main.ScreenToWorldPoint(mousePos);

    }

    public void OnMove(InputAction.CallbackContext input)
    {
        movement.SetDirection(input.ReadValue<Vector2>());
    }

    private IEnumerator OnDie()
    {
        GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
        movement.SetDirection(Vector2.zero);
        Debug.Log("Start dying");
        yield return new WaitForSeconds(1);
        Debug.Log("Dead");
    }

    public void ChangeColor(uint lifePoint)
    {
        StartCoroutine(ChangeColorCoroutine());

        IEnumerator ChangeColorCoroutine()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var oldColor = spriteRenderer.color;
            spriteRenderer.color = Color.red;
            yield return null;
            while (life.isInvincible && life.currentLife > 0)
                yield return null;
            spriteRenderer.color = oldColor;
        }
    }
}
