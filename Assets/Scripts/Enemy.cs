using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character, Attackable
{
    [SerializeField]
    private GameObject healthBarPrefab;

    [SerializeField]
    private float attackSpeed;

    [SerializeField]
    private GameObject targetToFollow;

    [SerializeField]
    private double maxDistanceFromTargetToMove;

    [SerializeField]
    private List<GameObject> loot;

    protected override void Start()
    {
        CreateHealthBar();
        base.Start();
    }
    
    protected override void Update()
    {
        SetMovementDirection();
        CyclicAttack();
        CheckHealth();
        base.Update();
    }

    private void CreateHealthBar()
    {
        GameObject newHealthBar = Instantiate(healthBarPrefab, new Vector2(transform.position.x, transform.position.y + 1), transform.rotation, transform);
        Health = newHealthBar.transform.GetChild(0).gameObject.GetComponent<Stat>();
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
        InvokeRepeating("PerformAttack", 0f, 1f / attackSpeed);
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
        foreach (GameObject o in loot)
        {
            if (o != null)
            {
                Instantiate(o, transform.position, transform.rotation, GameObject.Find("Loot").transform);
            }
        }
    }

    protected double CalculateDistanceFromTarget()
    {
        return Math.Sqrt(
            Math.Pow(Convert.ToDouble(targetToFollow.transform.position.x) - Convert.ToDouble(transform.position.x), 2)
            + Math.Pow(Convert.ToDouble(targetToFollow.transform.position.y) - Convert.ToDouble(transform.position.y), 2)
        );
    }

    private Vector2 CalculateVectorToTarget()
    {
        return new Vector2(
            targetToFollow.transform.position.x - transform.position.x,
            targetToFollow.transform.position.y - transform.position.y
        );
    }

    protected void SetMovementDirection()
    {
        if (CalculateDistanceFromTarget() > maxDistanceFromTargetToMove)
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

    protected override string GetEnemyTag()
    {
        return "player";
    }

    protected virtual bool IsRunningAway()
    {
        return false;
    }
}
