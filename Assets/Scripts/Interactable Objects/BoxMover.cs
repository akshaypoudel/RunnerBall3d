using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MonoBehaviour
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
            transform.Translate(Vector3.right*speed*Time.deltaTime,Space.World);
            Invoke("DestroyObject", 10);
        }
    }
    private void DestroyObject()
    {
        this.gameObject.SetActive(false);
    }
}
