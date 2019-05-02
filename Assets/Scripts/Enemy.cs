using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character, Attackable
{
    public float AttackSpeed;

    public GameObject TargetToFollow;

    public double MaxDistanceFromTargetToMove;

    public List<GameObject> Loot;

    // Update is called once per frame
    protected override void Update()
    {
        SetMovementDirection();
        CyclicAttack();
        CheckHealth();
        base.Update();
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

    private void CheckHealth()
    {
        if (Health.CurrentValue <= 0)
        {
            EnemyDeadAction();
        }
    }

    protected void EnemyDeadAction()
    {
        Destroy(gameObject);
        DropLoot();
    }

    protected void DropLoot()
    {
        foreach (GameObject o in Loot)
        {
            if (o != null)
            {
                Instantiate(o, transform.position, transform.rotation);
            }
        }
    }

    protected double CalculateDistanceFromTarget()
    {
        return Math.Sqrt(
            Math.Pow(Convert.ToDouble(TargetToFollow.transform.position.x) - Convert.ToDouble(transform.position.x), 2)
            + Math.Pow(Convert.ToDouble(TargetToFollow.transform.position.y) - Convert.ToDouble(transform.position.y), 2)
        );
    }

    private Vector2 CalculateVectorToTarget()
    {
        return new Vector2(
            TargetToFollow.transform.position.x - transform.position.x,
            TargetToFollow.transform.position.y - transform.position.y
        );
    }

    protected void SetMovementDirection()
    {
        if (CalculateDistanceFromTarget() > MaxDistanceFromTargetToMove)
        {
            Direction = Vector2.zero;
        }
        else
        {
            Vector2 d = CalculateVectorToTarget();

            if (IsRunningAway())
            {
                Direction = new Vector2(-d.x, -d.y).normalized;
            }
            else
            {
                Direction = d.normalized;
            }
        }
    }

    protected override string getEnemyTag()
    {
        return "player";
    }

    protected virtual bool IsRunningAway()
    {
        return false;
    }
}
