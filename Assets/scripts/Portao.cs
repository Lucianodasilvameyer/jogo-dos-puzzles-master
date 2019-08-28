using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portao : MonoBehaviour
{
    public string _playerTag = "Player";//??
    public Animator _alavanca;//??
    public Animator _portao1;
    public Animator _portao2;

    private bool _alacancaAcionada;
    private bool _playerColidindo;

    // Start is called before the first frame update
    void Start()
    {
        _alacancaAcionada = false;
        _playerColidindo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerColidindo && !_alacancaAcionada && Input.GetKeyDown(KeyCode.P))
        {
            _alavanca.SetTrigger("Ligar");
            _portao1.SetTrigger("Abrir");
            _alacancaAcionada = true;
        }
    }
    private void OnTriggerEnter(Collider other)// se entrou na tag o _playerColidindo fica true ??
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerColidindo = true;
        }
    }
    private void onTriggerExit(Collider other)//se saiu na tag o _playerColidindo fica false???
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerColidindo = false;
        }
    }
}
