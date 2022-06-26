using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private GameObject invemtory_ui;
    private int invemtory_uiw = 10;
    private float slot_w = 2f;
    private float step_between_slot = 0.5f;

    private int centerx;
    private int z;

    public GameObject invemtory_slot;

    public int size;
    public GameObject[] slots;

    public static Inventory Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        invemtory_ui = GameObject.FindGameObjectWithTag("inventory_ui");
        invemtory_ui.SetActive(false);

        centerx = (int)invemtory_ui.transform.position.x;
        z = (int)invemtory_ui.transform.position.z - 1;

        float dx = centerx - invemtory_uiw/2 - slot_w/2;

        for (int i = 0; i < size; i++) {
            float new_x = dx;
            Vector3 new_pos = new Vector3(new_x, 0, z);
            GameObject slot = Instantiate(invemtory_slot, new_pos, Quaternion.Euler(0, 0, 0));
            slot.transform.SetParent(invemtory_ui.transform);
            slots[i] = slot;

            dx += slot_w + step_between_slot;
        }

    }

    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            flip();
        }
    }

    public void flip()
    {
        invemtory_ui.SetActive(!invemtory_ui.activeSelf);
    }

    public void add_to_inventory(GameObject obj)
    {
        int i = 0;
        while (slots[i].GetComponent<Inventory_slot>().filled)
        {
            i++;
            if (i >= size) return; //инвентарь закончился
        }
        slots[i].GetComponent<Inventory_slot>().set_item(obj);
    }
}
