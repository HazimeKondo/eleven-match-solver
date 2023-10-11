using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScorers : MonoBehaviour
{
    [SerializeField] private GameObject _shirtPlayerPrefab;

    public void SetScorers(params int[] scorers)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (var scorer in scorers)
        {
            Instantiate(_shirtPlayerPrefab, transform).GetComponentInChildren<TMP_Text>().text = ToRoman(scorer);
        }
    }
    
    private string ToRoman(int value)
    {
        switch (value)
        {
            case -1: return "I";
            case -2: return "II";
            case -3: return "III";
            default: return value.ToString();
        }
    }
}
