using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.StateMachine;

public class BugState : State<BugStates>
{
    public BugState(BugStateMachine stateMachine) : base(stateMachine)
    {
    }
}
