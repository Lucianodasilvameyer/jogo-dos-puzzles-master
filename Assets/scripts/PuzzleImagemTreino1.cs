using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PuzzleImagemTreino1 : MonoBehaviour
{
    [SerializeField]
    private Tile[,] tiles; //aqui ja guardaria os GameObjects?

    // Start is called before the first frame update
    void Start()
    {                                                    //esta parte abaixo serve para criar uma lista com todos os tiles?sim
        GameObject[] tilesGo = GameObject.FindGameObjectsWithTag("Tile");// aqui acha todos os GameObjects com a tag tile?sim
        List<GameObject> tilesGoList = tilesGo.ToList();//aqui passa os gameObjects com a tag tile para uma lista de objetos?sim
                                                      
        tiles = new Tile[3, 3];
        //aqui em baixo coloco os gameobjects dentro da matriz
        for(int i=0; i < tiles.GetLength(0); i++)
        {
            for(int j=0; j < tiles.GetLength(1); j++)// GetLength retorna o tamanho de linhas e colunas separado
            {
                if (tilesGoList.Any())//entra neste if se tiver qualquer elemento na lista?sim, tem q garantir se tem algum elemento na lista
                {
                    tiles[i, j] = tilesGoList.Last().GetComponent<Tile>();// agora que tem colocar os tiles na matriz
                    tilesGoList.Remove(tilesGoList.Last());
                }                                            //agora a matriz tiles esta com todos os elementos?sim
            }
        }                                           //agora ja tem uma matriz com todos os tiles;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void embaralhar()
    {
        Tile[,] TilesCopia = (Tile[,])tiles.Clone(); //aqui fez um clone da variavel de arrays tiles?sim    // aqui o shuffle embaralha a matriz, dai tem q fazer a copia para embaralhar no mundo 
        Utilities.Shuffle<Tile>(TilesCopia);   //aqui o clone pode ou não estar vazio?  //falta fazer 
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
