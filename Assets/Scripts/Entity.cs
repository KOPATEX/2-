using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int lives;
    
    public DropItem[] dropList;
    

    public virtual void GetDamage ()
    {
        lives--;
        if (lives < 1)
            Die();
        
    }


    public virtual void Die()
    {
        lives = 0;
        Destroy(this.gameObject);
        CheckDrop();
        //Inventory.Instance.add_to_inventory(this.gameObject);
    }

    public void CheckDrop()
    {
        if (dropList.Length > 0)
        {
            int rnd = (int)Random.Range(0, 100);

            foreach (var item in dropList)
            {
                if (item.chance >= rnd)
                {
                    item.CreateDropItem(gameObject.transform.position);
                    return;
                }
            }
        }
    }
}
