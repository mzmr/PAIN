using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float MoveSpeed;
    public GameObject TargetToFollow;
    public double minDistanceFromTargetToMove;
    public double maxDistanceFromTargetToMove;
    private Rigidbody2D rigidBody;
    private double distanceFromTarget;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {      
        rigidBody.velocity = GetUpdatedVelocity();
    }

    private Vector3 GetUpdatedVelocity()
    {
        double distanceFromTarget = CalculateDistanceFromTarget();
        if (distanceFromTarget < minDistanceFromTargetToMove || maxDistanceFromTargetToMove < distanceFromTarget)
        {
            return new Vector3(0f, 0f, 0f);
        }
        var newVelocity = CalculateVelocityToTarget().normalized;
        newVelocity *= MoveSpeed;
        return newVelocity;
    }

    private double CalculateDistanceFromTarget()
    {
        return Math.Sqrt(
            Math.Pow(Convert.ToDouble(TargetToFollow.transform.position.x) - Convert.ToDouble(transform.position.x), 2)
            + Math.Pow(Convert.ToDouble(TargetToFollow.transform.position.y) - Convert.ToDouble(transform.position.y), 2)
        );
    }

    private Vector3 CalculateVelocityToTarget()
    {
        return new Vector3(
            TargetToFollow.transform.position.x - transform.position.x,
            TargetToFollow.transform.position.y - transform.position.y,
            0f
        );
    }

}
