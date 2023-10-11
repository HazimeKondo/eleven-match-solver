using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Goalkeeper : MonoBehaviour
{
    [Header("VisualElements")]
    [SerializeField] private TMP_Text _powerTxt;
    [SerializeField] private GlovesHolder _glovesHolder;

    [FormerlySerializedAs("_agent")]
    [Header("Properties")] 
    [SerializeField] private Team.GoalkeeperAgent _goalkeeper;

    public int Power => _goalkeeper.power;
    public int Gloves => _goalkeeper.gloves;

    public Team.GoalkeeperAgent Agent => _goalkeeper;
    
    private void Awake()
    {
        UpdateVisual();
    }

    public void SetAgent(Team.GoalkeeperAgent agent)
    {
        _goalkeeper = agent;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        _powerTxt.text = _goalkeeper.power.ToString();
        _glovesHolder.SetGloves(_goalkeeper.gloves);
    }
    
    public int IncreasePower()
    {
        ++_goalkeeper.power;
        _powerTxt.text = _goalkeeper.power.ToString();
        return _goalkeeper.power;
    }

    public int DecreasePower()
    {
        if (--_goalkeeper.power < 0) _goalkeeper.power = 0;
        _powerTxt.text = _goalkeeper.power.ToString();
        return _goalkeeper.power;
    }
    
    
    public int IncreaseGloves()
    {
        ++_goalkeeper.gloves;
        _glovesHolder.SetGloves(_goalkeeper.gloves);
        return _goalkeeper.gloves;
    }

    public int DecreaseGloves()
    {
        if (--_goalkeeper.gloves < 0) _goalkeeper.gloves = 0;
        _glovesHolder.SetGloves(_goalkeeper.gloves);
        return _goalkeeper.gloves;
    }
    public void AddListener(Action show)
    {
        GetComponentInChildren<Button>().onClick.AddListener(() => show.Invoke());
    }
}