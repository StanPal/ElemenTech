using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerManager playerManager;
    public List<Transform> mSpawnPoints = new List<Transform>();
    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        if (playerManager.FireHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            playerManager.FireHero.SetActive(true);
            playerManager.FireHero = Instantiate(playerManager.FireHero);
            RandomizeSpawn(playerManager.FireHero); 
            playerManager.mPlayersList[0] = playerManager.FireHero;
            
        }
        if (playerManager.WaterHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            playerManager.WaterHero.SetActive(true);
            playerManager.WaterHero = Instantiate(playerManager.WaterHero);
            RandomizeSpawn(playerManager.WaterHero);
            playerManager.mPlayersList[1] = playerManager.WaterHero;

        }
        if (playerManager.EarthHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            playerManager.EarthHero.SetActive(true);
            playerManager.EarthHero = Instantiate(playerManager.EarthHero);
            RandomizeSpawn(playerManager.EarthHero);
            playerManager.mPlayersList[2] = playerManager.EarthHero;

        }
        if (playerManager.AirHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            playerManager.AirHero.SetActive(true);
            playerManager.AirHero = Instantiate(playerManager.AirHero);
            RandomizeSpawn(playerManager.AirHero);
            playerManager.mPlayersList[3] = playerManager.AirHero;

        }
    }

    void RandomizeSpawn(GameObject player)
    {
        int randIndex = Random.Range(0, mSpawnPoints.Count);
        player.transform.position = mSpawnPoints[randIndex].position;
        mSpawnPoints.RemoveAt(randIndex);
    }
}
