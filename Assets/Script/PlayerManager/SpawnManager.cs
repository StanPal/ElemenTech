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
        PlayerManager._teamOne.Clear();
        PlayerManager._teamTwo.Clear();

        if (PlayerManager.FireHero.GetComponent<HeroMovement>().ControllerInput != HeroMovement.Controller.None)
        {
            GameObject fireHero = Instantiate(PlayerManager.FireHero);
            fireHero.SetActive(true);
            PlayerManager._playersList[0] = fireHero;
            if (PlayerManager._playersList[0].tag == "Team1")
            {
                PlayerManager._teamOne.Add(PlayerManager._playersList[0]);
            }
            if (PlayerManager._playersList[0].tag == "Team2")
            {
                PlayerManager._teamTwo.Add(PlayerManager._playersList[0]);
            }
            RandomizeSpawn(fireHero);

        }
        if (PlayerManager.WaterHero.GetComponent<HeroMovement>().ControllerInput != HeroMovement.Controller.None)
        {
            GameObject waterHero = Instantiate(PlayerManager.WaterHero);
            waterHero.SetActive(true);
            PlayerManager._playersList[1] = waterHero;
            if (PlayerManager._playersList[1].tag == "Team1")
            {
                PlayerManager._teamOne.Add(PlayerManager._playersList[1]);
            }
            if (PlayerManager._playersList[1].tag == "Team2")
            {
                PlayerManager._teamTwo.Add(PlayerManager._playersList[1]);
            }
            RandomizeSpawn(waterHero);

        }
        if (PlayerManager.EarthHero.GetComponent<HeroMovement>().ControllerInput != HeroMovement.Controller.None)
        {
            GameObject earthHero = Instantiate(PlayerManager.EarthHero);
            earthHero.SetActive(true);
            PlayerManager._playersList[3] = earthHero;
            if (PlayerManager._playersList[3].tag == "Team1")
            {
                PlayerManager._teamOne.Add(PlayerManager._playersList[3]);
            }
            if (PlayerManager._playersList[3].tag == "Team2")
            {
                PlayerManager._teamTwo.Add(PlayerManager._playersList[3]);
            }
            RandomizeSpawn(earthHero);

        }
        if (PlayerManager.AirHero.GetComponent<HeroMovement>().ControllerInput != HeroMovement.Controller.None)
        {
            GameObject airHero = Instantiate(PlayerManager.AirHero);
            airHero.SetActive(true);
            PlayerManager._playersList[2] = airHero;
            if (PlayerManager._playersList[2].tag == "Team1")
            {
                PlayerManager._teamOne.Add(PlayerManager._playersList[2]);
            }
            if (PlayerManager._playersList[2].tag == "Team2")
            {
                PlayerManager._teamTwo.Add(PlayerManager._playersList[2]);
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
