using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alavanca : MonoBehaviour
{
    public string _playerTag = "Player";//??
    public Animator _alavanca;//aqui tem q arrastar o respectivo animator para ativalo na hora certa? 
    public Animator portao; //as alavancas ja controlam os portães, por isso esta parte não é necessaria?

    [SerializeField]
    bool usaPortao;

    private bool _alavancaAcionada;
    private bool _playerColidindo;

    [SerializeField]
    private bool destravado;

    // Start is called before the first frame update
    void Start()
    {
        _alavanca = GetComponent<Animator>(); 


        _alavancaAcionada = false;
        _playerColidindo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerColidindo && !_alavancaAcionada && Input.GetAxisRaw("Interaction") == 1 && destravado && usaPortao)// o isTimerOn da função IsRunning é true enquanto o timer do Sino estiver rodando  
        {                                                                                                       // não é necessario colocar o textoCountdown.enabled aqui por que o         

            abrirPortao();
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

    public void acionar()
    {
        _alavanca.ResetTrigger("errar");// 
        _alavanca.SetTrigger("acionar");
    }
  /*public void desce
    {
        acionar();
    }
    */
    public void erro()
    {
        _alavanca.ResetTrigger("acionar");
        _alavanca.SetTrigger("errar");
        
    }

    public void abrirPortao()
    {
        if (!usaPortao) return;

        _alavanca.SetTrigger("Ligar");
        portao.SetTrigger("Abrir");
        _alavancaAcionada = true;

        PlayerPrefs.SetInt(gameObject.name, 1);
    }
}
