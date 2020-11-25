using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerSelect : MonoBehaviour
{
    public List<Button> buttonList = new List<Button>();
    private List<int> controllerList = new List<int>();
    private PlayerManager playerManager; 
    int cntselect_1 = 0;
    int cntselect_2 = 0;
    int cntselect_3 = 0;
    int cntselect_4 = 0;

    private void Awake()
    {
        GameLoader.CallOnComplete(Initialize);
    }

    private void Initialize()
    {
        playerManager = ServiceLocator.Get<PlayerManager>();
    }

    void Start()
    {
        buttonList[0].GetComponentInChildren<Text>().text = ((HeroMovement.Controller)cntselect_1).ToString();
        buttonList[1].GetComponentInChildren<Text>().text = ((HeroMovement.Controller)cntselect_2).ToString();
        buttonList[2].GetComponentInChildren<Text>().text = ((HeroMovement.Controller)cntselect_3).ToString();
        buttonList[3].GetComponentInChildren<Text>().text = ((HeroMovement.Controller)cntselect_4).ToString();
    }

    private void Update()
    {

    }


    public void SelectController1()
    {
        cntselect_1++;
        if (cntselect_1 > 4)
            cntselect_1 = 0;

        switch (cntselect_1)
        {
            case 0:
                buttonList[0].GetComponentInChildren<Text>().text = "Controller None";
                playerManager.FireHero.GetComponent<HeroMovement>().controllerInput = (HeroMovement.Controller)cntselect_1;
                break;
            case 1:
                buttonList[0].GetComponentInChildren<Text>().text = "Keyboard";
                playerManager.FireHero.GetComponent<HeroMovement>().controllerInput =
               (HeroMovement.Controller)cntselect_1;
                break;
            case 2:
                buttonList[0].GetComponentInChildren<Text>().text = "PS4";
                playerManager.FireHero.GetComponent<HeroMovement>().controllerInput =
            (HeroMovement.Controller)cntselect_1;
                break;
            case 3:
                buttonList[0].GetComponentInChildren<Text>().text = "XBOX";
                playerManager.FireHero.GetComponent<HeroMovement>().controllerInput =
            (HeroMovement.Controller)cntselect_1;
                break;
            case 4:
                buttonList[0].GetComponentInChildren<Text>().text = "KeyboardLayout2";
                playerManager.FireHero.GetComponent<HeroMovement>().controllerInput =
            (HeroMovement.Controller)cntselect_1;
                break;
            default:
                break;
        }

    }
    public void SelectController2()
    {
        cntselect_2++;
        if (cntselect_2 > 4)
            cntselect_2 = 0;

        switch (cntselect_2)
        {
            case 0:
                buttonList[1].GetComponentInChildren<Text>().text = "Controller None";
                playerManager.EarthHero.GetComponent<HeroMovement>().controllerInput = (HeroMovement.Controller)cntselect_2;
                break;
            case 1:
                buttonList[1].GetComponentInChildren<Text>().text = "Keyboard";
                playerManager.EarthHero.GetComponent<HeroMovement>().controllerInput =
               (HeroMovement.Controller)cntselect_2;
                break;
            case 2:
                buttonList[1].GetComponentInChildren<Text>().text = "PS4";
                playerManager.EarthHero.GetComponent<HeroMovement>().controllerInput =
            (HeroMovement.Controller)cntselect_2;
                break;
            case 3:
                buttonList[1].GetComponentInChildren<Text>().text = "XBOX";
                playerManager.EarthHero.GetComponent<HeroMovement>().controllerInput =
            (HeroMovement.Controller)cntselect_2;
                break;
            case 4:
                buttonList[1].GetComponentInChildren<Text>().text = "KeyboardLayout2";
                playerManager.EarthHero.GetComponent<HeroMovement>().controllerInput =
            (HeroMovement.Controller)cntselect_2;
                break;
            default:
                break;
        }

    }
    public void SelectController3()
    {
        cntselect_3++;
        if (cntselect_3 > 4)
            cntselect_3 = 0;

        switch (cntselect_3)
        {
            case 0:
                buttonList[2].GetComponentInChildren<Text>().text = "Controller None";
                playerManager.WaterHero.GetComponent<HeroMovement>().controllerInput = (HeroMovement.Controller)cntselect_3;
                break;
            case 1:
                buttonList[2].GetComponentInChildren<Text>().text = "Keyboard";
                playerManager.WaterHero.GetComponent<HeroMovement>().controllerInput =
               (HeroMovement.Controller)cntselect_3;
                break;
            case 2:
                buttonList[2].GetComponentInChildren<Text>().text = "PS4";
                playerManager.WaterHero.GetComponent<HeroMovement>().controllerInput =
            (HeroMovement.Controller)cntselect_3;
                break;
            case 3:
                buttonList[2].GetComponentInChildren<Text>().text = "XBOX";
                playerManager.WaterHero.GetComponent<HeroMovement>().controllerInput =
            (HeroMovement.Controller)cntselect_3;
                break;
            case 4:
                buttonList[2].GetComponentInChildren<Text>().text = "KeyboardLayout2";
                playerManager.WaterHero.GetComponent<HeroMovement>().controllerInput =
            (HeroMovement.Controller)cntselect_3;
                break;
            default:
                break;
        }

    }
    public void SelectController4()
    {
        cntselect_4++;
        if (cntselect_4 > 4)
            cntselect_4 = 0;

        switch (cntselect_4)
        {
            case 0:
                buttonList[3].GetComponentInChildren<Text>().text = "Controller None";
                playerManager.AirHero.GetComponent<HeroMovement>().controllerInput = (HeroMovement.Controller)cntselect_4;
                break;
            case 1:
                buttonList[3].GetComponentInChildren<Text>().text = "Keyboard";
                playerManager.AirHero.GetComponent<HeroMovement>().controllerInput =
               (HeroMovement.Controller)cntselect_4;
                break;
            case 2:
                buttonList[3].GetComponentInChildren<Text>().text = "PS4";
                playerManager.AirHero.GetComponent<HeroMovement>().controllerInput =
            (HeroMovement.Controller)cntselect_4;
                break;
            case 3:
                buttonList[3].GetComponentInChildren<Text>().text = "XBOX";
                playerManager.AirHero.GetComponent<HeroMovement>().controllerInput =
            (HeroMovement.Controller)cntselect_4;
                break;
            case 4:
                buttonList[3].GetComponentInChildren<Text>().text = "KeyboardLayout2";
                playerManager.AirHero.GetComponent<HeroMovement>().controllerInput =
            (HeroMovement.Controller)cntselect_4;
                break;
            default:
                break;
        }
    }

}
