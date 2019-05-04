using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private float maxMana;

    [SerializeField]
    private Stat mana;
    
    private const string IDLE_LAYER = "IdleLayer";
    private const string MOVE_LAYER = "WalkLayer";
    private const string ATTACK_LAYER = "AttackLayer";

    private Vector2 lastDirection;

    private Coroutine attackRoutine;

    // Start is called before the first frame update
    protected override void Start()
    {
        mana.Initialize(maxMana, maxMana);
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
            lastDirection = Direction;
        }

        Direction = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.I))
        {
            AddHealth(-10);
            mana.CurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            AddHealth(10);
            mana.CurrentValue += 10;
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

    private void StartAttack()
    {
        IsAttacking = true;
        Animator.SetBool("attack", IsAttacking);
        PerformAttack();
    }

    private void StopAttack()
    {
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            IsAttacking = false;
            Animator.SetBool("attack", IsAttacking);
        }
    }

    private void MoveWhileAttacking()
    {
        if (IsAttacking)
        {
            Direction = lastDirection;
        }
    }

    protected override string GetEnemyTag()
    {
        return "enemy";
    }

    protected override string GetIdleLayerName()
    {
        return IDLE_LAYER;
    }

    protected override string GetMoveLayerName()
    {
        return MOVE_LAYER;
    }

    protected override string GetAttackLayerName()
    {
        return ATTACK_LAYER;
    }

}
