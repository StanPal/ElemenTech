using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerManager playerManager;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        if(playerManager.FireHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        playerManager.FireHero = Instantiate(playerManager.FireHero);
        if (playerManager.WaterHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
          playerManager.WaterHero = Instantiate(playerManager.WaterHero);
        if (playerManager.EarthHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
            playerManager.EarthHero = Instantiate(playerManager.EarthHero);
        if (playerManager.AirHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
            playerManager.AirHero = Instantiate(playerManager.AirHero);

    }
}
