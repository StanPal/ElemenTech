using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerSelect : MonoBehaviour
{
    public Canvas mCanvas;
    public Button mCharacterSelect;
    int select = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (select)
        {
            case 0:
                mCharacterSelect.GetComponentInChildren<Text>().text = "None";
                break;
            case 1:
                mCharacterSelect.GetComponentInChildren<Text>().text = "KeyBoard";
                break;
            case 2:
                mCharacterSelect.GetComponentInChildren<Text>().text = "PS4";
                break;                
            default:
                break;
        }
    }

    public void SelectController()
    {
        select++;
        if (select > 2)
            select = 0;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
