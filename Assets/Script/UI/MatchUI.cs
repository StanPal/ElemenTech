using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchUI : MonoBehaviour
{
    public Canvas MatchCanvas;
    public Text TeamOneScore;
    public Text TeamTwoScore;
    public Text TeamThreeScore;
    public Text TeamFourScore;
    public Text Transition;
    private ScoreManager _scoreManager;
    private PlayerManager _playerManager;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _scoreManager = ServiceLocator.Get<ScoreManager>();
        _playerManager = ServiceLocator.Get<PlayerManager>();
    }

    public void displayTeamScore()
    {
        if (_scoreManager.TeamOneActive)
        {
            TeamOneScore.text = "Team 1: " + _scoreManager.TeamOneScore;
        }
        if (_scoreManager.TeamTwoActive)
        {
            TeamTwoScore.text = "Team 2: " + _scoreManager.TeamTwoScore;
        }
        if(_scoreManager.TeamThreeActive)
        {
            TeamThreeScore.text = "Team 3: " + _scoreManager.TeamThreeScore;

        }
        if (_scoreManager.TeamFourActive)
        {
            TeamFourScore.text = "Team 4: " + _scoreManager.TeamFourScore;
        }
        Time.timeScale = 0;

        if ((SceneManager.GetActiveScene().buildIndex + 1) == SceneManager.sceneCountInBuildSettings - 2 ||
           (_scoreManager.TeamOneScore == _scoreManager.BestOfValue || _scoreManager.TeamTwoScore == _scoreManager.BestOfValue ||
            _scoreManager.TeamThreeScore == _scoreManager.BestOfValue || _scoreManager.TeamFourScore == _scoreManager.BestOfValue ))
        {
            StartCoroutine(TransitionToGameEndScene());
        }
        else
        {
            StartCoroutine(TransitionToNextScene());
        }
    }

    private IEnumerator TransitionToGameEndScene()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(1);
        _scoreManager.IsMatchOver = false;
        SceneManager.LoadScene("GameEnd");
    }

    private IEnumerator TransitionToNextScene()
    {
        Time.timeScale = 1;
        Transition.text = "Next Match will begin in...";
        yield return new WaitForSeconds(2);
        Transition.text = "3";
        yield return new WaitForSeconds(1);
        Transition.text = "2";
        yield return new WaitForSeconds(1);
        Transition.text = "1";
        yield return new WaitForSeconds(1);
        _scoreManager.IsMatchOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
