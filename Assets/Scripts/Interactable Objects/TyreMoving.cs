using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyreMoving : MonoBehaviour
{
    Transform Player;
    Rigidbody _rigidbody;
    public float tyreRotateSpeed;
    public float tyreMoveSpeed;
    public float distanceFromPlayer;

    void Start()
    {
        Player = GameObject.Find("Player").transform;
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {

        if (Player.position.z >= transform.position.z - distanceFromPlayer)
        {
            transform.Rotate(0, 0, -90 * Time.deltaTime * tyreRotateSpeed);
            transform.Translate(Vector3.back * Time.deltaTime * tyreMoveSpeed, Space.World);
            StartCoroutine(destroyObject());
        }
    }
    IEnumerator destroyObject()
    {
        yield return new WaitForSeconds(8);
        Destroy(this.gameObject);
    }
}

