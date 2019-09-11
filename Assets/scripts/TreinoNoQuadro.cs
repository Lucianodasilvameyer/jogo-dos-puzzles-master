using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreinoNoQuadro : MonoBehaviour
{
   [SerializeField] 
    public int id;

    public static int quantidadeDeQuadros;

    [SerializeField]
    int QuantidadeDeQuadros_;

    [SerializeField]
    Alavanca alavanca;
    // Start is called before the first frame update
    void Start()
    {
        


        quantidadeDeQuadros = QuantidadeDeQuadros_;//para q igualar? 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetId()
    {
        return id;//??
    }
    public void Pintar(Color32 color)
    {
        GetComponent<MeshRenderer>().material.color = color;//não é aqui q escolhe a cor?
    }
    public void descerAlavanca()
    {
        alavanca.acionar();
    }
    public void subirAlavanca()
    {
        alavanca.errar();
    }


}
