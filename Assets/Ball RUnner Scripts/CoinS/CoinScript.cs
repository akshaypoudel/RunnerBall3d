using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [HideInInspector]
    public static int CoinValue;
    void FixedUpdate()
    {
        transform.Rotate(90 * Time.deltaTime,0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            CoinValue++;
            Destroy(gameObject);
        }
    } 
}
