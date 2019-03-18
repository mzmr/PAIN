using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected Stat health;

    [SerializeField]
    protected Stat mana;

    [SerializeField]
    private float speed;

    public float Damage;

    protected Vector2 direction;

    protected Vector2 lastDirection;

    protected Animator animator;

    private Rigidbody2D rigidBody;

    protected bool isAttacking = false;

    protected Coroutine attackRoutine;

    public bool IsMoving
    {
        get => direction.x != 0 || direction.y != 0;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        MoveWhileAttacking();
        HandleLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rigidBody.velocity = direction.normalized * speed;
    }

    private void MoveWhileAttacking()
    {
        if (isAttacking)
        {
            direction = lastDirection;
        }
    }

    public void HandleLayers()
    {
        if (isAttacking)
        {
            ActivateLayer("AttackLayer");
        }
        else if (IsMoving)
        {
            ActivateLayer("WalkLayer");

            animator.SetFloat("x", direction.x);
            animator.SetFloat("y", direction.y);
        }
        else
        {
            ActivateLayer("IdleLayer");
        }
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < animator.layerCount; ++i)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
    }

    public void StartAttack()
    {
        isAttacking = true;
        animator.SetBool("attack", isAttacking);
    }

    public void StopAttack()
    {
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            isAttacking = false;
            animator.SetBool("attack", isAttacking);
        }
    }

    public void TakeDamage(float damage)
    {
        health.CurrentValue -= damage;
    }
}
