using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{

    private const float initHealth = 20;
    private const string IDLE_LAYER = "IdleAndMoveLayer";
    private const string MOVE_LAYER = "IdleAndMoveLayer";
    private const string ATTACK_LAYER = "IdleAndMoveLayer";

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
