using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSelect : MonoBehaviour
{
    public List<Button> ButtonList = new List<Button>();
    private List<int> _controllerList = new List<int>();
    private PlayerManager _PlayerManager;
    private HeroMovement _fireHeroMovement;
    private HeroMovement _waterHeroMovement;
    private HeroMovement _airHeroMovement;
    private HeroMovement _earthHeroMovement;

    private static int ControllerSelect1 = 0;
    private static int ControllerSelect2 = 0;
    private static int ControllerSelect3 = 0;
    private static int ControllerSelect4 = 0;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        _PlayerManager = ServiceLocator.Get<PlayerManager>();
        _fireHeroMovement = _PlayerManager.FireHero.GetComponent<HeroMovement>();
        _waterHeroMovement = _PlayerManager.WaterHero.GetComponent<HeroMovement>();
        _airHeroMovement = _PlayerManager.AirHero.GetComponent<HeroMovement>();
        _earthHeroMovement = _PlayerManager.EarthHero.GetComponent<HeroMovement>();
    }

    void Start()
    {
        ButtonList[0].GetComponentInChildren<Text>().text = ((HeroMovement.Controller)ControllerSelect1).ToString();
        ButtonList[1].GetComponentInChildren<Text>().text = ((HeroMovement.Controller)ControllerSelect2).ToString();
        ButtonList[2].GetComponentInChildren<Text>().text = ((HeroMovement.Controller)ControllerSelect3).ToString();
        ButtonList[3].GetComponentInChildren<Text>().text = ((HeroMovement.Controller)ControllerSelect4).ToString();
    }

    public void SelectController1()
    {
        ControllerSelect1++;
        if (ControllerSelect1 > 5)
            ControllerSelect1 = 0;

        switch (ControllerSelect1)
        {
            case 0:
                ButtonList[0].GetComponentInChildren<Text>().text = "Controller None";
                _fireHeroMovement.ControllerInput = (HeroMovement.Controller)ControllerSelect1;
                break;
            case 1:
                ButtonList[0].GetComponentInChildren<Text>().text = "Keyboard";
                _fireHeroMovement.GetComponent<HeroMovement>().ControllerInput =
               (HeroMovement.Controller)ControllerSelect1;
                break;
            case 2:
                ButtonList[0].GetComponentInChildren<Text>().text = "PS4";
                _fireHeroMovement.GetComponent<HeroMovement>().ControllerInput =
            (HeroMovement.Controller)ControllerSelect1;
                break;
            case 3:
                ButtonList[0].GetComponentInChildren<Text>().text = "XBOX";
                _fireHeroMovement.GetComponent<HeroMovement>().ControllerInput =
            (HeroMovement.Controller)ControllerSelect1;
                break;
            case 4:
                ButtonList[0].GetComponentInChildren<Text>().text = "KeyboardLayout2";
                _fireHeroMovement.GetComponent<HeroMovement>().ControllerInput =
            (HeroMovement.Controller)ControllerSelect1;
                break;
            case 5:
                ButtonList[0].GetComponentInChildren<Text>().text = "GamePad";
                _fireHeroMovement.GetComponent<HeroMovement>().ControllerInput =
            (HeroMovement.Controller)ControllerSelect1;
                break;
            default:
                break;
        }
    }

    public void SelectController2()
    {
        ControllerSelect2++;
        if (ControllerSelect2 > 5)
            ControllerSelect2 = 0;

        switch (ControllerSelect2)
        {
            case 0:
                ButtonList[1].GetComponentInChildren<Text>().text = "Controller None";
                _earthHeroMovement.ControllerInput = (HeroMovement.Controller)ControllerSelect2;
                break;
            case 1:
                ButtonList[1].GetComponentInChildren<Text>().text = "Keyboard";
                _earthHeroMovement.ControllerInput =
               (HeroMovement.Controller)ControllerSelect2;
                break;
            case 2:
                ButtonList[1].GetComponentInChildren<Text>().text = "PS4";
                _earthHeroMovement.ControllerInput =
            (HeroMovement.Controller)ControllerSelect2;
                break;
            case 3:
                ButtonList[1].GetComponentInChildren<Text>().text = "XBOX";
                _earthHeroMovement.ControllerInput =
            (HeroMovement.Controller)ControllerSelect2;
                break;
            case 4:
                ButtonList[1].GetComponentInChildren<Text>().text = "KeyboardLayout2";
                _earthHeroMovement.ControllerInput =
            (HeroMovement.Controller)ControllerSelect2;
                break;
            case 5:
                ButtonList[1].GetComponentInChildren<Text>().text = "GamePad";
                _earthHeroMovement.ControllerInput =
            (HeroMovement.Controller)ControllerSelect2;
                break;
            default:
                break;
        }
    }

    public void SelectController3()
    {
        ControllerSelect3++;
        if (ControllerSelect3 > 5)
            ControllerSelect3 = 0;

        switch (ControllerSelect3)
        {
            case 0:
                ButtonList[2].GetComponentInChildren<Text>().text = "Controller None";
                _waterHeroMovement.ControllerInput = (HeroMovement.Controller)ControllerSelect3;
                break;
            case 1:
                ButtonList[2].GetComponentInChildren<Text>().text = "Keyboard";
                _waterHeroMovement.ControllerInput =
               (HeroMovement.Controller)ControllerSelect3;
                break;
            case 2:
                ButtonList[2].GetComponentInChildren<Text>().text = "PS4";
                _waterHeroMovement.ControllerInput =
            (HeroMovement.Controller)ControllerSelect3;
                break;
            case 3:
                ButtonList[2].GetComponentInChildren<Text>().text = "XBOX";
                _waterHeroMovement.ControllerInput =
            (HeroMovement.Controller)ControllerSelect3;
                break;
            case 4:
                ButtonList[2].GetComponentInChildren<Text>().text = "KeyboardLayout2";
                _waterHeroMovement.ControllerInput =
            (HeroMovement.Controller)ControllerSelect3;
                break;
            case 5:
                ButtonList[2].GetComponentInChildren<Text>().text = "Gamepad";
                _waterHeroMovement.ControllerInput =
            (HeroMovement.Controller)ControllerSelect3;
                break;
            default:
                break;
        }
    }

    public void SelectController4()
    {
        ControllerSelect4++;
        if (ControllerSelect4 > 5)
            ControllerSelect4 = 0;

        switch (ControllerSelect4)
        {
            case 0:
                ButtonList[3].GetComponentInChildren<Text>().text = "Controller None";
                _airHeroMovement.ControllerInput = (HeroMovement.Controller)ControllerSelect4;
                break;
            case 1:
                ButtonList[3].GetComponentInChildren<Text>().text = "Keyboard";
                _airHeroMovement.ControllerInput =
               (HeroMovement.Controller)ControllerSelect4;
                break;
            case 2:
                ButtonList[3].GetComponentInChildren<Text>().text = "PS4";
                _airHeroMovement.ControllerInput =
            (HeroMovement.Controller)ControllerSelect4;
                break;
            case 3:
                ButtonList[3].GetComponentInChildren<Text>().text = "XBOX";
                _airHeroMovement.ControllerInput =
            (HeroMovement.Controller)ControllerSelect4;
                break;
            case 4:
                ButtonList[3].GetComponentInChildren<Text>().text = "KeyboardLayout2";
                _airHeroMovement.ControllerInput =
            (HeroMovement.Controller)ControllerSelect4;
                break;
            case 5:
                ButtonList[3].GetComponentInChildren<Text>().text = "GamePad";
                _airHeroMovement.ControllerInput =
            (HeroMovement.Controller)ControllerSelect4;
                break;
            default:
                break;
        }
    }
}
