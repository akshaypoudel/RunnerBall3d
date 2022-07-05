using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    public bool isGameScene;
    public MoveLogic moveLogic;
    PlayerPrefsSaveSystem playerPrefsSaveSystem = new PlayerPrefsSaveSystem();
    public BGDisable bgDisable;
    public TMP_Text donutText;
    public TMP_Text diamondText;
    public Sensitivity sensitivity;

    public int welcomeBonusDiamondAmount;
    public int welcomeBonusDonutAmount;

    public GameObject qualityDropdownMenu;
    public GameObject SelectControls;
    public GameObject touchControlSelectedButton;
    public GameObject gyroControlSelectedButton;
    private GameObject BGAudioGameObject;


    public static string qualitySettingsPrefs = "qualitySettings";
    public static string FirstTimePlayingPrefs = "Controls";
    public static string nameOfScenePrefs = "nameOfScene";
    public static string whichControls = "PrefsControls";
    public static string bgAudioPref = "BackGroundAudio";
    public static string sfxAudioPref = "SFXAudio";
    private string playingFirstTime = "PlayingFirstTime";

    private string encryptedDonut = "EncryptedDonut";
    private string encryptedDiamond = "EncryptedDiamond";



    public Slider MusicSlider;
    public Slider SFXSlider;
    public Slider LoadingMenuSlider;


    public AudioSource BackGroundAudioSource;
    public AudioSource[] SoundEffects;


    public GameObject loadingScreen;
    public GameObject WelcomeBonusClaimButton;
    public GameObject WelcomeBonusClaimUI;
    public TMP_Text progressText;

    private void Awake()
    {
        sensitivity.InitializeSensitivityUI();

        if (BackGroundAudioSource == null && isGameScene)
        {
            BGAudioGameObject = GameObject.FindWithTag("Audio");
            BackGroundAudioSource = BGAudioGameObject.GetComponent<AudioSource>();
        }
        CheckAndLoadSettings();

        if (!isGameScene)
            CheckIfFirstTimePlaying();
       
    }

    private void CheckIfFirstTimePlaying()
    {

        if (PlayerPrefs.HasKey(playingFirstTime))
        {
            WelcomeBonusClaimButton.SetActive(false);
        }
        else
        {
            WelcomeBonusClaimButton.SetActive(true);
        }
    }

    private void Start()
    {
        if (!isGameScene)
            donutText.text = MoveLogic.numberOfDonuts.ToString();
        CheckAndLoadSettings();

    }
    private void CheckAndLoadSettings()
    {
        CheckControlsAndLoadGame();
        CheckBackGroundAudioAndLoad();
        CheckSFXAudioAndLoad();
        CheckQualitySettingsAndLoad();
    }

    #region QualitySettings
    private void CheckQualitySettingsAndLoad()
    {
        if (!PlayerPrefs.HasKey(qualitySettingsPrefs))
        {
            PlayerPrefs.SetInt(qualitySettingsPrefs, 0);
            LoadQualitySettings();
        }
        else
            LoadQualitySettings();
    }
    public void LoadQualitySettings()
    {
        int qualityIndex = PlayerPrefs.GetInt(qualitySettingsPrefs);
        qualityDropdownMenu.GetComponent<Dropdown>().value = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        SaveQualitySettings(qualityIndex);
    }
    public void SaveQualitySettings(int SaveIndex)
    {
        PlayerPrefs.SetInt(qualitySettingsPrefs, SaveIndex);
    }
    #endregion

    #region Controls

    private void CheckControlsAndLoadGame()
    {
        if (PlayerPrefs.HasKey(FirstTimePlayingPrefs))
        {
            if (PlayerPrefs.GetInt(whichControls) == 0)
            {
                touchControlSelectedButton.SetActive(true);
            }
            else if (PlayerPrefs.GetInt(whichControls) == 1)
            {
                gyroControlSelectedButton.SetActive(true);
            }
        }
    }
    public void CheckControlsAndLoadLevel(int SceneIndex)
    {
        if (PlayerPrefs.HasKey(FirstTimePlayingPrefs))
        {
            loadingScreen.SetActive(true);
            StartCoroutine(LoadAsynchronously(SceneIndex));
        }
        else
        {
            PlayerPrefs.SetInt(nameOfScenePrefs, SceneIndex);
            SelectControls.SetActive(true);
        }
    }
    public void TouchControlLoadGame()
    {
        PlayerPrefs.SetInt(whichControls, 0);
        PlayerPrefs.SetInt(FirstTimePlayingPrefs, 1);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsynchronously(PlayerPrefs.GetInt(nameOfScenePrefs)));
    }
    public void GyroControlLoadGame()
    {
        PlayerPrefs.SetInt(whichControls, 1);
        PlayerPrefs.SetInt(FirstTimePlayingPrefs, 1);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadAsynchronously(PlayerPrefs.GetInt(nameOfScenePrefs)));
    }
    IEnumerator LoadAsynchronously(int a)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(a);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingMenuSlider.value = progress;
            float p1 = progress * 100f;
            progressText.text = (int)p1 + "%";
            yield return null;
        }
    }
    public void SaveTouchControls()
    {
        PlayerPrefs.SetInt(whichControls, 0);
        if (isGameScene)
            MoveLogic.tempControlsValue = PlayerPrefs.GetInt(whichControls);
    }
    public void SaveGyroControls()
    {
        PlayerPrefs.SetInt(whichControls, 1);
        if (isGameScene)
            MoveLogic.tempControlsValue = PlayerPrefs.GetInt(whichControls);
    }



    #endregion

    #region BackGroundAudios

    private void CheckBackGroundAudioAndLoad()
    {
        if (!PlayerPrefs.HasKey(bgAudioPref))
        {
            PlayerPrefs.SetFloat(bgAudioPref, 1);
            LoadBackGroundAudio();
        }
        else
        {
            LoadBackGroundAudio();
        }

    }
    public void SetBackGroundAudio()
    {
        BackGroundAudioSource.volume = MusicSlider.value;
        SaveBackGroundAudio();
    }
    public void SaveBackGroundAudio()
    {
        PlayerPrefs.SetFloat(bgAudioPref, MusicSlider.value);
    }
    private void LoadBackGroundAudio()
    {
        MusicSlider.value = PlayerPrefs.GetFloat(bgAudioPref);
        BackGroundAudioSource.volume = PlayerPrefs.GetFloat(bgAudioPref);
    }
    #endregion

    #region SFXAudio

    private void CheckSFXAudioAndLoad()
    {
        if (!PlayerPrefs.HasKey(sfxAudioPref))
        {
            PlayerPrefs.SetFloat(sfxAudioPref, 1);
            LoadSFXAudio();
        }
        else
        {
            LoadSFXAudio();
        }
    }

    public void SetSFXAudio()
    {
        if (!isGameScene)
        {
            for (int i = 0; i < SoundEffects.Length; i++)
            {
                SoundEffects[i].volume = SFXSlider.value;

            }
            SaveSFXAudio();
        }
        else
        {
            SoundManager.volume = SFXSlider.value;
            SaveSFXAudio();
        }

    }
    public void SaveSFXAudio()
    {
        PlayerPrefs.SetFloat(sfxAudioPref, SFXSlider.value);
    }
    public void LoadSFXAudio()
    {
        if (!isGameScene)
        {
            for (int i = 0; i < SoundEffects.Length; i++)
            {
                SoundEffects[i].volume = SFXSlider.value;
            }
            SFXSlider.value = PlayerPrefs.GetFloat(sfxAudioPref);
        }
        else
        {
            SFXSlider.value = PlayerPrefs.GetFloat(sfxAudioPref);
            SoundManager.volume = SFXSlider.value;
        }
    }

    #endregion


    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }

    public void WelcomeBonusClaim()
    {
        GiveDonutReward(welcomeBonusDonutAmount);
        GiveDiamondReward(welcomeBonusDiamondAmount);
        bgDisable.DisableAllBgComponents();
        WelcomeBonusClaimUI.SetActive(true);
        PlayerPrefs.SetString(playingFirstTime, "playedFirstTime");
    }

    public void GiveDiamondReward(int rewardAmount)
    {
        int diamond = playerPrefsSaveSystem.ReturnDecryptedScore(encryptedDiamond);
        int rewardDiamond = diamond + rewardAmount;

        diamondText.text = rewardDiamond.ToString();

        playerPrefsSaveSystem.EncryptPrefsPositive(rewardAmount, encryptedDiamond);
    }

    public void GiveDonutReward(int rewardAmount)
    {
        int a = playerPrefsSaveSystem.ReturnDecryptedScore(encryptedDonut);
        int reward = a + rewardAmount;

        donutText.text = reward.ToString();

        playerPrefsSaveSystem.EncryptPrefsPositive(rewardAmount, encryptedDonut);
    }
}
