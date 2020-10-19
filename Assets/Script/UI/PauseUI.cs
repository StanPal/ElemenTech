using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    private HeroActions mHero;
    public Canvas mCanvas;

    private void Awake()
    {
        mHero = FindObjectOfType<HeroActions>().GetComponent<HeroActions>();
        
        mHero.onPausePeformed += PauseGame;
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
}
