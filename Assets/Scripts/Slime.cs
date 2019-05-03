using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    private const string IDLE_LAYER = "Base Layer";
    private const string MOVE_LAYER = "Base Layer";
    private const string ATTACK_LAYER = "Base Layer";

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

}
