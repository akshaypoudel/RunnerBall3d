using System.Collections;
using UnityEngine;

public class CartMovingScript : MonoBehaviour
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
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            foreach (var wheel in wheels)
                wheel.transform.Rotate(0, 0, 90f * Time.deltaTime * tyreMovingSpeed);
            Invoke("DestroyObject", 8);


        }
    }

    private void DestroyObject()
    {
        this.gameObject.SetActive(false);
    }
}
