using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner2 : MonoBehaviour
{
   [SerializeField] GameObject[] groundTile;
   private int randomNumber;
//    [SerializeField]private int maxTiles;
    Vector3 nextSpawnPoint;
    List<int> list;
    private int count=0;

    private void Start () {
        randomNumber=0;
        count=0;
        list= new List<int>();
        for (int i = 0; i < 4; i++) {
            SpawnTile();
        }
        // GenerateRandom();
        // extra();
    }
    public void SpawnTile ()
    {
        GameObject temp = Instantiate(groundTile[randomNumber], nextSpawnPoint ,Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;
        randomNumber++;
        if(randomNumber>=groundTile.Length)
        {
            randomNumber=0;
        }
        // randomNumber=Random.Range(0,groundTile.Length);
        // if(count < list.Capacity)
        // {
        //     Debug.Log(count);
        //     randomNumber=list[count++];
        //     Debug.Log(count);
        // }
        // else
        // {
        //     count=0;
        //     Debug.Log("Count = "+count);
        //     GenerateRandom();
        //     randomNumber=list[count++];
        // }
    }

    // void GenerateRandom()
    // {
    //     list.Clear();
    //     for (int j = 0; j < groundTile.Length; j++)
    //     {
    //         int Rand = Random.Range(0,groundTile.Length);
    //         while(list.Contains(Rand))
    //         {
    //             Rand = Random.Range(0,groundTile.Length);
    //         }
    //         list.Add(Rand);
    //         Debug.Log("Random Numbers is: "+list[j]);
    //     }
    // }



}
