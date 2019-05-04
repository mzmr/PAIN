using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : Stat
{
    [SerializeField]
    private GameObject fill;
    
    protected override float GetFillAmount()
    {
        return fill.transform.localScale.x;
    }

    protected override void SetFillAmount(float fillAmount)
    {
        var newScale = fill.transform.localScale;
        newScale.x = fillAmount;
        fill.transform.localScale = newScale;
    }
}
