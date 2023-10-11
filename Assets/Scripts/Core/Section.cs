using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Section : MonoBehaviour
{
    private List<Player> _players = new List<Player>();

    private void Awake()
    {
        foreach (var player in GetComponentsInChildren<Player>().ToList())
        {
            player.SetOwner(this);
        }
    }

    public void AddPlayer(Player player)
    {
        _players.Add(player);
        player.transform.SetParent(transform);
    }

    public void RemovePlayer(Player player)
    {
        _players.Remove(player);
    }

    public Team.Agent[] ToAgents()
    {
        return _players.Select(_ => new Team.Agent() { offensive = _.IsOffensive, power = _.Power , shirt = _.Shirt}).ToArray();
    }

    public void Clear()
    {
        foreach (var player in _players)
        {
            Destroy(player.gameObject);
        }
        _players.Clear();
    }
}
