using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShopItemManager : MonoBehaviour
{

    private int availableDiamondCount;
    public TMP_Text donutUIText;
    public TMP_Text diamondUIText;
    public ShopItemScriptableObjects[] shopItemSO;
    public GameObject[] shopPanelsGameObjects;
    public ShopTemplate[] shopTemplates;
    public string password;
    PlayerPrefsSaveSystem saveSystem = new PlayerPrefsSaveSystem();

    public int indexOfBuyButton;
    public int indexOfImage;

    //strings

    private string encryptedDonutPrefs = "EncryptedDonut";
    private string encryptedDiamondPrefs = "EncryptedDiamond";

    void Start()
    {
        GetCoins();
        LoadPanel();
        CheckPurchasable();
    }
    public void GetCoins()
    {
        availableDiamondCount = saveSystem.ReturnDecryptedScore(encryptedDiamondPrefs);
    }



    public void LoadPanel()
    {
        for (int i = 0; i < shopItemSO.Length; i++)
        {
            shopTemplates[i].TitleText.text = shopItemSO[i].Title;
            shopTemplates[i].DescriptionText.text = shopItemSO[i].Description;
            shopTemplates[i].bgSprite = shopItemSO[i].bgImage;
            LoadImageOfPanels(i);
        }
    }
    public void LoadImageOfPanels(int i)
    {
        shopPanelsGameObjects[i].transform.GetChild(indexOfImage).gameObject.GetComponent<Image>().sprite = shopItemSO[i].bgImage;
    }
    public void CheckPurchasable()
    {
        for (int i = 0; i < shopPanelsGameObjects.Length; i++)
        {
            if (availableDiamondCount >= shopItemSO[i].diamondCost)
            {
                shopPanelsGameObjects[i].transform.GetChild(indexOfBuyButton).gameObject.GetComponent<Button>().interactable = true;
            }
            else
            {
                shopPanelsGameObjects[i].transform.GetChild(indexOfBuyButton).gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }
    public void PurchaseItem(int buttonNumber)
    {
        if (availableDiamondCount >= shopItemSO[buttonNumber].diamondCost)
        {
            availableDiamondCount = availableDiamondCount - shopItemSO[buttonNumber].diamondCost;
            diamondUIText.text = availableDiamondCount.ToString();
            int donutCountsValue = saveSystem.ReturnDecryptedScore(encryptedDonutPrefs);
            int totalDonuts = donutCountsValue + shopItemSO[buttonNumber].donutValue;
            donutUIText.text = totalDonuts.ToString();
            int tempDiamond = shopItemSO[buttonNumber].diamondCost;
            int tempDonut = shopItemSO[buttonNumber].donutValue;
            saveSystem.EncryptPrefsPositive(tempDonut, encryptedDonutPrefs);
            saveSystem.EncryptPrefsNegative(tempDiamond, encryptedDiamondPrefs);
            CheckPurchasable();
        }
    }
}
