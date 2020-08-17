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

    // Prefabs for Golems
    public GameObject FireGolem;
    public GameObject WaterGolem;
    public GameObject EarthGolem;
    public GameObject AirGolem;

    // Static Lists for both Players and Golems, reason for static is
    // that this list should be the only copy so there is no need to instantiate it again
    static public List<GameObject> mPlayersList;
    static public List<GameObject> mGolemList;

    private void Start()
    {
        mPlayersList = new List<GameObject>();
        mGolemList = new List<GameObject>();

        mPlayersList.Add(FireHero);
        mPlayersList.Add(WaterHero);
        mPlayersList.Add(AirHero);
        mPlayersList.Add(EarthHero);

        mGolemList.Add(FireGolem);
        mGolemList.Add(WaterGolem);
        mGolemList.Add(EarthGolem);
        mGolemList.Add(AirGolem);
    }

    private void Update()
    {

    }

    //Run through the list check if there is a hero dead return their index in the list
    private int GetDeadHeroIndex()
    {
        for(int i = 0; i < mPlayersList.Count; i++)
        {

            //if(mPlayersList[i].GetComponents<Hero>().IsDead)
            //{
            //  mPlayersList[i].GetComponents<Hero>().ToggleActive();
            //  return i;
            //}
        }
        return 0;
    }

    //Grab the deadheroes index which will be the same as their monsters index in the monster list
    //Afterwards the monster will now become active while the hero will become inactive, allowing the switch in prefabs
    private void SwitchHeroToGolem()
    {
        //mGolemList[GetDeadHeroIndex()].GetComponents<Golem>().ToggleActive();
    }

}
