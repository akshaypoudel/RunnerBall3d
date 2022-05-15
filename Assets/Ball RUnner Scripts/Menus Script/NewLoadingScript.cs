using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class NewLoadingScript : MonoBehaviour
{
    public Slider slider;
    public TMP_Text progressText;

    void Start()
    {
        StartCoroutine(LoadAsynchronously(1));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        int i = 0;
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            float p1 = progress * 100f;
            progressText.text = (int)p1 + "%";
            i++;
            yield return null;
        }
    }
}
