using System;
using TMPro;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets i;
    public TMP_Text DonutText;
    public TMP_Text DiamondText;
    public SoundAudioClip[] soundAudioClipArray;
    PlayerPrefsSaveSystem playerPrefsSaveSystem = new PlayerPrefsSaveSystem();
    public bool isMenuScene = true;


    public string encryptedDonutPrefs = "EncryptedDonut";
    public string encryptedDiamondPrefs = "EncryptedDiamond";

    private void Awake()
    {
        i = this;
    }

    private void Start()
    {
        if (isMenuScene)
        {
            playerPrefsSaveSystem.DecryptPrefs(DonutText, encryptedDonutPrefs);
            playerPrefsSaveSystem.DecryptPrefs(DiamondText, encryptedDiamondPrefs);
        }

    }


    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

}
