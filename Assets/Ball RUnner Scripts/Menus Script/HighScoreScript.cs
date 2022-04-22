using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HighScoreScript : MonoBehaviour
{
    public GameObject player;
    public TMP_Text Distance;
    int dist;
    
    // Update is called once per frame
    void Start()
    {
        dist = 0;
    }
    void FixedUpdate()
    {
        if(!MoveLogic.isGameOver)
        {
            Distance.text = dist.ToString() + " m";
            dist++;
        }
    }
}
