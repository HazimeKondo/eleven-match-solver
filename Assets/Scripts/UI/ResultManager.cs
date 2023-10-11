using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [Header("Visual Elements")] 
    [SerializeField] private GameObject _modalBg;
    [SerializeField] private TMP_Text _scoreTxt;
    [SerializeField] private Button _closeButton;
    [SerializeField] private PlayerScorers _scorers;

    [Header("Data")]
    private NPCTeam _npcteamData;
    [SerializeField] private Field _field;

    private void Awake()
    {
        _modalBg.SetActive(false);
        _closeButton.onClick.AddListener(Close);
    }

    public void Show(NPCTeam npcTeam)
    {
        _npcteamData = npcTeam;
        _modalBg.SetActive(true);
        CheckResult();
    }
    
    public void Close()
    {
        _modalBg.SetActive(false);
    }
    
    [ContextMenu("CheckResult")]
    private void CheckResult()
    {
        var result = ResultHandler.Check(_field.GetActualTeam(), _npcteamData.TeamData);
        
        var log = $"  Player  {result.TeamGoals} x {result.NPCGoals} NPC \n" +
                  $" Player Kickers: \n";
        foreach (var shirt in result.TeamPlayers)
        {
            log += $"{shirt} \n";
        }
        Debug.Log(log);

        _scoreTxt.text = $"{result.TeamGoals} X {result.NPCGoals}";
        _scorers.SetScorers(result.TeamPlayers);
    }
}