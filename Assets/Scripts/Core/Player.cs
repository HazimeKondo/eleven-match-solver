using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("VisualElements")] [SerializeField]
    private TMP_Text _shirtNumberTxt;

    [SerializeField] private TMP_Text _powerTxt;
    [SerializeField] private Image _typeIcon;
    [SerializeField] private Sprite _offesiveSprite;
    [SerializeField] private Sprite _defensiveSprite;

    [Header("Properties")] [SerializeField]
    private Team.Agent _agent;

    private Section _ownerSection;
    public bool IsOffensive => _agent.offensive;

    public int Power => _agent.power;
    public int Shirt => _agent.shirt;

    public Team.Agent Agent => _agent;

    private void Awake()
    {
        UpdateVisual();
    }

    public void SetAgent(Team.Agent agent)
    {
        _agent = agent;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        _shirtNumberTxt.text = ToRoman(_agent.shirt);
        _typeIcon.sprite = (_agent.offensive) ? _offesiveSprite : _defensiveSprite;
        _powerTxt.text = _agent.power.ToString();
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

    public bool ChangeType()
    {
        _typeIcon.sprite = (_agent.offensive) ? _defensiveSprite : _offesiveSprite;
        return _agent.offensive = !_agent.offensive;
    }

    public int IncreasePower()
    {
        ++_agent.power;
        _powerTxt.text = _agent.power.ToString();
        return _agent.power;
    }

    public int DecreasePower()
    {
        if (--_agent.power < 0) _agent.power = 0;
        _powerTxt.text = _agent.power.ToString();
        return _agent.power;
    }

    public void SetOwner(Section section)
    {
        if(_ownerSection) _ownerSection.RemovePlayer(this);
        (_ownerSection = section).AddPlayer(this);
    }

    public void AddListener(Action<Player> show)
    {
        GetComponentInChildren<Button>().onClick.AddListener(() => show.Invoke(this));
    }
}