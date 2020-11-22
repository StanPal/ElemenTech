using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerManager playerManager;
    public List<Transform> mSpawnPoints = new List<Transform>();
    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        playerManager = ServiceLocator.Get<PlayerManager>();
        if (playerManager.FireHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            GameObject fireHero = Instantiate(playerManager.FireHero);
            fireHero.SetActive(true);
            playerManager.mPlayersList[0] = fireHero;
            RandomizeSpawn(fireHero);

        }
        if (playerManager.WaterHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            GameObject waterHero = Instantiate(playerManager.WaterHero);
            waterHero.SetActive(true);
            playerManager.mPlayersList[1] = waterHero;
            RandomizeSpawn(waterHero);

        }
        if (playerManager.EarthHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            playerManager.EarthHero.SetActive(true);
            playerManager.EarthHero = Instantiate(playerManager.EarthHero);
            RandomizeSpawn(playerManager.EarthHero);
            playerManager.mPlayersList[3] = playerManager.EarthHero;

        }
        if (playerManager.AirHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            GameObject airHero = Instantiate(playerManager.AirHero);
            airHero.SetActive(true);
            playerManager.mPlayersList[2] = airHero;
            RandomizeSpawn(airHero);

        }
    }

    void RandomizeSpawn(GameObject player)
    {
        int randIndex = Random.Range(0, mSpawnPoints.Count);
        player.transform.position = mSpawnPoints[randIndex].position;
        mSpawnPoints.RemoveAt(randIndex);
    }

}
