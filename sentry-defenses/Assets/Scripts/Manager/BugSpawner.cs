using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using Sentry;
using Random = UnityEngine.Random;

public class BugSpawner : MonoSingleton<BugSpawner>
{
    [Serializable]
    class SentryBug
    {
        public float lat;
        public float lon;
        public string platform;
    }

    [SerializeField] private List<GameObject> _bugPrefabs;
    [SerializeField] private Transform _leftCenterTransform;
    [SerializeField] private Transform _rightCenterTransform;

    private float _leftOuterBound;
    private float _leftCenterBound;
    private float _rightCenterBound;
    private float _rightOuterBound;
    private float _bottomBound;
    private float _topBound;

    private ConcurrentStack<SentryBug> _sentryBugs;
    public int BugBuffer;
    
    private Camera _camera;
    public float MaxSpawnDistance = 5.0f;

    private HttpClient _client;
    private Task _startUpTask;
    private ISpan _spawnChild = null;

    private void Awake()
    {
        _camera = Camera.main;
        _client = new HttpClient(new SentryHttpMessageHandler());

        _sentryBugs = new ConcurrentStack<SentryBug>();

        _startUpTask = RetrieveSentryBugs();

        var bottomLeft = _camera.ScreenToWorldPoint(new Vector3(0, 0));
        var topRight = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        
        _leftOuterBound = bottomLeft.x;
        _leftCenterBound = _leftCenterTransform.transform.position.x;
        _rightCenterBound = _rightCenterTransform.transform.position.x;
        _rightOuterBound = topRight.x;
        _bottomBound = bottomLeft.y;
        _topBound = topRight.y;
    }

    private void OnDestroy()
    {
        _client.Dispose();
    }

    private void Update()
    {
        BugBuffer = _sentryBugs.Count;
    }

    private async Task RetrieveSentryBugs()
    {
        var currentTransaction = SentrySdk.GetSpan();
        FinishChildSpan();
        var data = await _client.GetStringAsync(
            "https://europe-west3-nth-wording-322409.cloudfunctions.net/sentry-game-server").ConfigureAwait(false);

        var bugs = JsonHelper.FromJson<SentryBug>(data);
        foreach (var bug in bugs)
        {
            _sentryBugs.Push(bug);    
        }

        if (currentTransaction?.Operation == "state.machine")
        {
            currentTransaction.Finish(SpanStatus.Ok);
        }
    }
    
    private SentryBug GetSentryBug()
    {
        if (!_startUpTask.IsCompleted)
        {
            _startUpTask.GetAwaiter().GetResult();
        }

        if (_sentryBugs.Count <= 0)
        {
            _startUpTask = RetrieveSentryBugs();
            _startUpTask.GetAwaiter().GetResult();
        }

        _sentryBugs.TryPop(out var bug);
        return bug;
    }

    public void FinishChildSpan()
    {
        _spawnChild?.Finish(SpanStatus.Ok);
        _spawnChild = null;
    }

    public GameObject Spawn()
    {
        _spawnChild ??= SentrySdk.GetSpan()?.StartChild("unit.spawn");

        var sentryBug = GetSentryBug();
        if (sentryBug == null)
        {
            return null;
        }

        string platform = sentryBug.platform;
        var platformPrefab = new Dictionary<string, GameObject>(){
            {"javascript", _bugPrefabs[0]},
            {"python", _bugPrefabs[1]},
        };
        if (!platformPrefab.ContainsKey(platform)) {
            if (UnityEngine.Random.value < 0.5) {
                platform = "javascript";
            } else {
                platform = "python";
            }
        }


        var position = new Vector3(0, Random.Range(_bottomBound, _topBound));
        if (Random.Range(0, 2) > 0)
        {
            position.x = Random.Range(_leftOuterBound, _leftCenterBound);
        }
        else
        { 
            position.x = Random.Range(_rightCenterBound, _rightOuterBound);
        }
        
        var bugGameObject = Instantiate(platformPrefab[platform], position, Quaternion.identity);
        bugGameObject.transform.SetParent(transform);

        return bugGameObject;
    }
}
