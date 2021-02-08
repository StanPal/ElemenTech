using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private PlayerManager mPlayerManager;
    [SerializeField] private MatchUI _matchUi;
    private bool isMatchOver = false;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        mPlayerManager = ServiceLocator.Get<PlayerManager>();
        _scoreManager = ServiceLocator.Get<ScoreManager>();
        _matchUi = FindObjectOfType<MatchUI>();
    }

    private void Update()
    {
        if (!_scoreManager.PracticeMode)
        {
            if (!isMatchOver)
            {
                if (mPlayerManager.TeamOne.Count == 1 && mPlayerManager.TeamTwo.Count == 0)
                {
                    LevelEnd(1, 1);
                }
                else if (mPlayerManager.TeamOne.Count == 2 && mPlayerManager.TeamTwo.Count == 0)
                {
                    LevelEnd(1, 1);
                }
                else if (mPlayerManager.TeamTwo.Count == 1 && mPlayerManager.TeamOne.Count == 0)
                {
                    LevelEnd(2, 1);
                }
                else if (mPlayerManager.TeamTwo.Count == 2 && mPlayerManager.TeamOne.Count == 0)
                {
                    LevelEnd(2, 1);
                }
                else if (mPlayerManager.TeamTwo.Count == 0 && mPlayerManager.TeamOne.Count == 0)
                {
                    LevelEnd(1, 0);
                }
            }
        }
    }

    private void LevelEnd(int team, int score)
    {
        isMatchOver = true;
        _scoreManager.AddPoints(team, score);
        _matchUi.MatchCanvas.gameObject.SetActive(true);
        _matchUi.displayTeamScore();
        mPlayerManager.TeamOne.Clear();
        mPlayerManager.TeamTwo.Clear();
    }
}
