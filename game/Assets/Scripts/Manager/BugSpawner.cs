using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sentry;

public class BugSpawner : MonoSingleton<BugSpawner>
{
    [Serializable]
    class SentryBug
    {
        public float lat;
        public float lon;
        public string platform;
    }

    public List<GameObject> BugPrefabs;
    
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
        FinishChildSpan();
        var data = await _client.GetStringAsync(
            "https://europe-west3-nth-wording-322409.cloudfunctions.net/sentry-game-server").ConfigureAwait(false);

        var bugs = JsonHelper.FromJson<SentryBug>(data);
        foreach (var bug in bugs)
        {
            _sentryBugs.Push(bug);    
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
            {"javascript", BugPrefabs[0]},
            {"python", BugPrefabs[1]},
        };
        if (!platformPrefab.ContainsKey(platform)) {
            if (UnityEngine.Random.value < 0.5) {
                platform = "javascript";
            } else {
                platform = "python";
            }
        }
        
        var randomPosition = new Vector3(sentryBug.lat, sentryBug.lon, 0) * MaxSpawnDistance;
        var bugGameObject = Instantiate(platformPrefab[platform], randomPosition, Quaternion.identity);
        bugGameObject.transform.SetParent(transform);

        return bugGameObject;
    }
}
