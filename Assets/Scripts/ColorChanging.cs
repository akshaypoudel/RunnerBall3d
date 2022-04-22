using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColorChanging : MonoBehaviour
{
    float timeLeft;
    Color targetColor = Color.white;
    public List<Color> colors;


 
    /*void Update()
    {
        if (timeLeft <= Time.deltaTime)
        {
            // transition complete
            // assign the target color
            this.GetComponent<Image>().color  = targetColor;

            // start a new transition
            targetColor = colors[Random.Range(0,colors.Count)];
            
            timeLeft = 1.0f;
        }
        else
        {
            // transition in progress
            // calculate interpolated color
            this.GetComponent<Image>().color = Color.Lerp(this.GetComponent<Image>().color, targetColor, Time.deltaTime / timeLeft);

            // update the timer
            timeLeft -= Time.deltaTime;
        }
    }*/
}
