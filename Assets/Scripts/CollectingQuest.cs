using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingQuest : QuestObject
{
    [SerializeField] public int TargetValue;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnItemCollectionAction(Money money)
    {
        if (money.moneyManager.CurrentGoldAmount >= TargetValue)
        {
            EndQuest();
        }
    }
}
