using UnityEngine;


[CreateAssetMenu(fileName = "BallSkinMaterial", menuName = "Scriptable Objects/New Ball Material", order = 2)]
public class BallSkinMaterialSO : ScriptableObject
{
    public Material[] materialOfObject;
}