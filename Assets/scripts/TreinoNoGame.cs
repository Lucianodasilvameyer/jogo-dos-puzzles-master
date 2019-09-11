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

        int[] numeros = { 1, 5, 10 };
        int[] segundoArray = new int[3];
        segundoArray[0] = 100;
        segundoArray[1] = 5;

        numeros = segundoArray;


        GameObject []QuadroRe = GameObject.FindGameObjectsWithTag("Quadro"); //pq criar um array aqui?

        quadros = new Quadro[QuadroRe.Length]; // aqui tem q deixar espaço livre para usar o array ou simplesmente criar espaço no array quadros definindo q ele terá o mesmo espaço que o QuadroRe 

        for(int i =0; i< QuadroRe.Length; i++)//pq aqui tem q percorrer o array QuadroRe  
        {
            quadros[i] = QuadroRe[i].GetComponent<Quadro>();//aqui pega cada quadro e coloca dentro do array quadros
        }

        if (PlayerPrefs.GetInt("NewGame") == 1)
        {
            PlayerPrefs.SetInt(alavancaLabirinto.name, 0);
            PlayerPrefs.SetInt(alavancaPecas.name, 0);
            PlayerPrefs.SetInt(alavancaQuadros.name, 0);
        }
        else
        {
            if (PlayerPrefs.GetInt(alavancaQuadros.name) == 1)
            {
                alavancaQuadros.abrirPortao();
                pintarOsQuadros(Color.blue);

                foreach (Quadro q in quadros)
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

                puzzle.setupCompleto();//aqui a função do script do puzzleimagem q soluciona o jogo
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void pintarOsQuadros(Color32 color)
    {
        foreach (Quadro q in quadros) //aqui o parametro utiliza todos os quadros?sim
        {
            //q.Pintar(color);
        }
    }
    public void resetarTodosOsQuadros()
    {
        foreach(Quadro q in quadros)
        {
            q.subirAlavanca(); //no script do player quando o quadroCertoAtual - 1 for diferente do quadroQueEstouClicando vai chamar a função de resetarTodosOsQuadros do script do Game q vai chamar a função subir alavanca do script do quadro q vai chamar a função erro(errar) da alavanca q vai mudar as condições para ativar a animação da alavanca subindo
        }
    }
    public void destravarAlavancaQuadros()//mas e as alavancas do labirinto e do jogo das peças? no caso do labirinto é o sino q libera a alavanca q destranca o portão
    {
        alavancaQuadros.destravar(true);
    }
}
