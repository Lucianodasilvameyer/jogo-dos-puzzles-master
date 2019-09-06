using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    [SerializeField]
    Quadro[] quadros;

    private void Start()
    {


        GameObject[] quadrosGo = GameObject.FindGameObjectsWithTag("Quadro");

        quadros = new Quadro[quadrosGo.Length]; // o espaço extra foi salvo no quadros daqui

        for(int i = 0; i < quadrosGo.Length; i++)
        {
            quadros[i] = quadrosGo[i].GetComponent<Quadro>(); //aqui esta tudo junto no quadrosGO
        }

    }
    public void pintarTodosOsQuadros(Color32 color)
    {

        foreach(Quadro q in quadros)
        {
            q.tint(color);
        }
    }
 

}
