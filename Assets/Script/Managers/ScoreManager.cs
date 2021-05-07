using UnityEngine;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
   
    [SerializeField] private int _teamOneScore = 0;
    [SerializeField] private int _teamTwoScore = 0;
    [SerializeField] private int _teamThreeScore = 0;
    [SerializeField] private int _teamFourScore = 0;
    [SerializeField] bool _isPracticeMode = false;
    [SerializeField] bool _isMatchOver = false;
    [SerializeField] private  int _bestOfValue = 2;

    public bool TriggerReset = false;
    public int BestOfValue { get => _bestOfValue; set => _bestOfValue = value; }
    public int TeamOneScore { get => _teamOneScore; }
    public int TeamTwoScore { get => _teamTwoScore; }
    public int TeamThreeScore { get => _teamThreeScore; }
    public int TeamFourScore { get => _teamFourScore; }
    public bool IsMatchOver { get => _isMatchOver; set => _isMatchOver = value; }
    public bool PracticeMode { get { return _isPracticeMode;} set { _isPracticeMode = value; } }

    private void Awake()
    {
        ServiceLocator.Register<ScoreManager>(this);
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            ResetScore();
        }

    }

    public void AddPoints(int team, int points)
    {
        switch (team)
        {
            case 1:
                _teamOneScore++;
                break;
            case 2:
                _teamTwoScore++;
                break;
            case 3:
                _teamThreeScore++;
                break;
            case 4:
                _teamFourScore++;
                break;
            default:
                break;
        }
    }

    public void ResetScore()
    {
        _teamOneScore = 0;
        _teamTwoScore = 0;
        _teamThreeScore = 0;
        _teamFourScore = 0;
        _isMatchOver = false;
    }
}
