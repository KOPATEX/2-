using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaleItem : MonoBehaviour
{
    public float actual_price;
    
    public bool saled;

    void OnMouseUp()
    {
        if (saled) return;

        if (actual_price > CoinScore.coinCount)
        {
            this.gameObject.transform.parent.GetComponent<Salesman>().sayPoor();
            return;
        }

        saled = true;
        CoinScore.coinCount -= actual_price;
        Inventory.Instance.add_to_inventory(this.gameObject);
        this.gameObject.transform.parent.GetComponent<Salesman>().sayGood();

        Destroy(this.gameObject);
    }
}
