using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnManager : MonoBehaviour
{
    private PlayerManager _playerManager;
    private ScoreManager _scoreManager;
    [SerializeField] private GameObject fireLogo;
    [SerializeField] private GameObject WaterLogo;
    [SerializeField] private GameObject AirLogo;
    [SerializeField] private GameObject EarthLogo;
    [SerializeField] private List<Transform> _startSpawnPoints = new List<Transform>();
    [SerializeField] private List<Transform> _respawnPoints = new List<Transform>();
    private GameObject logo;
    private PlayerInputManager _playerInputManager;
    private bool _isSpawning;

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

    private void Update()
    {
        if(_isSpawning)
        {
            ////calculate what the new Y position will be
            //float newY = Mathf.Sin(Time.time * 5f);
            ////set the object's Y to the new calculated Y
            //logo.transform.position = new Vector3(logo.transform.position.x, newY, logo.transform.position.z) * 0.5f;
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
        StartCoroutine(Spawn(randIndex, player));
    }
    private IEnumerator Spawn(int index, GameObject player)
    {
        player.SetActive(false);
        player.transform.position = _respawnPoints[index].position;
        switch (player.GetComponent<HeroStats>().GetElement)
        {
            case Elements.ElementalAttribute.Fire:
                logo = Instantiate(fireLogo, _respawnPoints[index].position, Quaternion.identity);
                break;
            case Elements.ElementalAttribute.Earth:
                logo = Instantiate(EarthLogo);
                logo.transform.position = _respawnPoints[index].position;
                break;
            case Elements.ElementalAttribute.Water:
                logo = Instantiate(WaterLogo);
                logo.transform.position = _respawnPoints[index].position;
                break;
            case Elements.ElementalAttribute.Air:
                logo = Instantiate(AirLogo);
                logo.transform.position = _respawnPoints[index].position;
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(2f);
        //_isSpawning = false;
        Destroy(logo);
        player.SetActive(true);
    }


    //private IEnumerator Flash(GameObject logo)
    //{
    //    logo.SetActive(true);
    //    for (int i = 0; i < 2; i++)
    //    {
    //        SetSpriteColor(logo, Color.white);
    //        yield return new WaitForSeconds(0.1f);
    //        SetSpriteColor(logo,_originalSpriteColor);
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //    logo.SetActive(false);
    //}

    //private void SetSpriteColor(GameObject logo, Color spriteColor)
    //{
    //    logo.GetComponent<SpriteRenderer>().color = spriteColor;
    //}

}
