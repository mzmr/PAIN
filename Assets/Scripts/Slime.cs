using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    private const float initHealth = 20;
    private const string IDLE_LAYER = "Base Layer";
    private const string MOVE_LAYER = "Base Layer";
    private const string ATTACK_LAYER = "Base Layer";

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

}
