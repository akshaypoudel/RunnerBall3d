using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Slider slider;
    public GameObject loadingscreen;
    public Text progressText;

    public void loadLevel()
    {
        int numbOfCompletedLevels = 2;
        StartCoroutine(LoadAsynchronously(numbOfCompletedLevels)); 
    }

    IEnumerator LoadAsynchronously(int a)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(a);
        loadingscreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }
        Time.timeScale=1f;
    }
}
