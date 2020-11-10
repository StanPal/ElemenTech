using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    private PlayerManager playerManager;
    public Canvas mCanvas;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.FireHero.GetComponent<HeroActions>().onPausePeformed += PauseGame;
        playerManager.AirHero.GetComponent<HeroActions>().onPausePeformed += PauseGame;
        playerManager.WaterHero.GetComponent<HeroActions>().onPausePeformed += PauseGame;
        playerManager.EarthHero.GetComponent<HeroActions>().onPausePeformed += PauseGame;
    }

    public void PauseGame()
    {
        Debug.Log("Reached");
        mCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        mCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
