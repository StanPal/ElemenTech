using UnityEngine;

public class ScoreManager : MonoBehaviour
{    
    [SerializeField] private int _TeamOneScore = 0;
    [SerializeField] bool _IsPracticeMode = false;
    [SerializeField] private int _TeamTwoScore = 0;

    public int TeamOneScore { get { return _TeamOneScore; } }
    public int TeamTwoScore { get { return _TeamTwoScore; } }
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
                _TeamOneScore++;
                break;
            case 2:
                _TeamTwoScore++;
                break; 
            default:
                break;
        }
    }

    public void ResetScore()
    {
        _TeamOneScore = 0;
        _TeamTwoScore = 0;
    }
}
