using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TabGroup : MonoBehaviour
{
    public GameObject skinsButton;
    public GameObject shopButton;
    public Vector3 normalSize;
    public Vector3 animatedSize;

    public void Animate(GameObject button)
    {
        if(button == skinsButton)
        {
            skinsButton.transform.localScale = animatedSize;
            shopButton.transform.localScale = normalSize;  
        }
        else
        {
            skinsButton.transform.localScale = normalSize;
            shopButton.transform.localScale = animatedSize;
        }
    }

}
