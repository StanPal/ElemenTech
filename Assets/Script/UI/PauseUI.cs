using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    private Hero mHero;
    public Canvas mCanvas;

    private void Start()
    {
        mHero = FindObjectOfType<Hero>().GetComponent<Hero>();
        mHero.onPausePeformed += PauseGame;
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
}
