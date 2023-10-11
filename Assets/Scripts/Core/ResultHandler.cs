using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ResultHandler
{
    public struct ResultData
    {
        public int TeamGoals;
        public int NPCGoals;
        public int[] TeamPlayers;
    }

    public static ResultData Check(Team playerTeam, Team npcTeam)
    {
        //NPC goals
        var npckickers = CalculatePossible(npcTeam, playerTeam, Team.Area.Left)
            .Concat(CalculatePossible(npcTeam, playerTeam, Team.Area.Right))
            .Concat(CalculatePossible(npcTeam, playerTeam, Team.Area.Attack))
            .Concat(CalculatePossible(npcTeam, playerTeam, Team.Area.Mid))
            .Concat(CalculatePossible(npcTeam, playerTeam, Team.Area.Defense)).OrderBy(_ => _.power).ToList();

        var gloves = playerTeam.GK.gloves;

        var npcGoalers = new List<Team.Agent>();

        foreach (var kicker in npckickers)
        {
            if (gloves> 0 && kicker.power <= playerTeam.GK.power)
            {
                gloves--;
                continue;
            }
            npcGoalers.Add(kicker);
        }
        
        //Player goals
        var playerkickers = CalculatePossible(playerTeam, npcTeam, Team.Area.Left)
            .Concat(CalculatePossible(playerTeam, npcTeam, Team.Area.Right))
            .Concat(CalculatePossible(playerTeam, npcTeam, Team.Area.Attack))
            .Concat(CalculatePossible(playerTeam, npcTeam, Team.Area.Mid))
            .Concat(CalculatePossible(playerTeam, npcTeam, Team.Area.Defense)).OrderBy(_ => _.power).ToList();

        gloves = npcTeam.GK.gloves;
        
        var playerGoalers = new List<Team.Agent>();

        foreach (var kicker in playerkickers)
        {
            if (gloves> 0 && kicker.power <= npcTeam.GK.power)
            {
                gloves--;
                continue;
            }
            playerGoalers.Add(kicker);
        }
        
        var result = new ResultData()
        {
            TeamGoals = playerGoalers.Count,
            NPCGoals = npcGoalers.Count,
            TeamPlayers = playerGoalers.Select(_ => _.shirt).ToArray()
        };

        return result;
    }

    private static Team.Agent[] CalculatePossible(Team attackingTeam, Team defendingTeam, Team.Area area)
    {
        var attacking = attackingTeam.GetGroup(area, true);
        var defending = defendingTeam.GetGroup(area, false);
        if (attacking.Length <= 0) return Array.Empty<Team.Agent>();
        if (defending.Length <= 0) return attacking;

        List<int> defPowers = new List<int>();
        foreach (var defendant in defending)
        {
            defPowers.Add(defendant.power);
        }

        Stack<Team.Agent> att = new Stack<Team.Agent>();
        foreach (var agent in attacking)
        {
            att.Push(agent);
        }

        var goalkickers = new List<Team.Agent>();
        while (att.Count > 0)
        {
            var attacker = att.Pop();
            var defId = 0;
            var goalkick = true;
            for (; defId < defPowers.Count; defId++)
            {
                if (defPowers[defId] >= attacker.power)
                {
                    defPowers.RemoveAt(defId);
                    goalkick = false;
                    break;
                }
            }

            if (goalkick)
                goalkickers.Add(attacker);
        }

        return goalkickers.ToArray();
    }
}