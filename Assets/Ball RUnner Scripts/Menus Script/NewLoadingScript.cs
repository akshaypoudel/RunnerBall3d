using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewLoadingScript : MonoBehaviour
{
    public Slider slider;
    public GameObject loadingscreen;
    public Text progressText;

    void Start()
    {
        StartCoroutine(LoadAsynchronously(1)); 
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01((float)(operation.progress / 0.9));
            slider.value = progress;
            progressText.text = progress * 100 + "%";
            yield return null;
        }
    }
}
