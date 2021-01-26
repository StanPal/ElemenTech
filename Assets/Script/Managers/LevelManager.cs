using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ScoreManager _ScoreManager;
    [SerializeField] private PlayerManager _PlayerManager;
    [SerializeField] private MatchUI _MatchUI;
    private bool isMatchOver = false;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _PlayerManager = ServiceLocator.Get<PlayerManager>();
        _ScoreManager = ServiceLocator.Get<ScoreManager>();
        _MatchUI = FindObjectOfType<MatchUI>();
    }

    private void Update()
    {
        if (!_ScoreManager.PracticeMode)
        {
            if (!isMatchOver)
            {
                if (_PlayerManager.TeamOne.Count == 1 && _PlayerManager.TeamTwo.Count == 0)
                {
                    LevelEnd(1, 1);
                }
                else if (_PlayerManager.TeamOne.Count == 2 && _PlayerManager.TeamTwo.Count == 0)
                {
                    LevelEnd(1, 1);
                }
                else if (_PlayerManager.TeamTwo.Count == 1 && _PlayerManager.TeamOne.Count == 0)
                {
                    LevelEnd(2, 1);
                }
                else if (_PlayerManager.TeamTwo.Count == 2 && _PlayerManager.TeamOne.Count == 0)
                {
                    LevelEnd(2, 1);
                }
                else if (_PlayerManager.TeamTwo.Count == 0 && _PlayerManager.TeamOne.Count == 0)
                {
                    LevelEnd(1, 0);
                }
            }
        }
    }

    private void LevelEnd(int team, int score)
    {
        isMatchOver = true;
        mScoreManager.AddPoints(team, score);
        MatchUI.MatchCanvas.gameObject.SetActive(true);
        MatchUI.displayTeamScore();
        mPlayerManager.TeamOne.Clear();
        mPlayerManager.TeamTwo.Clear();

    }
}
