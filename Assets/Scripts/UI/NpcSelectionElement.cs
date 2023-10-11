using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NpcSelectionElement : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private TMP_Text _nameTxt;
    
    public Action<NpcSelectionElement> onClick;

    private NPCTeam _npcTeam;

    public NPCTeam GetTeam => _npcTeam;
    public string GetTeamName => _npcTeam.CardId;

    private void Awake()
    {
        _btn.onClick.AddListener(Clicked);
    }

    public void SetTeam(NPCTeam npcTeam)
    {
        _npcTeam = npcTeam;
        _nameTxt.text = GetTeamName;
    }

    public void Clicked()
    {
        onClick?.Invoke(this);
        SetActive(false);
    }

    public void SetActive(bool value)
    {
        _btn.interactable = value;
    }
}