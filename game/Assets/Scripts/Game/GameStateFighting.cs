using System.Collections.Generic;
using UnityEngine;

public class GameStateFighting : GameState
{
    private readonly BugSpawner _bugSpawner;

    private List<GameObject> _bugs;
    
    public GameStateFighting(GameStateMachine stateMachine) : base(stateMachine)
    {
        _bugSpawner = BugSpawner.Instance;

        _bugs = new List<GameObject>();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        
        for (int i = 0; i < 5; i++)
        {
            var bug = _bugSpawner.Spawn();
            _bugs.Add(bug);
        }
    }

    public override void Tick()
    {
        base.Tick();

        if (_bugs.Count <= 0)
        {
            Debug.Log("GAME OVER");
        }
    }
}
