using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerManager playerManager;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        if (playerManager.FireHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            playerManager.FireHero.SetActive(true);
            playerManager.FireHero = Instantiate(playerManager.FireHero);
            playerManager.mPlayersList[0] = playerManager.FireHero;
            
        }
        if (playerManager.WaterHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            playerManager.WaterHero.SetActive(true);
            playerManager.WaterHero = Instantiate(playerManager.WaterHero);
            playerManager.mPlayersList[1] = playerManager.WaterHero;

        }
        if (playerManager.EarthHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            playerManager.EarthHero.SetActive(true);
            playerManager.EarthHero = Instantiate(playerManager.EarthHero);
            playerManager.mPlayersList[2] = playerManager.EarthHero;

        }
        if (playerManager.AirHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            playerManager.AirHero.SetActive(true);
            playerManager.AirHero = Instantiate(playerManager.AirHero);
            playerManager.mPlayersList[3] = playerManager.AirHero;

        }

    }
}
