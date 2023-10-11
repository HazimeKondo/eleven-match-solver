using System;
using System.IO;
using UnityEngine;

public static class SaveAndLoad
{
   private const string TEAM_KEY = "team01";

   [Serializable]
   public class TeamSave
   {
      public Team team;
      public Team.Agent[] bench;
   }

   public static string SaveTeam(Team team, Team.Agent[] bench)
   {
      var json = JsonUtility.ToJson(new TeamSave() { team = team, bench = bench });
      // var streamwriter = new StreamWriter(Path.Combine(Application.persistentDataPath, TEAM_KEY));
      // streamwriter.Write(json);
      // streamwriter.Close();
      PlayerPrefs.SetString(TEAM_KEY, json);
      PlayerPrefs.Save();
      return json;
   }

   public static TeamSave LoadTeam(string defaultJson)
   {
      var json = PlayerPrefs.GetString(TEAM_KEY, defaultJson);
      return JsonUtility.FromJson<TeamSave>(json);
   }

   public static TeamSave TeamFromJson(string json)
   {
      return JsonUtility.FromJson<TeamSave>(json);
   }

}