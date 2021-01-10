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
        playerManager.TeamOne.Clear();
        playerManager.TeamTwo.Clear();

        if (playerManager.FireHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            GameObject fireHero = Instantiate(playerManager.FireHero);
            fireHero.SetActive(true);
            playerManager.mPlayersList[0] = fireHero;
            if (playerManager.mPlayersList[0].tag == "Team1")
            {
                playerManager.TeamOne.Add(playerManager.mPlayersList[0]);
            }
            if (playerManager.mPlayersList[0].tag == "Team2")
            {
                playerManager.TeamTwo.Add(playerManager.mPlayersList[0]);
            }
            RandomizeSpawn(fireHero);

        }
        if (playerManager.WaterHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            GameObject waterHero = Instantiate(playerManager.WaterHero);
            waterHero.SetActive(true);
            playerManager.mPlayersList[1] = waterHero;
            if (playerManager.mPlayersList[1].tag == "Team1")
            {
                playerManager.TeamOne.Add(playerManager.mPlayersList[1]);
            }
            if (playerManager.mPlayersList[1].tag == "Team2")
            {
                playerManager.TeamTwo.Add(playerManager.mPlayersList[1]);
            }
            RandomizeSpawn(waterHero);

        }
        if (playerManager.EarthHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            GameObject earthHero = Instantiate(playerManager.EarthHero);
            earthHero.SetActive(true);
            playerManager.mPlayersList[3] = earthHero;
            if (playerManager.mPlayersList[3].tag == "Team1")
            {
                playerManager.TeamOne.Add(playerManager.mPlayersList[3]);
            }
            if (playerManager.mPlayersList[3].tag == "Team2")
            {
                playerManager.TeamTwo.Add(playerManager.mPlayersList[3]);
            }
            RandomizeSpawn(earthHero);

        }
        if (playerManager.AirHero.GetComponent<HeroMovement>().controllerInput != HeroMovement.Controller.None)
        {
            GameObject airHero = Instantiate(playerManager.AirHero);
            airHero.SetActive(true);
            playerManager.mPlayersList[2] = airHero;
            if (playerManager.mPlayersList[2].tag == "Team1")
            {
                playerManager.TeamOne.Add(playerManager.mPlayersList[2]);
            }
            if (playerManager.mPlayersList[2].tag == "Team2")
            {
                playerManager.TeamTwo.Add(playerManager.mPlayersList[2]);
            }
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
