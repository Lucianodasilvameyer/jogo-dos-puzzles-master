using TMPro;
using UnityEngine;

public class Sino : MonoBehaviour
{
    public TextMeshProUGUI textoCountdown;

    [SerializeField]
    private float TempoDebatidaMax;

    private Timer timer;
    [SerializeField]
    Alavanca alavanca;

    // Start is called before the first frame update

    // [SerializeField]
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

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (timer != null)
        {
            if (!timer.isDone)
            {
                TempoDoSom = timer.GetTimeRemaining(); //GetTimeRemaining diz quanto tempo falta para acaber 
            }                                          //aqui salva no tempoDoSom para mostra o tempo nas propriedades e no texto
        }
    }

    public void iniciarCountdown()
    {
        // TempoDeBatidaInicial = Time.time;
        if (timer != null)
            timer.Cancel(); // se não tiver null, precisa cancelar o timer anterior

        timer = Timer.Register(TempoDebatidaMax, desativarCountdown); // aqui cria o timer

        textoCountdown.enabled = true;
        alavanca.destravar(true);


    }

    public void desativarCountdown()
    {
        textoCountdown.enabled = false;
        alavanca.destravar(false);
    }

    public bool IsRunning()
    {
        return !timer.isDone;//aqui retorna true ou false dependendo do timer e da função iniciar coutdown
    }
}