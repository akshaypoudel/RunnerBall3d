using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighScoreScript : MonoBehaviour
{
    public GameObject player;
    public Text Distance;
    
    // Update is called once per fram
    void Start()
    {
        Time.timeScale=1f;
    }
    void FixedUpdate()
    {
        int dist = Mathf.RoundToInt(player.transform.position.z*2f);
        Distance.text = dist.ToString() + " m";
    }
}
