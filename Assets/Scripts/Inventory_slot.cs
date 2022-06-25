using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_slot : MonoBehaviour
{
    public GameObject item_to_show;
    public bool filled;

    void Awake()
    {
        item_to_show = null;
        filled = false;
    }

    public void set_item(GameObject obj)
    {
        filled = true;
        item_to_show = obj;
        Vector3 new_pos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z -2);
        GameObject tmp = Instantiate(obj, new_pos, Quaternion.Euler(0, 0, 0));
        tmp.transform.SetParent(this.gameObject.transform);
        tmp.GetComponent<Animator>().enabled = true;

    }
}
