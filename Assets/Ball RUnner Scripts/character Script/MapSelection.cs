using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelection : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] maps;
    public int selectedmaps = 0;

    public void NextCharacter()
    {
        maps[selectedmaps].SetActive(false);
        selectedmaps = (selectedmaps + 1) % maps.Length;
        maps[selectedmaps].SetActive(true);
    }
    public void PreviousCharacter()
    {
        maps[selectedmaps].SetActive(false);
        selectedmaps--;
        if(selectedmaps<0)
        {
            selectedmaps += maps.Length;
        }
        maps[selectedmaps].SetActive(true);
    }

    public void Selected()
    {
        //this code will not work if there is more than 2 maps in game
        if(selectedmaps==0)
        {
            selectedmaps+=2;
        }
        else if(selectedmaps==1)
        {
            selectedmaps+=2;
        }
        PlayerPrefs.SetInt("completedlevelcount",selectedmaps);
    }
}
