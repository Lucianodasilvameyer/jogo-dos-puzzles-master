using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("NewGame", 1);
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("NewGame", 0);
        SceneManager.LoadScene(1);
    }
}
