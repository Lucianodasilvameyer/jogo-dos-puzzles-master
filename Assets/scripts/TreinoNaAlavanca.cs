using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreinoNaAlavanca : MonoBehaviour
{
    public string _playerTag;//desnecessario?
    public Animator _alavanca; //aqui tem q arrastar o respectivo animator controller para ativalo na hora certa? 
    public Animator portao; //as alavancas ja controlam os portães, por isso esta parte do portão não é necessaria?

    private bool playerColidindo;
    private bool alavancaAcionada;

    [SerializeField]
    bool utilizarPortao;

    [SerializeField]
    private bool destravada; //pq colocar [serializefield] no destravada e no utilizarPortao?
    // Start is called before the first frame update
    void Start()
    {
        _alavanca = GetComponent<Animator>();//para q serve esta parte sendo q tem q arrastar cada respectiva alavanca para um animator diferente?

        playerColidindo = false; //não poderia setar o playerColidindo e o alavancaAcionada como false antes quando foram declaradas?
        alavancaAcionada = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(playerColidindo && !alavancaAcionada && Input.GetKeyDown(KeyCode.E) && destravada && utilizarPortao) //pq esta verificação e a função abrir abrirPortao vão dentro do update?
       {
            abrirPortao();  
       }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerColidindo = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerColidindo = false;
        }
    }
    public void detravar(bool b)
    {
        destravada = b;//no script do Player quando o quadroCertoAtual for igual a Quadro.quantidadeDeQuadros vai chamar a função destravarAlavancaQuadros do game, q por sua vez vai chamar a função destravar da alavanca , a qual ira decidir se o destravado(a) q vai nas condições para abrir o portão esta true ou false;  
    }
    public void acionar()
    {
        _alavanca.ResetTrigger("errar"); //no site do Unity  tanto o ResetTrigger quanto o SetTrigger redefinem o valor do parametro de acionamento fornecido, mas mesmo assim quando pq a função de descerAlavanca do script do quadro chama esta parte?
        _alavanca.SetTrigger("acionar");
    }
    /*public void descerAlavanca() //pq não posso colocar esta função ja no script da Alavanca
    {
        acionar();
    }
    */
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
