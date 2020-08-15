using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] bool mouseLook = false;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Transform directionIndicatorPivot;
    

    private Controls controls;
    private Rigidbody2D rb;
    private Animator anim;

    private Vector3 worldMousePos;

    private void Awake()
    {
        controls = new Controls();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        controls.Player.MeleeAttack.performed += ctx => DoMeleeAttack();
        
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        worldMousePos = Camera.main.ScreenToWorldPoint(controls.Player.Look.ReadValue<Vector2>());
    }

    private void FixedUpdate()
    {
        SetIndicatorDirection(worldMousePos);

        if (mouseLook)
        {
            FaceDirection(worldMousePos);
        }
        
        DoMove(controls.Player.Move.ReadValue<Vector2>());
    }

    private void DoMeleeAttack()
    {
        FaceDirection(worldMousePos);
        Debug.Log("Melee Attack");
    }

    private void DoMove(Vector2 direction)
    {
        if (direction.sqrMagnitude == 0f)
        {
            anim.SetFloat("Speed", 0f);
            return;
        }
        if (!mouseLook)
        {
            FaceDirection(direction);
        }

        anim.SetFloat("Speed", moveSpeed);
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    private void FaceDirection(Vector2 vector)
    {
        Vector2 directionFromPlayer = vector - new Vector2(transform.position.x, transform.position.y);
        
        anim.SetFloat("Horizontal", vector.x);
        anim.SetFloat("Vertical", vector.y);
    }

    private void SetIndicatorDirection(Vector2 vector)
    {
        Vector2 directionFromPlayer = vector - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(directionFromPlayer.y, directionFromPlayer.x) * Mathf.Rad2Deg;
        directionIndicatorPivot.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

    }
}
