using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamSelect : MonoBehaviour
{
    public List<Button> TeamButtonList = new List<Button>();
    private List<int> _controllerList = new List<int>();
    private PlayerManager _playerManager;
    private static int ControllerSelect1 = 0;
    private static int ControllerSelect2 = 0;
    private static int ControllerSelect3 = 0;
    private static int ControllerSelect4 = 0;
    [SerializeField] private float totaltags = 4;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _playerManager = ServiceLocator.Get<PlayerManager>();
        TeamButtonList[0].GetComponentInChildren<Text>().text = _playerManager.FireHero.tag.ToString();
        TeamButtonList[1].GetComponentInChildren<Text>().text = _playerManager.EarthHero.tag.ToString();
        TeamButtonList[2].GetComponentInChildren<Text>().text = _playerManager.WaterHero.tag.ToString();
        TeamButtonList[3].GetComponentInChildren<Text>().text = _playerManager.AirHero.tag.ToString();
    }    

    private void FixedUpdate()
    {
        switch (ControllerSelect1)
        {
            case 0:
                TeamButtonList[0].GetComponentInChildren<Text>().text = "Team None";
                break;
            case 1:
                TeamButtonList[0].GetComponentInChildren<Text>().text = "Team 1";
                _playerManager.FireHero.tag = "Team1";
                break;
            case 2:
                TeamButtonList[0].GetComponentInChildren<Text>().text = "Team 2";
                _playerManager.FireHero.tag = "Team2";
                break;
            case 3:
                TeamButtonList[0].GetComponentInChildren<Text>().text = "Team3";
                _playerManager.FireHero.tag = "Team3";
                break;
            case 4:
                TeamButtonList[0].GetComponentInChildren<Text>().text = "Team4";
                _playerManager.FireHero.tag = "Team4";
                break;
            default:
                break;
        }

        switch (ControllerSelect2)
        {
            case 0:
                TeamButtonList[1].GetComponentInChildren<Text>().text = "Team None";
                break;
            case 1:
                TeamButtonList[1].GetComponentInChildren<Text>().text = "Team 1";
                _playerManager.EarthHero.tag = "Team1";
                break;
            case 2:
                TeamButtonList[1].GetComponentInChildren<Text>().text = "Team 2";
                _playerManager.EarthHero.tag = "Team2";
                break;
            case 3:
                TeamButtonList[1].GetComponentInChildren<Text>().text = "Team3";
                _playerManager.EarthHero.tag = "Team3";
                break;
            case 4:
                TeamButtonList[1].GetComponentInChildren<Text>().text = "Team4";
                _playerManager.EarthHero.tag = "Team4";
                break;
            default:
                break;
        }

        switch (ControllerSelect3)
        {
            case 0:
                TeamButtonList[2].GetComponentInChildren<Text>().text = "Team None";
                break;
            case 1:
                TeamButtonList[2].GetComponentInChildren<Text>().text = "Team 1";
                _playerManager.WaterHero.tag = "Team1";
                break;
            case 2:
                TeamButtonList[2].GetComponentInChildren<Text>().text = "Team 2";
                _playerManager.WaterHero.tag = "Team2";
                break;
            case 3:
                TeamButtonList[2].GetComponentInChildren<Text>().text = "Team3";
                _playerManager.WaterHero.tag = "Team4";
                break;
            case 4:
                TeamButtonList[2].GetComponentInChildren<Text>().text = "Team4";
                _playerManager.WaterHero.tag = "Team4";
                break;
            default:
                break;
        }

        switch (ControllerSelect4)
        {
            case 0:
                TeamButtonList[3].GetComponentInChildren<Text>().text = "Team None";
                break;
            case 1:
                TeamButtonList[3].GetComponentInChildren<Text>().text = "Team 1";
                _playerManager.AirHero.tag = "Team1";
                break;
            case 2:
                TeamButtonList[3].GetComponentInChildren<Text>().text = "Team 2";
                _playerManager.AirHero.tag = "Team2";
                break;
            case 3:
                TeamButtonList[3].GetComponentInChildren<Text>().text = "Team3";
                _playerManager.AirHero.tag = "Team3";
                break;
            case 4:
                TeamButtonList[3].GetComponentInChildren<Text>().text = "Team4";
                _playerManager.AirHero.tag = "Team4";
                break;
            default:
                break;
        }
    }

    public void SelectController1()
    {
        ControllerSelect1++;
        if (ControllerSelect1 > totaltags)
            ControllerSelect1 = 0;
    }

    public void SelectController2()
    {
        ControllerSelect2++;
        if (ControllerSelect2 > totaltags)
            ControllerSelect2 = 0;
    }

    public void SelectController3()
    {
        ControllerSelect3++;
        if (ControllerSelect3 > totaltags)
            ControllerSelect3 = 0;
    }

    public void SelectController4()
    {
        ControllerSelect4++;
        if (ControllerSelect4 > totaltags)
            ControllerSelect4 = 0;
    }
}
