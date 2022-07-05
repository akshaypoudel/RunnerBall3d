using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Sensitivity : MonoBehaviour
{
    public Slider touchSlider;
    public Slider gyroSlider;

    public PlayerSettings playerSettings;

    private string touchSensitivityPref = "TouchSensitivity";
    private string gyroSensitivityPref = "GyroSensitivity";

    public void InitializeSensitivityUI()
    {
        SetRangeOfSliders();
        CheckAndLoadTouchSlidersValue();
        CheckAndLoadGyroSlidersValue();
        touchSlider.onValueChanged.AddListener(delegate { OnTouchSliderValueChange(); });
        gyroSlider.onValueChanged.AddListener(delegate { OnGyroSliderValueChange(); });
    }
    private void SetRangeOfSliders()
    {
        touchSlider.minValue = playerSettings.minTouchSpeed;
        touchSlider.maxValue = playerSettings.maxTouchSpeed;

        gyroSlider.minValue = playerSettings.minGyroSpeed;
        gyroSlider.maxValue = playerSettings.maxGyroSpeed;
    }
    private void CheckAndLoadTouchSlidersValue()
    {
        if (PlayerPrefs.HasKey(touchSensitivityPref))
        {
            LoadTouchSliderValue();
        }
        else
        {
            SetTouchSliderValues();
        }

    }

    private void CheckAndLoadGyroSlidersValue()
    {
        if (PlayerPrefs.HasKey(gyroSensitivityPref))
        {
            LoadGyroSliderValue();
        }
        else
        {
            SetGyroSliderValue();
        }
    }

    private void SetGyroSliderValue()
    {
        gyroSlider.value = PlayerSettings.playerGyroControlSpeed;
    }

    private void SetTouchSliderValues()
    {
        touchSlider.value = PlayerSettings.playerTouchControlSpeed;
    }

    public void OnTouchSliderValueChange()
    {
        PlayerSettings.playerTouchControlSpeed = touchSlider.value;
        SaveTouchSliderValue();
    }
    public void OnGyroSliderValueChange()
    {
        PlayerSettings.playerGyroControlSpeed = gyroSlider.value;
        SaveGyroSliderValue();
    }

    public void SaveTouchSliderValue()
    {
        PlayerPrefs.SetFloat(touchSensitivityPref, touchSlider.value);
    }
    public void LoadTouchSliderValue()
    {
        touchSlider.value = PlayerPrefs.GetFloat(touchSensitivityPref);
        PlayerSettings.playerTouchControlSpeed = PlayerPrefs.GetFloat(touchSensitivityPref);
    }
    public void SaveGyroSliderValue()
    {
        PlayerPrefs.SetFloat(gyroSensitivityPref, gyroSlider.value);
    }
    public void LoadGyroSliderValue()
    {
        gyroSlider.value = PlayerPrefs.GetFloat(gyroSensitivityPref);
        PlayerSettings.playerGyroControlSpeed = PlayerPrefs.GetFloat(gyroSensitivityPref);
    }
}
