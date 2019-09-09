using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alavanca : MonoBehaviour
{
    public string _playerTag = "Player";//??
    public Animator _alavanca;//??
    public Animator _portao1;
    public Animator _portao2;
    public Animator _portao3;

    private bool _alacancaAcionada;
    private bool _playerColidindo;

    [SerializeField]
    private bool destravado;

    // Start is called before the first frame update
    void Start()
    {
      


        _alacancaAcionada = false;
        _playerColidindo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerColidindo && !_alacancaAcionada && Input.GetKeyDown(KeyCode.E) && destravado)// o isTimerOn da função IsRunning é true enquanto o timer do Sino estiver rodando  
        {                                                                                                       // não é necessario colocar o textoCountdown.enabled aqui por que o         

            _alavanca.SetTrigger("Ligar");
            _portao1.SetTrigger("Abrir");
            _alacancaAcionada = true;
        }
    }
    private void OnTriggerEnter(Collider other)// quando entrou na tag o _playerColidindo fica true 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerColidindo = true;


        }
    }
    private void onTriggerExit(Collider other)//quando saiu na tag o _playerColidindo fica false
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerColidindo = false;
        }
    }

    public void destravar(bool b)
    {
        destravado = b;
    }
}
