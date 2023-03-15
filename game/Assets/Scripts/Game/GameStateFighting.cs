using Manager;
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

    private float _startSpawnDelay = 3;
    private float _spawnDelay = 3;
    private float _spawnSpeedUp = 0.25f;
    private int _spawnCount = 1;
    
    
    public GameStateFighting(GameStateMachine stateMachine) : base(stateMachine)
    {
        _data = GameData.Instance;
        _bugSpawner = BugSpawner.Instance;
        _spawnDelay = _startSpawnDelay;

        var eventManager = EventManager.Instance;

        eventManager.OnReset += () =>
        {
            _spawnDelay = _startSpawnDelay;
            _spawnCount = 1;
        };
        
        eventManager.OnLevelUpXp += OnLevelUpXp;
    }

    private void OnLevelUpXp()
    {
        if (!IsActive)
        {
            return;
        }
        
        StateTransition(GameStates.PickUpgrade);
    }

    public override void OnEnter()
    {
        EventManager.Instance.ResumeGame();
        
        SentrySdk.ConfigureScope(scope => 
        {
            scope.SetTag("game.bugcount", _data.BugCount.ToString());

            _roundStartTransaction = SentrySdk.StartTransaction("round.start", "Start Round");
            scope.Transaction = _roundStartTransaction;

            _slowFrames = 0;
            _frozenFrames = 0;
            _totalFrames = 0;

            // bugsToSpawn = 5 + _data.Level * 2;
            bugsToSpawn = 1;
            
            scope.SetTag("game.alivebugs", bugsToSpawn.ToString());
            var turds = GameObject.FindObjectsOfType<SentryTower>();
            scope.SetTag("game.sentries", turds.Length.ToString());
        });

        timer = _spawnDelay; // So there is a bug spawning immediately
        
        base.OnEnter();
    }

    public override void Tick()
    {
        base.Tick();

        timer += Time.deltaTime;
        if (timer > _spawnDelay)
        {
            timer = 0;
            _spawnDelay -= _spawnSpeedUp;

            if (_spawnDelay < 1)
            {
                _spawnDelay = _startSpawnDelay;
                _spawnCount++;
            }

            for (var i = 0; i < _spawnCount; i++)
            {
                var bug = _bugSpawner.Spawn();
                _data.bugs.Add(bug);    
            }
        }
        
        // timer += Time.deltaTime;
        // if (bugsToSpawn > 0 && timer > 0.3f)
        // {
        //     timer = 0;
        //     for (int i = 0; i < _data.Level; i++)
        //     {
        //         var bug = _bugSpawner.Spawn();
        //         _data.bugs.Add(bug);
        //         bugsToSpawn -= 1;
        //         if (bugsToSpawn == 0)
        //         {
        //             break;
        //         }
        //     }
        // }
        
        var frameDeltaTime = Time.deltaTime;
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

        // if (_data.bugs.Count <= 0 && bugsToSpawn <= 0)
        // {
        //     _data.Level++;
        //     StateTransition(GameStates.Upgrading);
        //     return;
        // }
    }
}