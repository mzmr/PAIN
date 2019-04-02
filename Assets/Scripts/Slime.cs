using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{

    public GameObject DamageBurst;

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

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Instantiate(DamageBurst, transform.position, transform.rotation);
    }

}
