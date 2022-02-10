using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayer : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnPoint;
    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacters: ");
        GameObject prefab = characterPrefabs[selectedCharacter];
        GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
}
