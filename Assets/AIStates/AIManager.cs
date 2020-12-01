using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayerManager : MonoBehaviour
{
    private List<AIPlayer> _aiPlayers = new List<AIPlayer>();

    private void Awake()
    {
        ServiceLocator.Register<AIPlayerManager>(this);
    }

    public void AddAIPlayer(AIPlayer player)
    {
        _aiPlayers.Add(player);
    }

    public AIPlayer GetPlayer(string name)
    {
        foreach(var player in _aiPlayers)
        {
            if (player.name.Equals(name))
            {
                return player;
            }
        }
        return null;
    }
}
