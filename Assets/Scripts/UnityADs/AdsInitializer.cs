using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;

    private bool canInitializeAds = false;
    [SerializeField] bool _testMode = true;
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
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}