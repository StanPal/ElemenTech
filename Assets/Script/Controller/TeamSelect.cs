using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamSelect : MonoBehaviour
{
    public List<Button> TeamButtonList = new List<Button>();
    private List<int> _ControllerList = new List<int>();
    private PlayerManager _PlayerManager;
    private static int _ControllerSelect1 = 0;
    private static int _ControllerSelect2 = 0;
    private static int _ControllerSelect3 = 0;
    private static int _ControllerSelect4 = 0;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _PlayerManager = ServiceLocator.Get<PlayerManager>();
        TeamButtonList[0].GetComponentInChildren<Text>().text = _PlayerManager.FireHero.tag.ToString();
        TeamButtonList[1].GetComponentInChildren<Text>().text = _PlayerManager.EarthHero.tag.ToString();
        TeamButtonList[2].GetComponentInChildren<Text>().text = _PlayerManager.WaterHero.tag.ToString();
        TeamButtonList[3].GetComponentInChildren<Text>().text = _PlayerManager.AirHero.tag.ToString();
    }    

    private void FixedUpdate()
    {
        switch (_ControllerSelect1)
        {
            case 0:
                TeamButtonList[0].GetComponentInChildren<Text>().text = "Team None";
                break;
            case 1:
                TeamButtonList[0].GetComponentInChildren<Text>().text = "Team 1";
                _PlayerManager.FireHero.tag = "Team1";
                break;
            case 2:
                TeamButtonList[0].GetComponentInChildren<Text>().text = "Team 2";
                _PlayerManager.FireHero.tag = "Team2";
                break;
            case 3:
                TeamButtonList[0].GetComponentInChildren<Text>().text = "FFA";
                _PlayerManager.FireHero.tag = "FFA";
                break;
            default:
                break;
        }

        switch (_ControllerSelect2)
        {
            case 0:
                TeamButtonList[1].GetComponentInChildren<Text>().text = "Team None";
                break;
            case 1:
                TeamButtonList[1].GetComponentInChildren<Text>().text = "Team 1";
                _PlayerManager.EarthHero.tag = "Team1";
                break;
            case 2:
                TeamButtonList[1].GetComponentInChildren<Text>().text = "Team 2";
                _PlayerManager.EarthHero.tag = "Team2";
                break;
            case 3:
                TeamButtonList[1].GetComponentInChildren<Text>().text = "FFA";
                _PlayerManager.EarthHero.tag = "FFA";
                break;
            default:
                break;
        }

        switch (_ControllerSelect3)
        {
            case 0:
                TeamButtonList[2].GetComponentInChildren<Text>().text = "Team None";
                break;
            case 1:
                TeamButtonList[2].GetComponentInChildren<Text>().text = "Team 1";
                _PlayerManager.WaterHero.tag = "Team1";
                break;
            case 2:
                TeamButtonList[2].GetComponentInChildren<Text>().text = "Team 2";
                _PlayerManager.WaterHero.tag = "Team2";
                break;
            case 3:
                TeamButtonList[2].GetComponentInChildren<Text>().text = "FFA";
                _PlayerManager.WaterHero.tag = "FFA";
                break;
            default:
                break;
        }

        switch (_ControllerSelect4)
        {
            case 0:
                TeamButtonList[3].GetComponentInChildren<Text>().text = "Team None";
                break;
            case 1:
                TeamButtonList[3].GetComponentInChildren<Text>().text = "Team 1";
                _PlayerManager.AirHero.tag = "Team1";
                break;
            case 2:
                TeamButtonList[3].GetComponentInChildren<Text>().text = "Team 2";
                _PlayerManager.AirHero.tag = "Team2";
                break;
            case 3:
                TeamButtonList[3].GetComponentInChildren<Text>().text = "FFA";
                _PlayerManager.AirHero.tag = "FFA";
                break;
            default:
                break;
        }
    }

    public void SelectController1()
    {
        _ControllerSelect1++;
        if (_ControllerSelect1 > 3)
            _ControllerSelect1 = 0;
    }

    public void SelectController2()
    {
        _ControllerSelect2++;
        if (_ControllerSelect2 > 3)
            _ControllerSelect2 = 0;
    }

    public void SelectController3()
    {
        _ControllerSelect3++;
        if (_ControllerSelect3 > 3)
            _ControllerSelect3 = 0;
    }

    public void SelectController4()
    {
        _ControllerSelect4++;
        if (_ControllerSelect4 > 3)
            _ControllerSelect4 = 0;
    }
}
