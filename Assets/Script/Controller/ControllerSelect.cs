using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerSelect : MonoBehaviour
{
    public List<Button> butonList = new List<Button>();
    private PlayerManager playerManager; 
    int select = 0;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    void Start()
    {
        butonList[0].GetComponentInChildren<Text>().text =
       select.ToString();
    }

    private void Update()
    {
        switch (select)
        {
            case 0:
                butonList[0].GetComponentInChildren<Text>().text = "None";
                break;
            case 1:
                butonList[0].GetComponentInChildren<Text>().text = "Keyboard";
                playerManager.FireHero.GetComponent<HeroMovement>().controllerInput =
  (HeroMovement.Controller)select;
                break;
            case 2:
                butonList[0].GetComponentInChildren<Text>().text = "PS4";
                playerManager.FireHero.GetComponent<HeroMovement>().controllerInput =
  (HeroMovement.Controller)select;
                break;
            case 3:
                butonList[0].GetComponentInChildren<Text>().text = "XBOX";
                playerManager.FireHero.GetComponent<HeroMovement>().controllerInput =
(HeroMovement.Controller)select;
                break;
            default:
                break;
        }
    }


    public void SelectController()
    {
        select++;
        if (select > 3)
            select = 0; 
     
    }

}
