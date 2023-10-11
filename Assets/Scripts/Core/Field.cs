using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
    [SerializeField] private Section[] _sections;
    [SerializeField] private Section _benchSection;

    [SerializeField] private Goalkeeper _goalkeeper;

    [SerializeField] private Player _playerprefab;
    [SerializeField] private string _defaultTeamJson;
    [SerializeField] private PlayerEditor _playerEditor;
    [SerializeField] private GoalkeeperEditor _goalkeeperEditor;
    [SerializeField] private Button _resetButton;

    private void Start()
    {
        LoadTeam();
        _goalkeeperEditor.onClose += SaveTeam;
        _playerEditor.onClose += SaveTeam;
        _resetButton.onClick.AddListener(ResetTeam);
    }

    public Team GetActualTeam()
    {
        return new Team(_sections, _goalkeeper.Agent);
    }

    [ContextMenu("Save Team")]
    public void SaveTeam()
    {
        Debug.Log(SaveAndLoad.SaveTeam(GetActualTeam(),_benchSection.ToAgents()));
    }

    [ContextMenu("Load Team")]
    private void LoadTeam()
    {
        SetTeam(SaveAndLoad.LoadTeam(_defaultTeamJson));
    }
    
    private void SetTeam(SaveAndLoad.TeamSave save)
    {
        ClearSections();
        for (int i = 0; i < 9; i++)
        {
            var agents = save.team.GetGroup(i);
            foreach (var agent in agents)
            {
                var player = Instantiate(_playerprefab.gameObject,_sections[i].transform).GetComponent<Player>();
                player.SetAgent(agent);
                player.SetOwner(_sections[i]);
                player.AddListener(_playerEditor.Show);
            }
        }

        foreach (var agent in save.bench)
        {
            var player = Instantiate(_playerprefab.gameObject,_benchSection.transform).GetComponent<Player>();
            player.SetAgent(agent);
            player.SetOwner(_benchSection);
            player.AddListener(_playerEditor.Show);
        }
        
        _goalkeeper.SetAgent(save.team.GK);
    }

    [ContextMenu("Reset Team")]
    private void ResetTeam()
    {
        SetTeam(SaveAndLoad.TeamFromJson(_defaultTeamJson));
    }

    private void ClearSections()
    {
        foreach (var section in _sections)
        {
            section.Clear();
        }
        _benchSection.Clear();
    }
}