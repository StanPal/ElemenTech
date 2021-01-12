using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    private PlayerManager PlayerManager;
    private ScoreManager scoreManager;
    public Canvas mCanvas;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        PlayerManager = ServiceLocator.Get<PlayerManager>();
        scoreManager = ServiceLocator.Get<ScoreManager>();
    }

    private void Start()
    {
        if (PlayerManager.PlayersList[0].gameObject != null)
        {
            PlayerManager.PlayersList[0].GetComponent<HeroActions>().onPausePeformed += PauseGame;
        }
        if (PlayerManager.PlayersList[1].gameObject != null)
        {
            PlayerManager.PlayersList[1].GetComponent<HeroActions>().onPausePeformed += PauseGame;
        }
        if (PlayerManager.PlayersList[2].gameObject != null)
        {
            PlayerManager.PlayersList[2].GetComponent<HeroActions>().onPausePeformed += PauseGame;
        }
        if (PlayerManager.PlayersList[3].gameObject != null)
        {
            PlayerManager.PlayersList[3].GetComponent<HeroActions>().onPausePeformed += PauseGame;
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
        PlayerManager.FireHero.GetComponent<HeroMovement>().ControllerInput = HeroMovement.Controller.None;
        PlayerManager.FireHero.SetActive(true);
        PlayerManager.WaterHero.GetComponent<HeroMovement>().ControllerInput = HeroMovement.Controller.None;
        PlayerManager.WaterHero.SetActive(false);

        PlayerManager.AirHero.GetComponent<HeroMovement>().ControllerInput = HeroMovement.Controller.None;
        PlayerManager.AirHero.SetActive(false);
        PlayerManager.EarthHero.GetComponent<HeroMovement>().ControllerInput = HeroMovement.Controller.None;
        PlayerManager.EarthHero.SetActive(false);


        PlayerManager.PlayersList[0] = PlayerManager.FireHero;
        PlayerManager.PlayersList[1] = PlayerManager.WaterHero;
        PlayerManager.PlayersList[2] = PlayerManager.AirHero;
        PlayerManager.PlayersList[3] = PlayerManager.EarthHero;

        PlayerManager.TeamOne.Clear();
        PlayerManager.TeamTwo.Clear();

        scoreManager.PracticeMode = false;
        Cursor.visible = true;
    }
}
