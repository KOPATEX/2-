using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : Entity
{

    public Sprite key_sprite;
   
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            
            Die();
            LevelController.Instance.KeysCount();
            Inventory.Instance.add_to_inventory(key_sprite);
        }
       
    }
}
