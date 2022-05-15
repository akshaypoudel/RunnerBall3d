using System.Collections;
using UnityEngine;

public class LogFalling : MonoBehaviour
{
    public float distanceFromPlayer;
    private Transform Player;
    private Rigidbody _rigidBody;
    private bool canFallAgain = true;


    void Start()
    {
        Player = GameObject.Find("Player").transform;
        _rigidBody = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
    }

    //Update is called once per frame
    void Update()
    {
        if (Player.position.z >= transform.position.z - distanceFromPlayer && canFallAgain)
        {
            canFallAgain = false;
            _rigidBody.useGravity = true;
            _rigidBody.constraints = RigidbodyConstraints.None;
            StartCoroutine(destroyObject());
        }

    }
    IEnumerator destroyObject()
    {
        yield return new WaitForSeconds(7);
        Destroy(this.gameObject);
    }
}
