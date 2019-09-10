using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadro : MonoBehaviour
{
    [SerializeField]
    int id;

    [SerializeField]
    public static int quantidadeDeQuadros; //a variavel do tipo static não precisa estanciar por qu sempre tem o mesmo valor

    [SerializeField]
    int quantidadeDeQuadros_;
    [SerializeField]
    Alavanca alavanca;
    // Start is called before the first frame update
    void Start()
    {
        quantidadeDeQuadros = quantidadeDeQuadros_;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getID()
    {
        return id;
    }

    public void tint(Color32 color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }
    public void descerAlavanca()
    {
        alavanca.acionar();
    }
    public void subirAlavanca()
    {
        alavanca.erro();
    }

}
