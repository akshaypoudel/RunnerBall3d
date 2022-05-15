using UnityEngine;

public class BGDisable : MonoBehaviour
{
    public GameObject[] bgComponents;

    public void DisableAllBgComponents()
    {
        foreach (GameObject go in bgComponents)
        {
            go.SetActive(false);
        }
    }
    public void EnableAllBgComponents()
    {
        foreach (GameObject g in bgComponents)
        {
            g.SetActive(true);
        }
    }
}







