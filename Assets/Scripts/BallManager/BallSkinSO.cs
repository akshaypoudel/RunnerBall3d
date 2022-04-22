using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "BallSkin", menuName = "Scriptable Objects/New Ball Skin", order = 1)]
public class BallSkinSO : ScriptableObject
{
    public string Price;
    public string NameOfSkin;
    private Image PreviewImage;
    public Material materialOfObject;
    public int baseCost;
}

