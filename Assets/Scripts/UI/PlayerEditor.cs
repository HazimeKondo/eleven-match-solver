using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEditor : MonoBehaviour
{
    [SerializeField] private GameObject _playerEditModal;

    [Header("General")] 
    [SerializeField] private ImageClickedEvent _bgClose;
    [SerializeField] private Player _modalPlayer;

    [Header("Player Type")] [SerializeField]
    private ChangeTypeButton _playerTypeChangeBtn;
    
    [Header("Power")]
    [SerializeField] private Button _powerUpButton;
    [SerializeField] private Button _powerDownButton;
    [SerializeField] private TMP_Text _powerTxt;

    [Header("Move")]
    [SerializeField] private Button[] _sectionBtns;
    [SerializeField] private Section[] _sections;

    private Player _editingPlayer;

    public Action onClose;

    private void Awake()
    {
        _playerEditModal.SetActive(false);
    }

    private void Start()
    {
        for (int i = 0; i < _sectionBtns.Length; i++)
        {
            var section = _sections[i];
            _sectionBtns[i].onClick.AddListener(() => SetPlayerToSection(section));
        }

        _bgClose.onClick += Close;
        _playerTypeChangeBtn.AddListener(ChangeType);
        _powerUpButton.onClick.AddListener(IncreasePower);
        _powerDownButton.onClick.AddListener(DecreasePower);
    }

    public void Show(Player player)
    {
        _editingPlayer = player;
        _modalPlayer.SetAgent(_editingPlayer.Agent);
        _playerEditModal.SetActive(true);
        _playerTypeChangeBtn.SetToOffensive(player.IsOffensive);
        _powerTxt.text = _editingPlayer.Power.ToString();
    }

    public void Close()
    {
        _playerEditModal.SetActive(false);
        onClose?.Invoke();
    }

    private void ChangeType()
    {
        _playerTypeChangeBtn.SetToOffensive(_editingPlayer.ChangeType());
        _playerTypeChangeBtn.SetToOffensive(_modalPlayer.ChangeType());
    }
    
    private void SetPlayerToSection(Section section)
    {
        _editingPlayer.SetOwner(section);
        Close();
    }

    private void IncreasePower()
    {
        _powerTxt.text = _editingPlayer.IncreasePower().ToString();
        _modalPlayer.IncreasePower();
    }
    
    private void DecreasePower()
    {
        _powerTxt.text = _editingPlayer.DecreasePower().ToString();
        _modalPlayer.DecreasePower();
    }
}