using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Prefabs for Hero's
    public GameObject FireHero;
    public GameObject WaterHero;
    public GameObject AirHero;
    public GameObject EarthHero;

    public List<GameObject> _playersList = new List<GameObject>();
    public List<GameObject> _teamOne = new List<GameObject>();
    public List<GameObject> _teamTwo = new List<GameObject>();

    [SerializeField] private bool _PlayTestMode = false;

    private void Awake()
    {
        ServiceLocator.Register<PlayerManager>(this);
        FireHero.GetComponent<HeroMovement>().ControllerInput = HeroMovement.Controller.None;
        WaterHero.GetComponentInChildren<HeroMovement>().ControllerInput = HeroMovement.Controller.None;
        AirHero.GetComponent<HeroMovement>().ControllerInput = HeroMovement.Controller.None;
        EarthHero.GetComponent<HeroMovement>().ControllerInput = HeroMovement.Controller.None;

        _playersList[0] = FireHero;
        _playersList[1] = WaterHero;
        _playersList[2] = AirHero;
        _playersList[3] = EarthHero;

        _teamOne.Capacity = 0;
        _teamTwo.Capacity = 0;
    }
}
