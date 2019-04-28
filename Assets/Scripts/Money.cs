using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int CoinValue;
    private MoneyManager moneyManager;

    // Start is called before the first frame update
    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.ToLower() == "player")
        {
            moneyManager.AddMoney(CoinValue);
            Destroy(gameObject);
        }
    }
}
