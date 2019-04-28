using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public Text MoneyText;
    public int CurrentGoldAmount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = CurrentGoldAmount.ToString();
    }

    public void AddMoney(int goldToAdd)
    {
        CurrentGoldAmount += goldToAdd;
    }

    public bool SubtractMoney(int goldToSubtract)
    {
        if (goldToSubtract > CurrentGoldAmount)
        {
            return false;
        }

        CurrentGoldAmount -= goldToSubtract;
        return true;
    }

}
