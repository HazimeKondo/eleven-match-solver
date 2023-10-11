using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GoalkeeperEditor : MonoBehaviour
{
    [SerializeField] private GameObject _goalkeeperEditModal;

    [Header("General")] 
    [SerializeField] private Button _bgClose;
    [SerializeField] private Goalkeeper _modalGoalkeeper;

    [Header("Power")]
    [SerializeField] private Button _powerUpButton;
    [SerializeField] private Button _powerDownButton;
    [SerializeField] private TMP_Text _powerTxt;

    [Header("Gloves")]
    [SerializeField] private Button _glovesUpButton;
    [SerializeField] private Button _glovesDownButton;
    [SerializeField] private TMP_Text _glovesTxt;

    
    [Header("Goalkeeper")]
    [SerializeField] private Goalkeeper _goalkeeper;

    public Action onClose;

    private void Awake()
    {
        _goalkeeperEditModal.SetActive(false);
    }

    private void Start()
    {
        _goalkeeper.AddListener(Show);
        _bgClose.onClick.AddListener(Close);
        _powerUpButton.onClick.AddListener(IncreasePower);
        _powerDownButton.onClick.AddListener(DecreasePower);
        _glovesUpButton.onClick.AddListener(IncreaseGloves);
        _glovesDownButton.onClick.AddListener(DecreaseGloves);
    }

    public void Show()
    {
        _modalGoalkeeper.SetAgent(_goalkeeper.Agent);
        _goalkeeperEditModal.SetActive(true);
        _powerTxt.text = _goalkeeper.Power.ToString();
        _glovesTxt.text = _goalkeeper.Gloves.ToString();
    }

    public void Close()
    {
        _goalkeeperEditModal.SetActive(false);
        onClose?.Invoke();
    }
    
    private void IncreasePower()
    {
        _powerTxt.text = _goalkeeper.IncreasePower().ToString();
        _modalGoalkeeper.IncreasePower();
    }
    
    private void DecreasePower()
    {
        _powerTxt.text = _goalkeeper.DecreasePower().ToString();
        _modalGoalkeeper.DecreasePower();
    }
    
    private void IncreaseGloves()
    {
        _glovesTxt.text = _goalkeeper.IncreaseGloves().ToString();
        _modalGoalkeeper.IncreaseGloves();
    }
    
    private void DecreaseGloves()
    {
        _glovesTxt.text = _goalkeeper.DecreaseGloves().ToString();
        _modalGoalkeeper.DecreaseGloves();
    }
}