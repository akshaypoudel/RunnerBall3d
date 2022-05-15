using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityCipher;
using UnityEngine;
using UnityEngine.UI;
public class BallSkinManager : MonoBehaviour
{
    private int coins;
    public TMP_Text coinUI;
    public BallSkinSO[] BallSkinSO;
    public GameObject[] BallSkinPanelGameObjects;
    public BallSkinTemplate[] BallItemTemplates;
    public Sprite[] stageBGImage;


    private List<GameObject> PurchaseButton;
    private List<GameObject> SelectButtons;
    private List<GameObject> SelectedButtons;
    private List<GameObject> FruitIcon;

    public int buttonIndexInStageTemplateGameObject;

    [SerializeField] private string passwordforsavefile;
    PlayerPrefsSaveSystem saveSystem = new PlayerPrefsSaveSystem();

    private string jsonFileName = "DE_889.json";
    private string indexOfMat = "indexOfMaterial1";
    private string persistantDataPath = "";

    public string encryptedDonutPrefs = "EncryptedDonut";
    public string encryptedDiamondPrefs = "EncryptedDiamond";

    private SaveLoadJSONData saveSystemWithJson;
    PlayerDataNumber playerDataNumber;


    private void Awake()
    {
        saveSystemWithJson = new SaveLoadJSONData();
        playerDataNumber = new PlayerDataNumber();
        persistantDataPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar;
    }
    void Start()
    {
        GetCoins();
        AssignButtons();
        ActivateGameObjects();
        LoadPanel();
        CheckPurchasable();
        ActivateSelectedButtons();
        DoesFileExists();

    }

    private void ActivateSelectedButtons()
    {
        if (PlayerPrefs.HasKey(indexOfMat))
            SetSelectedButtonActive();
        else
            PlayerPrefs.SetInt(indexOfMat, 0);
    }

    private void DoesFileExists()
    {
        if (File.Exists(persistantDataPath + jsonFileName))
        {
            LoadButtonState();
        }
    }

    private void ActivateGameObjects()
    {
        for (int i = 0; i < BallSkinPanelGameObjects.Length; i++)
        {
            BallSkinPanelGameObjects[i].SetActive(true);
        }
    }

    public void GetCoins()
    {
        coins = saveSystem.ReturnDecryptedScore(encryptedDonutPrefs);
    }

    public void AssignButtons()
    {
        if (PurchaseButton == null) PurchaseButton = new List<GameObject>();
        if (SelectButtons == null) SelectButtons = new List<GameObject>();
        if (SelectedButtons == null) SelectedButtons = new List<GameObject>();
        if (FruitIcon == null) FruitIcon = new List<GameObject>();

        for (int i = 0; i < BallSkinPanelGameObjects.Length; i++)
        {
            PurchaseButton.Add(BallSkinPanelGameObjects[i].transform.GetChild(buttonIndexInStageTemplateGameObject).gameObject);
            SelectButtons.Add(BallSkinPanelGameObjects[i].transform.GetChild(buttonIndexInStageTemplateGameObject + 1).gameObject);
            SelectedButtons.Add(BallSkinPanelGameObjects[i].transform.GetChild(buttonIndexInStageTemplateGameObject + 2).gameObject);
            FruitIcon.Add(BallSkinPanelGameObjects[i].transform.GetChild(buttonIndexInStageTemplateGameObject + 3).gameObject);
        }
    }

    public void LoadPanel()
    {
        for (int i = 0; i < BallSkinSO.Length; i++)
        {
            BallItemTemplates[i].TitleText.text = BallSkinSO[i].Price;
            BallItemTemplates[i].BGSprite = stageBGImage[i];
            BallItemTemplates[i].NameOfBallSkin = BallSkinSO[i].name;
            LoadImageOfPanels(i);
        }
    }
    public void LoadImageOfPanels(int i)
    {
        BallSkinPanelGameObjects[i].transform.GetChild(4).gameObject.GetComponent<Image>().sprite = BallItemTemplates[i].BGSprite;
    }
    public void CheckPurchasable()
    {
        for (int i = 0; i < BallSkinPanelGameObjects.Length; i++)
        {
            if (coins >= BallSkinSO[i].baseCost)
            {
                BallSkinPanelGameObjects[i].transform.GetChild(buttonIndexInStageTemplateGameObject).gameObject.GetComponent<Button>().interactable = true;
            }
            else
            {
                BallSkinPanelGameObjects[i].transform.GetChild(buttonIndexInStageTemplateGameObject).gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }
    public void PurchaseItem(int buttonNumber)
    {
        if (coins >= BallSkinSO[buttonNumber].baseCost)
        {
            BallSkinPanelGameObjects[buttonNumber].transform.GetChild(3).gameObject.SetActive(false);
            coins = coins - BallSkinSO[buttonNumber].baseCost;
            coinUI.text = coins.ToString();
            int tempCoin = BallSkinSO[buttonNumber].baseCost;
            saveSystem.EncryptPrefsNegative(tempCoin, encryptedDonutPrefs);
            CheckPurchasable();
            Select(buttonNumber);

        }
    }
    private void SetSelectedButtonActive()
    {
        for (int i = 0; i < SelectedButtons.Count; i++)
        {
            SelectedButtons[i].SetActive(false);
        }
        SelectedButtons[PlayerPrefs.GetInt(indexOfMat)].SetActive(true);
    }
    public void Selected(int buttonNumber)
    {
        if (SelectButtons[buttonNumber].activeSelf)
        {
            for (int i = 0; i < SelectedButtons.Count; i++)
            {
                SelectedButtons[i].SetActive(false);
            }
            SelectedButtons[buttonNumber].SetActive(true);
            PlayerPrefs.SetInt(indexOfMat, buttonNumber);
        }
    }
    public void Select(int buttonNumber)
    {
        bool isActive = true;
        bool isNotActive = false;
        PurchaseButton[buttonNumber].SetActive(isNotActive);
        SelectButtons[buttonNumber].SetActive(isActive);
        BallItemTemplates[buttonNumber].UnlockedObjectText.text = BallSkinSO[buttonNumber].NameOfSkin;
        FruitIcon[buttonNumber].SetActive(isNotActive);
        SaveButtonState(buttonNumber, BallSkinSO[buttonNumber].NameOfSkin, isActive, isNotActive);
    }
    public void SaveButtonState(int buttonNumber, string nameOfObject, bool isActive, bool isNotActive)
    {
        PlayerData playerdata = new PlayerData();
        playerdata.buttonNumber = buttonNumber;
        playerdata.dataToSetActive = isActive;
        playerdata.dataToSetInactive = isNotActive;
        playerdata.removeFruitIcon = isNotActive;
        playerdata.nameOfUnlockedObject = nameOfObject;
        SaveEncryptedData(playerdata);

    }
    private void SaveEncryptedData(PlayerData playerdata)
    {
        PlayerDataEncrypted playerdataencrypt = new PlayerDataEncrypted();
        string encrypt1 = RijndaelEncryption.Encrypt(playerdata.buttonNumber.ToString(), passwordforsavefile);
        string encrypt4 = RijndaelEncryption.Encrypt(playerdata.nameOfUnlockedObject, passwordforsavefile);


        playerdataencrypt.No = encrypt1;
        playerdataencrypt.yieldd = playerdata.dataToSetActive;
        playerdataencrypt.NetworkBuild = playerdata.dataToSetInactive;
        playerdataencrypt.TIO00_UGG = playerdata.dataToSetInactive;
        playerdataencrypt.name = encrypt4;
        playerDataNumber.NetworkingCommand.Add(playerdataencrypt);
        saveSystemWithJson.SavePlayerDataNumber(playerDataNumber, jsonFileName);
    }
    public void LoadButtonState()
    {
        PlayerDataEncrypted playerdata = new PlayerDataEncrypted();
        playerDataNumber = saveSystemWithJson.LoadPlayerDataNumber(jsonFileName);


        for (int i = 0; i < playerDataNumber.NetworkingCommand.Count; i++)
        {

            playerdata = playerDataNumber.NetworkingCommand[i];
            //decrypting the buttonNumber of the object
            int number = int.Parse(RijndaelEncryption.Decrypt(playerdata.No, passwordforsavefile));
            //decrypting the name of the unlocked object
            string data2 = RijndaelEncryption.Decrypt(playerdata.name, passwordforsavefile);


            SelectButtons[number].SetActive(playerdata.yieldd);
            PurchaseButton[number].SetActive(playerdata.NetworkBuild);
            FruitIcon[number].SetActive(playerdata.TIO00_UGG);
            BallItemTemplates[number].UnlockedObjectText.text = data2;

        }



    }
}
