using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Stat : MonoBehaviour
{
    [SerializeField]
    private float lerpSpeed;

    private float currentFill;
    private float currentValue;

    public float MyMaxValue { get; set; }
    public float CurrentValue {
        get => currentValue;

        set
        {
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFill = currentValue / MyMaxValue;
            UpdateTextValue();
        }
    }

    protected abstract float GetFillAmount();
    protected abstract void SetFillAmount(float fillAmount);
    protected virtual void UpdateTextValue() { }

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float fillAmount = GetFillAmount();
        if (currentFill != fillAmount)
        {
            SetFillAmount(Mathf.Lerp(fillAmount, currentFill, Time.deltaTime * lerpSpeed));
        }
    }

    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        CurrentValue = currentValue;
    }
}
