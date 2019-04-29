using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Pickable
{

    public int PotionValue;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.ToLower() != "player")
        {
            return;
        }
        Destroy(gameObject);
    }
}
