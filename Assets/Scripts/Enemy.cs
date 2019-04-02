using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, Attackable
{
    public float Health;

    public float Damage;

    public float MoveSpeed;

    public float AttackSpeed;

    public GameObject TargetToFollow;

    public double MaxDistanceFromTargetToMove;

    protected Rigidbody2D RigidBody;

    protected CollisionData CollisionData;

    protected bool IsAttacking = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        CollisionData = new CollisionData();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        RigidBody.velocity = GetUpdatedVelocity();
        CyclicAttack();
        CheckHealth();
    }

    protected abstract Vector3 GetUpdatedVelocity();

    protected void OnCollisionEnter2D(Collision2D other)
    {
        CollisionData.ActivateCollision(other);
    }

    protected void OnCollisionExit2D(Collision2D other)
    {
        CollisionData.DeactivateCurrentCollision();
    }

    protected void CyclicAttack()
    {
        if (CollisionData.IsInCollision() && !IsAttacking)
        {
            StartPerformingAttacks();
        }
        else if(!CollisionData.IsInCollision() && IsAttacking)
        {
            StopPerformingAttacks();
        }
    }

    private void StartPerformingAttacks()
    {
        IsAttacking = true;
        InvokeRepeating("PerformAttack", 0f, 1f / AttackSpeed);
    }

    private void StopPerformingAttacks()
    {
        IsAttacking = false;
        CancelInvoke("PerformAttack");
    }

    protected void PerformAttack()
    {
        if (CollisionData.GetGameObjectTag().ToLower() == "player")
        {
            var player = CollisionData.GetGameObject().GetComponent<Attackable>();
            GiveDamage(player, Damage);
        }
    }

    private void CheckHealth()
    {
        if (Health <= 0)
        {
            EnemyDeadAction();
        }
    }

    protected void EnemyDeadAction()
    {
        Destroy(gameObject);
    }

    protected double CalculateDistanceFromTarget()
    {
        return Math.Sqrt(
            Math.Pow(Convert.ToDouble(TargetToFollow.transform.position.x) - Convert.ToDouble(transform.position.x), 2)
            + Math.Pow(Convert.ToDouble(TargetToFollow.transform.position.y) - Convert.ToDouble(transform.position.y), 2)
        );
    }

    protected Vector3 CalculateVelocityToTarget()
    {
        return new Vector3(
            TargetToFollow.transform.position.x - transform.position.x,
            TargetToFollow.transform.position.y - transform.position.y,
            0f
        );
    }

    public void GiveDamage(Attackable target, float damage)
    {
        target.TakeDamage(damage);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

}
