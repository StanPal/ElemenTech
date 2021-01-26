using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinish : MonoBehaviour
{
    private ScoreManager _ScoreManager;
    private PlayerManager _PlayerManager;
    public TMP_Text MatchOutcome;
    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _ScoreManager = ServiceLocator.Get<ScoreManager>();
        _PlayerManager = ServiceLocator.Get<PlayerManager>();
        DisplayScore();
    }

    private void DisplayScore()
    {
        if (_ScoreManager.TeamOneScore > _ScoreManager.TeamTwoScore)
        {
            MatchOutcome.text = "Team One Wins! Score: " + _ScoreManager.TeamOneScore + "-" + _ScoreManager.TeamTwoScore;
        }
        else if (_ScoreManager.TeamOneScore < _ScoreManager.TeamTwoScore)
        {
            MatchOutcome.text = "Team Two Wins! Score: " + _ScoreManager.TeamTwoScore + "-" + _ScoreManager.TeamOneScore;
        }
        else
        {
            MatchOutcome.text = "Tie! Score: " + _ScoreManager.TeamOneScore + "-" + _ScoreManager.TeamTwoScore;
        }
    }

    public void BackToMainMenu()
    {
        _ScoreManager.ResetScore();
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
        _PlayerManager.FireHero.GetComponent<HeroMovement>().ControllerInput = HeroMovement.Controller.None;
        _PlayerManager.FireHero.SetActive(false);
        _PlayerManager.WaterHero.GetComponent<HeroMovement>().ControllerInput = HeroMovement.Controller.None;
        _PlayerManager.WaterHero.SetActive(false);

        _PlayerManager.AirHero.GetComponent<HeroMovement>().ControllerInput = HeroMovement.Controller.None;
        _PlayerManager.AirHero.SetActive(false);
        _PlayerManager.EarthHero.GetComponent<HeroMovement>().ControllerInput = HeroMovement.Controller.None;
        _PlayerManager.EarthHero.SetActive(false);


        _PlayerManager.PlayersList[0] = _PlayerManager.FireHero;
        _PlayerManager.PlayersList[1] = _PlayerManager.WaterHero;
        _PlayerManager.PlayersList[2] = _PlayerManager.AirHero;
        _PlayerManager.PlayersList[3] = _PlayerManager.EarthHero;
    }
}
