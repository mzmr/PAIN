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
        return GetFollowTargetVelocity();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Instantiate(DamageBurst, transform.position, transform.rotation);
    }

}
