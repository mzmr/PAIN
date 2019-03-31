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
        
    }

    protected override Vector3 GetUpdatedVelocity()
    {
        double distance = CalculateDistanceFromTarget();
        if  (distance > MaxDistanceFromTargetToMove)
        {
            return Vector3.zero;
        }
        var newVelocity = CalculateVelocityToTarget().normalized;
        newVelocity *= MoveSpeed;
        return newVelocity;
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        CollisionData.ActivateCollision(other);
    }

    protected override void OnCollisionExit2D(Collision2D other)
    {
        CollisionData.DeactivateCurrentCollision();
    }
}
