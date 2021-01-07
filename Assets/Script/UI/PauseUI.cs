using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    private PlayerManager playerManager;
    private ScoreManager scoreManager;
    public Canvas mCanvas;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        playerManager = ServiceLocator.Get<PlayerManager>();
        scoreManager = ServiceLocator.Get<ScoreManager>();
    }

    private void Start()
    {
        if (playerManager.mPlayersList[0].gameObject != null)
        {
            playerManager.mPlayersList[0].GetComponent<HeroActions>().onPausePeformed += PauseGame;
        }
        if (playerManager.mPlayersList[1].gameObject != null)
        {
            playerManager.mPlayersList[1].GetComponent<HeroActions>().onPausePeformed += PauseGame;
        }
        if (playerManager.mPlayersList[2].gameObject != null)
        {
            playerManager.mPlayersList[2].GetComponent<HeroActions>().onPausePeformed += PauseGame;
        }
        if (playerManager.mPlayersList[3].gameObject != null)
        {
            playerManager.mPlayersList[3].GetComponent<HeroActions>().onPausePeformed += PauseGame;
        }
    }
    

    public void PauseGame()
    {
        mCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        mCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMainMenu()
    {
        ResetPlayers();
        scoreManager.ResetScore();
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void ResetPlayers()
    {
        playerManager.FireHero.GetComponent<HeroMovement>().controllerInput = HeroMovement.Controller.None;
        playerManager.FireHero.SetActive(true);
        playerManager.WaterHero.GetComponent<HeroMovement>().controllerInput = HeroMovement.Controller.None;
        playerManager.WaterHero.SetActive(false);

        playerManager.AirHero.GetComponent<HeroMovement>().controllerInput = HeroMovement.Controller.None;
        playerManager.AirHero.SetActive(false);
        playerManager.EarthHero.GetComponent<HeroMovement>().controllerInput = HeroMovement.Controller.None;
        playerManager.EarthHero.SetActive(false);


        playerManager.mPlayersList[0] = playerManager.FireHero;
        playerManager.mPlayersList[1] = playerManager.WaterHero;
        playerManager.mPlayersList[2] = playerManager.AirHero;
        playerManager.mPlayersList[3] = playerManager.EarthHero;

        playerManager.TeamOne.Clear();
        playerManager.TeamTwo.Clear();

        scoreManager.PracticeMode = false;
        Cursor.visible = true;
    }
}
