using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    public Button[] levels;

    
    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for(int i=0; i < levels.Length; i++)
        {
            if (i + 1 > levelReached)
                levels[i].interactable = false;

        }
    }

    
    public void Select(int numberInBuild)
    {
        SceneManager.LoadScene(numberInBuild);
    }
}
