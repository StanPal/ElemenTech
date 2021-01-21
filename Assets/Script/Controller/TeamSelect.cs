using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamSelect : MonoBehaviour
{
    //public SpriteRenderer spriteRenderer;

    public List<Button> teamButtonList = new List<Button>();
    private List<int> controllerList = new List<int>();
    private PlayerManager playerManager;
    int cntselect_1 = 0;
    int cntselect_2 = 0;
    int cntselect_3 = 0;
    int cntselect_4 = 0;
    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
       // spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Start()
    {

    }

    private void FixedUpdate()
    {
        switch (cntselect_1)
        {
            case 0:
                teamButtonList[0].GetComponentInChildren<Text>().text = "Team None";
                break;
            case 1:
                teamButtonList[0].GetComponentInChildren<Text>().text = "Team 1";
                playerManager.FireHero.tag = "Team1";
               // spriteRenderer.color = Color.blue;

                break;
            case 2:
                teamButtonList[0].GetComponentInChildren<Text>().text = "Team 2";
                playerManager.FireHero.tag = "Team2";
               // spriteRenderer.color = Color.red;
                break;
            default:
                break;
        }

        switch (cntselect_2)
        {
            case 0:
                teamButtonList[1].GetComponentInChildren<Text>().text = "Team None";
                break;
            case 1:
                teamButtonList[1].GetComponentInChildren<Text>().text = "Team 1";
                playerManager.EarthHero.tag = "Team1";
              //  spriteRenderer.color = Color.blue;
                break;
            case 2:
                teamButtonList[1].GetComponentInChildren<Text>().text = "Team 2";
                playerManager.EarthHero.tag = "Team2";
              //  spriteRenderer.color = Color.red;
                break;
            default:
                break;
        }

        switch (cntselect_3)
        {
            case 0:
                teamButtonList[2].GetComponentInChildren<Text>().text = "Team None";
                break;
            case 1:
                teamButtonList[2].GetComponentInChildren<Text>().text = "Team 1";
                playerManager.WaterHero.tag = "Team1";
             //   spriteRenderer.color = Color.blue;
                break;
            case 2:
                teamButtonList[2].GetComponentInChildren<Text>().text = "Team 2";
                playerManager.WaterHero.tag = "Team2";
              //  spriteRenderer.color = Color.red;
                break;
            default:
                break;
        }

        switch (cntselect_4)
        {
            case 0:
                teamButtonList[3].GetComponentInChildren<Text>().text = "Team None";
                break;
            case 1:
                teamButtonList[3].GetComponentInChildren<Text>().text = "Team 1";
                playerManager.AirHero.tag = "Team1";
              //  spriteRenderer.color = Color.blue;
                break;
            case 2:
                teamButtonList[3].GetComponentInChildren<Text>().text = "Team 2";
                playerManager.AirHero.tag = "Team2";
               // spriteRenderer.color = Color.red;
                break;
            default:
                break;
        }
    }


    public void SelectController1()
    {
        cntselect_1++;
        if (cntselect_1 > 2)
            cntselect_1 = 0;

    }
    public void SelectController2()
    {
        cntselect_2++;
        if (cntselect_2 > 2)
            cntselect_2 = 0;

    }
    public void SelectController3()
    {
        cntselect_3++;
        if (cntselect_3 > 2)
            cntselect_3 = 0;

    }
    public void SelectController4()
    {
        cntselect_4++;
        if (cntselect_4 > 2)
            cntselect_4 = 0;

    }
}
