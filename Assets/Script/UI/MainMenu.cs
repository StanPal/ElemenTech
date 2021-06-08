using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{    
    private ScoreManager scoreManager;
    private SoundManager _soundManager; 

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        scoreManager = ServiceLocator.Get<ScoreManager>();
        _soundManager = ServiceLocator.Get<SoundManager>();
    }

    private void Start()
    {
      
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("GameEnd"))
        {
            Cursor.visible = true;
        }
    }

    public void StartGame()
    {
       // _soundManager.PlayMusic(0).Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayWorkingLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayControlLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void PlayEngineLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void PracticeMode()
    {
        if (!scoreManager.PracticeMode)
            scoreManager.PracticeMode = true;
        else
            scoreManager.PracticeMode = false;
    }
    
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Maps()
    {
        SceneManager.LoadScene("Maps");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");

        Application.Quit();
    }

    public void BackToMain()
    {
        _soundManager.MainMenuMusic();
        SceneManager.LoadScene(0);
    }

    public void Menue()
    {
        SceneManager.LoadScene("GameBegin");
    }
}
