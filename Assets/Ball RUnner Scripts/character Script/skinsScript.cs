using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skinsScript : MonoBehaviour
{
    public GameObject skin;
    public void Skins()
    {
        Time.timeScale = 0f;
        skin.SetActive(true);
    }
    public void crossButton()
    {
        Time.timeScale = 1f;
        skin.SetActive(false);
    }
}
