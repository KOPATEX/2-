using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salesman : MonoBehaviour
{

    public GameObject man_prefab;
    public GameObject price_text_prefab;

    private int n_items;
    public GameObject[] item_prefabs;

    public float sale_koef = 1;

    void Awake()
    {
        GameObject man = Instantiate(man_prefab, transform.position, Quaternion.Euler(0, 0, 0));
        man.transform.SetParent(this.transform);

        n_items = item_prefabs.Length;
        float new_x = transform.position.x + 2;

        foreach (GameObject obj in item_prefabs)
        {
            Vector3 new_pos = new Vector3(new_x, transform.position.y, transform.position.z);
            GameObject item = Instantiate(obj, new_pos, Quaternion.Euler(0, 0, 0));

            SaleItem saleitem = item.AddComponent<SaleItem>() as SaleItem;

            Entity entity = item.GetComponent<Entity>();
            saleitem.actual_price = entity.price * sale_koef;

            saleitem.saled = false;
            Destroy(entity);

            item.transform.SetParent(this.transform);

            GameObject price_text = Instantiate(price_text_prefab, new_pos, Quaternion.Euler(0, 0, 0));
            price_text.transform.SetParent(item.transform);
            price_text.GetComponent<TextMesh>().text = saleitem.actual_price.ToString();

            new_x += 5;
        }
    }

    public void sayPoor()
    {
        StartCoroutine(sayRootine("нищук вали работать"));
    }

    public void sayGood()
    {
        StartCoroutine(sayRootine("приходите еще!"));
    }

    IEnumerator sayRootine(string s)
    {
        Vector3 new_pos = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        GameObject poor_text = Instantiate(price_text_prefab, new_pos, Quaternion.Euler(0, 0, 0));
        poor_text.transform.SetParent(this.gameObject.transform);
        poor_text.gameObject.GetComponent<TextMesh>().text = s;

        yield return new WaitForSeconds(6f);
        Destroy(poor_text);
    }
}
