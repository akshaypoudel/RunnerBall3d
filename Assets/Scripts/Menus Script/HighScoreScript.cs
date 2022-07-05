using TMPro;
using UnityEngine;


public class HighScoreScript : MonoBehaviour
{
    public TMP_Text Distance;
    private float multiplier = 1;
    [HideInInspector] public float score;
    void FixedUpdate()
    {
        if (!MoveLogic.isGameOver)
        {
            score += multiplier;
            Distance.text = score.ToString() + " m";
        }
        if (score > 2000 && score < 3000) MoveLogic.playerMovementSpeed = 16f;
        if (score > 3000 && score < 4500) MoveLogic.playerMovementSpeed = 17.5f;
        if (score > 4500 && score < 5500) MoveLogic.playerMovementSpeed = 18.2f;
        if (score > 5500 && score < 6500) MoveLogic.playerMovementSpeed = 19f;
        if (score > 6500 && score < 8000) MoveLogic.playerMovementSpeed = 20f;
        if (score > 8000) MoveLogic.playerMovementSpeed = 23f;


    }

}
