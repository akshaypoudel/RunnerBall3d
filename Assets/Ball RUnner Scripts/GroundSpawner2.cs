using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner2 : MonoBehaviour
{
    [SerializeField] GameObject[] groundTile;
    private int randomNumber;
    public Vector3 nextSpawnPoint;
    List<int> list;
    private int count;

    private void Start () {
        list = new List<int>();
        //randomNumber=0;
        count = 0;
        GenerateRandom();
        randomNumber = list[count++];
        for (int i = 0; i < 3; i++) 
        {
            SpawnTile();
        }
    }
    public void SpawnTile ()
    {
        GameObject temp = Instantiate(groundTile[randomNumber], nextSpawnPoint ,Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;
        if (count < groundTile.Length)
        {
            randomNumber = list[count];
            ++count;
        }
        else
        {
            count = 0;
            GenerateRandom();
            randomNumber = list[count++];
        }
    }
    void GenerateRandom()
    {
        int Rand;
        list.Clear();
        for (int j = 0; j < groundTile.Length; j++)
        {
            Rand = Random.Range(0, groundTile.Length);
            while (list.Contains(Rand))
            {
                Rand = Random.Range(0, groundTile.Length);
            }
            list.Add(Rand);
        }
    }


}
