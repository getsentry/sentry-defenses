using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using UnityEngine;
using Sentry;

public class GameStateFighting : GameState
{
    private readonly BugSpawner _bugSpawner;

    private GameData _data;
    private int bugsToSpawn;
    private float timer = 0f;
    private int _slowFrames = 0;
    private int _frozenFrames = 0;
    private int _totalFrames = 0;
    private ITransaction _roundStartTransaction = null;

    public GameStateFighting(GameStateMachine stateMachine) : base(stateMachine)
    {
        _data = GameData.Instance;
        _bugSpawner = BugSpawner.Instance;
    }

    public override void OnEnter()
    {
        SentrySdk.ConfigureScope(scope => 
        {
            scope.SetTag("game.level", _data.Level.ToString());

            _roundStartTransaction = SentrySdk.StartTransaction("round.start", "Start Round");
            scope.Transaction = _roundStartTransaction;

            _slowFrames = 0;
            _frozenFrames = 0;
            _totalFrames = 0;

            base.OnEnter();

            bugsToSpawn = 5 + _data.Level * 2;

            scope.SetTag("game.bugs", bugsToSpawn.ToString());
            var turds = GameObject.FindObjectsOfType<SentryTower>();
            scope.SetTag("game.sentries", turds.Length.ToString());
        });
    }

    public override void Tick()
    {
        base.Tick();
        var frameDeltaTime = Time.deltaTime;
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
        
        if (frameDeltaTime >= 0.3f)
        {
            _frozenFrames++; 
        }
        else if(frameDeltaTime >= 0.01f)
        { 
            _slowFrames++;
        }
        _totalFrames++;
        if (_roundStartTransaction is { } startTransaction && bugsToSpawn <= 0)
        {
            _bugSpawner.FinishChildSpan();

            // TODO: Send as measurements
            startTransaction.SetExtra("frames_total", _totalFrames.ToString());
            startTransaction.SetExtra("frames_slow", _slowFrames.ToString());
            startTransaction.SetExtra("frames_frozen", _frozenFrames.ToString());
            startTransaction.Finish(SpanStatus.Ok);
            _roundStartTransaction = null;
        }
        if (_data.HitPoints <= 0)
        {
            StateTransition(GameStates.GameOver);
            return;
        }

        if (_data.bugs.Count <= 0 && bugsToSpawn <= 0)
        {
            _data.Level++;
            StateTransition(GameStates.Upgrading);
            return;
        }
    }
}