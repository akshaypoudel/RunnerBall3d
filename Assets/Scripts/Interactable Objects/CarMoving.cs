using System.Collections;
using UnityEngine;

public class CarMoving : MonoBehaviour
{
    private Transform Player;
    [SerializeField] private int distanceFromPlayer;
    [SerializeField] private int speed;
    [SerializeField] private int tyreMovingSpeed;
    [SerializeField] private GameObject[] wheels;

    void Start()
    {
        Player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.z >= transform.position.z - distanceFromPlayer)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            foreach (var wheel in wheels)
                wheel.transform.Rotate(90f * Time.deltaTime * tyreMovingSpeed, 0, 0);
            StartCoroutine(destroyObject());

        }
    }

    IEnumerator destroyObject()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }
}
