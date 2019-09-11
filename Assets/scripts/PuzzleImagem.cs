using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleImagem : MonoBehaviour    //aonde este script foi arrastado?
{
    [SerializeField]
    public Tile[,] tiles;

    [SerializeField]
    private bool modoFacil;

    [SerializeField]
    Alavanca alavanca;

    bool isComplete = false;
    // Start is called before the first frame update
    private void Start()
    {                                       //esta parte abaixo serve para criar uma lista com a localização de cada tile?
        GameObject[] tilesGO = GameObject.FindGameObjectsWithTag("Tile");//aqui acha todos os GameObjects com a tag tile?
        List<GameObject> tilesGoList = tilesGO.ToList(); //aqui passa os gameObjects com a tag tile para uma lista de objetos?
                                               //em seguida é necessario transformar esses objetos em posições da matriz?
        tiles = new Tile[3, 3];

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)// GetLength retorna o tamanho de linhas e colunas separado
            {
                if (tilesGoList.Any())//entra neste if se tiver qualquer elemento na lista?
                {
                    tiles[i, j] = tilesGoList.Last().GetComponent<Tile>();// agora que tem as coordenadas falta colocar dentro da matriz tiles[i,j]?
                    tilesGoList.Remove(tilesGoList.Last());
                }                                     //agora a matriz tiles esta com todos os elementos?  
            }
        }

        if (modoFacil) setUpfacil();
        else shuffle();

        ReposicionarTiles();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void shuffle()
    {
        Tile[,] tilesCopia = (Tile[,])tiles.Clone(); // aqui fez um clone do array Tile[,])tiles? 

        Utilities.Shuffle<Tile>(tiles);

        /*for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tilesCopia != null)
                {
                    if (tilesCopia.Length > 0)
                    {
                        tiles[i, j].transform.position = tilesCopia[i, j].transform.position;
                    }
                }
            }
        }*/
    }

    private void ReposicionarTiles()
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                Vector3 pos = Vector3.zero;
                pos.x += (i - 1) * tiles[i, j].transform.localScale.x;
                pos.z += (j - 1) * -1 * tiles[i, j].transform.localScale.z;

                tiles[i, j].transform.localPosition = pos;
                tiles[i, j].gameObject.name = "Tile(" + i + ", " + j + ")";
            }
        }
    }

    public bool isPuzzleComplete()//esta função deveria ir dentro do update?
    {
        int idCheck = 0;

        if (isComplete) return true;

        for (int j = 0; j < tiles.GetLength(1); j++)
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                if (tiles[i, j].id != idCheck++)//aqui verifica e depois soma
                {

                    isComplete = false;
                    return isComplete;
                }
            }
        }

        alavanca.destravar(true);//aqui destranca a alavanca do jogo das peças
        isComplete = true;
        return isComplete;
    }

    public bool isAnyMoving()
    {
        foreach (Tile t in tiles)
        {
            if (t.IsMoving() == true) return true;
        }
        return false;
    }

    public void setUpfacil()
    {
        GameObject[] tilesGO = GameObject.FindGameObjectsWithTag("Tile");
        List<GameObject> tilesGoList = tilesGO.ToList();
        int IDcheck = 0;

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                int index = 0;
                if (i == 2 && j == 1)
                {
                    index = tilesGoList.FindIndex(x => x.GetComponent<Tile>().id == 8);
                }
                else if (i == 2 && j == 2)
                {
                    index = tilesGoList.FindIndex(x => x.GetComponent<Tile>().id == 7);
                }
                else
                    index = tilesGoList.FindIndex(x => x.GetComponent<Tile>().id == IDcheck++);

                tiles[j, i] = tilesGoList[index].GetComponent<Tile>();
                tilesGoList.RemoveAt(index);
            }
        }
    }


    public void setupCompleto()
    {
        GameObject[] tilesGO = GameObject.FindGameObjectsWithTag("Tile");
        List<GameObject> tilesGoList = tilesGO.ToList();
        int IDcheck = 0;

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                int index = 0;
               
                    index = tilesGoList.FindIndex(x => x.GetComponent<Tile>().id == IDcheck++);

                tiles[j, i] = tilesGoList[index].GetComponent<Tile>();
                tilesGoList.RemoveAt(index);
            }
        }

        ReposicionarTiles();
    }
}