using UnityEngine;

public class GroundTile2 : MonoBehaviour
{
    GroundSpawner2 groundSpawner2;

    private void Start()
    {
        //groundSpawner2 = GameObject.FindObjectOfType<GroundSpawner2>();
        groundSpawner2 = GameObject.Find("GroundManager").GetComponent<GroundSpawner2>();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.transform.CompareTag("Player"))
        {
            //if (groundSpawner2 != null)
            groundSpawner2.SpawnTile();
            Destroy(gameObject,3f);
        }
    }
}
