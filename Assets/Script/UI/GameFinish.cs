using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinish : MonoBehaviour
{
    private ScoreManager _scoreManager;
    private PlayerManager _playerManager;
    public TMP_Text matchOutcome;
    private SoundManager _soundManager;
    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _scoreManager = ServiceLocator.Get<ScoreManager>();
        _playerManager = ServiceLocator.Get<PlayerManager>();
        _soundManager = ServiceLocator.Get<SoundManager>();
        DisplayScore();
    }

    private void Start()
    {
        Cursor.visible = true;
    }

    private void DisplayScore()
    {
        if (_scoreManager.TeamOneScore > _scoreManager.TeamTwoScore &&
            _scoreManager.TeamOneScore > _scoreManager.TeamThreeScore &&
            _scoreManager.TeamOneScore > _scoreManager.TeamFourScore)
        {
            matchOutcome.text = "Team One Wins! Score: " + _scoreManager.TeamOneScore;
        }
        else if (_scoreManager.TeamTwoScore > _scoreManager.TeamOneScore &&
            _scoreManager.TeamTwoScore > _scoreManager.TeamThreeScore &&
            _scoreManager.TeamTwoScore > _scoreManager.TeamFourScore)
        {
            matchOutcome.text = "Team Two Wins! Score: " + _scoreManager.TeamTwoScore;
        }
       else if (_scoreManager.TeamThreeScore > _scoreManager.TeamOneScore &&
            _scoreManager.TeamThreeScore > _scoreManager.TeamTwoScore &&
            _scoreManager.TeamThreeScore > _scoreManager.TeamFourScore)
        {
            matchOutcome.text = "Team Three Wins! Score: " + _scoreManager.TeamThreeScore;
        }
        else if (_scoreManager.TeamFourScore > _scoreManager.TeamOneScore &&
            _scoreManager.TeamFourScore > _scoreManager.TeamTwoScore &&
            _scoreManager.TeamFourScore > _scoreManager.TeamThreeScore)
        {
            matchOutcome.text = "Team Four Wins! Score: " + _scoreManager.TeamFourScore;
        }
       else if (_scoreManager.TeamOneScore == _scoreManager.TeamTwoScore)
        {
            matchOutcome.text = "Tie! Score: " + _scoreManager.TeamOneScore + "-" + _scoreManager.TeamTwoScore;
        }
        else if (_scoreManager.TeamOneScore == _scoreManager.TeamThreeScore)
        {
            matchOutcome.text = "Tie! Score: " + _scoreManager.TeamOneScore + "-" + _scoreManager.TeamThreeScore;
        }
        else if (_scoreManager.TeamOneScore == _scoreManager.TeamFourScore)
        {
            matchOutcome.text = "Tie! Score: " + _scoreManager.TeamOneScore + "-" + _scoreManager.TeamFourScore;
        }
        else if(_scoreManager.TeamTwoScore == _scoreManager.TeamThreeScore)
        {
            matchOutcome.text = "Tie! Score: " + _scoreManager.TeamTwoScore + "-" + _scoreManager.TeamThreeScore;
        }
        else if (_scoreManager.TeamTwoScore == _scoreManager.TeamFourScore)
        {
            matchOutcome.text = "Tie! Score: " + _scoreManager.TeamTwoScore + "-" + _scoreManager.TeamFourScore;
        }
        else if (_scoreManager.TeamThreeScore == _scoreManager.TeamFourScore)
        {
            matchOutcome.text = "Tie! Score: " + _scoreManager.TeamThreeScore + "-" + _scoreManager.TeamFourScore;
        }
        
    }

    public void BackToMainMenu()
    {
        _soundManager.MainMenuMusic();
        _scoreManager.IsMatchOver = false;
        _scoreManager.ResetScore();
        ResetPlayers();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    private void ResetPlayers()
    {
        _playerManager.mPlayersList[0] = _playerManager.FireHero;
        _playerManager.mPlayersList[1] = _playerManager.WaterHero;
        _playerManager.mPlayersList[2] = _playerManager.AirHero;
        _playerManager.mPlayersList[3] = _playerManager.EarthHero;

        _playerManager.TeamOne.Clear();
        _playerManager.TeamTwo.Clear();
    }
}
