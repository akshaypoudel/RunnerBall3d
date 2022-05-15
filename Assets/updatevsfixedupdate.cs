using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updatevsfixedupdate : MonoBehaviour
{

    void Update()
    {
        Debug.Log($"{Time.time} - Update : {Time.unscaledDeltaTime}");

    }
    private void FixedUpdate()
    {
        Debug.LogWarning($"{Time.time} - FixedUpdate : {Time.deltaTime}");

    }
    private void LateUpdate()
    {
        Debug.LogWarning($"{Time.time} - FixedUpdate : {Time.deltaTime}");

    }
}
