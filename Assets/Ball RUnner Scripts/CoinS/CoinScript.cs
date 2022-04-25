using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinScript : MonoBehaviour
{
    [HideInInspector]
    public static int CoinValue;
    public int rotateSpeed;
    public int direction;


    void FixedUpdate()
    {
        if (direction == 0)
            transform.Rotate(90 * rotateSpeed * Time.deltaTime, 0, 0,Space.World);
        else if (direction == 1)
            transform.Rotate(0, 90 * rotateSpeed * Time.deltaTime, 0,Space.World);
        else if (direction == 2)
            transform.Rotate(0, 0, 90 * rotateSpeed * Time.deltaTime,Space.World);

    }

}
