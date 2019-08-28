using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sino : MonoBehaviour
{
    public TextMeshProUGUI textoCountdown;

    [SerializeField]
    private float TempoDeBatidaInicial;

    [SerializeField]
    private float TempoDebatidaMax;

    
    public bool isTimerOn = false;



    // Start is called before the first frame update

    [SerializeField]
    private float _tempoDoSom;

    public float TempoDoSom
    {
        get
        {
            return _tempoDoSom;
        }
        set
        {
            if (value <= 0)
            {
                _tempoDoSom = 0;
            }

            _tempoDoSom = value;
            textoCountdown.text = TempoDoSom.ToString();//aqui tem q converter o TempoParaBatida para string pq o .text é do tipo string


        }
    }
    void Start()
    {
        TempoDoSom = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= TempoDeBatidaInicial + TempoDebatidaMax && isTimerOn)
        {

            isTimerOn = false;
            textoCountdown.enabled = false;
        }

        if (isTimerOn)
        {
            TempoDoSom = Mathf.Abs((TempoDeBatidaInicial + TempoDebatidaMax) - Time.time);

        }

    }

    public void iniciarCountdown()
    {
        TempoDeBatidaInicial = Time.time;
        isTimerOn = true;
        textoCountdown.enabled = true;
    }

    public bool IsRunning()
    {
        return isTimerOn;
    }
}
