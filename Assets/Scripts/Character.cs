using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, Attackable
{
    [SerializeField]
    protected Stat Health;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float damage;

    [SerializeField]
    private GameObject damageBurst;

    [SerializeField]
    private float maxHealth;

    protected Vector2 Direction;

    protected Animator Animator;

    protected bool IsAttacking = false;

    protected CombatCollisionData CollisionData;

    private Rigidbody2D rigidBody;

    public bool IsMoving
    {
        get => Math.Abs(Direction.x) > 0.1 || Math.Abs(Direction.y) > 0.1;
    }

    protected abstract string GetEnemyTag();
    protected abstract string GetIdleLayerName();
    protected abstract string GetMoveLayerName();
    protected abstract string GetAttackLayerName();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Health.Initialize(maxHealth, maxHealth);
        rigidBody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        CollisionData = new CombatCollisionData();
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
            ActivateLayer(GetAttackLayerName());
        }
        else if (IsMoving)
        {
            ActivateLayer(GetMoveLayerName());

            Animator.SetFloat("x", Direction.x);
            Animator.SetFloat("y", Direction.y);
        }
        else
        {
            ActivateLayer(GetIdleLayerName());
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
        var attackedCharacter = CollisionData.AttackedCharacter;
        if (attackedCharacter.tag.ToLower() == GetEnemyTag())
        {
            var enemy = attackedCharacter.GetComponent<Attackable>();
            GiveDamage(enemy, damage);
        }
    }

    public void GiveDamage(Attackable target, float damage)
    {
        target.TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        Health.CurrentValue -= damage;
        var clonedDamageBurst = Instantiate(damageBurst, transform.position, transform.rotation);
        Destroy(clonedDamageBurst, 1f);
    }

    public void AddHealth(int health)
    {
        Health.CurrentValue += health;
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        CollisionData.AttemptToActivateCombatCollision(other, gameObject);
    }

    protected void OnCollisionExit2D(Collision2D other)
    {
        CollisionData.DeactivateCombatCollision();
    }

}
