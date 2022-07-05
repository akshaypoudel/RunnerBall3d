using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BallRunner.DailyRewards;


[CreateAssetMenu(fileName = "RewardsDB", menuName = "Scriptable Objects/RewardsDataBase")]
public class RewardDatabase : ScriptableObject
{
	public Reward[] rewards;

	public int rewardsCount
	{
		get { return rewards.Length; }
	}

	public Reward GetReward(int index)
	{
		return rewards[index];
	}
}
