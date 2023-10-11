using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Team
{
    public enum Area
    {
        Left,
        Right,
        Attack,
        Mid,
        Defense
    }
 
    [Serializable]
    public struct Agent
    {
        public bool offensive;
        public int power;
        public int shirt;
    }

    [Serializable]
    public struct GoalkeeperAgent
    {
        public int power;
        public int gloves;
    }

    [SerializeField] private GoalkeeperAgent gk;
    
    [SerializeField] private Agent[] al;
    [SerializeField] private Agent[] ac;
    [SerializeField] private Agent[] ar;

    [SerializeField] private Agent[] ml;
    [SerializeField] private Agent[] mc;
    [SerializeField] private Agent[] mr;
    
    [SerializeField] private Agent[] dl;
    [SerializeField] private Agent[] dc;
    [SerializeField] private Agent[] dr;

    public readonly string Formation;

    public GoalkeeperAgent GK => gk;

    public Agent[] GetGroup(Area area, bool offensive)
    {
        return GetFromArea(area).Where(_ => _.offensive == offensive).OrderBy(_ => _.power).ToArray();
    }

    public Agent[] GetGroup(int i)
    {
        switch (i)
        {
            case 0: return al;
            case 1: return ac;
            case 2: return ar;
            case 3: return ml;
            case 4: return mc;
            case 5: return mr;
            case 6: return dl;
            case 7: return dc;
            case 8: return dr;
            default: return null;
        }
    }

    private IEnumerable<Agent> GetFromArea(Area area)
    {
        switch (area)
        {
            case Area.Left: return al.Concat(ml).Concat(dl);
            case Area.Right: return ar.Concat(mr).Concat(dr);
            case Area.Attack: return ac;
            case Area.Mid: return mc;
            case Area.Defense: return dc;
            default: return null;
        }
    }

    public Team(Section[] sections, GoalkeeperAgent goalkeeperAgent)
    {
        gk = goalkeeperAgent;
        al = sections[0].ToAgents();
        ac = sections[1].ToAgents();
        ar = sections[2].ToAgents();

        ml = sections[3].ToAgents();
        mc = sections[4].ToAgents();
        mr = sections[5].ToAgents();
        
        dl = sections[6].ToAgents();
        dc = sections[7].ToAgents();
        dr = sections[8].ToAgents();

        Formation = $"{dl.Length + dc.Length + dr.Length}-" +
                    $"{ml.Length + mc.Length + mr.Length}-" +
                    $"{al.Length + ac.Length + ar.Length}";
    }

    public void Invert()
    {
        (al, dl) = (dl, al);
        (ac, dc) = (dc, ac);
        (ar, dr) = (dr, ar);
    }
}
