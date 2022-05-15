using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private bool isTimerActive = true;
    public int startTime;
    private float currentTime;

    public GameObject GameOverScreen;
    public GameObject GameOverUI;

    [Header("---------TextMeshPro--------\n")]

    public TMP_Text TimerText;
    public TMP_Text DiamondText;
    public TMP_Text finalDonutText;
    public TMP_Text finalDiamondText;
    public TMP_Text ScoreTextInGameOverScreen;
    public TMP_Text ScoreCalculatingTXT;
    public TMP_Text HighScoreTextInGameOverScreen;
    public TMP_Text DiamondPlayAgainTXT;

    [Header("---------End--------\n")]


    public Button diamondPlayAgainButton;
    private string EPrefs = "EncryptedDiamond";
    private string highScorePrefs = "HighScore";

    public HighScoreScript highScoreScript;
    public MoveLogic moveLogic;
    PlayerPrefsSaveSystem Playerprefssavesystem = new PlayerPrefsSaveSystem();

    private int currentScore;
    private int highScore;

    public int diamondsValueForPlayAgain = 1;
    void Start()
    {
        currentTime = startTime * 1;
        DiamondPlayAgainTXT.text = diamondsValueForPlayAgain.ToString();
        Playerprefssavesystem.DecryptPrefs(DiamondText, EPrefs);

    }


    void Update()
    {
        if (isTimerActive)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                isTimerActive = false;

                GameOverScreenActivate();
                DonutAndDiamondAmountCalcuate();
            }
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            TimerText.text = time.Seconds.ToString();
        }
    }
    public void GameOverScreenActivate()
    {
        GameOverUI.gameObject.SetActive(false);
        GameOverScreen.SetActive(true);
        ScoreTextInGameOverScreen.text = ScoreCalculatingTXT.text;
        HighScoreLogic();
    }

    public void DonutAndDiamondAmountCalcuate()
    {
        finalDonutText.text = MoveLogic.numberOfDonuts.ToString();
        finalDiamondText.text = MoveLogic.numberOfDiamonds.ToString();

        MoveLogic.numberOfDonuts = 0;
        MoveLogic.numberOfDiamonds = 0;
    }

    private void HighScoreLogic()
    {
        currentScore = (int)highScoreScript.score;
        if (!PlayerPrefs.HasKey(highScorePrefs))
        {
            PlayerPrefs.SetInt(highScorePrefs, highScore);
            highScore = currentScore;
            HighScoreTextInGameOverScreen.text = highScore.ToString() + " M";
        }
        else
        {
            int highscore = PlayerPrefs.GetInt(highScorePrefs);
            if (currentScore > highscore)
            {
                HighScoreTextInGameOverScreen.text = currentScore.ToString() + " M";
                PlayerPrefs.SetInt(highScorePrefs, currentScore);
            }
            else
            {
                HighScoreTextInGameOverScreen.text = PlayerPrefs.GetInt(highScorePrefs).ToString() + " M";
            }
        }
    }


    public void PlayAgainWithDiamonds()
    {
        int diamonds = Playerprefssavesystem.ReturnDecryptedScore(EPrefs);
        if (diamonds >= diamondsValueForPlayAgain)
        {
            ReduceTheDiamond(diamondsValueForPlayAgain);
            diamondsValueForPlayAgain *= 2;
            currentTime = startTime;
            moveLogic.PlayAgain();
        }
        else
        {
            ChangeTheVisibilityOfPlayAgainTextWithDiamond();
        }
    }


    public void Refresh()
    {
        int diamonds = Playerprefssavesystem.ReturnDecryptedScore(EPrefs);

        if (!(diamonds >= diamondsValueForPlayAgain))
        {
            ChangeTheVisibilityOfPlayAgainTextWithDiamond();
        }


    }
    private void ChangeTheVisibilityOfPlayAgainTextWithDiamond()
    {
        diamondPlayAgainButton.interactable = false;
        DiamondPlayAgainTXT.color = new Color(1f, 1f, 1f, 0.5f);
    }

    private void ReduceTheDiamond(int val)
    {
        Playerprefssavesystem.EncryptPrefsNegative(val, EPrefs);
        int a = int.Parse(DiamondText.text);
        val = a - val;
        if (val < 0)
            DiamondText.text = "0";
        else
            DiamondText.text = val.ToString();
    }
}
