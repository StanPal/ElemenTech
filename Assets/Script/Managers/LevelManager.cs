using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private ScoreManager mScoreManager;
    [SerializeField]
    private PlayerManager mPlayerManager;
    [SerializeField]
    private MatchUI MatchUI;
    private bool isMatchOver = false;
    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        mPlayerManager = ServiceLocator.Get<PlayerManager>();
        mScoreManager = ServiceLocator.Get<ScoreManager>();
        MatchUI = FindObjectOfType<MatchUI>();
    }

    private void Update()
    {
        if (!isMatchOver)
        {
            if (mPlayerManager.TeamOne.Count == 1 && mPlayerManager.TeamTwo.Count == 0)
            {
                LevelEnd(1, 1);
            }
            if (mPlayerManager.TeamTwo.Count == 1 && mPlayerManager.TeamOne.Count == 0)
            {
                LevelEnd(2, 1);
            }
        }
    }

    private void LevelEnd(int team, int score)
    {
        isMatchOver = true;
        mScoreManager.AddPoints(team, score);
        MatchUI.mMatchCanvas.gameObject.SetActive(true);
        MatchUI.displayTeamScore();
        mPlayerManager.TeamOne.Clear();
        mPlayerManager.TeamTwo.Clear();

    }
}
