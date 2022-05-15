using TMPro;
using UnityEngine;


public class HighScoreScript : MonoBehaviour
{
    public TMP_Text Distance;
    public float multiplier = 1;
    [HideInInspector] public float score;


    void FixedUpdate()
    {
        if (!MoveLogic.isGameOver)
        {
            score += multiplier;
            Distance.text = score.ToString() + " m";
        }
    }

}
