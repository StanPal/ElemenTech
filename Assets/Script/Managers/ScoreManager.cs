using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{    
    [SerializeField]
    private int mTeamOneScore = 0;
    public int TeamOneScore { get { return mTeamOneScore; } }
    [SerializeField]
    private int mTeamTwoScore = 0;
    public int TeamTwoScore { get { return mTeamTwoScore; } }
    [SerializeField]
    bool isPracticeMode = false;
    public bool PracticeMode { get { return isPracticeMode;} set { isPracticeMode = value; } }
    private void Awake()
    {        
        ServiceLocator.Register<ScoreManager>(this);
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

    public void ResetScore()
    {
        mTeamOneScore = 0;
        mTeamTwoScore = 0;
    }
}
