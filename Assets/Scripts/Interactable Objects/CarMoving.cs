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

    void Update()
    {
        if (Player.transform.position.z >= transform.position.z - distanceFromPlayer)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if(wheels != null)
            {
                foreach (var wheel in wheels)
                    wheel.transform.Rotate(90f * Time.deltaTime * tyreMovingSpeed, 0, 0);
            }
            Invoke("DestroyObject", 8);
        }
    }

    private void DestroyObject()
    {
        this.gameObject.SetActive(false);
    }
}
