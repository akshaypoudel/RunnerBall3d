using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using TMPro;
 
public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public Button _showAdButton;
    
    public GameObject showAdButton;
    private string _androidAdUnitId = "Rewarded_Android";
    private string encryptedPrefs = "EncryptedDonut";
    [SerializeField] private bool isMenuScene = false;
    private bool isAdsLoaded = false;

    public TMP_Text donutText;

    public GameObject donutRewardUI;
    public BGDisable bGDisable;

    private bool isRewarded = false;
    private int rewardDonutAmountAfterWatchingADs = 100;
    PlayerPrefsSaveSystem playerPrefsSaveSystem = new PlayerPrefsSaveSystem();


    [SerializeField] MoveLogic _moveLogic;
    string _adUnitId; // This will remain null for unsupported platforms

    public void InitializeRewarded()
    {
        _adUnitId = _androidAdUnitId;

        CheckAndActivateShowAdButtons(true,true);
    }

    public void CheckAndActivateShowAdButtons(bool showAdButtonInGameScene,bool showAdButtonInMenuScene)
    {

        if (!isMenuScene)
            _showAdButton.interactable = showAdButtonInGameScene;
        else
            showAdButton.SetActive(showAdButtonInMenuScene);
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        Advertisement.Load(_adUnitId, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (adUnitId.Equals(_adUnitId))
        {
            isAdsLoaded = true;
            _showAdButton.onClick.AddListener(ShowAd);
            CheckAndActivateShowAdButtons(true, true);
        }
        else
        {
            isAdsLoaded = false;
        }
    }

    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        if(isAdsLoaded)
        {
            CheckAndActivateShowAdButtons(false, false);
            isRewarded = false ;
            Time.timeScale = 0f;
            Advertisement.Show(_adUnitId, this);
        }
        else
        {
            Time.timeScale = 1f;
            CheckAndActivateShowAdButtons(false, false);
            LoadAd();
        }
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Time.timeScale = 1f;

            if(!isRewarded)
            {
                isRewarded = true;  
                if (isMenuScene)
                {
                    GivePlayerDonutReward();
                }
                else
                {
                   _moveLogic.StartTimerForNewLife();
                }

            }
            LoadAd();
        }
    }
    public void GivePlayerDonutReward()
    {
        int a = playerPrefsSaveSystem.ReturnDecryptedScore(encryptedPrefs);
        int reward = a + rewardDonutAmountAfterWatchingADs;

        bGDisable.DisableAllBgComponents();

        donutRewardUI.SetActive(true);

        donutText.text = reward.ToString();       

        playerPrefsSaveSystem.EncryptPrefsPositive(rewardDonutAmountAfterWatchingADs, encryptedPrefs);
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Time.timeScale = 1f;
        LoadAd();
        CheckAndActivateShowAdButtons(false, false);
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Time.timeScale = 1f;
        CheckAndActivateShowAdButtons(false, false);
        LoadAd();
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        _showAdButton.onClick.RemoveAllListeners();
    }
}
