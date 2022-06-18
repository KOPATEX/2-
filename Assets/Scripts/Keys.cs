using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : Entity
{
   
    private void start()
    {
        //lives = 1;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
           // Hero.Instance.GetDamage();
            //lives=0;
            Die();
            LevelController.Instance.KeysCount();
            
        }
       
    }
}
