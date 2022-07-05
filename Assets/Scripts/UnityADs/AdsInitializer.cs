using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string _androidGameId;
    private bool canInitializeAds = false;
    private bool _testMode = false;
    private string _gameId;
    [SerializeField] RewardedAds _rewardedAds;
    [SerializeField] InterstialAds _interstialAds;

    
    void Awake()
    {
        InitializeAds();
        if(canInitializeAds)
        {
            _rewardedAds.InitializeRewarded();
            _interstialAds.InitializeInterstitial();
        }
    }

    // Load content to the Ad Unit:
    public void InitializeAds()
    {
        if(Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.WindowsEditor)
        {
            canInitializeAds = true;
            _gameId = _androidGameId;
            Advertisement.Initialize(_gameId, _testMode, this);
        }
        else
            canInitializeAds = false;   
        
    }
    public void OnInitializationComplete()
    {
        _rewardedAds.LoadAd();
        _interstialAds.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Time.timeScale = 1f;

        _rewardedAds.CheckAndActivateShowAdButtons(false, false);
    }
}