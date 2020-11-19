﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Prefabs for Heros
    public GameObject FireHero;
    public GameObject WaterHero;
    public GameObject AirHero;
    public GameObject EarthHero;

    public List<GameObject> mPlayersList = new List<GameObject>();
    [SerializeField]
    private bool playTestMode = false;
    private void Awake()
    {
        ServiceLocator.Register<PlayerManager>(this);
        if (!playTestMode)
        {
            FireHero.GetComponent<HeroMovement>().controllerInput = HeroMovement.Controller.None;
            WaterHero.GetComponent<HeroMovement>().controllerInput = HeroMovement.Controller.None;
            AirHero.GetComponent<HeroMovement>().controllerInput = HeroMovement.Controller.None;
            EarthHero.GetComponent<HeroMovement>().controllerInput = HeroMovement.Controller.None;

            FireHero.SetActive(false);
            WaterHero.SetActive(false);
            AirHero.SetActive(false);
            EarthHero.SetActive(false);
        }

        mPlayersList[0] = FireHero;
        mPlayersList[1] = WaterHero;
        mPlayersList[2] = AirHero;
        mPlayersList[3] = EarthHero;
  
    }

}
