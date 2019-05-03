using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Enemy
{
    private const float initHealth = 5;
    private const string IDLE_LAYER = "IdleLayer";
    private const string MOVE_LAYER = "WalkLayer";
    private const string ATTACK_LAYER = "WalkLayer";

    protected override string getAttackLayerName()
    {
        return ATTACK_LAYER;
    }

    protected override string getIdleLayerName()
    {
        return IDLE_LAYER;
    }

    protected override string getMoveLayerName()
    {
        return MOVE_LAYER;
    }

    protected override float getInitHealth()
    {
        return initHealth;
    }

    protected override bool IsRunningAway()
    {
        return true;
    }
}
