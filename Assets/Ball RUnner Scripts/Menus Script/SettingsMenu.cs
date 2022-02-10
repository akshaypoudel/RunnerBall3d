using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume(float volume)
    {
        if (volume == -20)
        {
            soundManager manager=new soundManager();
            manager.onbuttonpress();
            Debug.Log("Volume changed : " + volume);
        }
        else
        {
            audioMixer.SetFloat("MyVolume", volume);
        }
    }
    
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
