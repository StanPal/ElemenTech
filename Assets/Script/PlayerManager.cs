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

    public List<GameObject> mPlayersList = new List<GameObject>();

    private void Awake()
    {
        FireHero.SetActive(false);
        WaterHero.SetActive(false);
        AirHero.SetActive(false);
        EarthHero.SetActive(false);
        mPlayersList.Add(FireHero);
        mPlayersList.Add(WaterHero);
        mPlayersList.Add(AirHero);
        mPlayersList.Add(EarthHero);
    }
}
