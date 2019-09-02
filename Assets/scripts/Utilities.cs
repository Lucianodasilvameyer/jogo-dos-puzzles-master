using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Utilities
{
    public static void Shuffle<T>(T[] array)
    {

        System.Random rng = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    public static void Shuffle<T>(T[,] array)
    {

        System.Random random = new System.Random();
        int lengthRow = array.GetLength(1);

        for (int i = array.Length - 1; i > 0; i--)
        {
            int i0 = i / lengthRow;
            int i1 = i % lengthRow;

            int j = random.Next(i + 1);
            int j0 = j / lengthRow;
            int j1 = j % lengthRow;

            T temp = array[i0, i1];
            array[i0, i1] = array[j0, j1];
            array[j0, j1] = temp;
        }
    }

    public static void Shuffle<T>(List<T> array)
    {

        System.Random rng = new System.Random();
        int n = array.Count;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    public static T getReference<T>(string tag)
    {
        return GameObject.FindGameObjectWithTag(tag).GetComponent<T>();
    }



    public static int[] findIndex(Tile[,] array, Tile element)
    {
        int firstIndex = -1, lastIndex = -1;
        bool found = false;
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if(array[i,j] == element)
                {
                    firstIndex = i;
                    lastIndex = j;
                    found = true;
                }

                if (found) break;
            }
            if (found) break;

        }

        int[] idx = { firstIndex, lastIndex };

        return idx;
    }

}
