using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScore : MonoBehaviour
{
    public static int coinCount;
    private Text coinConter;

    void Start()
    {
        coinConter = GetComponent<Text>();
        coinCount = 0;
    }

   
    void Update()
    {
        coinConter.text = "X" + coinCount;
    }
}
