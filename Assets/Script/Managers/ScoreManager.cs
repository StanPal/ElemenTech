using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private PlayerManager playerManager;
    private int mTeamOneScore = 0;
    private int mTeamTwoScore = 0;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        playerManager = ServiceLocator.Get<PlayerManager>();
    }

    public void AddPoints(int team, int points)
    {
        switch (team)
        {
            case 1:
                mTeamOneScore++;
                break;
            case 2:
                mTeamTwoScore++;
                break; 
            default:
                break;
        }
    }
}
