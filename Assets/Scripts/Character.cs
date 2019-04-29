using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, Attackable
{
    [SerializeField]
    protected Stat Health;

    [SerializeField]
    protected Stat Mana;

    [SerializeField]
    private float speed;

    public float Damage;

    protected Vector2 Direction;

    protected Vector2 LastDirection;

    protected Animator Animator;

    private Rigidbody2D rigidBody;

    protected bool IsAttacking = false;

    protected Coroutine AttackRoutine;

    protected CollisionData CollisionData;

    public bool IsMoving
    {
        get => Math.Abs(Direction.x) > 0.1 || Math.Abs(Direction.y) > 0.1;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        CollisionData = new CollisionData();
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
        rigidBody.velocity = Direction.normalized * speed;
    }

    private void MoveWhileAttacking()
    {
        if (IsAttacking)
        {
            Direction = LastDirection;
        }
    }

    public void HandleLayers()
    {
        if (IsAttacking)
        {
            ActivateLayer("AttackLayer");
        }
        else if (IsMoving)
        {
            ActivateLayer("WalkLayer");

            Animator.SetFloat("x", Direction.x);
            Animator.SetFloat("y", Direction.y);
        }
        else
        {
            ActivateLayer("IdleLayer");
        }
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < Animator.layerCount; ++i)
        {
            Animator.SetLayerWeight(i, 0);
        }

        Animator.SetLayerWeight(Animator.GetLayerIndex(layerName), 1);
    }

    public void StartAttack()
    {
        IsAttacking = true;
        Animator.SetBool("attack", IsAttacking);
        AttackEnemy();
    }

    protected void AttackEnemy()
    {
        if (!CollisionData.IsInCollision())
        {
            return;
        }
        var collidingObject = CollisionData.GetCollidingGameObject(gameObject);
        if (collidingObject == null)
        {
            return;
        }
        if (collidingObject.tag != null && collidingObject.tag.ToLower() == "enemy")
        {
            var enemy = collidingObject.GetComponent<Attackable>();
            GiveDamage(enemy, Damage);
        }
    }

    public void StopAttack()
    {
        if (AttackRoutine != null)
        {
            StopCoroutine(AttackRoutine);
            IsAttacking = false;
            Animator.SetBool("attack", IsAttacking);
        }
    }

    public void GiveDamage(Attackable target, float damage)
    {
        target.TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        Health.CurrentValue -= damage;
    }

    public void AddHealth(int health)
    {
        Health.CurrentValue += health;
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        CollisionData.ActivateCollision(other);
    }

    protected void OnCollisionExit2D(Collision2D other)
    {
        CollisionData.DeactivateCurrentCollision();
    }

}
