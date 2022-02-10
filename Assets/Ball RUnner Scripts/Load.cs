using UnityEngine;
using UnityEngine.SceneManagement;


public class Load : MonoBehaviour
{
    perlText mytext;
    public void LoadScene(string scene_name)
    {
       // mytext.PerlAmount1 = 0;
        SceneManager.LoadScene(scene_name);
    }
}
