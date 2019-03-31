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
        RigidBody.velocity = GetUpdatedVelocity();
    }

    protected override Vector3 GetUpdatedVelocity()
    {
        double distance = CalculateDistanceFromTarget();
        if  (distance > MaxDistanceFromTargetToMove)
        {
            return new Vector3(0f, 0f, 0f);
        }
        var newVelocity = CalculateVelocityToTarget().normalized;
        newVelocity *= MoveSpeed;
        return newVelocity;
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            var player = other.gameObject.GetComponent<Attackable>();
            player.TakeDamage(Damage);
        }
    }

}
