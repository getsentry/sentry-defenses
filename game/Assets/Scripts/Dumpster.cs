using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dumpster : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _spawnDistance = 1.0f;
    
    public List<BugData> Bugs;
    
    [SerializeField] private GameObject _bugPrefab;
    [SerializeField] private float _spawnDelay = 0.8f;

    private DumpsterVisuals visuals;

    private void Awake()
    {
        visuals = GetComponent<DumpsterVisuals>();
    }

    void Start()
    {
        visuals.Spawn(OnSpawning);
    }

    private void OnSpawning()
    {
        SpawnBug();
        StartCoroutine(TriggerSpawn());
    }

    private IEnumerator TriggerSpawn()
    {
        yield return new WaitForSeconds(_spawnDelay);
        visuals.Spawn(OnSpawning);
    }

    private void SpawnBug()
    {
        var random = Random.insideUnitCircle;
        var spawnPosition = new Vector3(random.x, random.y, 0).normalized + transform.position;

        var bug = Instantiate(_bugPrefab, spawnPosition, Quaternion.identity);
        bug.GetComponent<Bug>().SetTarget(_target);
    }
}
