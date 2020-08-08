using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;

    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.MeleeAttack.performed += ctx => DoMeleeAttack();
        //controls.Player.Move.performed += ctx => DoMove(ctx.ReadValue<Vector2>());
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
        DoMove(controls.Player.Move.ReadValue<Vector2>());
    }

    private void DoMeleeAttack()
    {
        Debug.Log("Melee Attack");
    }

    private void DoMove(Vector2 direction)
    {
        transform.position += new Vector3(direction.x, direction.y, 0f) * moveSpeed * Time.deltaTime;
    }
}
