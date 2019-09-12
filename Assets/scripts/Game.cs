using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    public TextMeshProUGUI textoGameOver;

    bool gameOver = false;

    [SerializeField]
    Quadro[] quadros;

    [SerializeField]
    Alavanca alavancaQuadros;

    [SerializeField]
    Alavanca alavancaLabirinto;

    [SerializeField]
    Alavanca alavancaPecas;
    [SerializeField]
    PuzzleImagem puzzle;


    private void Start()
    {
        //1 == true
        //0 == false
        if (PlayerPrefs.GetInt("NewGame") == 1) // caso comece um novo jogo
        {
            //limpar os puzzles completados
            PlayerPrefs.SetInt(alavancaQuadros.name, 0);
            PlayerPrefs.SetInt(alavancaLabirinto.name, 0);
            PlayerPrefs.SetInt(alavancaPecas.name, 0);
        }
        else //caso seja modo "Continue"
        {
            //verifica quais puzzles já havia completado
            if (PlayerPrefs.GetInt(alavancaQuadros.name) == 1)
            {
                //puzzle quadros completo
                alavancaPecas.abrirPortao();
                pintarTodosOsQuadros(Color.green);
                foreach (Quadro q in quadros)
                {
                    q.descerAlavanca();
                }
            }

            if (PlayerPrefs.GetInt(alavancaLabirinto.name) == 1)
            {
                //puzzle labirinto completo
                alavancaLabirinto.abrirPortao();
            }

            if (PlayerPrefs.GetInt(alavancaPecas.name) == 1)
            {
                //puzzle pecas completo
                alavancaPecas.abrirPortao();
                puzzle.setupCompleto();
            }
        }



        GameObject[] quadrosGo = GameObject.FindGameObjectsWithTag("Quadro");

        quadros = new Quadro[quadrosGo.Length]; // o espaço extra foi salvo no quadros daqui

        for (int i = 0; i < quadrosGo.Length; i++)
        {
            quadros[i] = quadrosGo[i].GetComponent<Quadro>(); //aqui esta tudo junto no quadrosGO
        }

    }
    public void pintarTodosOsQuadros(Color32 color)
    {

        foreach (Quadro q in quadros)
        {
            q.tint(color);
        }
    }
    public void resetarTodosOsQuadros()
    {
        foreach (Quadro q in quadros)
        {
            q.subirAlavanca();
        }
    }


    public void destravarAlavancaQuadros()
    {
        alavancaQuadros.destravar(true);
    }
    public bool isGameOver()
    {
        return gameOver;
    }

    public void GameOver()
    {
        textoGameOver.gameObject.SetActive(true);
        textoGameOver.text = "GameOver";
        gameOver = true;


    }
}