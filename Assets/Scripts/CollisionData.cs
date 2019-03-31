using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionData
{
    private Collision2D collision;

    public Collision2D Collision
    {
        get => collision;
    }

    public void ActivateCollision(Collision2D collision)
    {
        this.collision = collision;
    }

    public void DeactivateCurrentCollision()
    {
        collision = null;
    }

    public bool IsInCollision()
    {
        return collision != null;
    }

    public string GetColliderGameObjectTag()
    {
        return collision.gameObject.tag;
    }

    public GameObject GetGameObject()
    {
        return Collision.gameObject;
    }
}
