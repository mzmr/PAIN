using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Enemy
{
    private const string IDLE_LAYER = "IdleLayer";
    private const string MOVE_LAYER = "WalkLayer";
    private const string ATTACK_LAYER = "WalkLayer";

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

    protected override bool IsRunningAway()
    {
        return true;
    }
}
