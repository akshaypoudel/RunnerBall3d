using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
 
public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button _showAdButton;
    [SerializeField] GameObject showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] private bool isMenuScene = false;


    [SerializeField] MoveLogic _moveLogic;
    string _adUnitId; // This will remain null for unsupported platforms

    public void InitializeRewarded()
    {
        _adUnitId = _androidAdUnitId;

        if(!isMenuScene)
            _showAdButton.interactable = true;
        else
            showAdButton.SetActive(true);
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
            _showAdButton.onClick.AddListener(ShowAd);
            if (!isMenuScene)
                _showAdButton.interactable = true;
            else
                showAdButton.SetActive(true);
        }
    }

    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        if(!isMenuScene)
            _showAdButton.interactable=false;
        else
            showAdButton.SetActive(false);

        Time.timeScale = 0f;
        Advertisement.Show(_adUnitId, this);
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Time.timeScale = 1f;
            //PlayerReward
            if (isMenuScene)
                GivePlayerDonutReward();
            else
                _moveLogic.PlayAgain();

            Advertisement.Load(_adUnitId, this);
        }
    }
    public void GivePlayerDonutReward()
    {

    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Failed To Load Ads: ");
        if (!isMenuScene)
            _showAdButton.interactable = false;
        else
            showAdButton.SetActive(false);
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
        if (!isMenuScene)
            _showAdButton.interactable = false;
        else
            showAdButton.SetActive(false);
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        _showAdButton.onClick.RemoveAllListeners();
    }
}
