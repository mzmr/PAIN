using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, Attackable
{
    [SerializeField]
    protected Stat Health;

    [SerializeField]
    protected float speed; //change to private

    public float Damage;

    public GameObject DamageBurst;

    protected Vector2 Direction;

    protected Animator Animator;

    protected Rigidbody2D rigidBody; //change to private

    protected bool IsAttacking = false;

    protected CollisionData CollisionData;

    public bool IsMoving
    {
        get => Math.Abs(Direction.x) > 0.1 || Math.Abs(Direction.y) > 0.1;
    }

    protected abstract string getEnemyTag();
    protected abstract float getInitHealth();
    protected abstract string getIdleLayerName();
    protected abstract string getMoveLayerName();
    protected abstract string getAttackLayerName();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Health.Initialize(getInitHealth(), getInitHealth());
        rigidBody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        CollisionData = new CollisionData();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
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

    public void HandleLayers()
    {
        if (IsAttacking)
        {
            ActivateLayer(getAttackLayerName());
        }
        else if (IsMoving)
        {
            ActivateLayer(getMoveLayerName());

            Animator.SetFloat("x", Direction.x);
            Animator.SetFloat("y", Direction.y);
        }
        else
        {
            ActivateLayer(getIdleLayerName());
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

    protected void PerformAttack()
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
        if (collidingObject.tag != null && collidingObject.tag.ToLower() == getEnemyTag())
        {
            var enemy = collidingObject.GetComponent<Attackable>();
            GiveDamage(enemy, Damage);
        }
    }

    public void GiveDamage(Attackable target, float damage)
    {
        target.TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        Health.CurrentValue -= damage;
        Instantiate(DamageBurst, transform.position, transform.rotation);
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
