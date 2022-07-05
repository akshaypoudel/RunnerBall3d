using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SC_FPSCounter : MonoBehaviour
{
 
    public float updateInterval = 0.5f; 
    float accum = 0.0f;
    int frames = 0;
    float timeleft;
    float fps;
    public TMP_Text fpsText;

 
    void Start()
    {
        timeleft = updateInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        if (timeleft <= 0.0)
        {
            fps = (accum / frames);

            timeleft = updateInterval;
            fpsText.text = fps.ToString();
            accum = 0.0f;
            frames = 0;
        }
    }

   
}