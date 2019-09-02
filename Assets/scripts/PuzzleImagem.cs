using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleImagem : MonoBehaviour
{
    [SerializeField]
    public Tile[,] tiles;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject[] tilesGO = GameObject.FindGameObjectsWithTag("Tile");
        List<GameObject> tilesGoList = tilesGO.ToList();

        tiles = new Tile[3, 3];

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (tilesGoList.Any())
                {
                    tiles[i, j] = tilesGoList.Last().GetComponent<Tile>();
                    tilesGoList.Remove(tilesGoList.Last());
                }
            }
        }

        shuffle();
        ReposicionarTiles();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void shuffle()
    {
        Tile[,] tilesCopia = (Tile[,])tiles.Clone();

        Utilities.Shuffle<Tile>(tilesCopia);

        for (int i = 0; i < tiles.GetLength(0); i++)
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
        }
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
                tiles[i,j].gameObject.name = "Tile(" + i + ", " + j +  ")";
            }
        }
    }


    public bool isPuzzleComplete()
    {
        int idCheck = 0;

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {

                if (tiles[i, j].id != idCheck)
                {
                    return false;
                }
                else
                {
                    idCheck++;
                }

            }
        }
        return true;
    }

    public bool isAnyMoving()
    {
        foreach(Tile t in tiles)
        {
            if (t.IsMoving() == true) return true;
        }
        return false;
    }
}