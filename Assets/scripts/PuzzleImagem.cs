using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleImagem : MonoBehaviour
{
    [SerializeField]
    private Tile[] tiles;



    // Start is called before the first frame update
    private void Start()
    {
        GameObject[] tilesGO = GameObject.FindGameObjectsWithTag("Tile");

        tiles = new Tile[tilesGO.Length];

        for (int i = 0; i < tilesGO.Length; i++)
        {
            tiles[i] = tilesGO[i].GetComponent<Tile>();
        }

        

    shuffle();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void shuffle()
    {

         
  List<Vector3> tilesCopia;
    tilesCopia = tiles.Select(x => x.GetComponent<Transform>().position).ToList();
        Utilities.Shuffle<Vector3>(tilesCopia);

        for (int i = 0; i < tiles.Length; i++)
        {
            if(tilesCopia != null)
            {
                if (tilesCopia.Any())
                {
                    print("entrou");
                    tiles[i].transform.position = tilesCopia.Last();
                    tilesCopia.Remove(tilesCopia.Last());
                }
            }
           
        }
    }
}