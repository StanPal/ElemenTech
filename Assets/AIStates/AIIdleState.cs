﻿using UnityEngine;

public class AIIdleState : StateMachineBehaviour
{
    [SerializeField] private string name = "";
    [SerializeField] private AIPlayer _player = null;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        var manager = ServiceLocator.Get<AIPlayerManager>();
        _player = manager.GetPlayer(0);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        _player.setReturnFalse();
        _player.showHP();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
