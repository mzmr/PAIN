using System.Collections.Generic;
using UnityEngine;

public class CombatCollisionData
{
    public Character AttackingCharacter;
    public Character AttackedCharacter;

    private static readonly List<string> CombatTags = new List<string>(
        (new[] { "Player", "Enemy"}));

    public void AttemptToActivateCombatCollision(Collision2D collision, GameObject attackingGameObject)
    {
        if (!IsGameObjectValid(collision.gameObject) || !IsGameObjectValid(collision.otherCollider.gameObject))
        {
            return;
        }

        if (collision.gameObject.tag == collision.otherCollider.gameObject.tag)
        {
            return;
        }

        AttackingCharacter = attackingGameObject.GetComponent<Character>();
        AttackedCharacter = GetOtherCollidingGameObject(collision, attackingGameObject).GetComponent<Character>();
    }

    private bool IsGameObjectValid(GameObject gameObject)
    {
        return gameObject != null && CombatTags.Contains(gameObject.tag);
    }

    private GameObject GetOtherCollidingGameObject(Collision2D collision, GameObject attackingGameObject)
    {
        if (attackingGameObject.tag == collision.gameObject.tag)
        {
            return collision.otherCollider.gameObject;
        }
        if (attackingGameObject.tag == collision.otherCollider.gameObject.tag)
        {
            return collision.gameObject;
        }
        return null;
    }

    public void DeactivateCombatCollision()
    {
        ResetCharacters();
    }

    private void ResetCharacters()
    {
        AttackingCharacter = null;
        AttackedCharacter = null;
    }

    public bool IsInCollision()
    {
        return AttackedCharacter != null && AttackingCharacter != null;
    }
}
