using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcSelection : MonoBehaviour
{
    [Header("Visual Elements")] 
    [SerializeField] private Transform _content;
    [SerializeField] private NpcSelectionElement _elementPrefab;
    [SerializeField] private TMP_Text _selectedTxt;
    [SerializeField] private ImageClickedEvent _closeBtn;
    [SerializeField] private GameObject _modalBg;
    [SerializeField] private Button _confirm;

    [Header("Data")]
    [SerializeField] private ResultManager _resultManager;
    [SerializeField] private NPCTeam[] _npcTeams;

    private NpcSelectionElement _selectedElement;
    private void Awake()
    {
        _closeBtn.onClick += Close;
        _confirm.onClick.AddListener(Confirm);
        Close();
        foreach (var npcTeam in _npcTeams)
        {
            var newElement = Instantiate(_elementPrefab.gameObject, _content).GetComponent<NpcSelectionElement>();

            newElement.SetTeam(npcTeam);
            newElement.onClick += ChooseTeam;
            if (_selectedElement == null)
            {
                newElement.Clicked();
            }
        }
    }

    public void Show()
    {
        _modalBg.SetActive(true);
    }

    public void Close()
    {
        _modalBg.SetActive(false);
    }
    
    private void ChooseTeam(NpcSelectionElement element)
    {
        _selectedElement?.SetActive(true);
        _selectedElement = element;
        _selectedTxt.text = _selectedElement.GetTeamName;
    }

    private void Confirm()
    {
        Close();
        _resultManager.Show(_selectedElement.GetTeam);
    }
}