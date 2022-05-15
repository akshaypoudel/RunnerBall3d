using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other1)
    {
        if(other1.gameObject.CompareTag("Enemy"))
        {
            other1.gameObject.SetActive(false);
        }
    }

}
