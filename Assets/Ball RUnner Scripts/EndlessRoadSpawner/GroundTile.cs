using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{

    GroundSpawner groundSpawner;
    public static bool isCalling;
    // public GameObject SpawnPoint;

    private void Start () {
        groundSpawner = GameObject.Find("GroundManager").GetComponent<GroundSpawner>();
	}

    private void OnTriggerExit (Collider other)
    {
        // groundSpawner.SpawnTile();
        // StaticReference.value=1;
        // StaticReference.isCalling=true;
        isCalling=true;
            Destroy(gameObject, 1);
    }

}
