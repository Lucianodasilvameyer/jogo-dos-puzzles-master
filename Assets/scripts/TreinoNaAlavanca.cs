using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreinoNaAlavanca : MonoBehaviour
{
    
    public Animator _alavanca; //aqui tem q arrastar o respectivo animator controller para ativalo na hora certa? sim
    public Animator portao; //tem q fazer a referencia igual para controlar o a animação do portão pelo codigo

    private bool playerColidindo = false;
    private bool alavancaAcionada = false;//quqndo não tem serializefield pode setar antes do start?

    [SerializeField]
    bool utilizarPortao;

    [SerializeField]
    private bool destravada; //pq colocar [serializefield] no destravada e no utilizarPortao? para caso mude de valor ver no inspector
    // Start is called before the first frame update
    void Start()
    {
        _alavanca = GetComponent<Animator>();//aqui coloca por codigo o animator na alavanca

          
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            if (!alavancaAcionada && Input.GetKeyDown(KeyCode.E) && destravada && utilizarPortao) 
            {
                abrirPortao();
            }
            
        }
    }
    public void detravar(bool b)
    {
        destravada = b;//no script do Player quando o quadroCertoAtual for igual a Quadro.quantidadeDeQuadros vai chamar a função destravarAlavancaQuadros do game, q por sua vez vai chamar a função destravar da alavanca , a qual ira decidir se o destravado(a) q vai nas condições para abrir o portão esta true ou false;  
    }
    public void acionar()
    {
        _alavanca.ResetTrigger("errar"); //o resttrigger serve para deixar a condição desligada
        _alavanca.SetTrigger("acionar");//o SetTrigger serve para deixar a condição ativa
    }
    
    public void errar() //no script do player quando quadroCertoAtual for ou não for igual a Quadro.quantidadeDeQuadros vai chamar a função descerAlavanca do quadro q por sua vez vai chamar a função acionar da alavanca a qual vai mudar as condições para a animação da alavanca descer ou para subir?
    {
        _alavanca.ResetTrigger("acionar");
        _alavanca.SetTrigger("errar");
    }
    public void abrirPortao()
    {
        if (!utilizarPortao)//não sei onde verifica se esta ou não usando o portão?
        return;

        _alavanca.SetTrigger("Ligar");
        portao.SetTrigger("Abrir");
        alavancaAcionada = true;

        PlayerPrefs.SetInt(gameObject.name, 1);//??
    }
}
