using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;

   private void Awake()
   {
        pausePanel.SetActive(false);
   }

   
    public void SetPause()
    {
        pausePanel.SetActive(true);
        this.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void PauseOff()
    {
        pausePanel.SetActive(false);
        this.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
}
