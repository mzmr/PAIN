using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private Stat health;

    [SerializeField]
    private Stat mana;

    private float initHealth = 100;
    private float initMana = 50;

    // Start is called before the first frame update
    protected override void Start()
    {
        health.Initialize(initHealth, initHealth);
        mana.Initialize(initMana, initMana);
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
            lastDirection = direction;
        }

        direction = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.I))
        {
            health.CurrentValue -= 10;
            mana.CurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.CurrentValue += 10;
            mana.CurrentValue += 10;
        }

        if (!isAttacking)
        {
            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector2.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector2.down;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector2.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector2.right;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                attackRoutine = StartCoroutine(Attack());
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
