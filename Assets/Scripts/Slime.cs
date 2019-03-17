using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        rigidBody.velocity = GetUpdatedVelocity();
    }

    protected override Vector3 GetUpdatedVelocity()
    {
        double distanceFromTarget = CalculateDistanceFromTarget();
        if (distanceFromTarget < MinDistanceFromTargetToMove || MaxDistanceFromTargetToMove < distanceFromTarget)
        {
            return new Vector3(0f, 0f, 0f);
        }
        var newVelocity = CalculateVelocityToTarget().normalized;
        newVelocity *= MoveSpeed;
        return newVelocity;
    }

}
