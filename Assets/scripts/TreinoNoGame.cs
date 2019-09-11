using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreinoNoGame : MonoBehaviour
{
    Quadro[] quadros;

    [SerializeField]
    Alavanca alavancaLabirinto;//aqui faz a referencia pra mecher neles pelo codigo?

    [SerializeField]
    Alavanca alavancaQuadros;

    [SerializeField]
    Alavanca alavancaPecas; //mas o alavancaPecas e o puzzle não são o mesmo? 

    [SerializeField]
    PuzzleImagem puzzle;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("NewGame") == 1)
        {
            PlayerPrefs.SetInt(alavancaLabirinto.name, 0);
            PlayerPrefs.SetInt(alavancaPecas.name,0);
            PlayerPrefs.SetInt(alavancaQuadros.name,0);
        }
        else
        {
            if (PlayerPrefs.GetInt(alavancaQuadros.name)==1)
            {
                alavancaQuadros.abrirPortao();
                pintarOsQuadros(Color.blue);

                foreach(Quadro q in quadros)
                {
                    q.descerAlavanca();
                }
            }
            if (PlayerPrefs.GetInt(alavancaLabirinto.name) == 1)
            {
                alavancaLabirinto.abrirPortao();
            }
            if (PlayerPrefs.GetInt(alavancaPecas.name) == 1)
            {
                alavancaPecas.abrirPortao();

                puzzle.setupCompleto();//como o puzzle se refere a alavancaPecas? //o setupCompleto é uma função interna?
            }

        }


        GameObject []QuadroRe = GameObject.FindGameObjectsWithTag("Quadro"); //pq criar um array aqui?

        quadros = new Quadro[QuadroRe.Length];//pq criar um quadro novo aqui? //pq colocar o tamanho do array entre conchetes?

        for(int i =0; i< QuadroRe.Length; i++)//pq aqui tem q percorrer o array QuadroRe  
        {
            quadros[i] = QuadroRe[i].GetComponent<Quadro>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pintarOsQuadros(Color32 color)//o parametro Color32 color serve para que? 
    {
        foreach (Quadro q in quadros) //aqui o parametro utiliza todos os quadros?
        {
            q.Pintar(color);
        }
    }
    public void resetarTodosOsQuadros()
    {
        foreach(Quadro q in quadros)
        {
            q.subirAlavanca(); //no script do player quando o quadroCertoAtual - 1 for diferente do quadroQueEstouClicando vai chamar a função de resetarTodosOsQuadros do script do Game q vai chamar a função subir alavanca do script do quadro q vai chamar a função erro(errar) da alavanca q vai mudar as condições para ativar a animação da alavanca subindo
        }
    }
    public void destravarAlavancaQuadros()//mas e as alavancas do labirinto e do jogo das peças?
    {
        alavancaQuadros.destravar(true);
    }
}
