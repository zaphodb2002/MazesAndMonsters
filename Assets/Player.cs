using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] bool mouseLook = false;
    [SerializeField] float moveSpeed = 3f;

    

    private Controls controls;
    private Rigidbody2D rb;
    private Animator anim;

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

    private void FixedUpdate()
    {
        if (mouseLook)
        {
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(controls.Player.Look.ReadValue<Vector2>());
            FaceDirection(worldMousePos);
        }
        
        DoMove(controls.Player.Move.ReadValue<Vector2>());
    }

    private void DoMeleeAttack()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(controls.Player.Look.ReadValue<Vector2>());
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
}
