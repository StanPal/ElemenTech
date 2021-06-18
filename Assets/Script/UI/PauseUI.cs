using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public event System.Action OnResetTeams;
    private PlayerManager _PlayerManager;
    private ScoreManager _ScoreManager;
    private SoundManager _SoundManager;
    public Canvas mCanvas;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _PlayerManager = ServiceLocator.Get<PlayerManager>();
        _ScoreManager = ServiceLocator.Get<ScoreManager>();
        _SoundManager = ServiceLocator.Get<SoundManager>();
    }    

    private void Start()
    {
        if (_PlayerManager.mPlayersList[0].gameObject != null)
        {
            _PlayerManager.mPlayersList[0].GetComponent<HeroActions>().onPausePeformed += PauseGame;
        }
        if (_PlayerManager.mPlayersList[1].gameObject != null)
        {
            _PlayerManager.mPlayersList[1].GetComponent<HeroActions>().onPausePeformed += PauseGame;
        }
        if (_PlayerManager.mPlayersList[2].gameObject != null)
        {
            _PlayerManager.mPlayersList[2].GetComponent<HeroActions>().onPausePeformed += PauseGame;
        }
        if (_PlayerManager.mPlayersList[3].gameObject != null)
        {
            _PlayerManager.mPlayersList[3].GetComponent<HeroActions>().onPausePeformed += PauseGame;
        }


    }


    public void PauseGame()
    {
        Cursor.visible = true;
        mCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        mCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void BackToMainMenu()
    {

        StartCoroutine(backtoMain());
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _ScoreManager.IsMatchOver = false;
        Time.timeScale = 1f;
    }

    private IEnumerator backtoMain()
    {
        ResetPlayers();
        _ScoreManager.ResetScore();
        _PlayerManager.TeamOne.Clear();
        _PlayerManager.TeamTwo.Clear();
        
        yield return new WaitForSeconds(0.1f);
        _SoundManager.MainMenuMusic();
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    private void ResetPlayers()
    {
        _ScoreManager.TriggerReset = true;
        OnResetTeams?.Invoke();
        Cursor.visible = true;
    }
}
