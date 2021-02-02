using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerManager PlayerManager;
    public List<Transform> _SpawnPoints = new List<Transform>();

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        PlayerManager = ServiceLocator.Get<PlayerManager>();
        PlayerManager.TeamOne.Clear();
        PlayerManager.TeamTwo.Clear();

        if (PlayerManager.FireHero.GetComponent<HeroMovement>().ControllerInput != HeroMovement.Controller.None)
        {
            GameObject fireHero = Instantiate(PlayerManager.FireHero);
            fireHero.SetActive(true);
            PlayerManager.PlayersList[0] = fireHero;
            if (PlayerManager.PlayersList[0].tag == "Team1")
            {
                PlayerManager.TeamOne.Add(PlayerManager.PlayersList[0]);
            }
            if (PlayerManager.PlayersList[0].tag == "Team2")
            {
                PlayerManager.TeamTwo.Add(PlayerManager.PlayersList[0]);
            }
            RandomizeSpawn(fireHero);

        }
        if (PlayerManager.WaterHero.GetComponent<HeroMovement>().ControllerInput != HeroMovement.Controller.None)
        {
            GameObject waterHero = Instantiate(PlayerManager.WaterHero);
            waterHero.SetActive(true);
            PlayerManager.PlayersList[1] = waterHero;
            if (PlayerManager.PlayersList[1].tag == "Team1")
            {
                PlayerManager.TeamOne.Add(PlayerManager.PlayersList[1]);
            }
            if (PlayerManager.PlayersList[1].tag == "Team2")
            {
                PlayerManager.TeamTwo.Add(PlayerManager.PlayersList[1]);
            }
            RandomizeSpawn(waterHero);

        }
        if (PlayerManager.EarthHero.GetComponent<HeroMovement>().ControllerInput != HeroMovement.Controller.None)
        {
            GameObject earthHero = Instantiate(PlayerManager.EarthHero);
            earthHero.SetActive(true);
            PlayerManager.PlayersList[3] = earthHero;
            if (PlayerManager.PlayersList[3].tag == "Team1")
            {
                PlayerManager.TeamOne.Add(PlayerManager.PlayersList[3]);
            }
            if (PlayerManager.PlayersList[3].tag == "Team2")
            {
                PlayerManager.TeamTwo.Add(PlayerManager.PlayersList[3]);
            }
            RandomizeSpawn(earthHero);

        }
        if (PlayerManager.AirHero.GetComponent<HeroMovement>().ControllerInput != HeroMovement.Controller.None)
        {
            GameObject airHero = Instantiate(PlayerManager.AirHero);
            airHero.SetActive(true);
            PlayerManager.PlayersList[2] = airHero;
            if (PlayerManager.PlayersList[2].tag == "Team1")
            {
                PlayerManager.TeamOne.Add(PlayerManager.PlayersList[2]);
            }
            if (PlayerManager.PlayersList[2].tag == "Team2")
            {
                PlayerManager.TeamTwo.Add(PlayerManager.PlayersList[2]);
            }
            RandomizeSpawn(airHero);
        }
    }

    private void RandomizeSpawn(GameObject player)
    {
        int randIndex = Random.Range(0, _SpawnPoints.Count);
        player.transform.position = _SpawnPoints[randIndex].position;
        _SpawnPoints.RemoveAt(randIndex);
    }
}
