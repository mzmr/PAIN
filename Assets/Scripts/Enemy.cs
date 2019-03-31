using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, Attackable
{
    public float InitHealth;

    public float Damage;

    public float MoveSpeed;

    public GameObject TargetToFollow;

    public double MaxDistanceFromTargetToMove;

    protected Rigidbody2D RigidBody;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected abstract Vector3 GetUpdatedVelocity();

    protected abstract void OnCollisionEnter2D(Collision2D other);

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
        InitHealth -= damage;
    }
}
