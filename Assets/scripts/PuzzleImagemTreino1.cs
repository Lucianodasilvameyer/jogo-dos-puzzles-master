using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleImagemTreino1 : MonoBehaviour
{
    [SerializeField]
    private Tile[,] tiles; //aqui ja guardaria os GameObjects?

    // Start is called before the first frame update
    void Start()
    {                                                    //esta parte abaixo serve para criar uma lista com a localização de cada tile?
        GameObject[] tilesGo = GameObject.FindGameObjectsWithTag("Tile");// aqui acha todos os GameObjects com a tag tile?
        List<GameObject> tilesGoList = tilesGo.ToList();//aqui passa os gameObjects com a tag tile para uma lista de objetos?
                                                        //em seguida é necessario transformar esses objetos em posições da matriz?
        tiles = new Tile[3, 3];

        for(int i=0; i < tiles.GetLength(0); i++)
        {
            for(int j=0; j < tiles.GetLength(1); j++)// GetLength retorna o tamanho de linhas e colunas separado
            {
                if (tilesGoList.Any()))//entra neste if se tiver qualquer elemento na lista?
                {
                    tiles[i, j] = tilesGoList.Last().GetComponent<Tile>();// agora que tem as coordenadas falta colocar dentro da matriz tiles[i,j]?
                    tilesGoList.Remove(tilesGoList.Last());
        }                                            //agora a matriz tiles esta com todos os elementos?
    }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void embaralhar()
    {
        Tile[,] TilesCopia = (Tile[,])tiles.Clone(); //aqui fez um clone da variavel de arrays tiles?
        Utilities.Shuffle<Tile>(TilesCopia);   //aqui o clone não estaria vazio?  //falta fazer uma referencia para usar a função do utilities?
                                                 //aqui os Tiles são GameObjects?
        for(int i = 0; i < tiles.GetLength(0); i++)
        {
            for(int j=0; j < tiles.GetLength(1); j++)
            {
                if (TilesCopia != null) //este null quer dizer q a caixa tem lugar para guardar coisas? 
                {
                    if (TilesCopia.Length > 0)
                    {
                        tiles[i, j].transform.position = TilesCopia[i, j].transform.position; //para embaralhar neste caso pega o array matriz tiles e o iguala a sua copia? 
                    }
                }
            }
        }

    }
    

}
