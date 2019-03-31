using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Attackable
{
    void GiveDamage(Attackable target, float damage);
    void TakeDamage(float damage);
}
