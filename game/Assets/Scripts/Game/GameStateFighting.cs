using System.Collections.Generic;
using UnityEngine;

public class GameStateFighting : GameState
{
    private readonly BugSpawner _bugSpawner;

    private GameData _data;
    private int bugsToSpawn;
    private float timer = 0f;

    public GameStateFighting(GameStateMachine stateMachine) : base(stateMachine)
    {
        _data = GameData.Instance;
        _bugSpawner = BugSpawner.Instance;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        
        bugsToSpawn = 5 + _data.Level * 2;
    }

    public override void Tick()
    {
        base.Tick();

        timer += Time.deltaTime;
        if (bugsToSpawn > 0 && timer > 0.3f)
        {
            timer = 0;
            for (int i = 0; i < _data.Level; i++)
            {
                var bug = _bugSpawner.Spawn();
                _data.bugs.Add(bug);
                bugsToSpawn -= 1;
                if (bugsToSpawn == 0)
                {
                    break;
                }
            }
        }

        if (_data.HitPoints <= 0)
        {
            StateTransition(GameStates.GameOver);
            return;
        }

        if (_data.bugs.Count <= 0 && bugsToSpawn == 0)
        {
            _data.Level++;
            StateTransition(GameStates.Upgrading);
            return;
        }
    }
}