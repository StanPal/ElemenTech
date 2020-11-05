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
        playerManager = FindObjectOfType<PlayerManager>();
    }

    void Start()
    {
        buttonList[0].GetComponentInChildren<Text>().text =
       cntselect_1.ToString();
        buttonList[1].GetComponentInChildren<Text>().text =
        cntselect_2.ToString();
        buttonList[2].GetComponentInChildren<Text>().text =
        cntselect_3.ToString();
        buttonList[3].GetComponentInChildren<Text>().text =
        cntselect_4.ToString();
    }

    private void Update()
    {
        switch (cntselect_1)
        {
            case 0:
                buttonList[0].GetComponentInChildren<Text>().text = "Controller None";
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
            default:
                break;
        }

        switch (cntselect_2)
        {
            case 0:
                buttonList[1].GetComponentInChildren<Text>().text = "Controller None";
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
            default:
                break;
        }

        switch (cntselect_3)
        {
            case 0:
                buttonList[2].GetComponentInChildren<Text>().text = "Controller None";
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
            default:
                break;
        }

        switch (cntselect_4)
        {
            case 0:
                buttonList[3].GetComponentInChildren<Text>().text = "Controller None";
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
            default:
                break;
        }
    }


    public void SelectController1()
    {
        cntselect_1++;
        if (cntselect_1 > 3)
            cntselect_1 = 0; 
     
    }
    public void SelectController2()
    {
        cntselect_2++;
        if (cntselect_2 > 3)
            cntselect_2 = 0;

    }
    public void SelectController3()
    {
        cntselect_3++;
        if (cntselect_3 > 3)
            cntselect_3 = 0;

    }
    public void SelectController4()
    {
        cntselect_4++;
        if (cntselect_4 > 3)
            cntselect_4 = 0;

    }

}
