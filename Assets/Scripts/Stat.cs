using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    private Image content;

    [SerializeField]
    public Text statValue;

    [SerializeField]
    private GameObject fill;

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

            if (statValue != null)
            {
                statValue.text = currentValue + "/" + MyMaxValue;
            }
        }
    }

    [SerializeField]
    public float lerpSpeed;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        content = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (statValue != null)
        {
            if (currentFill != content.fillAmount)
            {
                content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
            }
        }

        if (fill != null)
        {
            var newScale = fill.transform.localScale;
            newScale.x = Mathf.Lerp(newScale.x, currentFill, Time.deltaTime * lerpSpeed);
            fill.transform.localScale = newScale;
        }
    }

    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        CurrentValue = currentValue;
    }
}
