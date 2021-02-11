using UnityEngine;

public class ScoreManager : MonoBehaviour
{    
    [SerializeField] private int __teamOneScore = 0;
    [SerializeField] bool _IsPracticeMode = false;
    [SerializeField] private int __teamTwoScore = 0;

    public int _teamOneScore { get { return __teamOneScore; } }
    public int _teamTwoScore { get { return __teamTwoScore; } }
    public bool PracticeMode { get { return _IsPracticeMode;} set { _IsPracticeMode = value; } }

    private void Awake()
    { 
        ServiceLocator.Register<ScoreManager>(this);
    }

    public void AddPoints(int team, int points)
    {
        switch (team)
        {
            case 1:
                __teamOneScore++;
                break;
            case 2:
                __teamTwoScore++;
                break; 
            default:
                break;
        }
    }

    public void ResetScore()
    {
        __teamOneScore = 0;
        __teamTwoScore = 0;
    }
}
