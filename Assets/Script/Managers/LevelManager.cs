using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelConfigData levelConfigData = null;
    [SerializeField] private PlayerSkills playerSkills = null;

    private ScoreManager scoreManager;
    private PlayerManager playerManager;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        //UnityEngine.Physics.gravity = levelConfigData.Gravity;
        //playercontroller.minjumpdist = levelConfigData.MinJumpDist;

        scoreManager = ServiceLocator.Get<ScoreManager>();
        playerManager = ServiceLocator.Get<PlayerManager>();
    }

    private void Update()
    {
        if(playerManager.mPlayersList.Count == 1)
        {
            // Get whatever team that player is on.
            // Give them points.

            // for example add 100 points to team 1
            scoreManager.AddPoints(1, 100);
        }
    }
}
