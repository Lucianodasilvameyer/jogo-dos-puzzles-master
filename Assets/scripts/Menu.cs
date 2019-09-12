using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{

    [SerializeField]
    Toggle opcaoJoystick;
    // Start is called before the first frame update
    void Start()
    {
        UsarJoystick(opcaoJoystick);
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


    public void UsarJoystick(Toggle opcao)
    {
        if(opcao.isOn)
        PlayerPrefs.SetInt("UseJoystick", 1);
        else
            PlayerPrefs.SetInt("UseJoystick", 0);
    }


    public void Quit()
    {
        Debug.Log("Saiu");
        Application.Quit();
    }
}
