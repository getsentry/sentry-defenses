using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BugSpawner : MonoSingleton<BugSpawner>
{
    [Serializable]
    class SentryBug
    {
        public int count;
        public float lat;
        public float lon;
        public string platform;
    }

    public List<GameObject> BugPrefabs;
    
    private ConcurrentStack<SentryBug> _sentryBugs;
    private Camera _camera;
    public float MaxSpawnDistance = 5.0f;

    private HttpClient _client;

    private Task _startUpTask;
    
    private void Awake()
    {
        _camera = Camera.main;
        _client = new HttpClient();

        _sentryBugs = new ConcurrentStack<SentryBug>();
        
        _startUpTask = RetrieveSentryBugs();
    }

    private void OnDestroy()
    {
        _client.Dispose();
    }

    private async Task RetrieveSentryBugs()
    {
        var data = await _client.GetStringAsync(
            "https://europe-west3-nth-wording-322409.cloudfunctions.net/sentry-game-server");

        data = @$"{{""Items"":{data}}}";
        
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
            RetrieveSentryBugs().GetAwaiter().GetResult();                    
        }

        _sentryBugs.TryPop(out var bug);
        return bug;
    }

    public GameObject Spawn()
    {
        var sentryBug = GetSentryBug();
        var randomPosition = new Vector3(sentryBug.lat, sentryBug.lon, 0) * MaxSpawnDistance;
        var bugGameObject = Instantiate(BugPrefabs[0], randomPosition, Quaternion.identity);
        bugGameObject.transform.SetParent(transform);
        
        return bugGameObject;
    }
}
