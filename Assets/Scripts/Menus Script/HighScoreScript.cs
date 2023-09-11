using TMPro;
using UnityEngine;


public class HighScoreScript : MonoBehaviour
{
    public TMP_Text Distance;
    private float scoreMultiplier = 1;
    private float score;
    void FixedUpdate()
    {
        if (!MoveLogic.isGameOver)
        {
            score += scoreMultiplier;
            Distance.text = score.ToString() + " m";
        }
    }
    public float GetCurrentScore()
    {
        return score;
    }

}
