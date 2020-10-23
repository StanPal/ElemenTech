using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Prefabs for Heros
    public GameObject FireHero;
    public GameObject WaterHero;
    public GameObject AirHero;
    public GameObject EarthHero;    

    public List<HeroActions> mPlayersList = new List<HeroActions>();

    private void Awake()
    {
        mPlayersList.Add(FireHero.GetComponent<HeroActions>());
        mPlayersList.Add(WaterHero.GetComponent<HeroActions>());
        mPlayersList.Add(AirHero.GetComponent<HeroActions>());
        mPlayersList.Add(EarthHero.GetComponent<HeroActions>());

    }
}
