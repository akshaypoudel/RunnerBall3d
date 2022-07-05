using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BallRunner.DailyRewards
{
    public class DailyReward : MonoBehaviour
    {
        private int nextRewardIndex = 0;
        private bool isRewardReady = false;

        private string nextRewardPrefs = "Next_Reward_Index";
        private string rewardClaimPrefs = "Reward_Claim_Datetime";

        private string playingFirstTime = "PlayingFirstTime2";

        [SerializeField] private GameObject noMoreRewardsPanel;
        [SerializeField] private GameObject notificationIcon;

        [SerializeField] private GameObject donutRewardIcon;
        [SerializeField] private GameObject diamondRewardIcon;

        [SerializeField] private TMP_Text rewardAmountText;

        [SerializeField] private RewardDatabase rewardDB;
        [SerializeField] private SettingsMenu settingsMenu;

        private int nextRewardDelay = 23; //next reward after 23 hours
        //check if reward is available every 5 seconds

        private float checkForRewardDelay = 2f;

        void Start()
        {
            Initialize();

            StopAllCoroutines();
            if(PlayerPrefs.HasKey(playingFirstTime))
            {
                StartCoroutine(CheckForRewards());
            }
            else
            {
                PlayerPrefs.SetString(playingFirstTime, "FirstTimeClaimedGift");
                ActivateRewards();
            }
        }

        private void Initialize()
        {
            nextRewardIndex = PlayerPrefs.GetInt(nextRewardPrefs, 0);

            if (string.IsNullOrEmpty(PlayerPrefs.GetString(rewardClaimPrefs)))
                PlayerPrefs.SetString(rewardClaimPrefs, DateTime.Now.ToString());

        }

        IEnumerator CheckForRewards()
        {
            while (true)
            {
                if (!isRewardReady)
                {
                    DateTime currentDatetime = DateTime.Now;
                    DateTime rewardClaimDatetime = DateTime.Parse(PlayerPrefs.GetString(rewardClaimPrefs, currentDatetime.ToString()));

                    TimeSpan time = (currentDatetime - rewardClaimDatetime);

                    //get total Hours between this 2 dates
                    double elapsedHours = time.TotalHours;

                    if (elapsedHours >= nextRewardDelay)
                        ActivateRewards();
                    else
                        DeactivateReward();
                }

                yield return new WaitForSeconds(checkForRewardDelay);
            }
        }

        public void OnClaimButtonClick()
        {
            Reward reward = rewardDB.GetReward(nextRewardIndex);

            if(reward.type == RewardsType.donut)
            {
                settingsMenu.GiveDonutReward(reward.amount);
            }
            else
            {
                settingsMenu.GiveDiamondReward(reward.amount);
            }

            nextRewardIndex++;

            if(nextRewardIndex>=rewardDB.rewardsCount)
            {
                nextRewardIndex = 0;
            }
            PlayerPrefs.SetInt(nextRewardPrefs, nextRewardIndex);


            PlayerPrefs.SetString(rewardClaimPrefs, DateTime.Now.ToString());

            DeactivateReward();
        }

        private void DeactivateReward()
        {
            isRewardReady = false;

            donutRewardIcon.SetActive(false);
            diamondRewardIcon.SetActive(false);

            noMoreRewardsPanel.SetActive(true);
            notificationIcon.SetActive(false);
        }
        private void ActivateRewards()
        {
            isRewardReady=true;

            notificationIcon.SetActive(true);
            noMoreRewardsPanel.SetActive(false);

            Reward reward = rewardDB.GetReward(nextRewardIndex);

            if(reward.type == RewardsType.donut)
            {
                donutRewardIcon.SetActive(true);
                diamondRewardIcon.SetActive(false);
            }
            else
            {
                donutRewardIcon.SetActive(false);
                diamondRewardIcon.SetActive(true);
            }

            rewardAmountText.text = $"+{reward.amount}";
        }
    }
    public enum RewardsType
    {
        donut,
        diamond
    }
    [System.Serializable]
    public struct Reward
    {
        public RewardsType type;
        public int amount;
    }

}
