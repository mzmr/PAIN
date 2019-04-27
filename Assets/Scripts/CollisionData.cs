using System;
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

    private string GetGameObjectTag()
    {
        return collision.gameObject.tag;
    }

    private GameObject GetGameObject()
    {
        return Collision.gameObject;
    }

    private GameObject GetOtherColliderGameObject()
    {
        return collision.otherCollider.gameObject;
    }

    private string GetOtherColliderGameObjectTag()
    {
        return collision.otherCollider.gameObject.tag;
    }

    public bool DifferentColliders()
    {
        return GetGameObjectTag() != GetOtherColliderGameObjectTag();
    }

    public string GetCollidingGameObjectTag(GameObject gameObject)
    {
        if (gameObject.tag == GetGameObjectTag())
        {
            return GetOtherColliderGameObjectTag();
        }
        else if(gameObject.tag == GetOtherColliderGameObjectTag())
        {
            return GetGameObjectTag();
        }
        return "";
    }

    public GameObject GetCollidingGameObject(GameObject thisGameObject)
    {
        if (thisGameObject.tag == GetGameObjectTag())
        {
            return GetOtherColliderGameObject();
        }
        else if (thisGameObject.tag == GetOtherColliderGameObjectTag())
        {
            return GetGameObject();
        }
        return null;
    }

}
