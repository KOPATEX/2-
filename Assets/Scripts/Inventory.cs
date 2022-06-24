using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject invemtory_obj;
    private bool showed = false;

    public GameObject[] slots;
    public static Inventory Instance { get; set; }

    private void Awake()
    {
        Instance = this;

    }

    public void flip()
    {

        if (!showed) {
            show();
        }
        else {
            hide();
        }
    }

    public void show()
    {
        invemtory_obj.SetActive(true);
        showed = true;
    }

    public void hide()
    {
        invemtory_obj.SetActive(false);
        showed = false;
    }

    public void add_to_inventory(Sprite sprite)
    {
        int i = 0;
        while (slots[i].GetComponent<Inventory_slot>().img.enabled){
            i++;
        }

        slots[i].GetComponent<Inventory_slot>().set_item(sprite);
    }
}
