using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.StateMachine;

public class PlayerState : State<PlayerStates>
{
    public PlayerState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
}
