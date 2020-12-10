using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchUI : MonoBehaviour
{
    public Canvas mMatchCanvas;
    public Text mTeamOneScore;
    public Text mTeamTwoScore;
    public Text mTransition;

    private ScoreManager mScoreManager;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        mScoreManager = ServiceLocator.Get<ScoreManager>();
    }

    public void displayTeamScore()
    {
        mTeamOneScore.text = "Team 1: " + mScoreManager.TeamOneScore;
        mTeamTwoScore.text = "Team 2: " + mScoreManager.TeamTwoScore;
        Time.timeScale = 0;
        StartCoroutine(TransitionToNextScene());
    }

    IEnumerator TransitionToNextScene()
    {
        Time.timeScale = 1;
        mTransition.text = "Next Match will begin in...";
        yield return new WaitForSeconds(2);
        mTransition.text = "3";
        yield return new WaitForSeconds(1);
        mTransition.text = "2";
        yield return new WaitForSeconds(1);
        mTransition.text = "1";
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
