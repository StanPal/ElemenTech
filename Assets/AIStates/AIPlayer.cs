using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    [SerializeField] private Animator _aiController;
    public string name;

    private Transform target = null;
    private AIPlayerManager _manager = null;

    void Start()
    {
        _manager = ServiceLocator.Get<AIPlayerManager>();
        _manager.AddAIPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        _aiController.SetFloat("distToTarget", Vector2.Distance(transform.position, target.position));
    }

    public void Attack()
    {

    }

    public void Dance()
    {

    }
}
