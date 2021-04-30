using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DelayedStart : MonoBehaviour
{
    public GameObject countDown;
    public TMP_Text _three;
    public TMP_Text _two;
    public TMP_Text _one;
    public TMP_Text _fight;


    [SerializeField] private int countdownTime;
    private bool _countDone = false;

    private void Awake()
    {
        _three.gameObject.SetActive(false);
        _two.gameObject.SetActive(false);
        _one.gameObject.SetActive(false);
        _fight.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartDelay");
    }


    IEnumerator StartDelay()
    { 
       Time.timeScale = 0;
      while (!_countDone)
        {            
            _three.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            _three.gameObject.SetActive(false);
            _two.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            _two.gameObject.SetActive(false);
            _one.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            _one.gameObject.SetActive(false);
            _fight.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(0.5f);
            _countDone = true;
        }
        Time.timeScale = 1;
        countDown.gameObject.SetActive(false);
    }
}
