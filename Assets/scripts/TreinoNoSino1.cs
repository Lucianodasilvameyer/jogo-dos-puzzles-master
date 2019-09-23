using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreinoNoSino1 : MonoBehaviour
{
    public TextMeshProUGUI textoCowntdown;

    [SerializeField]
    private float tempoDeBatidaMax;

    [SerializeField]
    Alavanca alavanca;

    private Timer timer;//o Timer seria uma variavel interna?


    private float _tempoDoSom;
    
    public float tempoDoSom
    {
        get
        {
            return _tempoDoSom;
        }
        set
        {
            if (value <= 0)//mas aqui o valor do value não foi definido no start?
            {
                _tempoDoSom = 0;
            }
            _tempoDoSom = value; 
            textoCowntdown.text = tempoDoSom.ToString();//convertendo o tempoDoSom para string?
        } 
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(timer != null)// aqui significa q o tempo de cowntdown esta ativo
       {
            if (!timer.isDone)//aqui significa q o tempo de cowntdown não terminou?
            {
                tempoDoSom = timer.GetTimeRemaining(); //o GetTimeRemaining mostra quanto tempo falta
            }
       } 
    }
    public void iniciarCowntdown()
    {
        if (timer != null)
            timer.Cancel();

        timer = Timer.Register(tempoDeBatidaMax, desativarCowntdown);// aqui cria o timer?
        alavanca.destravar(true);//aqui libera a alavanca do labirinto
        textoCowntdown.enabled = true;



    }
    public void desativarCowntdown()
    {
        textoCowntdown.enabled = false;
        alavanca.destravar(false);
    }
    public bool isRunning()
    {
        return !timer.isDone; ;//??
    }
}
