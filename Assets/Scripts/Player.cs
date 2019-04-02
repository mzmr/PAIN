using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    private float initHealth = 100;

    private float initMana = 50;

    // Start is called before the first frame update
    protected override void Start()
    {
        Health.Initialize(initHealth, initHealth);
        Mana.Initialize(initMana, initMana);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();
        base.Update();
    }

    private void GetInput()
    {
        if (IsMoving)
        {
            LastDirection = Direction;
        }

        Direction = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.I))
        {
            Health.CurrentValue -= 10;
            Mana.CurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Health.CurrentValue += 10;
            Mana.CurrentValue += 10;
        }

        if (!IsAttacking)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Direction += Vector2.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                Direction += Vector2.down;
            }
            if (Input.GetKey(KeyCode.A))
            {
                Direction += Vector2.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                Direction += Vector2.right;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                AttackRoutine = StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        StartAttack();
        yield return new WaitForSeconds(0.375f);
        StopAttack();
    }

    
}
