using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTypeButton : MonoBehaviour
{
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _deactiveColor;

    [SerializeField] private Image _offensiveIcon;
    [SerializeField] private Image _defensiveIcon;

    public void SetToOffensive(bool value)
    {
        _offensiveIcon.color = value ? _activeColor : _deactiveColor;
        _defensiveIcon.color = !value ? _activeColor : _deactiveColor;
    }

    public void AddListener(Action callback)
    {
        GetComponentInChildren<Button>().onClick.AddListener(() => callback?.Invoke());
    }
}