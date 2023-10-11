using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NPCTeam))]
public class NPCTeamEditor : Editor
{
    private string json;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var obj = target as NPCTeam; 
        if (GUILayout.Button("Invert"))
        {
            obj.TeamData.Invert();
            EditorUtility.SetDirty(obj);
        }
        if (GUILayout.Button("Import"))
        {
            obj.TeamData = JsonUtility.FromJson<Team>(json);
        }

        if (GUILayout.Button("Export"))
        {
            json = JsonUtility.ToJson(obj.TeamData);
        }
        json = GUILayout.TextArea(json);
    }
}
