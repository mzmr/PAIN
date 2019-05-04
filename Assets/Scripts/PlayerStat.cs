using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : Stat
{
    private Image content;

    [SerializeField]
    public Text statValue;
    
    protected override void Start()
    {
        base.Start();
        content = GetComponent<Image>();
    }

    protected override void UpdateTextValue()
    {
        if (statValue != null)
        {
            statValue.text = CurrentValue + "/" + MyMaxValue;
        }
    }

    protected override float GetFillAmount()
    {
        return content.fillAmount;
    }

    protected override void SetFillAmount(float fillAmount)
    {
        content.fillAmount = fillAmount;
    }
}
