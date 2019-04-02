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

    public string GetGameObjectTag()
    {
        return collision.gameObject.tag;
    }

    public GameObject GetGameObject()
    {
        return Collision.gameObject;
    }

    public GameObject GetOtherColliderGameObject()
    {
        return collision.otherCollider.gameObject;
    }

    public string GetOtherColliderGameObjectTag()
    {
        return collision.otherCollider.gameObject.tag;
    }

}
