using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Entity
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Die();
            CoinScore.coinCount += 1;
            Inventory.Instance.add_to_inventory(this.gameObject);
        }

    }
}
