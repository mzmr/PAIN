using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    private const string IDLE_LAYER = "IdleAndMoveLayer";
    private const string MOVE_LAYER = "IdleAndMoveLayer";
    private const string ATTACK_LAYER = "IdleAndMoveLayer";

    protected override string GetAttackLayerName()
    {
        return ATTACK_LAYER;
    }

    protected override string GetIdleLayerName()
    {
        return IDLE_LAYER;
    }

    protected override string GetMoveLayerName()
    {
        return MOVE_LAYER;
    }

    protected override void EnemyDeadAction()
    {
        var batQuest = FindObjectOfType<BatQuest>();
        if (batQuest != null)
        {
            batQuest.RegisterKilledEnemy();
        }
        base.EnemyDeadAction();
    }
}
