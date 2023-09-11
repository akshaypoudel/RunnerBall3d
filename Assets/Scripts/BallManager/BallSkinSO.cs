using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "BallSkin", menuName = "Scriptable Objects/New Ball Skin", order = 1)]
public class BallSkinSO : ScriptableObject
{
    public string Price;
    public string NameOfSkin;
    public int baseCost;
}


