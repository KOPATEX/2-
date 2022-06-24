using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_slot : MonoBehaviour
{
    public Image img;

    void Start()
    {
        img = GetComponent<Image>();
        img.enabled = false;
        //if (img.value){
        //    this.gameObject.SetActive(false);
        //}
    }

    public void set_item(Sprite sprite)
    {
        img.sprite = sprite;
        img.enabled = true;
    }
}
