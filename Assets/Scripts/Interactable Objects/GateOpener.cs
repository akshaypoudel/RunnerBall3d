using System.Collections;
using UnityEngine;

public class GateOpener : MonoBehaviour
{
    private Transform Player;
    public float distanceFromPlayer;
    public float speed;
    private void Start()
    {
        Player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (Player.position.z >= transform.position.z - distanceFromPlayer)
        {
            if (transform.rotation.y <= 0f && transform.rotation.y >= -90f)
            {
                transform.Rotate(0, -90f * Time.deltaTime * speed, 0);
                Invoke("DestroyObject", 10);

            }

        }
    }
    private void DestroyObject()
    {
        this.gameObject.SetActive(false);
    }
}
