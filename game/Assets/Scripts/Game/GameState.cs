using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.StateMachine;

public class GameState : State<GameStates>
{
    public GameState(StateMachine<GameStates> stateMachine) : base(stateMachine)
    {
    }
}
