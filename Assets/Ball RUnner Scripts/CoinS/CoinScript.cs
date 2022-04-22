using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinScript : MonoBehaviour
{
    [HideInInspector]
    public static int CoinValue;
    public int rotateSpeed;


    void Update()
    {
        transform.Rotate(Vector3.up* rotateSpeed * Time.deltaTime);
    }

}
