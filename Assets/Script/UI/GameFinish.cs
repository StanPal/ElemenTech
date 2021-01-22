using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameFinish : MonoBehaviour
{
    private ScoreManager ScoreManager;
    private PlayerManager playerManager;
    public TMP_Text matchOutcome;
    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        ScoreManager = ServiceLocator.Get<ScoreManager>();
        playerManager = ServiceLocator.Get<PlayerManager>();
        DisplayScore();
    }

    private void DisplayScore()
    {
        if (ScoreManager.TeamOneScore > ScoreManager.TeamTwoScore)
        {
            matchOutcome.text = "Team One Wins! Score: " + ScoreManager.TeamOneScore + "-" + ScoreManager.TeamTwoScore;
        }
        else if (ScoreManager.TeamOneScore < ScoreManager.TeamTwoScore)
        {
            matchOutcome.text = "Team Two Wins! Score: " + ScoreManager.TeamTwoScore + "-" + ScoreManager.TeamOneScore;
        }
        else
        {
            matchOutcome.text = "Tie! Score: " + ScoreManager.TeamOneScore + "-" + ScoreManager.TeamTwoScore;
        }
    }

    public void BackToMainMenu()
    {
        ScoreManager.ResetScore();
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
        playerManager.FireHero.GetComponent<HeroMovement>().ControllerInput = HeroMovement.Controller.None;
        playerManager.FireHero.SetActive(false);
        playerManager.WaterHero.GetComponent<HeroMovement>().ControllerInput = HeroMovement.Controller.None;
        playerManager.WaterHero.SetActive(false);

        playerManager.AirHero.GetComponent<HeroMovement>().ControllerInput = HeroMovement.Controller.None;
        playerManager.AirHero.SetActive(false);
        playerManager.EarthHero.GetComponent<HeroMovement>().ControllerInput = HeroMovement.Controller.None;
        playerManager.EarthHero.SetActive(false);


        playerManager.mPlayersList[0] = playerManager.FireHero;
        playerManager.mPlayersList[1] = playerManager.WaterHero;
        playerManager.mPlayersList[2] = playerManager.AirHero;
        playerManager.mPlayersList[3] = playerManager.EarthHero;
    }
}
