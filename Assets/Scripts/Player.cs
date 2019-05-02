using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    protected Stat Mana;

    private const float initHealth = 100;
    private const float initMana = 50;
    private const string IDLE_LAYER = "IdleLayer";
    private const string MOVE_LAYER = "WalkLayer";
    private const string ATTACK_LAYER = "AttackLayer";

    private Vector2 LastDirection;

    protected Coroutine AttackRoutine;

    // Start is called before the first frame update
    protected override void Start()
    {
        Mana.Initialize(initMana, initMana);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();
        MoveWhileAttacking();
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

    private void StartAttack()
    {
        IsAttacking = true;
        Animator.SetBool("attack", IsAttacking);
        PerformAttack();
    }

    private void StopAttack()
    {
        if (AttackRoutine != null)
        {
            StopCoroutine(AttackRoutine);
            IsAttacking = false;
            Animator.SetBool("attack", IsAttacking);
        }
    }

    private void MoveWhileAttacking()
    {
        if (IsAttacking)
        {
            Direction = LastDirection;
        }
    }

    protected override string getEnemyTag()
    {
        return "enemy";
    }

    protected override float getInitHealth()
    {
        return initHealth;
    }

    protected override string getIdleLayerName()
    {
        return IDLE_LAYER;
    }

    protected override string getMoveLayerName()
    {
        return MOVE_LAYER;
    }

    protected override string getAttackLayerName()
    {
        return ATTACK_LAYER;
    }
}
