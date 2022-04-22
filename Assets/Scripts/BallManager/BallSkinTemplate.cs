using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallSkinTemplate : MonoBehaviour
{
    public TMP_Text TitleText;
    public TMP_Text UnlockedObjectText;
    [HideInInspector] public string NameOfBallSkin;
    [HideInInspector] public Sprite BGSprite;
    [HideInInspector] public TMP_Text CostText;
}
