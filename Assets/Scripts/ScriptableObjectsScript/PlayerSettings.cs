using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings",menuName = "Scriptable Objects/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    public float minTouchSpeed { get; private set; } = 0.007f;
    public float maxTouchSpeed { get; private set; } = 0.028f;

    public float minGyroSpeed { get; private set; } = 25;
    public float maxGyroSpeed { get; private set; } = 53;

    public static float playerTouchControlSpeed = 0.018f;
    public static float playerGyroControlSpeed = 36;

}
