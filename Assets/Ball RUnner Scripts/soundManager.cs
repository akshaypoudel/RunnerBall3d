using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public bool muted = false;
    public ParticleSystem stars;
    // Update is called once per frame
    void Start()
    {
        Time.timeScale=1f;
        stars.Play();
    }

    public void onbuttonpress()
    {
        if(muted==false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }
    }
}
