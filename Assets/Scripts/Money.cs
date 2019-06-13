using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : Pickable
{
    public int CoinValue;
    public MoneyManager moneyManager;
    [SerializeField] private CollectingQuest collectingQuest;

    // Start is called before the first frame update
    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.ToLower() == "player")
        {
            moneyManager.AddMoney(CoinValue);
            collectingQuest = FindObjectOfType<CollectingQuest>();
            if (collectingQuest != null)
            {
                collectingQuest.OnItemCollectionAction(this);
            }
            Destroy(gameObject);
        }
    }
}
