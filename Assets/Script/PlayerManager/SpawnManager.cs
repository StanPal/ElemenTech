using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    private PlayerManager _playerManager;
    private ScoreManager _scoreManager;
    [SerializeField] private List<Transform> _startSpawnPoints = new List<Transform>();
    [SerializeField] private List<Transform> _respawnPoints = new List<Transform>();

    private PlayerInputManager _playerInputManager;
    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
        _respawnPoints = _startSpawnPoints;
    }

    private void Initialize()
    {
        _playerManager = ServiceLocator.Get<PlayerManager>();
        _scoreManager = ServiceLocator.Get<ScoreManager>();
        _playerManager.TeamOne.Clear();
        _playerManager.TeamTwo.Clear();
        _playerManager.TeamThree.Clear();
        _playerManager.TeamFour.Clear();

        if (_playerManager.FireHero.GetComponent<HeroMovement>().ControllerInput != HeroMovement.Controller.None)
        {
            GameObject fireHero = Instantiate(_playerManager.FireHero);    
            //fireHero.GetComponent<HeroMovement>().PlayerInput = fireHero.GetComponent<PlayerInput>().KeyboardMouseScheme;
            fireHero.SetActive(true);
            _playerManager.mPlayersList[0] = fireHero;
            //_playerInputManager.playerPrefab = _playerManager.mPlayersList[0];
            if (_playerManager.mPlayersList[0].tag == "Team1")
            {
                _playerManager.TeamOne.Add(_playerManager.mPlayersList[0]);
                _scoreManager.TeamOneActive = true;
            }
            if (_playerManager.mPlayersList[0].tag == "Team2")
            {
                _playerManager.TeamTwo.Add(_playerManager.mPlayersList[0]);
                _scoreManager.TeamTwoActive = true;
            }
            if (_playerManager.mPlayersList[0].tag == "Team3")
            {
                _playerManager.TeamThree.Add(_playerManager.mPlayersList[0]);
                _scoreManager.TeamThreeActive = true;

            }
            if (_playerManager.mPlayersList[0].tag == "Team4")
            {
                _playerManager.TeamFour.Add(_playerManager.mPlayersList[0]);
                _scoreManager.TeamFourActive = true;
            }
            RandomizeSpawn(fireHero);

        }
        if (_playerManager.WaterHero.GetComponent<HeroMovement>().ControllerInput != HeroMovement.Controller.None)
        {
            GameObject waterHero = Instantiate(_playerManager.WaterHero);
            waterHero.SetActive(true);
            _playerManager.mPlayersList[1] = waterHero;
            if (_playerManager.mPlayersList[1].tag == "Team1")
            {
                _playerManager.TeamOne.Add(_playerManager.mPlayersList[1]);
                _scoreManager.TeamOneActive = true;
            }
            if (_playerManager.mPlayersList[1].tag == "Team2")
            {
                _playerManager.TeamTwo.Add(_playerManager.mPlayersList[1]);
                _scoreManager.TeamTwoActive = true;
            }
            if (_playerManager.mPlayersList[1].tag == "Team3")
            {
                _playerManager.TeamThree.Add(_playerManager.mPlayersList[1]);
                _scoreManager.TeamThreeActive = true;

            }
            if (_playerManager.mPlayersList[1].tag == "Team4")
            {
                _playerManager.TeamFour.Add(_playerManager.mPlayersList[1]);
                _scoreManager.TeamFourActive = true;

            }
            RandomizeSpawn(waterHero);

        }
        if (_playerManager.EarthHero.GetComponent<HeroMovement>().ControllerInput != HeroMovement.Controller.None)
        {
            GameObject earthHero = Instantiate(_playerManager.EarthHero);
            earthHero.SetActive(true);
            _playerManager.mPlayersList[3] = earthHero;
            if (_playerManager.mPlayersList[3].tag == "Team1")
            {
                _playerManager.TeamOne.Add(_playerManager.mPlayersList[3]);
                _scoreManager.TeamOneActive = true;
            }
            if (_playerManager.mPlayersList[3].tag == "Team2")
            {
                _playerManager.TeamTwo.Add(_playerManager.mPlayersList[3]);
                _scoreManager.TeamTwoActive = true;
            }
            if (_playerManager.mPlayersList[3].tag == "Team3")
            {
                _playerManager.TeamThree.Add(_playerManager.mPlayersList[3]);
                _scoreManager.TeamThreeActive = true;

            }
            if (_playerManager.mPlayersList[3].tag == "Team4")
            {
                _playerManager.TeamFour.Add(_playerManager.mPlayersList[3]);
                _scoreManager.TeamFourActive = true;

            }
            RandomizeSpawn(earthHero);

        }
        if (_playerManager.AirHero.GetComponent<HeroMovement>().ControllerInput != HeroMovement.Controller.None)
        {
            GameObject airHero = Instantiate(_playerManager.AirHero);
            airHero.SetActive(true);
            _playerManager.mPlayersList[2] = airHero;
            if (_playerManager.mPlayersList[2].tag == "Team1")
            {
                _playerManager.TeamOne.Add(_playerManager.mPlayersList[2]);
                _scoreManager.TeamOneActive = true;
            }
            if (_playerManager.mPlayersList[2].tag == "Team2")
            {
                _playerManager.TeamTwo.Add(_playerManager.mPlayersList[2]);
                _scoreManager.TeamTwoActive = true;
            }
            if (_playerManager.mPlayersList[2].tag == "Team3")
            {
                _playerManager.TeamThree.Add(_playerManager.mPlayersList[2]);
                _scoreManager.TeamThreeActive = true;
            }
            if (_playerManager.mPlayersList[2].tag == "Team4")
            {
                _playerManager.TeamFour.Add(_playerManager.mPlayersList[2]);
                _scoreManager.TeamFourActive = true;
            }
            RandomizeSpawn(airHero);
        }
    }

    private void RandomizeSpawn(GameObject player)
    {
        int randIndex = Random.Range(0, _startSpawnPoints.Count);
        player.transform.position = _startSpawnPoints[randIndex].position;
        _startSpawnPoints.RemoveAt(randIndex);
    }

    public void RespawnPlayer(GameObject player)
    {
        int randIndex = Random.Range(0, _respawnPoints.Count);
        player.transform.position = _respawnPoints[randIndex].position;
    }

}
