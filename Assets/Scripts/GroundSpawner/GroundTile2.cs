using System.Collections;
using UnityEngine;

public class GroundTile2 : MonoBehaviour
{
    GroundSpawner2 groundSpawner2;

    private void Start()
    {
        groundSpawner2 = GameObject.Find("GroundManager").GetComponent<GroundSpawner2>();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.transform.CompareTag("Player"))
        {
            groundSpawner2.SpawnTile();
            StartCoroutine(DestroyGround(gameObject));
        }
    }
    IEnumerator DestroyGround(GameObject ground)
    {
        yield return new WaitForSeconds(3f);
        ground.SetActive(false);
        yield return new WaitForSeconds(60f);
        Destroy(ground);
    }
}
