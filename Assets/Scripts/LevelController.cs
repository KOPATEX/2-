using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private int keysOnScene;
    public static LevelController Instance { get; set; }

    
    private void Awake ()
    {
        Instance = this;

    }

   
    public void KeysCount()
    {
        GameObject[] keys = GameObject.FindGameObjectsWithTag("keys");
        keysOnScene = keys.Length;
        keysOnScene -= 1;
        
        if (keysOnScene == 0)
        {
            Hero.Instance.Invoke("SetLosePanel", 1.1f);
        }
    }
    private void Update()
    {
        //Debug.Log("ключей" + keysOnScene);
    }
}
