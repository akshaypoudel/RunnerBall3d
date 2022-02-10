using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
   public GameObject groundTile;
    Vector3 nextSpawnPoint=Vector3.zero;

    private void Update() {
        if(GroundTile.isCalling)
        {
            SpawnTile();
            GroundTile.isCalling=false;
        }
    }
    public void SpawnTile ()
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint ,Quaternion.identity);
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;
        // StaticReference.isCalling=false;
        // nextSpawnPoint=spawnPoint.transform.position;
    }


    private void Start () {
        for (int i = 0; i < 3; i++) {
            SpawnTile();
        }
    }
}
