using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float InitHealth;

    public float Damage;

    public float MoveSpeed;

    public GameObject TargetToFollow;

    public double MinDistanceFromTargetToMove;

    public double MaxDistanceFromTargetToMove;

    protected Rigidbody2D rigidBody;

    protected double distanceFromTarget;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected abstract Vector3 GetUpdatedVelocity();

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
}
