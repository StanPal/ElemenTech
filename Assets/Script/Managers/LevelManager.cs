using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private ScoreManager mScoreManager;
    private PlayerManager mPlayerManager;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        mScoreManager = ServiceLocator.Get<ScoreManager>();
        mPlayerManager = ServiceLocator.Get<PlayerManager>();
    }

    private void Update()
    {
        if(mPlayerManager.mPlayersList.Count == 1)
        {
            switch (mPlayerManager.mPlayersList[0].GetComponent<HeroStats>().team)
            {
                case HeroStats.TeamSetting.Team1:
                    mScoreManager.AddPoints(1, 1);
                    break;
                case HeroStats.TeamSetting.Team2:
                    mScoreManager.AddPoints(2, 1);
                    break;
                case HeroStats.TeamSetting.FFA:
                    break;
                default:
                    break;
            }


        }
    }
}
