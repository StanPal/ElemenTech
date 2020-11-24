using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _team1Score = 0;
    private int _team2Score = 0;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        ServiceLocator.Register<ScoreManager>(this);
    }

    public void AddPoints(int team, int score)
    {
        switch (team)
        {
            case 1:
                _team1Score += score;
                break;
            case 2:
                _team2Score += score;
                break;
        }
    }
}
